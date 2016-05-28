using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface IPasswordHasher<TPassword>
    {
        Task<TPassword> HashPassword(string password, byte[] salt, CancellationToken cancellationToken);
    }
}
