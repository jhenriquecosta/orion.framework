using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Exceptions
{
    public class ExceptionHelper : IExceptionHelper
    {
        private Exception _ex;

        public void StoreException(Exception ex)
        {
            _ex = ex ?? throw new ArgumentNullException(nameof(ex));
        }

        public Exception GetLatestException()
        {
            return _ex;
        }
    }
}
