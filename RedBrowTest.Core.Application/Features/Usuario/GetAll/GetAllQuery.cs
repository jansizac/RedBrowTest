using MediatR;

namespace RedBrowTest.Core.Application.Features.Usuario.GetAll
{
    public class GetAllQuery : IRequest<GetAllResponse>
    {
        public GetAllQuery(string? search,
                           int page = 1,
                           int limit = 100)
        {
            Search = search;
            Page = page;
            Limit = limit;
        }

        public string? Search { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
