using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.PackageFormTxn
{
    public class PackageFormTxn
    {
        public object SrNo { get; set; }
        public int PackageFormID { get; set; }
        public int PackageID {get; set; }
        public string PackageName { get; set; }
        public string FormName { get; set; }
        public int FormID { get; set; }
        public bool AddNew { get; set; }
        public bool Edit { get; set; }
        public bool Cancel { get; set; }
        public bool View { get; set; }
        public bool Print { get; set; }
        public bool Email { get; set; }
        public bool EmailWithAttachment { get; set; }
        public bool ExportToPDF { get; set; }
        public bool ExportToExcel { get; set; }
        public bool Search { get; set; }
        public bool IsActive { get; set; }        
        public int CreatedOrModifiedBy { get; set; }

    }
}
