using System.ComponentModel.DataAnnotations;

namespace Orion.Framework.Security.Domain.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
