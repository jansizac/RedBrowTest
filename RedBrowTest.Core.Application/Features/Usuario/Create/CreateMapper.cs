using AutoMapper;

namespace RedBrowTest.Core.Application.Features.Usuario.Create
{
    public sealed class CreateMapper : Profile
    {
        public CreateMapper()
        {
            CreateMap<CreateCommand, Domain.Usuario>();
            CreateMap<Domain.Usuario, CreateResponse>();
        }
    }
}
