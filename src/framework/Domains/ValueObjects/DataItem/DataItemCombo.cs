using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Domains.ValueObjects
{
    public class DataItemCombo
    {
        public int Key { get; set; }
        public string Text { get; set; }
        public object Value { get; set; }
        public string Descricao { get; set; }
        public object Id { get; set; }
    }
}
