using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.Exceptions
{
    public class EntityNotFoundExcetption : Exception
    {
        public EntityNotFoundExcetption()
        {
        }

        public EntityNotFoundExcetption(Guid entityId, string message):base($"No entity found having id: {entityId}")
        {

        }
    }
}
