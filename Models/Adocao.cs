using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace myMeow2.Models
{
    public class Adocao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GatinhoId { get; set; } // Chave estrangeira para Gatinho

        [ForeignKey("GatinhoId")]
        public Gatinho Gatinho { get; set; } // Navegação para o gatinho adotado

        [Required]
        public int AdotanteId { get; set; } // Chave estrangeira para Adotante

        [ForeignKey("AdotanteId")]
        public Adotante Adotante { get; set; } // Navegação para o adotante

        [Required]
        public DateTime Data { get; set; } // Data da adoção
    }



}
