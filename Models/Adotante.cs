using System.ComponentModel.DataAnnotations;
namespace myMeow2.Models
{
    public class Adotante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress] // Valida se é um email
        public string Email { get; set; }

        [Phone] // Valida o número de telefone
        [StringLength(15)]
        public string Telefone { get; set; }
    }
}
