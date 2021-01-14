using System;
using Zeus.Domains.Auditing;

namespace Zeus.Domains
{
 
    public class EntityAudited : ICreationAudited<int>, IModificationAudited<int>
    {
        public DateTime? CreatedTime { get; set; }
        public int CreatedUser { get; set; }
        public DateTime? ChangedTime { get; set; }
        public int ChangedUser { get; set; }
    }
}
