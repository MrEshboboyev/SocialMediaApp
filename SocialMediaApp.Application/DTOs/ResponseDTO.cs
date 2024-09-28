namespace SocialMediaApp.Application.DTOs
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        // Constructor for success response
        public ResponseDTO(T? data, string message = "Operation successful!")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor for error response
        public ResponseDTO(string message)
        {
            Success = false;
            Message = message;
            Data = default; // Data is null for error cases
        }
    }
}
