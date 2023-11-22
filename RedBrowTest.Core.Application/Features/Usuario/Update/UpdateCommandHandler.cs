using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RedBrowTest.Core.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Core.Application.Features.Usuario.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, UpdateResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UpdateCommandHandler> logger;
        private readonly IMapper mapper;

        public UpdateCommandHandler(IUnitOfWork unitOfWork,
                                    ILogger<UpdateCommandHandler> logger,
                                    IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<UpdateResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            // buscamos el usuario
            var usuario = await unitOfWork.UsuariosRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null)
            {
                throw new Exception($"No existe un usuario con el id '{request.IdUsuario}'.");
            }

            //mapeamos el request al usuario
            mapper.Map(request, usuario, typeof(UpdateCommand), typeof(Domain.Usuario));
            // actualizamos el usuario
            usuario = await unitOfWork.UsuariosRepository.UpdateAsync(usuario);
            logger.LogInformation($"Usuario actualizado con id: {usuario.IdUsuario}");
            // mapeamos y retornamos el resultado
            return mapper.Map<UpdateResponse>(usuario);
        }
    }
}
