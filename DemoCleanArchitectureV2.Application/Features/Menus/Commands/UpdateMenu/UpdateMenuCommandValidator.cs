using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id > 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên menu không được để trống.")
                .MaximumLength(150);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("DisplayOrder nên > 0");
            
        }
    }
}
