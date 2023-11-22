namespace RedBrowTest.Core.Application.Models
{
    public class ErrorResponse
    {
        public string? ErrorCode { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }

        public ErrorResponse(string errorMessage, string? errorCode = null)
        {
            ErrorMessages = new List<string> { errorMessage };
            ErrorCode = errorCode;
        }

        public ErrorResponse(IEnumerable<string> errorMessages, string? errorCode = null)
        {
            ErrorMessages = errorMessages;
            ErrorCode = errorCode;
        }
    }
}
