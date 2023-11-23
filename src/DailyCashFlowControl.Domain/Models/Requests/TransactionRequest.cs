using FluentValidation;

namespace DailyCashFlowControl.Domain.Models.Requests
{
    public record TransactionRequest(string? Type, decimal? Value, string? Description, string? HubClientId);

    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            //debit | credit
            RuleFor(x => x.Description).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Campo obrigatório")
                .Must(type => type.ToLower() == "debit" || type.ToLower() == "credit")
            .WithMessage("Os valores aceitos são 'debit' or 'credit'");
            RuleFor(x => x.Value).NotNull().WithMessage("Campo obrigatório")
                .GreaterThan(0)
            .WithMessage("Infome um valor maior que zero");
        }
    }

}
