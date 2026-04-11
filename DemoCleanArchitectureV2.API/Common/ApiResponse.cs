namespace DemoCleanArchitectureV2.API.Common
{
    // Lớp định nghĩa Response của Api
    // Custom Api
    public class ApiResponse<T>
    {
        public int Code { get; set; } = 1000;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Method dùng để hiển thị nếu response phản hồi thành công
        public static ApiResponse<T> Ok(T data, string message = "Success", int code = 1000)
            => new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Code = code
            };

        // Method dùng để hiển thị nếu response phản hồi thất bại
        public static ApiResponse<T> Fail(string message, int code = 1001, List<string>? errors = null)
            => new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Code = code,
                Errors = errors
            };
    }
}
