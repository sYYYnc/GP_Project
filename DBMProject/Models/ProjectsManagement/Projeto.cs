using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Models.ProjectsManagement
{
    public class Projeto
    {
        public int ProjetoId { get; set; }

        [DisplayName("Nome do projeto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indique o nome do projeto")]
        public string ProjectName { get; set; }

        [DisplayName("Tecnologia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indique a tecnologia do projeto")]
        public string Technology { get; set; }

        [DisplayName("Tamanho (Mb)")]
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double Size { get; set; }

        [DisplayName("Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, associe uma descrição do projeto para melhor percepção")]
        public string Description { get; set; }

        [DisplayName("Ciclo de Estudos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indique o ciclo de estudos associado ao projeto")]
        public int AcademicDegreeId { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        
        public string ProjectFileName { get; set; }
    }
}