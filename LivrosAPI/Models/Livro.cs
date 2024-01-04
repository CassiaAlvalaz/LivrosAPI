using System.ComponentModel.DataAnnotations;

namespace LivrosAPI.Models;

public class Livro
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Título não pode ser em branco.")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O Autor não pode ser em branco.")]
    public string Autor { get; set; }
    [Range(1, 1000, ErrorMessage = "O número de páginas não pode ser menor que 1 e maior que 1000.")]
    public int Paginas { get; set; }
}
