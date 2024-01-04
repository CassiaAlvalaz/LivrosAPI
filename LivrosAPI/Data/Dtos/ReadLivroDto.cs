using System.ComponentModel.DataAnnotations;

namespace LivrosAPI.Data.Dtos;

public class ReadLivroDto
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Paginas { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
