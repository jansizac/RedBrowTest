using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RedBrowTest.Core.Application.Contracts.Hashers;
using RedBrowTest.Core.Application.Contracts.Persistence;
using Domain = RedBrowTest.Core.Domain;

namespace RedBrowTest.Core.Application.Features.Usuario.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, CreateResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<CreateCommandHandler> logger;
        private readonly IPasswordHasher passwordHasher;

        public CreateCommandHandler(IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    ILogger<CreateCommandHandler> logger,
                                    IPasswordHasher passwordHasher)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
        }

        public async Task<CreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            // validamos si el email que se define en el request no existe previamente en la bd
            var usuario = await unitOfWork.UsuariosRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);
            if (usuario != null)
            {
                throw new Exception($"Ya existe un usuario registrado con el email '{request.Email}'.");
            }        
        
            // mapeamos nuestro command a nuestro modelo de dominio
            usuario = mapper.Map<Domain.Usuario>(request);
            // insertamos nuestro modelo de dominio en la base de datos
            usuario = await unitOfWork.UsuariosRepository.AddAsync(usuario);
            usuario.Password = passwordHasher.HashPassword(request.Password);
            logger.LogInformation($"Usuario creado con id: {usuario.IdUsuario}");
            // retornamos el mapeo del usuario creado
            return mapper.Map<CreateResponse>(usuario);            
        }
    }
}
