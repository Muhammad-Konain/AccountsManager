﻿using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;

namespace AccountsManager.Application.V1.Contracts
{
    public interface IVoucherService
    {
        Task CreateVoucher(VoucherCreateDTO voucherCreateDTO);
    }
}
