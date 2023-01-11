using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.ApplicationModels.V1.Validators.TransactionValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.Validators.VoucherValidators
{
    public sealed class VoucherCreateDTOValidator : AbstractValidator<VoucherCreateDTO>
    {
        public VoucherCreateDTOValidator()
        {
            RuleFor(x => x.VoucherType)
                .IsInEnum()
                .WithMessage("Must be a valid voucher type.");

            RuleForEach(x => x.Transactions)
                .SetValidator(new TransactionCreateValidator());

            RuleFor(x => x.Transactions)
                .Must(m => m.Count >= 2)
                .WithMessage("Vocuher must contain atleast one transaction for credit and one for debt.");
        }
    }
}
