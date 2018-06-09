using DBMProject.Data;
using DBMProject.Models.ProjectsManagement;
using DBMProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Controllers
{
    public class ProjetosClassificacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjetosClassificacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjetosClassificacao
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projetos.Include(p => p.AcademicDegree);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> Votar(int id)
        {
            var projeto = await _context.Projetos.Include(p => p.AcademicDegree)
               .SingleOrDefaultAsync(m => m.ProjetoId == id);

            var vm = new ClassificacaoViewModel
            {
                ProjetoId = id,
                Nome = projeto.ProjectName
            };

            return View(vm);

        }



        [HttpPost]
        public async Task<IActionResult> Votar(ClassificacaoViewModel rvm)
        {



            var projeto = await _context.Projetos.Include(p => p.AcademicDegree)
               .SingleOrDefaultAsync(m => m.ProjetoId == rvm.ProjetoId);
            if (projeto == null)
            {
                return NotFound();
            }

            var classificacaoActual = projeto.Classificacao;
            var nrDeVotos = projeto.NrDeVotos + 1;

            double novaClassificacao = (classificacaoActual + rvm.Voto) / nrDeVotos;
            var classificacaoRound = Math.Ceiling(novaClassificacao);

            projeto.Classificacao = classificacaoRound;
            projeto.NrDeVotos = nrDeVotos;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projetos", new { area = "" });

        }





        // GET: ProjetosClassificacao/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ProjetosClassificacao/Create
        public IActionResult Create()
        {
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName");
            return View();
        }

        // POST: ProjetosClassificacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,ProjectName,Technology,Size,Description,Localizacao,Validado,AcademicDegreeId,Classificacao,NrDeVotos,ProjectFileName")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName", projeto.AcademicDegreeId);
            return View(projeto);
        }

        // GET: ProjetosClassificacao/Edit/5
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

        // POST: ProjetosClassificacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,ProjectName,Technology,Size,Description,Localizacao,Validado,AcademicDegreeId,Classificacao,NrDeVotos,ProjectFileName")] Projeto projeto)
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

        // GET: ProjetosClassificacao/Delete/5
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

        private bool ProjetoExists(int id)
        {
            return _context.Projetos.Any(e => e.ProjetoId == id);
        }
    }
}
