using FindYourFriend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindYourFriend.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
