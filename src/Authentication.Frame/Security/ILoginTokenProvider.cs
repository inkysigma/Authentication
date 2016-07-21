using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface ILoginTokenProvider<in TUser, in TLogin>
    {
        Task<byte[]> HashTokensAsync(TUser user, byte[] salt, CancellationToken cancellationToken);

        Task<bool> CompareTokensAsync(TLogin login, TLogin second, CancellationToken cancellationToken);
    }
}
