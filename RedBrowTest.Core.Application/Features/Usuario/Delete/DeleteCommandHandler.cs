using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Application.Exceptions;

namespace RedBrowTest.Core.Application.Features.Usuario.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteResponse>
    {
        private readonly ILogger<DeleteCommandHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteCommandHandler(ILogger<DeleteCommandHandler> logger,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            // validamos que el usuario exista
            var usuario = await unitOfWork.UsuariosRepository.GetByIdAsync(request.Id);
            if (usuario == null)
            {
                throw new NotFoundException(nameof(Usuario), request.Id);
            }
            // validamos que el usuario que se quiere eliminar no sea admin@mail.com
            if (usuario.Email == "admin@mail.com")
            {
                throw new BadRequestException("No se puede eliminar el usuario admin");
            }

            // eliminamos el usuario de la bd
            await unitOfWork.UsuariosRepository.DeleteAsync(usuario);

            // mapeamos la respuesta y retornamos
            return mapper.Map<DeleteResponse>(usuario);
        }
    }
}
