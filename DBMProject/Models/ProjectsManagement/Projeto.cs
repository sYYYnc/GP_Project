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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome do projeto em falta")]
        public string ProjectName { get; set; }

        [DisplayName("Tecnologia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tecnologia do projeto em falta")]
        public string Technology { get; set; }

        [DisplayName("Tamanho")]
        public double Size { get; set; }

        [DisplayName("Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Associe uma descrição do projeto para melhor percepção")]
        public string Description { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Anexo do projeto em falta")]

        public string ProjectFileName { get; set; }
    }
}
