using AutoMapper;

namespace RedBrowTest.Core.Application.Features.Usuario.GetAll
{
    public class GetAllMapper : Profile
    {
        public GetAllMapper()
        {
            CreateMap<RedBrowTest.Core.Application.Models.Pagination.PagedResult<RedBrowTest.Core.Domain.Usuario>, GetAllResponse>()
             .ForMember(dest => dest.Registros, opt => opt.MapFrom(src => src.Items))
             .ForMember(dest => dest.Meta, opt => opt.MapFrom(src => src.Meta));
            CreateMap<Domain.Usuario, UsuarioResponse>();
        }
    }
}
