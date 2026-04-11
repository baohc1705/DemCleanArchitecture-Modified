using FluentValidation;
using MediatR;

// tạo một pipeline behavior để thêm vào luồng của một request xử lý validation 

namespace DemoCleanArchitectureV2.Application.Common.Validators
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            //Lấy tất cả validator của TRequest (CreateMenuCommandValidator)
            
            var validationFailure = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context)));

            var failures = validationFailure
                .SelectMany(v => v.Errors)
                .Where(f => f != null)
                .ToList();

            // Nếu lỗi thì ném ra validation exception
            // lỗi này sẽ được bắt lại và xử lý ở GlobalExceptionHandler middleware
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
