using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.Exceptions
{
    public class EntityNotFoundExcetption : Exception
    {
        public EntityNotFoundExcetption(Guid entityId):base($"No entity found having id: {entityId}")
        {
        }
        public EntityNotFoundExcetption(Guid entityId, string entityType) : base($"No {entityType} found having id: {entityId}")
        {
        }
        //public EntityNotFoundExcetption(List<Guid> entityIds, string entityType) : base($"No {entityType}(s) found having id(s): {entityIds.Cast<string>().ToList()}")
        public EntityNotFoundExcetption(List<Guid> entityIds, string entityType) : base($"No {entityType}(s) found having id(s): {string.Join(",", entityIds)}")
        {
        }

    }
}
