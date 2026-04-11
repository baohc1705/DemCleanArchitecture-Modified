using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.DeleteNews
{
    public class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
    {
        public DeleteNewsCommandValidator() => RuleFor(x => x.Id).GreaterThan(0);
    }
}
