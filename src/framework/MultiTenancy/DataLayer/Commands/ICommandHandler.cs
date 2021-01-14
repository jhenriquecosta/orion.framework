﻿using System.Threading;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandlerAsync<TCommand> where TCommand : ICommandAsync
    {
        Task Handle(TCommand command, CancellationToken token = default(CancellationToken));
    }
}
