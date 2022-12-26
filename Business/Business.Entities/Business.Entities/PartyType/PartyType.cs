using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Party Type Model */
namespace Business.Entities.PartyType
{
    public class PartyType
    {
        public int PartyTypeID { get; set; }

        [Required(ErrorMessage = "Please Enter Party Type Name")]
        public string PartyTypeText { get; set; }

        [Required(ErrorMessage = "Please Enter Remark")]
        public string Remark { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}