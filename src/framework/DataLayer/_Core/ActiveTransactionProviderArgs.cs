using System.Collections.Generic;

namespace Orion.Framework.DataLayer
{
    public class ActiveTransactionProviderArgs : Dictionary<string, object>
    {
        public static ActiveTransactionProviderArgs Empty = new ActiveTransactionProviderArgs();
    }
}
