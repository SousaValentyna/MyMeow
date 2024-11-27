using System.ComponentModel.DataAnnotations;
namespace myMeow2.Models
{
    public class Gatinho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(10)]
        public string Sexo { get; set; }

        [StringLength(50)]
        public string Cor { get; set; }

        [Required]
        public string Imagem { get; set; }

        [Required]
        public bool Adotado { get; set; } = false;

        // Construtor padrão (necessário para o EF Core)
        public Gatinho() { }

        // Construtor personalizado
        public Gatinho(string nome, string sexo, string imagem, string cor = null)
        {
            Nome = nome;
            Sexo = sexo;
            Imagem = imagem;
            Cor = cor;
            Adotado = false; // Sempre inicializa como não adotado
        }
    }
}
