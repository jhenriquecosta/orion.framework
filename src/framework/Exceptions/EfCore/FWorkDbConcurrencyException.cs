using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Orion.Framework.Exceptions
{
    public class FWorkDbConcurrencyException : FWorkException
    {
        /// <summary>
        ///     Creates a new <see cref="FWorkDbConcurrencyException" /> object.
        /// </summary>
        public FWorkDbConcurrencyException()
        {
        }

        /// <summary>
        ///     Creates a new <see cref="StoveException" /> object.
        /// </summary>
        public FWorkDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="FWorkDbConcurrencyException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public FWorkDbConcurrencyException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="FWorkDbConcurrencyException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public FWorkDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
