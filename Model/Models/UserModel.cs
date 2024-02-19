using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
