using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBMProject.Models.FilesManagement
{
    public class File
    {
        public int FileId { get; set; }

        [DisplayName("Nome do projeto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza o nome do projeto")]
        public string FileName { get; set; }

        [DisplayName("Tecnologia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduza a tecnologia utilizada")]
        public string Technology { get; set; }

        [DisplayName("Tamanho")]
        public double Size { get; set; }

        [DisplayName("Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Associe uma descrição do projeto para melhor percepção")]
        public string Description { get; set; }

    }
}
