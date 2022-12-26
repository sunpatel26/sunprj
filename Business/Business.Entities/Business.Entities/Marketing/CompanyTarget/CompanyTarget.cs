using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Marketing.CompanySale
{
    public class CompanyTarget
    {
        public int CompanyTargetID { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public string StartDate { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public String EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public decimal TargetValue { get; set; }
        public int FinancialYearID { get; set; }
        public string FinancialYear { get; set; }        
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }


    }
}
