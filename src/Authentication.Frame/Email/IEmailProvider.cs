using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Email
{
    public interface IEmailProvider
    {
        Task Email(IEmailTemplate template, string destination, CancellationToken cancellationToken);
    }
}
