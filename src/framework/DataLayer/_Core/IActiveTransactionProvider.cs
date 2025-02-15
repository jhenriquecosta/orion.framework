﻿using System.Data;

namespace Orion.Framework.DataLayer
{
    
    public interface IActiveTransactionProvider
    {
        /// <summary>
        ///     Gets the active transaction.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args);

        /// <summary>
        ///     Gets the active connection.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args);
    }
}
