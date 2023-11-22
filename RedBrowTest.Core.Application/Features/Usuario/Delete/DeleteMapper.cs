using AutoMapper;

namespace RedBrowTest.Core.Application.Features.Usuario.Delete
{
    public class DeleteMapper : Profile
    {
        public DeleteMapper()
        {
            CreateMap<Domain.Usuario, DeleteResponse>();
        }
    }
}
