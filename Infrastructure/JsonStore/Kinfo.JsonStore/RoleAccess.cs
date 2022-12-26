using Business.Entities;
using Business.SQL;
using System.Collections.Generic;

namespace Kinfo.JsonStore
{
    public class RoleAccess
    {
        public int Id { get; set; }

        public string RoleId { get; set; }
        public string UserID { get; set; }
        public string CompnayID { get; set; }
        public PagedDataTable<UserClaimsMetadata> UserClaims { get; set; }
    }
}