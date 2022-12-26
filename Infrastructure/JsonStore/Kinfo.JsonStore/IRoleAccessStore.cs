using System.Threading.Tasks;

namespace Kinfo.JsonStore
{
    public interface IRoleAccessStore
    {
        Task<bool> AddRoleAccessAsync(RoleAccess roleAccess);

        Task<bool> EditRoleAccessAsync(RoleAccess roleAccess);

        Task<bool> RemoveRoleAccessAsync(string userID);

        Task<RoleAccess> GetRoleAccessAsync(string userID);
        Task<bool> HasAccessToActionAsync(string actionId,string userID);
        Task<bool> HasAccessToActionAsync(string actionId, params string[] roles);
    }
}