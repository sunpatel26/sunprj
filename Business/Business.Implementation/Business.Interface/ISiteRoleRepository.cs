using Business.Entities;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISiteRoleRepository
    {
        Task<IdentityResult> CreateAsync(RoleMasterMetadata role, CancellationToken cancellationToken);
        Task<RoleMasterMetadata> FindByIdAsync(string roleId);
        Task<PagedDataTable<RoleMasterMetadata>> GetRolesByUserIdAsync(UserMasterMetadata user, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(RoleMasterMetadata role, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(RoleMasterMetadata role, CancellationToken cancellationToken);
        Task<RoleMasterMetadata> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);
        Task<PagedDataTable<RoleMasterMetadata>> GetAllRolesAsync();
        Task<PagedDataTable<RoleMasterMetadata>> GetAllRolesAsync(int companyID);

        #region "Claims"
        Task<PagedDataTable<RoleClaimsMetadata>> GetAllClaims(int roleID, int compnayID);
        Task<IdentityResult> AddPermissionClaim(RoleClaimsMetadata items);
        Task<IdentityResult> AddPermissionClaim(UserClaimsMetadata item);
        #endregion
    }
}
