using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Marketing.Meeting
{
    public class MarketingMeeting
    {
        public int MarketingMeetingID { get; set; }
        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MeetingDuration { get; set; }
        public bool Remainder { get; set; }
        public int RemainderTimeBeforeMeeting { get; set; }
        public string Description { get; set; }
        public int MeetingStatusID { get; set; }
        public string MeetingStatusTypeText { get; set; }
        public string ContactPerson { get; set; }
        public string MeetingRelatedTo { get; set; }
        public string MeetingLocation { get; set; }
        public string AssignTo { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }

    }
}
