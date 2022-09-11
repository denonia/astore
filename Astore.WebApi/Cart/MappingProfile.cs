using Astore.Domain;
using AutoMapper;

namespace Astore.WebApi.Cart;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartItem, GetCartItemResponse>()
            .ForMember(item => item.ArticleId, opt => opt.MapFrom(src => src.Article.Id));
        CreateMap<UpdateCartRequest, CartItem>();
    }
}