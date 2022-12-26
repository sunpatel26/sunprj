using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class UserClaimsMetadata
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }
}
