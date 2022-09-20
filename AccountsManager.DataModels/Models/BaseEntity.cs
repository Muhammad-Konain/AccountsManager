using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataModels.Models
{
    internal class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
