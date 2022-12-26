using System;

namespace Business.Entities.Marketing.CommunicationLog
{
    public class CommunicationLog
    {
        public int MarketingCommunicationLogID { get; set; }
        public string ContactByPerson { get; set; }
        public string ContactTo { get; set; }
        public int ContactChannelTypeID { get; set; }
        public string ContactChannelTypeText { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int VanueTypeID { get; set; }
        public string VanueTypeText { get; set; }
        public int PartyTypeID { get; set; }
        public string PartyTypeText { get; set; }
        public DateTime CommunicationLogDate { get; set; }
        public string PlaceOfMeeting { get; set; }
        public bool IsSentDocument { get; set; }
        public bool IsSentMarketingDocument { get; set; }
        public bool ReferenceBetterBusiness { get; set; }
        public string ReferenceMobileOrEmail { get; set; }
        public string Note { get; set; }
        public string Feedback { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
    }
}
