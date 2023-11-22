using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBrowTest.Core.Application.Features.Usuario.Create;
using RedBrowTest.Core.Application.Features.Usuario.Delete;
using RedBrowTest.Core.Application.Features.Usuario.GetAll;
using RedBrowTest.Core.Application.Features.Usuario.Update;
using RedBrowTest.Core.Application.Models;

namespace RedBrowTest.API.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsuariosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetUsuarios")]
        [ProducesResponseType(typeof(GetAllResponse), StatusCodes.Status200OK)]
        public async Task<GetAllResponse> GetUsuarios(string? search, int page = 1, int limit = 100)
        {
            return await mediator.Send(new GetAllQuery(search, page, limit));
        }

        [HttpPost(Name = "CreateUsuario")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<CreateResponse> CreateUsuario(CreateCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPut("{id}", Name = "UpdateUsuario")]
        [ProducesResponseType(typeof(UpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<UpdateResponse> UpdateUsuario(string id, UpdateCommand command)
        {
            command.IdUsuario = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}", Name = "DeleteUsuario")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<DeleteResponse> DeleteUsuario(string id)
        {
            return await mediator.Send(new DeleteCommand(id));
        }
    }
}
