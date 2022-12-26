using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Marketing.VisitingDetail
{
    public class VisitingDetail
    {
        public int MarketingVisitedDetailID { get; set; }
        public string VisitedByPerson { get; set; }
        public string VisitedTo { get; set; }
        public int VanueTypeID { get; set; }
        public string VanueTypeText { get; set; }
        public string PlaceOfMeeting { get; set; }
        public int PartyTypeID { get; set; }
        public string PartyTypeText { get; set; }
        public DateTime DateTime { get; set; }
        public string CompanyOrOrganazationName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool IsCollectVisitingCard { get; set; }
        public bool IsCollectMarketingDocs { get; set; }
        public bool IsSentDocument { get; set; }
        public bool IsSentMarketingDocs { get; set; }
        public bool ReferenceBetterBusiness { get; set; }
        public string ReferenceMobileOrEmail { get; set; }
        public string MeetingTotalTime { get; set; }
        public string MOM { get; set; }
        public string Feedback { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
