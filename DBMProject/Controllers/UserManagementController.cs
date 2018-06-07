using DBMProject.Data;
using DBMProject.Models;
using DBMProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;


namespace DBMProject.Controllers
{
    public class UserManagementController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Páginal inicial da Área do administrador
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexAdmin()
        {
            return View();
        }


        public IActionResult Index()
        {

            var viewModel = new UserManagementIndexViewModel
            {

                Users = _context.Users.OrderBy(u => u.Name).ToList()

            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var vm = new UserManagementAddRoleViewModel
            {
                Roles = GetAllRoles(),
                UserId = id,
                Nome = user.Name
            };
            return View(vm);

        }


        /// <summary>
        /// Adiciona o role ao utilizador
        /// </summary>
        /// <param name="rvm">UserManagementAddRoleViewModel rvm</param>
        /// <returns>View(result UserManagementAddRoleViewModel)</returns>
        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagementAddRoleViewModel rvm)
        {
            var user = await _userManager.FindByIdAsync(rvm.UserId);
            if (ModelState.IsValid)
            {

                var result = await _userManager.AddToRoleAsync(user, rvm.NewRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

            }
            rvm.Roles = GetAllRoles();
            rvm.Nome = user.Name;
            return View(rvm);
        }

        /// <summary>
        /// Obtem a lista de todos os roles na bd
        /// </summary>
        /// <returns></returns>
        private SelectList GetAllRoles() => new SelectList(_roleManager.Roles);


        /// <summary>
        /// método para remover o utilizador de um determinado id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");

        }



    }
}