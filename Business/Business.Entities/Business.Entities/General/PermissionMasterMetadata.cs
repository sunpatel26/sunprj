namespace Business.Entities
{
    public class PermissionMasterMetadata
    {
        public int PermissionID { get; set; }
        public string PermissionDesc { get; set; } 
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool Selected { get; set; }
    }
}
