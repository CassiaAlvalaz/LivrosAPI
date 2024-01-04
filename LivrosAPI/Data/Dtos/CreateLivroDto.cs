using System.ComponentModel.DataAnnotations;

namespace LivrosAPI.Data.Dtos;

public class CreateLivroDto
{
    [Required(ErrorMessage = "O Título não pode ser em branco.")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O Autor não pode ser em branco.")]
    [StringLength(30, ErrorMessage = "O nome do autor não pode exceder 30 caracteres")]
    public string Autor { get; set; }
    [Range(1, 1000, ErrorMessage = "O número de páginas não pode ser menor que 1 e maior que 1000.")]
    public int Paginas { get; set; }
}
