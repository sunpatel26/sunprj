using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeWithEntityListMetadata
    {
        public List<MasterEntityMetadata> EntityListModels { get; set; }
        public List<RequestTypeMetadata> RequestTypeModels { get; set; }
        public List<RoleMasterMetadata> RoleModels { get; set; }
    }
}
