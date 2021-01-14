using System.Collections.Generic;
using Orion.Framework.Infrastructurelications;
using Orion.Framework.Exceptions.Handlers;
using Orion.Framework.Validations;

namespace Orion.Framework.Exceptions
{
    /// <summary>
    /// The default implementation of the <see cref="IExceptionHandlerCollection"/>
    /// </summary>
    public class DefaultExceptionHandlerCollection : IExceptionHandlerCollection
    {
        private readonly List<IExceptionHandler> storage = new List<IExceptionHandler>();

        public IEnumerable<IExceptionHandler> Enumerate()
        {
            return storage;
        }

        public IExceptionHandlerCollection Add(IExceptionHandler handler)
        {
            Ensure.NotNull(handler, "handler");
            storage.Add(handler);
            return this;
        }
    }
}
