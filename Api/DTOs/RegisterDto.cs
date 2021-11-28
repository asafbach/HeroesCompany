
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage ="password must contain at least 8 characters,  One capital letter , one digit and One non-alphanumeric char")]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}