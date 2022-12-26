using Business.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IExternalLoginRepository
    {
        Task<List<UserLoginInfo>> ListForUserId(UserMasterMetadata user, CancellationToken cancellationToken);

        Task CreateExternalLoginUser(UserMasterMetadata user, UserLoginInfo login, CancellationToken cancellationToken);

        Task<int> GetUserIdByLoginProvider(string loginProvider, string providerKey, CancellationToken cancellationToken);

        Task RemoveLogin(UserMasterMetadata user, string loginProvider, string providerKey, CancellationToken cancellationToken);
    }
}
