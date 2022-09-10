using Astore.Domain;
using AutoMapper;

namespace Astore.WebApi.Articles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Article, GetAllArticlesResponse>()
            .ForMember(a => a.Category, opt => 
                opt.MapFrom(src => src.Category.Name));
        
        CreateMap<Article, GetArticleResponse>()
            .ForMember(a => a.Category, opt => 
                opt.MapFrom(src => src.Category.Name));

        CreateMap<Review, GetArticleResponseReview>()
            .ForMember(r => r.AuthorId, opt =>
                opt.MapFrom(src => src.Author.UserId));

        CreateMap<CreateArticleRequest, Article>();
        CreateMap<UpdateArticleRequest, Article>()
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
    }
}