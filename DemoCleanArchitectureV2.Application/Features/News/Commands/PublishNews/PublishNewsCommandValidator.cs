using FluentValidation;

namespace DemoCleanArchitectureV2.Application.Features.News.Commands.PublishNews
{
    public class PublishNewsCommandValidator : AbstractValidator<PublishNewsCommand>
    {
        public PublishNewsCommandValidator()
        {
            RuleFor(x => x.ScheduledAt)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.ScheduledAt.HasValue)
                .WithMessage("Thời gian lên lịch phải ở trong tương lai");
        }
    }
}
