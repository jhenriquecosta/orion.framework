using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Orion.Framework.Domains.ValueObjects
{
    public class DataMenuItem : TreeData
    {


        public string Home { get; set; }
        public string Component { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public int ApplicationId { get; set; }

    }

    //app_index.Text = "Sistemas";
    //app_index.IconCss = "mdi mdi-view-grid-plus-outline mdi-18px";
    //app_index.Url = "settings/sistema/manager";
    //menu.Items.Add(app_index);
   

}
