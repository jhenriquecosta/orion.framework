using Newtonsoft.Json;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Settings;
using Orion.Framework.Ui.Blazor.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Orion.Framework.Ui.Blazor.Icons
{
    
    public class IconManager
    {
        static List<DataItemCombo> internalData;
        IXTSysSettings _settings;
        public IconManager(IXTSysSettings settings)
        {
            _settings = settings;
        }

        public void CreateFileEnumFor(IconSource icon = IconSource.MaterialDesign, IconSize iconSize = IconSize.PX18)
        {
            const string quote = "\"";
            var filename = $"{icon.GetDescription<IconSource>()}.json";
            filename = Path.Combine(_settings.Folder.Icons, filename);
            var records = JsonConvert.DeserializeObject<List<OxIcon>>(System.IO.File.ReadAllText(filename));
            var _id = 0;
            var ws = File.CreateText("c:\\usr\\MaterialDesignIcon.cs");
            ws.WriteLine("using System.ComponentModel;");
            ws.WriteLine("namespace Works.Web.Common.Icons");
            ws.WriteLine("\t\t{");
            ws.WriteLine("\t\t\tpublic enum MaterialDesignIcon");
            ws.WriteLine("\t\t\t{");
            foreach(var record in records)
            {
               
                var prefix = "mdi";
                var name = $"{prefix}-{record.Name}";
                var desc = $"\t\t\t\t\t[Description({quote}{name}{quote})";
                ws.WriteLine(desc);
                var arr = record.Name.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
                var nameCapitalized = string.Empty;
                foreach (var oneString in arr)
                {
                    nameCapitalized += oneString.Substring(0, 1).ToUpper() + oneString.Substring(1);
                }

                ws.WriteLine($"\t\t\t\t\t{nameCapitalized},");
       
            }
            ws.WriteLine("\t\t\t}");
            ws.WriteLine("\t\t}");
            ws.Flush();
            ws.Close();

       
        }
        public List<DataItemCombo> GetIcons(IconSource icon = IconSource.MaterialDesign,IconSize iconSize=IconSize.PX18)
        {
            var lstIcons = new List<DataItemCombo>();
            var filename = $"{icon.GetDescription<IconSource>()}.json";
            filename = Path.Combine(_settings.Folder.Icons, filename);
            var records = JsonConvert.DeserializeObject<List<OxIcon>>(System.IO.File.ReadAllText(filename));
            var _id = 0;
            foreach(var record in records)
            {
                _id++;
                var data = new DataItemCombo();
                var prefix = string.Empty;
               
                if (icon == IconSource.MaterialDesign) prefix = "mdi";
                var suffix = $"{prefix}-{iconSize.GetDescription<IconSize>()}";
                var name = $"{prefix} {prefix}-{record.Name} {suffix}";
                data.Key = _id;
                data.Text = name;
                data.Descricao = record.Name;
                lstIcons.Add(data);
            }
            lstIcons = lstIcons.OrderBy(f=>f.Descricao).ToList();
            return lstIcons;
        }
        public object GetValue(string value)
        {  
          var icon = internalData.FirstOrDefault(f => f.Text.Equals(value));
          if (icon == null) icon = internalData.First();
          return icon.Key;           
        }
        public string GetName(object value)
        {
            var key = value.ToString().ToInt();
            var icon = internalData.FirstOrDefault(f => f.Key.Equals(key));
            if (icon == null) icon = internalData.First();
            return icon.Text;
        }
    }
}
