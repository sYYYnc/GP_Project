using DBMProject.Models.ProjectsManagement;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBMProject.Models
{
    public class Comentario
    {
        [Required]
        public int ComentarioId { get; set; }

        [ForeignKey("Projeto")]
        public int ProjetoId { get; set; }
        [DisplayName("Comentário")]
        public string Descricao { get; set; }

        public virtual Projeto Projeto { get; set; }

    }
}
