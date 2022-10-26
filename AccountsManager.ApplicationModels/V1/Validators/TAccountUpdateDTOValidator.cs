using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.Validators
{
    public class TAccountUpdateDTOValidator : AbstractValidator<TAccountUpdateDTO>
    {
        public TAccountUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.AccountType)
                .IsInEnum();
        }
    }
}
