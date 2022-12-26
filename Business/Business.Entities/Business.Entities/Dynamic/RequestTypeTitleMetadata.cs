using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeTitleMetadata
    {
        public int RequestTypeTitleID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsActive { get; set; }

        public int RequestTypeID { get; set; }
        public string RequestTypeName { get; set; }

        public List<RequestTypeControlMetadata> Controls { get; set; }

        public List<MasterEntityMetadata> DropdownValueList { get; set; }
    }
}
