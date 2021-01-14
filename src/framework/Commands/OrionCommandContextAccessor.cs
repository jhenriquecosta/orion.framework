using System;
using Orion.Framework.Dependency;
using Orion.Framework.Runtime;

namespace Orion.Framework.Commands
{
    public class OrionCommandContextAccessor : IOrionCommandContextAccessor, ITransientDependency
    {
        private const string CommandContextKey = "Orion.Commands.Context";

        private readonly IAmbientScopeProvider<CommandContext> _commandScopeProvider;

        public OrionCommandContextAccessor(IAmbientScopeProvider<CommandContext> commandScopeProvider)
        {
            _commandScopeProvider = commandScopeProvider;
        }

        public IDisposable Use(string correlationId)
        {
            return _commandScopeProvider.BeginScope(CommandContextKey, new CommandContext
            {
                CorrelationId = correlationId
            });
        }

        public IDisposable Use(Action<CommandContext> contextCallback)
        {
            var ctx = new CommandContext();
            contextCallback(ctx);
            return _commandScopeProvider.BeginScope(CommandContextKey, ctx);
        }

        public void Manipulate(Action<CommandContext> contextCallback)
        {
            contextCallback(CommandContext);
        }

        public CommandContext CommandContext => _commandScopeProvider.GetValue(CommandContextKey);
    }
}
