using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataModels.V1.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DeletedOn { get; set; }
    }
}
