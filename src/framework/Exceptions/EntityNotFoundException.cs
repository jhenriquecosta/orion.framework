using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Exceptions
{
    public class EntityNotFoundException : Warning
    {
        public EntityNotFoundException(string message)
          : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception exception, string code)
        : base(message, code, exception)
        {
            
        }

        public EntityNotFoundException(Type entityType, object id)
            : base(string.Format("{0} entity not found by id={1}.", entityType.Name, id))
        {
        }

        public EntityNotFoundException(Type entityType, string filter)
            : base(string.Format("{0} entity not found by filter {1}.", entityType.Name, filter))
        {
        }
    }
}
