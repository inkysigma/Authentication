using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface IPasswordHasher
    {
        Task<byte[]> HashPassword(string password, byte[] salt, CancellationToken cancellationToken);
    }
}
