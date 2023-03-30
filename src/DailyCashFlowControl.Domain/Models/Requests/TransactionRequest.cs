using FluentValidation;

namespace DailyCashFlowControl.Domain.Models.Requests
{
    public record TransactionRequest(string? Type, decimal? Value);

    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotNull();
        }
    }

}
