using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame.Result;
using System.Threading;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim>
    {
        private async Task<AuthenticationResult<TUser>> FetchUserAsync(TUser user, CancellationToken canellationToken)
        {

        }
    }
}
