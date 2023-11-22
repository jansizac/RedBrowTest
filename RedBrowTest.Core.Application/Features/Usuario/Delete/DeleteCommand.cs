using MediatR;

namespace RedBrowTest.Core.Application.Features.Usuario.Delete
{
    public class DeleteCommand : IRequest<DeleteResponse>
    {
        public DeleteCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = null!;
    }
}
