using System;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Applications.Dtos

{
    public class BaseDto : DtoBase
    {
        [ModelField(Ignore = true)]
        public bool IsDeleted { get; set; }

        [ModelField(Ignore = true)]
        public int Version { get; set; }

        [ModelField(Ignore = true)]        
        public virtual DateTime? CreatedTime { get; set; }

        [ModelField(Ignore = true)]
        public virtual int? CreatedUser { get; set; }

        [ModelField(Ignore = true)]
        public virtual DateTime? ChangedTime { get; set; }

        [ModelField(Ignore = true)]
        public virtual int? ChangedUser { get; set; }

    }
}
