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

            CreateMap<Recipe, RecipeDto>()
                .ForMember(dst => dst.RecipeID, opt => opt.MapFrom(src => src.RecipeID))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dst => dst.PrepareTime, opt => opt.MapFrom(src => src.PrepareTime))
                .ForMember(dst => dst.PostData, opt => opt.MapFrom(src => src.PostData))
                .ForMember(dst => dst.Portions, opt => opt.MapFrom(src => src.Portions))
                .ForMember(dst => dst.Difficulty, opt => opt.MapFrom(src => src.Difficulty))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID));
            CreateMap<RecipeDto, Recipe>()
                .ForMember(dst => dst.RecipeID, opt => opt.MapFrom(src => src.RecipeID))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dst => dst.PrepareTime, opt => opt.MapFrom(src => src.PrepareTime))
                .ForMember(dst => dst.PostData, opt => opt.MapFrom(src => src.PostData))
                .ForMember(dst => dst.Portions, opt => opt.MapFrom(src => src.Portions))
                .ForMember(dst => dst.Difficulty, opt => opt.MapFrom(src => src.Difficulty))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID));

            CreateMap<Ingridient, IngridientDto>()
                .ForMember(dst => dst.IngridientID, opt => opt.MapFrom(src => src.IngridientID))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<IngridientDto, Ingridient>()
                .ForMember(dst => dst.IngridientID, opt => opt.MapFrom(src => src.IngridientID))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UserDto, User>();

            //CreateMap<User, UserDto>();
        }
    }
}
