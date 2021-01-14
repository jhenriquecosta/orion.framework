using Microsoft.Extensions.Logging;
using Orion.Framework.Logs;

namespace Orion.Framework.DataLayer.Commands
{
    /// <summary>
    /// Inherit from this base class if your command execution is going to affect tenants (institutions)
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        public int UnitId { get; set; }
         
        public virtual ILog Logger { get; set; }
                

    }

    public abstract class BaseCommandAsync : ICommandAsync
    {
        public string InstitutionCode { get; set; }
        public virtual ILog Logger { get; set; }

    }
}
