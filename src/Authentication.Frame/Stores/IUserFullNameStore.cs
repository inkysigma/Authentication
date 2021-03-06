﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserFullNameStore<in TUser>: IStore
    {
        Task<QueryResult<string>> FetchFullNameAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> CreateUserAsync(TUser user, string name, CancellationToken cancellationToken);

        Task<ExecuteResult> SetUserFullNameAsync(TUser user, string name, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);
    }
}