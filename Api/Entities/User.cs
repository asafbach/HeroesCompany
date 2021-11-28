using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        internal static object FindFirst(string nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}