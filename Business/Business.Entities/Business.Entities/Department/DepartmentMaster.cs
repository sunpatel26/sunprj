using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;*/
using System.ComponentModel.DataAnnotations;
/*using System.ComponentModel.DataAnnotations.Schema;
*/
namespace Business.Entities.Department
{
    public class DepartmentMaster
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Please Enter the Department Name")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Please Enter the Department Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select the Department Group Name")]
        public string DepartmentGroupText { get; set; }

        [Required(ErrorMessage = "Please Select the Department Group ID")]
        public int DepartmentGroupID { get; set; }

        [Required(ErrorMessage = "Please Select the Check Box")]
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
