namespace RedBrowTest.Core.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public string? ErrorCode { get; set; }

        public BadRequestException(string message, string? errorCode = null) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
