using System;
using System.Collections.Generic;

namespace Orion.Framework.Security.Domain.Dtos
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OwnerUserId { get; set; }
        public string OwnerName { get; set; }
        public List<string> Users { get; set; }
    }
}