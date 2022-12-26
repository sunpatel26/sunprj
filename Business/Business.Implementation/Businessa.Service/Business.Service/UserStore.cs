using Business.Entities;
using Business.Interface;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Service
{
    public class UserStore : IUserStore<UserMasterMetadata>,
                             IUserEmailStore<UserMasterMetadata>,
                             IUserPhoneNumberStore<UserMasterMetadata>,
                             IUserTwoFactorStore<UserMasterMetadata>,
                             IUserPasswordStore<UserMasterMetadata>,
                             IUserRoleStore<UserMasterMetadata>,
                             IUserLoginStore<UserMasterMetadata>
    {
        private ISiteUserService siteUserRepository { get; set; }

        private ISiteRoleRepository _siteRoleRepository { get; set; }



        //private IExternalLoginRepository _externalLoginRepository { get; set; }

        public UserStore(ISiteUserService siteUserRepository, ISiteRoleRepository siteRoleRepository)
        {
            this.siteUserRepository = siteUserRepository;
            _siteRoleRepository = siteRoleRepository;

        }

        // public PagedDataTable<UserMasterMetadata> Users => siteUserRepository.GetAllUser();
       // public IQueryable<UserMasterMetadata> Users => siteUserRepository.FindById();
        public async Task<IdentityResult> CreateAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                int userID = await siteUserRepository.CreateUser(user, cancellationToken);
                if (userID > 0)
                {
                    user.UserID = userID;
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "001",
                    Description = "User not created"
                }); ;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ex.HResult.ToString(),
                    Description = ex.Message
                });
            }
        }

        public async Task<IdentityResult> DeleteAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserMasterMetadata> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindById(userId, cancellationToken);
        }

        public async Task<UserMasterMetadata> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindByName(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserID.ToString());
        }

        public Task<string> GetUserNameAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedUserNameAsync(UserMasterMetadata user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(UserMasterMetadata user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.UpdateUser(user, cancellationToken);
        }

        public Task SetEmailAsync(UserMasterMetadata user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(UserMasterMetadata user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<UserMasterMetadata> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindByEmail(normalizedEmail, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(UserMasterMetadata user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(UserMasterMetadata user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(UserMasterMetadata user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(UserMasterMetadata user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        #region Roles

        public async Task AddToRoleAsync(UserMasterMetadata user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                // No role found, so create one...
                //        roleId = await connection.ExecuteAsync($"INSERT INTO [SiteRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                //            new { roleName, normalizedName });
                //throw new Exception("No Role Found by Name!");
                RoleMasterMetadata _role = new RoleMasterMetadata() { Name = roleName, NormalizedName = roleName.ToUpper(), Description = roleName };

                await _siteRoleRepository.CreateAsync(_role, cancellationToken);
                var roles = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);
                await siteUserRepository.AddUserToRoleAsync(user.UserID, roles.RoleID, cancellationToken);
            }
            else
            {
                await siteUserRepository.AddUserToRoleAsync(user.UserID, role.RoleID, cancellationToken);
            }
    
        }

        public async Task RemoveFromRoleAsync(UserMasterMetadata user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Retrieve role to get id
            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                throw new Exception($"Couldn't retrieve role with name - {role}");
            }

            await siteUserRepository.RemoveUserFromRole(user.UserID, role.RoleID, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var roles = _siteRoleRepository.GetRolesByUserIdAsync(user, cancellationToken).Result.Select(s => s.Name).ToList<string>();
            return roles;
        }

        public async Task<bool> IsInRoleAsync(UserMasterMetadata user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Retrieve role to get id
            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                return false;
            }

            return await siteUserRepository.IsUserInRoleAsync(user.UserID, role.RoleID);
        }

        public async Task<IList<UserMasterMetadata>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region External Login

        public async Task AddLoginAsync(UserMasterMetadata user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserMasterMetadata> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLoginAsync(UserMasterMetadata user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Password

        public Task SetPasswordHashAsync(UserMasterMetadata user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region Security stamp
        public virtual Task<string> GetSecurityStampAsync(UserMasterMetadata user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(UserMasterMetadata user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion

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
