using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeControlListMetadata
    {
        public RequestTypeTitleMetadata RequestTypeTitle { get; set; }
        public List<DropdownMetadata> ValidationRules { get; set; }
        public List<DropdownMetadata> Types { get; set; }

        public List<DropdownMetadata> DataKeys { get; set; }
        public List<RequestTypeControlMetadata> Lists { get; set; }
        public int? CaseTypeIdQuerystringParam { get; set; }

        public int? ControlID { get; set; }
    }
}
