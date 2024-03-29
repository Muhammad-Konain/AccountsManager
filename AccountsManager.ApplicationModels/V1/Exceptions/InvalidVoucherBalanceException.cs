﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.Exceptions
{
    public class InvalidVoucherBalanceException : Exception
    {
        public InvalidVoucherBalanceException(decimal credit, decimal debt):base($"Invalid net voucher balance. Credit:{credit}, debt:{debt}")
        {
        }
    }
}
