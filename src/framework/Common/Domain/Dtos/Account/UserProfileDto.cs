using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Orion.Framework.Common.Domain.Dtos
{
    //Use Profile field until .NET Core has aspnetprofile table / functionality is created
    public class UserProfileDto
    {        
        [Key]
        public string UserId { get; set; }
        public long Id { get; set; }
        [Required]
        public string LastPageVisited { get; set; } = "/";
        public bool IsNavOpen { get; set; } = true;
        public bool IsNavMinified { get; set; } = false;
        public int Count { get; set; } = 0;
    }
}
