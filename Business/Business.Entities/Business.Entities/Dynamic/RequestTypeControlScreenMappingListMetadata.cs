using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeControlScreenMappingListMetadata
    {
        public List<DropdownMetadata> RequestTypes { get; set; }
        public List<DropdownMetadata> RequestTypeControls { get; set; }
        public List<DropdownMetadata> RenderTypes { get; set; }
        public List<DropdownMetadata> Screens { get; set; }
        public List<RequestTypeControlScreenMappingMetadata> Lists { get; set; }
    }
}
