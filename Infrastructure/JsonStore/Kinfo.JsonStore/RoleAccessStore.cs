using JsonFlatFileDataStore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kinfo.JsonStore
{
    public class RoleAccessStore : IRoleAccessStore
    {
        private readonly DataStore _store;

        public RoleAccessStore(DataStore store)
        {
            _store = store;
        }

        public Task<bool> AddRoleAccessAsync(RoleAccess roleAccess)
        {
            //roleAccess.Id = roleAccess.Id == 0 ? 1 : roleAccess.Id;
            var collection = _store.GetCollection<RoleAccess>();
            RemoveRoleAccessAsync(roleAccess.UserID);
            return collection.InsertOneAsync(roleAccess);
        }

        public Task<bool> EditRoleAccessAsync(RoleAccess roleAccess)
        {
            var collection = _store.GetCollection<RoleAccess>();
            var access = collection.AsQueryable().FirstOrDefault(ra => ra.UserID == roleAccess.UserID);
            if (access == null)
                return collection.InsertOneAsync(roleAccess);

            //roleAccess.Id = access.Id;
            return collection.ReplaceOneAsync(roleAccess.UserID, roleAccess);
        }

        public Task<bool> RemoveRoleAccessAsync(string userID)
        {
            var collection = _store.GetCollection<RoleAccess>();

            return collection.DeleteOneAsync(r => r.UserID == userID);
        }

        public Task<RoleAccess> GetRoleAccessAsync(string userID)
        {
            var collection = _store.GetCollection<RoleAccess>();

            return Task.FromResult(collection.AsQueryable().FirstOrDefault(ra => ra.UserID == userID));
        }

        public Task<bool> HasAccessToActionAsync(string actionId, params string[] roles)
        {
            if (roles == null || !roles.Any())
                return Task.FromResult(false);

            var accessList = _store.GetCollection<RoleAccess>()
                .AsQueryable()
                .Where(ra => roles.Contains(ra.RoleId))
                .SelectMany(ra => ra.UserClaims.Where(d=>d.ClaimValue.Contains(actionId)))
                .ToList();

            return Task.FromResult(accessList.Count>0);
        }
        public Task<bool> HasAccessToActionAsync(string actionId, string userID)
        {            
            var accessList = _store.GetCollection<RoleAccess>()
                .AsQueryable()
                .Where(ra => ra.UserID==userID)
                .SelectMany(ra => ra.UserClaims.Where(s=>s.ClaimValue.Contains(actionId)))
                .ToList();

            return Task.FromResult(accessList.Count>0);
        }
    }
}