using AutoMapper;
using LivrosAPI.Data.Dtos;
using LivrosAPI.Models;

namespace LivrosAPI.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<UpdateLivroDto, Livro>();
        CreateMap<Livro, UpdateLivroDto>();
        CreateMap<Livro, ReadLivroDto>();
    }
}
