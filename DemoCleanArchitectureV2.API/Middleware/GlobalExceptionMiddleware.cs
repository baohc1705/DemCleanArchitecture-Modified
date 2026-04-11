using DemoCleanArchitectureV2.API.Common;
using FluentValidation;
using System.Text.Json;

/*
 Định nghĩa class middleware để xử lý các exception
 Class được định nghĩa 1 lần để thêm vào pipeline kiểm tra
 không cần phải định nghĩa ở class khác
 */
namespace DemoCleanArchitectureV2.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
    

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Nếu request thành công thì chuyển sang pipeline tiếp theo
            }
            catch (Exception ex) // bắt exception nếu 1 request ném ra
            {
                await HandleException(context, ex); // xử lý các exception được ném
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            // custom api response 
            int statusCode = StatusCodes.Status500InternalServerError;
            string message = "Lỗi hệ thống";
            List<string> errors = new();

            switch (ex)
            {
                // Xử lý lỗi validation do FluentValidation ném ra (ValidationBehavior) 
                case ValidationException ve:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Dữ liệu không hợp lệ.";
                    errors = ve.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                //// Xử lý lỗi do domain layer ném ra
                //case DomainException de:
                //    statusCode = StatusCodes.Status400BadRequest;
                //    message = de.Message;
                //    break;

                //// Xử lý lỗi do các bussiness rule ném ra
                //case BusinessRuleException be:
                //    statusCode = StatusCodes.Status400BadRequest;
                //    message = be.Message;
                //    break;

                //case NotFoundException ne:
                //    statusCode = StatusCodes.Status404NotFound;
                //    message = ne.Message;
                //    break;

                case Exception e:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = e.Message;
                    break;
            }

            // Tạo đối tượng ApiResponse phản hồi
            var response = ApiResponse<object>.Fail(message, statusCode, errors);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
