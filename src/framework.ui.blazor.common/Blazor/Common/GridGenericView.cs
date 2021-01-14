using Orion.Framework.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Ui.Blazor.Common
{
   
    
    public class GridGenericView
    {
         public GridGenericView()
        {
        }
       
        public string Caption { get; set; }
        public string Descricao { get; set; }
        
        public Type View { get; set; }
        public Type Model { get; set; }
    }
}
