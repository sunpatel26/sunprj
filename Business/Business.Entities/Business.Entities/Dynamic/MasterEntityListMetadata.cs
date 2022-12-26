using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class MasterEntityListMetadata
    {
        public List<MasterEntityMetadata> MasterLists { get; set; }
        public List<MasterEntityEntryTypeMetadata> EntryTypeLists { get; set; }
    }
}
