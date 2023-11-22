﻿namespace RedBrowTest.Core.Application.Models.Pagination
{
    public class GenericPaginationResponse
    {
        public int TotalItems { get; set; }
        public int ItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
