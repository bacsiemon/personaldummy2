using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User.ResponseEntities
{
    public class RegisterResponseEntity
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? ErrorMessage { get; set; }

        public IdentityResult? Result { get; set; }
    }
}
