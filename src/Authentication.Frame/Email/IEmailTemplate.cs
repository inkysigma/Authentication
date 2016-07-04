using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Authentication.Frame.Email
{
    public interface IEmailTemplate
    {
        string Subject { get; set; }

        Task<string> StringifyAsync(CancellationToken cancellationToken);
        Task<string> PlainStringifyAsync(CancellationToken cancellationToken);

        Task LoadAsync(dynamic properties);
    }
}
