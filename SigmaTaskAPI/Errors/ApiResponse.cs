namespace SigmaTaskAPI.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        //-------------------------------
        public ApiResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request,you have made ",
                401 => "Authorized, you are not",
                404 => "Resourse was not found",
                500 => "interal server side error",
                _ => null
            };
        }
    }
}
