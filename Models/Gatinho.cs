using System.ComponentModel.DataAnnotations;
namespace myMeow2.Models
{
    public class Gatinho
    {
        [Key] // Define como chave primária
        public int Id { get; set; }

        [Required] // Torna obrigatório
        [StringLength(100)] // Limita a string a 100 caracteres
        public string Nome { get; set; }

        [Required]
        [StringLength(10)] // Ex.: "Macho" ou "Fêmea"
        public string Sexo { get; set; }

        [StringLength(50)]
        public string Cor { get; set; }

        [Required] // A imagem deve ser fornecida
        public string Imagem { get; set; } // Armazena a URL ou caminho da imagem

        [Required]
        public bool Adotado { get; set; } = false; // Inicializa como "não adotado"
    }
}
