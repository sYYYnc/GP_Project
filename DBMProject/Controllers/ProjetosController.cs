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

        /// <summary>
        /// Método responsável por retornar a View responsável por mostrar os projetos
        /// </summary>
        /// <returns>
        /// Retorna uma View onde são demonstrados os projetos que ja foram adicionados anteriormente
        /// </returns>
        // GET: Projetos
        public async Task<IActionResult> Index()
        {
            var projetosContext = _context.Projetos.Include(p => p.AcademicDegree).Where(m => m.Validado == true);
            //var projetosContext = _context.Projetos.Include(p => p.AcademicDegree);
            return View("IndexXex", await projetosContext.ToListAsync());
        }

        /// <summary>
        /// Método responsável por retornar a view que contém os detalhes de determinado projeto, recebendo 
        /// para isso o id do projeto correspondente.
        /// </summary>
        /// <returns>
        /// Retorna uma View que recebe como parametro o projeto correspondente ao id do projeto respetivo.
        /// </returns>



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
        /// <summary>
        /// Método responsável por passar para a View onde é possivel criar um novo projeto.
        /// </summary>
        /// <returns>
        /// Retorna a View onde é possivel criar um novo projeto.
        /// </returns>
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
        /// <summary>
        /// Método responsável por retornar a view com o projeto criado dando origem a um método post
        /// </summary>
        /// <returns>
        /// Retorna a view com o projeto criado dando origem a um método post
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,ProjectName,Technology,Description,AcademicDegreeId,Localizacao")] Projeto projeto, IFormFile file)
        {

            if (ModelState.IsValid)
            {
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

                _context.Projetos.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
            return View(projeto);
        }

        /// <summary>
        /// Método privado responsável pelo Upload do projeto que é recebido como parametro.
        /// </summary>
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

        /// <summary>
        /// Método responsável por retornar um boleano true caso o projeto com o id recebido por parametros exista.
        /// </summary>
        /// <returns>
        /// Retorna true ou false caso o projeto com o id recebido como paramrtro exista.
        /// </returns>
        private bool ProjetoExists(int id)
        {
            return _context.Projetos.Any(e => e.ProjetoId == id);
        }

        /// <summary>
        /// Método responsável por validar a file extension aravés do filename recebido como parametro.
        /// </summary>
        /// <returns>
        /// Retorna true ou false caso a file extension seja válida ou nao.
        /// </returns>
        private bool ValidateFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLower() == ".rar" || Path.GetExtension(fileName).ToLower() == ".zip";
        }

        /// <summary>
        /// Método responsável pelo download de um projeto através do seu search name e do file name recebidos por parametros
        /// </summary>
        /// <returns>
        /// Retorna o projeto que faz match com a pesquisa.
        /// </returns>
        public ActionResult DownloadProject(string searchName, string fileName)
        {
            return File("~/UploadedProjects/" + searchName, "application/x-zip-compressed", fileName + ".rar");
        }

        /// <summary>
        /// Método privado responsável por converter bytes em MBBytes recebendo como parametro os bytes.
        /// </summary>
        /// <returns>
        /// Retorna um double com o n+umero de MBytes respetivo.
        /// </returns>
        private double ConvertBytesToMBytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        /// <summary>
        /// Método responsável por executar o método post para a procura de um projeto. Recebe como parametros o nome
        /// que o utilizador pretende encontrar.
        /// </summary>
        /// <returns>
        /// Retorna a view com o projeto criado caso seja encontrado um projeto que faça match com a procura, caso contrario
        /// retorna erro e volta para a página de pesquisa.
        /// </returns>
        [HttpPost, ActionName("Search")]
        public IActionResult Search(string textoProcura)
        {
            if (textoProcura != null)
            {
                var projetos = _context.Projetos
                    .Where(c =>
                        c.ProjectName.Contains(textoProcura) ||
                        c.Technology.Contains(textoProcura) ||
                        c.Localizacao.Contains(textoProcura) ||
                        c.AcademicDegree.AcademicDegreeName.Contains(textoProcura))
                    .Include(c => c.AcademicDegree).Where(m => m.Validado == true);
                return View("Index", projetos);
            }
            else
                return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .Include(p => p.AcademicDegree)
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // POST: ProjetosClassificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projeto = await _context.Projetos.SingleOrDefaultAsync(m => m.ProjetoId == id);
            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Projetoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos.SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName", projeto.AcademicDegreeId);
            return View(projeto);
        }

        // POST: Projetoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,ProjectName,Technology,Size,Description,Localizacao,Validado,AcademicDegreeId,Classificacao,Autor,Imagem,NrDeVotos,ProjectFileName")] Projeto projeto)
        {
            if (id != projeto.ProjetoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projeto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetoExists(projeto.ProjetoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName", projeto.AcademicDegreeId);
            return View(projeto);
        }


    }
}
