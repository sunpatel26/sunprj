using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeTitleListMetadata
    {
        public List<DropdownMetadata> RequestTypes { get; set; }
        public List<RequestTypeTitleMetadata> Lists { get; set; }

        public RequestTypeMetadata RequestType { get; set; }
    }
}
