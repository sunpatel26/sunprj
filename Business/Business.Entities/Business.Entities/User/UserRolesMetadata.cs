using Business.SQL;

namespace Business.Entities
{
    public class UserRolesMetadata
    {
        public int UserID { get; set; }
        public PagedDataTable<RoleMasterMetadata> SelectedRole{ get; set; }
    }
}
