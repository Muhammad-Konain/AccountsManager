using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using FluentValidation;

namespace AccountsManager.ApplicationModels.V1.Validators
{
    public class TAccountCreateDTOValidator : AbstractValidator<TAccountCreateDTO>
    {
        public TAccountCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]+");

            RuleFor(x => x.AccountType)
                .IsInEnum();
        }
    }
}
