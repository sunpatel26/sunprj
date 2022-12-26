using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Marketing.SalesTarget
{
    public class SalesTarget
    {       
        public int SalesTargetID { get; set; }
        
        //public string SalesTargetName { get; set; }

        [Required(ErrorMessage = "Please Select Marketing Employee Name")]
        public int MarketingEmployeeID { get; set; }   
        
        public string MarketingEmployeeName { get; set; }

        [Required(ErrorMessage = "Please Select Customer Name")]
        public int CustomerID { get; set; } 

        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Select End Date")]
        public DateTime EndDate { get; set; }

        public decimal TargetValue { get; set; }

        [Required(ErrorMessage = "Please Select Financial Year")]
        public int FinancialYearID { get; set; }

        public string FinancialYear { get; set; }

        [Required(ErrorMessage = "Please Select Reporting Head Name")]
        public int ReportingHeadID { get; set; }

        public string ReportingHeadName { get; set; }

        public bool IsActive { get; set; }

        public int CreatedOrModifiedBy { get; set; }

        public object SrNo { get; set; }

        [Required(ErrorMessage = "Please Select Company Name")]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

    }
}
