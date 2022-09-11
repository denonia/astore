using Astore.Domain;
using AutoMapper;

namespace Astore.WebApi.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfile, GetUserResponse>();
        CreateMap<Review, GetUserResponseReview>()
            .ForMember(r => r.ArticleId, opt =>
                opt.MapFrom(src => src.Article.Id));
        CreateMap<Article, GetUserResponseArticle>()
            .ForMember(a => a.Category, opt =>
                opt.MapFrom(src => src.Category.Name));
        CreateMap<CartItem, GetUserResponseCartItem>();

        CreateMap<UpdateUserRequest, UserProfile>()
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
    }
}