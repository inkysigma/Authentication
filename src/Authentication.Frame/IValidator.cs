using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame
{
    public interface IValidator<in T>
    {
        Task<ValidateResult> ValidateAsync(T input, CancellationToken cancellationToken);
    }
}