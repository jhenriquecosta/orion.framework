using System;
using System.Collections.Generic;

namespace Orion.Framework.Security.Domain.Dtos
{
    public class RoleDto
    {
        public string Name { get; set; }

        public List<string> Permissions { get; set; }

        public string FormattedPermissions
        {
            get
            {
                return String.Join(", ", Permissions.ToArray());
            }
        }
    }
}
