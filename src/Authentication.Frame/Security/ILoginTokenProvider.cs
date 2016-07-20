using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface ILoginTokenProvider<in TUser, TLogin>
    {
        Task<TLogin> CreateTokenAsync(TUser user, byte[] salt, CancellationToken cancellationToken);

        Task<byte[]> EncryptAsync(TLogin login, byte[] input, CancellationToken cancellationToken);

        Task<byte[]> DecryptAsync(TLogin login, byte[] input, CancellationToken cancellationToken);
    }
}
