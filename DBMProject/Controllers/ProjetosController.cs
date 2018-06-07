using DBMProject.Data;
using DBMProject.Models.ProjectsManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ProjetosController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Projetos
        public async Task<IActionResult> Index()
        {
            var projetosContext = _context.Projetos.Include(p => p.AcademicDegree).Where(m => m.Validado == true);
            //var projetosContext = _context.Projetos.Include(p => p.AcademicDegree);
            return View(await projetosContext.ToListAsync());
        }

        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos.Include(p => p.AcademicDegree)
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }
        [Authorize]
        // GET: Projetos/Create
        public IActionResult Create()
        {
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
            return View();
        }

        // POST: Projetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,ProjectName,Technology,Description,AcademicDegreeId,ProjectFileName")] Projeto projeto, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                if (!ValidateFileExtension(file.FileName))
                {
                    ViewData["ExtensionError"] = "A extensão do ficheiro não é válida. Apenas são aceites ficheiros \".rar\" ou \".zip\"";
                    ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
                    return View(projeto);
                }

                if (projeto.AcademicDegreeId == 0)
                {
                    //ViewData["AcademicDegreeIdError"] = "Selecione o ciclo de estudos onde se enquadra o projeto";
                    ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
                    return View(projeto);
                }

                await UploadProject(projeto);

                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
            return View(projeto);
        }

        private async Task UploadProject(Projeto projeto)
        {
            var file = HttpContext.Request.Form.Files[0];

            var extensions = Path.GetExtension(file.FileName);

            projeto.Size = ConvertBytesToMBytes(file.Length);

            if (_context.Projetos.ToList().Count() == 0)
                projeto.ProjectFileName = "Projeto1" + extensions;
            else
                projeto.ProjectFileName = "Projeto" + (_context.Projetos.Max(p => p.ProjetoId) + 1) + extensions;

            var path = Path.Combine(_environment.WebRootPath, "UploadedProjects/");

            using (var stream = new FileStream(Path.Combine(path, projeto.ProjectFileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private bool ProjetoExists(int id)
        {
            return _context.Projetos.Any(e => e.ProjetoId == id);
        }

        private bool ValidateFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower() == ".rar" || Path.GetExtension(fileName).ToLower() == ".zip";
        }

        public ActionResult DownloadProject(string searchName, string fileName)
        {
            return File("~/UploadedProjects/" + searchName, "application/x-zip-compressed", fileName + ".rar");
        }

        private double ConvertBytesToMBytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        //Nível 5
        [HttpPost, ActionName("Search")]
        public IActionResult Search(string textoProcura)
        {
            if (textoProcura != null)
            {
                var projetos = _context.Projetos
                    .Where(c =>
                        c.ProjectName.Contains(textoProcura) ||
                        c.Technology.Contains(textoProcura) ||
                        c.AcademicDegree.AcademicDegreeName.Contains(textoProcura))
                    .Include(c => c.AcademicDegree);
                return View("Index", projetos);
            }
            else
                return RedirectToAction(nameof(Index));
        }
    }
}
