using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        // ErrorMessage ="password must contain One capital letter one digit and One non-alphanumeric char")]
        [Required]
        public string Password { get; set; }
    }
}