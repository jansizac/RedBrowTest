namespace RedBrowTest.Core.Application.Models.Pagination
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T>? Items { get; set; }
        public GenericPaginationResponse? Meta { get; set; }
    }
}
