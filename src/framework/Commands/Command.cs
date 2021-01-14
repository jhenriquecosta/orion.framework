using Orion.Framework.Core;

namespace Orion.Framework.Commands
{
    public abstract class Command : IMessage
    {
        public string CorrelationId { get; set; }
    }
}
