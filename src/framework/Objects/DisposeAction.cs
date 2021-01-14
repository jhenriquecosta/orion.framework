using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using JetBrains.Annotations;

namespace Orion.Framework.Objects
{
    public class DisposeAction : System.IDisposable
    {
        public static readonly DisposeAction Null = new DisposeAction(null);
        private Action _action;

        /// <summary>
        ///     Creates a new <see cref="DisposeAction" /> object.
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed.</param>
        public DisposeAction([CanBeNull] Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            // Interlocked allows the continuation to be executed only once
            Action action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}
