using Educode.Domain.Shared;

namespace Educode.Presentation.ApiModels
{
    public class ApiResponse
    {
        public static ApiResponse Create(object result, bool success)
        {
            return new ApiResponse(result, success);
        }

        public bool Success { get; set; }

        public object? Data { get; set; }

        public object? Error { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(object data, bool success)
        {
            Success = success;

            if (success)
            {
                Data = data;
            }
            else
            {
                Error = data;
            }
        }

        public ApiResponse(Error error)
        {
            Success = false;
            Error = error;
        }
    }
}
