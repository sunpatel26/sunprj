using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Master.MarketingCompanyFinancialYearMaster
{
    public class FinancialYearMaster
    {
        public int FinancialYearID { get; set; }
        public string FinancialYear { get; set; }
        public string FinYearDesc { get; set; }
        public string StartMonth { get; set; }
        public string EndMonth { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }

        public int CurrentYear { get; set; }

    }
}
