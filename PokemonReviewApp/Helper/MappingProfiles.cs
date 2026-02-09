using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
         MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}