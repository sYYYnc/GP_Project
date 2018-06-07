using DBMProject.Models.ProjectsManagement;
using System.Linq;

namespace DBMProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();
            InitAcademicDegrees(context);
        }

        private static void InitAcademicDegrees(ApplicationDbContext context)
        {
            if (context.AcademicDegrees.Any())
            {
                return;
            }
            var academicDegrees = new AcademicDegree[]
            {
                new AcademicDegree { AcademicDegreeName = "CTESP" },
                new AcademicDegree { AcademicDegreeName = "Licenciatura" },
                new AcademicDegree { AcademicDegreeName = "Pós-Graduação" },
                new AcademicDegree { AcademicDegreeName = "Mestrado" },
                new AcademicDegree { AcademicDegreeName = "Doutoramento" }
            };
            foreach (AcademicDegree ad in academicDegrees)
            {
                context.AcademicDegrees.Add(ad);
            }
            context.SaveChanges();


        }
    }
}
