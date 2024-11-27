using System.ComponentModel.DataAnnotations;

public class AdocaoViewModel
{
    public int GatinhoId { get; set; }

    public string GatinhoNome { get; set; }

    public string GatinhoImagem { get; set; }

    public string GatinhoSexo { get; set; }

    public bool GatinhoAdotado { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [StringLength(15)]
    public string Telefone { get; set; }
}
