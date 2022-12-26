using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeMetadata
    {
        public int CompanyID { get; set; }
        public int RequestTypeID { get; set; }
        public int EntityID { get; set; }
        public string EntityName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string AccessByRoles { get; set; }
        public string AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }

        public string AccessByRolesName { get; set; }
        public string AssignedToName { get; set; }
        public string CompanyName { get; set; }
    }
}
