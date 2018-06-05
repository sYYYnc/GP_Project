using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Models.ProjectsManagement
{
    public class AcademicDegree
    {
        public int AcademicDegreeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ciclo de estudos obrigatório")]
        [DisplayName("Ciclo de estudos")]
        public string AcademicDegreeName { get; set; }

    }
}
