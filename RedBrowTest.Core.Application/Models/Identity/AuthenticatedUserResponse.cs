﻿namespace RedBrowTest.Core.Application.Models.Identity
{
    public class AuthenticatedUserResponse
    {
        public string AccessToken { get; set; } = string.Empty!;
        public string RefreshToken { get; set; } = string.Empty!;
    }
}
