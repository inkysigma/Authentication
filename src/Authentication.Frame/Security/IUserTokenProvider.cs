using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface IUserTokenProvider<in TUser, out TToken>
    {
        TToken CreateToken(TUser user, string id, TokenField field);
    }

    public enum TokenField
    {
        Password,
        Email,
        Unlock
    }
}
