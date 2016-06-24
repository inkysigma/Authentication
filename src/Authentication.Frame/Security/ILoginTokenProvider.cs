using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface ILoginTokenProvider<in TUser, in TLogin>
    {
        Task<byte[]> HashTokensAsync(TLogin login, TUser user, byte[] salt, CancellationToken cancellationToken);

        Task<bool> CompareTokensAsync(TLogin login, TUser user, byte[] salt, byte[] hash, CancellationToken cancellationToken);
    }
}
