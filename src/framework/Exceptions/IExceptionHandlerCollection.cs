using System.Collections.Generic;
using Orion.Framework.Exceptions.Handlers;

namespace Orion.Framework.Exceptions
{
    /// <summary>
    /// The collection of the exception handlers.
    /// </summary>
    public interface IExceptionHandlerCollection
    {
        /// <summary>
        /// Enumerates registered handlers.
        /// </summary>
        /// <returns>The enumeration of registered handlers.</returns>
        IEnumerable<IExceptionHandler> Enumerate();

        /// <summary>
        /// Adds <paramref name="handler"/> to handle exceptions.
        /// </summary>
        /// <param name="handler">The handler to add.</param>
        /// <returns>Self (for fluency).</returns>
        IExceptionHandlerCollection Add(IExceptionHandler handler);
    }
}
