using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id nên lớn hơn 0");
        }
    }
}
