using FluentValidation;

namespace DailyCashFlowControl.Domain.Models.Requests
{
    public record TransactionRequest(string? Type, decimal? Value, string? Description);

    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotNull();
        }
    }

}
