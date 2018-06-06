using DBMProject.Data;
using DBMProject.Models.ProjectsManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Controllers
{
    public class ValidarProjetosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidarProjetosController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Controlador que retorna a View Index com a lista de projectos por validar
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Index()
        {
            return View(await _context.Projetos.Where(m => m.Validado != true).ToListAsync());
        }


        /// <summary>
        /// Controlador que valida o projecto com o Id escolhido
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> ValidarProjeto(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var projecto = await _context.Projetos.SingleOrDefaultAsync(m => m.ProjetoId == id);

            if (projecto == null)
            {
                return NotFound();
            }

            projecto.Validado = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }








        // GET: ValidarProjetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // GET: ValidarProjetos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValidarProjetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,ProjectName,Technology,Size,Description,Localizacao,Validado,ProjectFileName")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        // GET: ValidarProjetos/Edit/5
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
            return View(projeto);
        }

        // POST: ValidarProjetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,ProjectName,Technology,Size,Description,Localizacao,Validado,ProjectFileName")] Projeto projeto)
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
            return View(projeto);
        }

        // GET: ValidarProjetos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // POST: ValidarProjetos/Delete/5
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
