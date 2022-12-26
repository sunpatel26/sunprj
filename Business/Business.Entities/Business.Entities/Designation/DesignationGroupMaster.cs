using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Designation
{
    public class DesignationGroupMaster
    {
        public int DesignationGroupID { get; set; }

        [Required(ErrorMessage = "Please Select the Designation Level Name")]
        public  string DesignationGroupText { get; set; }

        [Required(ErrorMessage = "Please Select the Designation Level Remark")]
        public string Remark { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedOrModifiedBy { get; set; }
    }
}
