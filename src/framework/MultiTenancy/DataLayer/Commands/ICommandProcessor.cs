using System.Threading;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.Commands
{
    public interface ICommandProcessor
    {
        void Process(ICommand command);
        Task ProcessAsync(ICommandAsync command, CancellationToken token = default(CancellationToken));
    }
}
