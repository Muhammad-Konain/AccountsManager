using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using FluentValidation;

namespace AccountsManager.ApplicationModels.V1.Validators.TransactionValidators
{
    internal class TransactionCreateValidator : AbstractValidator<TransactionDTO>
    {
        public TransactionCreateValidator()
        {
            RuleFor(s=>s.Credit)
                .GreaterThanOrEqualTo(0);

            RuleFor(s=>s.Debt)
                .GreaterThanOrEqualTo(0);

        }
    }
}
