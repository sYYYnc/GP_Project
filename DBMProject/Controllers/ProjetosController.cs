using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBMProject.Data;
using DBMProject.Models.ProjectsManagement;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
            return View(await _context.Projeto.ToListAsync());
        }

        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // GET: Projetos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,ProjectName,Technology,Description")] Projeto projeto, string projectFileName)
        {
            
            if (ModelState.IsValid)
            {
                if (!ValidateFileExtension(projectFileName))
                {
                    ViewData["ExtensionError"] = "A extensão do ficheiro não é válida. Apenas são aceites ficheiros \".rar\"";
                    return View(projeto);
                }

                var extensions = Path.GetExtension(projectFileName);

                if (_context.Projeto.ToList().Count() == 0)
                    projeto.ProjectFileName = "Projeto1" + extensions;
                else
                    projeto.ProjectFileName = "Projeto" + (_context.Projeto.Max(p => p.ProjetoId) + 1) + extensions;

                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(projeto);
        }

        private bool ProjetoExists(int id)
        {
            return _context.Projeto.Any(e => e.ProjetoId == id);
        }

        private bool ValidateFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower() == ".rar";
        }

        [HttpPost]
        public JsonResult UploadProject(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "UploadedProjects/");

            List<string> errors = new List<string>();

            if (file != null)
            {
                string fileName;

                var extensions = Path.GetExtension(file.FileName);

                if (_context.Projeto.ToList().Count() == 0)
                    fileName = "Projeto1" + extensions;
                else
                    fileName = "Projeto" + (_context.Projeto.Max(p => p.ProjetoId) + 1) + extensions;

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
            }

            return Json(errors);
        }

        public ActionResult DownloadProject(string searchName, string fileName)
        {
            return File("~/UploadedProjects/" + searchName, "application/zip", fileName);
        }
        
    }
}
