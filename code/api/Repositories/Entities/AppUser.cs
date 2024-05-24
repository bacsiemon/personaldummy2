﻿
using Microsoft.AspNetCore.Identity;

namespace Repositories.Entities
{
    public class AppUser : IdentityUser
    {

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryDate { get; set; }

        public Employee? Employee { get; set; }
    }
}
