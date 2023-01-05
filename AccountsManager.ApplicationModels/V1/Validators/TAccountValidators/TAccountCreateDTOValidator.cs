using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using FluentValidation;

namespace AccountsManager.ApplicationModels.V1.Validators.TAccountValidators
{
    public sealed class TAccountCreateDTOValidator : AbstractValidator<TAccountCreateDTO>
    {
        public TAccountCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.AccountType)
                .IsInEnum()
                .WithMessage("Must be a valid account type.");
        }
    }
}
