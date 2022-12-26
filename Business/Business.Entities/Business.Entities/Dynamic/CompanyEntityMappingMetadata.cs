using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class CompanyEntityMappingMetadata
    {
        public List<MasterEntityMetadata> CompanyListModel { get; set; }
        public List<MasterEntityMetadata> EntityListModel { get; set; }
        public List<CompanyEntityMetadata> CompanyEntityListModel { get; set; }

    }
}
