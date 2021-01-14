using System.ComponentModel.DataAnnotations;

namespace Orion.Framework.Common.Domain.Dtos
{

    public class ConfirmEmailDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
