using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master.FormType
{
    public class FormTypeMaster
    {
        public int FormTypeID { get; set; }

        [Required(ErrorMessage = "Please Enter the Form Type Name")]
        public string FormTypeText { get; set; }

        [Required(ErrorMessage = "Please Enter the Description")]
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public object SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
