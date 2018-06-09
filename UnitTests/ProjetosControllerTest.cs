using DBMProject.Controllers;
using DBMProject.Data;
using DBMProject.Models.ProjectsManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ProjetosControllerTest
    {
        private ApplicationDbContext _context;

        private ProjetosController _controller;

        public ProjetosControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server = tcp:dbmproject20180525110553dbserver.database.windows.net,1433; Initial Catalog = GPprojeto_db; Persist Security Info = False; User ID = Projetom7; Password =M7projeto_2018; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30");
            _context = new ApplicationDbContext(optionsBuilder.Options);

            _controller = new ProjetosController(_context, null);
        }

        [Fact]
        public void testIsViewResultIndex()
        {
            var result = _controller.Index();

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void testIsViewResultDetails()
        {
            var result = _controller.Details(1);

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void testIsViewResultCreate()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void testIsViewResultCreateReturnedProject()
        {
            Projeto projeto = new Projeto {
                ProjectName = "Teste1",
                Size = 1.67,
                Technology = "Teste1",
                Validado = false,
                Localizacao = "Setúbal",
                AcademicDegreeId = 1,
                Description = "Teste1",
                ProjectFileName = "teste1.rar" };

            var result = _controller.Create(projeto, null);

            Assert.IsType<Task<IActionResult>>(result);
        }
        
    }
}
