using System;


namespace Orion.Framework.Domains
{

    public abstract class AuditedEntity : EntityBase, IFullAudited
    {
        public virtual DateTime? CreatedTime { get; set; }
        public virtual int? CreatedUser { get; set; }
        public virtual DateTime? ChangedTime { get; set; }
        public virtual int? ChangedUser { get; set; }
    }
}
