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
        /// <summary>
        /// Método responsável por retornar os detalhes do projeto que corresponde ao id recebido por parametros.
        /// </summary>
        /// <returns>
        /// Retorna a View com o projeto respetivo
        /// </returns>
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
        /// <summary>
        /// Método responsável por passar para a View onde é possivel criar um novo projeto.
        /// </summary>
        /// <returns>
        /// Retorna a View onde é possivel criar um novo projeto.
        /// </returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValidarProjetos/Create
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
        /// <summary>
        /// Método responsável por retornar a view responsável por editar o projeto com o id passado por parametros.
        /// </summary>
        /// <returns>
        /// Retorna a view com o projeto que se pretende editar
        /// </returns>
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
        /// <summary>
        /// Método responsável pelo post responsável por editar o projeto com o id passado por parametros.
        /// </summary>
        /// <returns>
        /// Retorna a view respetiva depois do projeto ter sido editado.
        /// </returns>
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
        /// <summary>
        /// Método responsável por retornar a view responsável por eliminar o projeto com o id passado por parametros
        /// </summary>
        /// <returns>
        /// Retorna a view com o projeto que se pretende eliminar
        /// </returns>
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
        /// <summary>
        /// Método responsável pelo post responsável por eliminar o projeto com o id passado por parametros.
        /// </summary>
        /// <returns>
        /// Retorna a view respetiva depois do projeto ter sido eliminado.
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projeto = await _context.Projetos.SingleOrDefaultAsync(m => m.ProjetoId == id);
            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
