using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master.Package
{
    public class PackageMaster
    {
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Please enter the package name")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Please enter the description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select the package type")]
        public int PackageTypeID { get; set; }
        public string PackageTypeText { get; set; }

        [Required(ErrorMessage = "Please enter the package color code")]
        public string PackageColor { get; set; }
               
        public bool IsActive { get; set; } = true;
        public object SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }

    }
}
