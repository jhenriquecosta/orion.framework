using System;
using System.Reflection;
using Framework.DataLayer.Models.Mappings.Attributes;

namespace Framework.DataLayer.NHibernate.Manager.Audit
{
    public static class PropertyTracking
    {
        public static bool IsTrackingEnabled(PropertyConfigKey property, Type entityType)
        {
            bool isEnabled;
            if (!TrackingDataStore.PropertyConfigStore.TryGetValue(property, out isEnabled))
            {
                isEnabled = entityType.GetProperty(property.PropertyName).GetCustomAttribute<IgnoreInAuditLogAttribute>() == null;
                TrackingDataStore.PropertyConfigStore.TryAdd(property, isEnabled);
            }
            return isEnabled;
        }
    }
}
