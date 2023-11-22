using AutoMapper;

namespace RedBrowTest.Core.Application.Features.Usuario.Update
{
    public class UpdateMapper : Profile
    {
        public UpdateMapper()
        {
            CreateMap<UpdateCommand, Domain.Usuario>();
            CreateMap<Domain.Usuario, UpdateResponse>();
        }
    }
}
