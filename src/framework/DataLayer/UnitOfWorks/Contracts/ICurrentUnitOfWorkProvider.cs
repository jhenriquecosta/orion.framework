using Orion.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.UnitOfWorks.Contracts
{
    /// <summary>
    /// Used to get/set current <see cref="IUnitOfWork"/>. 
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// Gets/sets current <see cref="IUnitOfWork"/>.
        /// </summary>
        IUnitOfWork Current { get; set; }
        
    }
}
