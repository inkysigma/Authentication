using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Email
{
    public interface IEmailProvider
    {
        Task Email(IEmailTemplate template, string destination, CancellationToken cancellationToken);
    }
}
