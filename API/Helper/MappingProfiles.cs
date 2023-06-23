using AutoMapper;
using Model.DTO;
using Model.MODEL;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Opinion, OpinionDto>();
            CreateMap<OpinionDto, Opinion>();
            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Ingridient, IngridientDto>();
            CreateMap<IngridientDto, Ingridient>();
        }
    }
}
