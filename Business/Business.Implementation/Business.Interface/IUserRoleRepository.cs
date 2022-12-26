using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserRoleRepository
    {
        Task AddUserToRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken);
        Task<List<UserMasterMetadata>> GetUsersInRoleAsync(string NormalizedRoleName, CancellationToken cancellationToken);
        Task<bool> IsUserInRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken);
        Task RemoveUserFromRole(int UserId, int RoleId, CancellationToken cancellationToken);
    }
}
