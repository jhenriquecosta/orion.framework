using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Objects
{
    /// <summary>
    /// Extended <see cref="System.IDisposable"/> with flag whether obect is already disposed.
    /// </summary>
    public interface IDisposable : System.IDisposable
    {
        /// <summary>
        /// <c>true</c> if object is already disposed; <c>false</c> otherwise.
        /// </summary>
        bool IsDisposed { get; }
    }
}
