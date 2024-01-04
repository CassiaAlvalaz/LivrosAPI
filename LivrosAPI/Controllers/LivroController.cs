using AutoMapper;
using LivrosAPI.Data;
using LivrosAPI.Data.Dtos;
using LivrosAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LivrosAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class LivroController : ControllerBase
{

    private LivrosContext _context;
    private IMapper _mapper;

    public LivroController(LivrosContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um livro ao banco de dados.
    /// </summary>
    /// <param name="livroDto">Objeto com os campos necessários para criação de um livro</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaLivro([FromBody] CreateLivroDto livroDto)
    {
        Livro livro = _mapper.Map<Livro>(livroDto);
        _context.Livros.Add(livro);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ListaLivrosPorID), new {id = livro.Id}, livro);
    }

    /// <summary>
    /// Lista a relação de livros que consta no banco de dados.
    /// </summary>
    /// <response code="200">Caso a busca seja feita com sucesso.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IEnumerable<ReadLivroDto> ListaLivros([FromQuery] int skip = 1, [FromQuery] int take = 5)
    {
        return _mapper.Map<List<ReadLivroDto>>(_context.Livros.Skip(skip).Take(take));
    }


    /// <summary>
    /// Lista a relação de livros que consta no banco de dados por id.
    /// </summary>
    /// <response code="200">Caso a busca por id seja feita com sucesso.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public IActionResult ListaLivrosPorID(int id)
    {
        var livro =  _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        var livroDto = _mapper.Map<ReadLivroDto>(livro);
        return Ok(livroDto);
    }


    /// <summary>
    /// Atualização das informações de livros já cadastrados no banco de dados.
    /// </summary>
    /// <response code="204">Caso a busca seja feita com sucesso.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public IActionResult AtualizaLivro(int id, [FromBody] UpdateLivroDto livroDto)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        _mapper.Map(livroDto, livro);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualização dos livros já cadastrados no banco de dados por parâmetro.
    /// </summary>
    /// <response code="204">Caso a busca seja feita com sucesso.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public IActionResult AtualizaLivroPatch(int id, JsonPatchDocument<UpdateLivroDto> patch)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();

        var livroParaAtualizar = _mapper.Map<UpdateLivroDto>(livro);
        patch.ApplyTo(livroParaAtualizar, ModelState);

        if (!TryValidateModel(livroParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(livroParaAtualizar, livro);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Faz a exclusão do filme cadastrado no banco de acordo com o id.
    /// </summary>
    /// <response code="204">Caso a exclusão seja feita com sucesso.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public IActionResult DeletaLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        _context.Remove(livro);
        _context.SaveChanges();
        return NoContent();
    }
}
