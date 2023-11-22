using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Application.Exceptions;

namespace RedBrowTest.Core.Application.Features.Usuario.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllQueryHandler> logger;

        public GetAllQueryHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  ILogger<GetAllQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GetAllResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            if (request.Page < 1)
            {
                throw new BadRequestException("La pagina debe ser mayor a 0");
            }
            if (request.Limit == 0)
            {
                throw new BadRequestException("El limite debe ser mayor a 0");
            }


            var response = await unitOfWork.UsuariosRepository.GetPagedAsync(
                request.Page,
                request.Limit,
                x => string.IsNullOrEmpty(request.Search) || 
                    (
                        (x.Nombre.ToLower().Contains(request.Search.ToLower())) ||
                        (x.Email.ToLower().Contains(request.Search.ToLower()))
                    )
                );

            return mapper.Map<GetAllResponse>(response);
        }
    }
}
