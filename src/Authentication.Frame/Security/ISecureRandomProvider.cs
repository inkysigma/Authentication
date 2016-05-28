using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Security
{
    public interface ISecureRandomProvider
    {
        Task<byte[]> GenerateRandomAsync(CancellationToken token);
    }
}