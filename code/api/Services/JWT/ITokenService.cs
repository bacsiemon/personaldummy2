using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.JWT
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
