using System.Collections.Generic;

namespace Api.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<HeroDto> Heroes {get; set;}
        public string Token { get; set; }

    }
}