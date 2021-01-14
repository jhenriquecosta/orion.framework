using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Orion.Framework.Exceptions
{
    /// <summary>
    ///     Base exception type for those are thrown by Stove system for Stove specific exceptions.
    /// </summary>
    [Serializable]
    public class FWorkException : Exception
    {
        /// <summary>
        ///     Creates a new <see cref="StoveException" /> object.
        /// </summary>
        public FWorkException()
        {
        }

        /// <summary>
        ///     Creates a new <see cref="StoveException" /> object.
        /// </summary>
        public FWorkException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="StoveException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public FWorkException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="StoveException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public FWorkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
