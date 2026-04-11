using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.UpdateNews
{
    public class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        public UpdateNewsCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title không được để trống.")
                .MaximumLength(300).WithMessage("Title tối đa 300 ký tự.");

            RuleFor(x => x.Summary)
                .NotEmpty().WithMessage("Summary không được để trống.")
                .MaximumLength(500).WithMessage("Summary tối đa 500 ký tự.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content không được để trống.");

            RuleFor(x => x.ThumbnailUrl)
                .NotEmpty().WithMessage("ThumbnailUrl không được để trống.");         
        }
    }
}
