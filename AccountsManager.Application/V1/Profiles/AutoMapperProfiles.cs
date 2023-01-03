using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.DataModels.V1.Models;
using AutoMapper;

namespace AccountsManager.Application.V1.Profiles
{
    public sealed class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TAccountCreateDTO, TAccount>();
            CreateMap<TAccount, TAccountReadDTO>();

            CreateMap<TAccountUpdateDTO, TAccount>();
            
            CreateMap<VoucherCreateDTO, Voucher>();
            CreateMap<Voucher, VoucherReadDTO>();

            CreateMap<TransactionCreateDTO, Transaction>();
        }
    }
}
