using Astore.Domain;
using AutoMapper;

namespace Astore.WebApi.Reviews;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateReviewRequest, Review>();
        CreateMap<UpdateReviewRequest, Review>();
        
        CreateMap<Review, GetReviewResponse>()
            .ForMember(r => r.AuthorId, opts => opts.MapFrom(src => src.Author.UserId));
    }
}