using System;

namespace Business.Entities.Marketing.EmployeeSalesTargetSummary
{
    public class EmployeeSalesTargetSummary
    {
        public int SalesTargetID { get; set; }
        public string CompanyName { get; set; }
        public decimal CompanyTargetValue { get; set; }
        public int MarketingEmployeeID { get; set; }
        public string MarketingEmployeeName { get; set; }
        public string CustomerName { get; set; }
        public decimal TargetValue { get; set; }
        public int FinancialYearID { get; set; }
        public string FinancialYear { get; set; }
        public object SrNo { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
