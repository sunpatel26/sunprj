using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class CompanyEntityMetadata
    {
        public int CompanyEntityID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
    }
}
