﻿using System.Threading.Tasks;

namespace Orion.Framework.Applications.Operations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeleteAsync {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">，："1,2"</param>
        Task DeleteAsync( string ids );
    }
}