using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master
{
    public class EmailGroupMaster
    {
        public int EmailGroupID { get; set; }
        public string EmailID { get; set; }
        public string EmailGroupName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int CreatedOrModifiedBy { get; set; }


    }
}
