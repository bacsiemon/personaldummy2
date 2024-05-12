
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class AppUser : IdentityUser 
    {
        public string FullName { get; set; } = string.Empty;


        public DateTime DateOfBirth { get; set; }


        public string HomeAddress { get; set; } = string.Empty;


        public string JobPosition { get; set; } = string.Empty;

        public int Salary { get; set; }

    }
}
