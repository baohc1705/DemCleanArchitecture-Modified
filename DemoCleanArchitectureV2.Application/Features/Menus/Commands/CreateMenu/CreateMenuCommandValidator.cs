using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {

        public CreateMenuCommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên menu không được để trống.")
                .MaximumLength(150).WithMessage("Tên menu tối đa 150 ký tự");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Đường dẫn không được để trống");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Thứ tự hiển thị phải >= 0");

        }
    }
}
