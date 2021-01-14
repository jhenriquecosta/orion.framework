using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public interface IOxGridBase
    {
        Task ReloadAsync(object data);
        
    }
}
