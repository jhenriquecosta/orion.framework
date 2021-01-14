using System;

namespace Orion.Framework.Commands
{
    public static class CommandContextAccessorExtensions
    {
        public static string GetCorrelationIdOrEmpty(this IOrionCommandContextAccessor accessor)
        {
            if (accessor.CommandContext != null)
            {
                return accessor.CommandContext.CorrelationId;
            }

            return Guid.Empty.ToString();
        }
    }
}
