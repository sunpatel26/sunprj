using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interface;
using Microsoft.AspNetCore.Identity;
namespace Business.Service
{

    public class RoleStore : IRoleStore<RoleMasterMetadata>
    {
        private ISiteRoleRepository _siteRoleRepository { get; set; }

        public RoleStore(ISiteRoleRepository siteRoleRepository)
        {
            _siteRoleRepository = siteRoleRepository;
        }

        public async Task<IdentityResult> CreateAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.CreateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.UpdateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.DeleteAsync(role, cancellationToken);
        }

        public Task<string> GetRoleIdAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleID.ToString());
        }

        public Task<string> GetRoleNameAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(RoleMasterMetadata role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(RoleMasterMetadata role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<RoleMasterMetadata> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.FindByIdAsync(roleId);
        }

        public async Task<RoleMasterMetadata> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _siteRoleRepository.FindByNameAsync(normalizedRoleName, cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this != null)
                {
                    //this.Dispose();
                }
            }
        }
    }

}
