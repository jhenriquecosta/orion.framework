using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Exceptions
{
    public interface IExceptionHelper
    {
        void StoreException(Exception ex);
        Exception GetLatestException();
    }
}
