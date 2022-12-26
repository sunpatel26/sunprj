namespace Business.Entities
{
    public class RoleClaimsMetadata 
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int CompanyID { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; } 
        public bool Selected { get; set; }
    }
}
