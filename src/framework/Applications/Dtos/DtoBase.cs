using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Applications.Dtos

{

    public abstract class DtoBase : RequestBase, IDto 
    {
       
        
        [ModelField(Display ="#",Width ="50",AllowEdit =false,Order = 1)]
        public string Id { get; set; }
    }
}
