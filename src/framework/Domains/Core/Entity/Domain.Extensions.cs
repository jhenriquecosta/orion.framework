using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Orion.Framework.Domains;

namespace Orion.Framework
{
    public static class DomainExtensions
    {
        public static DomainEntity ToEntity(this object source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(DomainEntity);
            }
            if (!(source is IEntity entity))
                return default(DomainEntity);
            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<DomainEntity>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}
