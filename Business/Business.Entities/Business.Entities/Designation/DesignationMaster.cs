using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Designation
{
    public class DesignationMaster
    {
        public int DesignationID { get; set; }
        public int SrNo { get; set; }

        [Required(ErrorMessage = "Please Enter the Designation Name")]
        public string DesignationText{ get; set; }

        [Required(ErrorMessage = "Please Enter the Designation Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select the Designation Level")]
        public string DesignationLevel { get; set; }

        [Required(ErrorMessage = "Please Select the Designation Level")]
        public int DesignationGroupID { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedOrModifiedBy { get; set; }
    }
}
