using Business.Entities;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISiteUserService
    {
        Task<int> CreateUser(UserMasterMetadata user, CancellationToken cancellationToken);
        Task<UserMasterMetadata> FindByEmail(string normalizedEmail, CancellationToken cancellationToken);
        Task<UserMasterMetadata> FindById(string userId, CancellationToken cancellationToken);
        PagedDataTable<UserMasterMetadata> GetAllUser(int companyID, int pageNo = 1, int pageSize = 0, string orderBy = "UserID", string sortBy = "ASC", string searchString = "");
        Task<IdentityResult> UpdateUser(UserMasterMetadata user, CancellationToken cancellationToken);

        Task<IdentityResult> AddUserToRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken);
        Task<bool> IsUserInRoleAsync(int UserId, int RoleId);
        Task<IdentityResult> RemoveUserFromRole(int UserId, int RoleId, CancellationToken cancellationToken);
        Task<UserMasterMetadata> FindByName(string normalizedUserName, CancellationToken cancellationToken);
        PagedDataTable<UserClaimsMetadata> GetAllUserClaimAuth(int companyID, int userID);

    }
}
