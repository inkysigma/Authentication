using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Result;

namespace Authentication.Frame
{
    public interface IValidator<in T>
    {
        Task<ValidateResult> ValidateAsync(T input, CancellationToken cancellationToken);
    }
}