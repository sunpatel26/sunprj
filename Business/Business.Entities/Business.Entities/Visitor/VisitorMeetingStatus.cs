using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class VisitorMeetingStatus
    {
        public string VisitorName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string MeetingRequestTitle { get; set; }
        public int VisitorMeetingStatusID { get; set; }
        public int VisitorMeetingRequestID { get; set; }
        public DateTime InTime { get; set; }
        public DateTime? OutTime { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string VisitorMeetingRequestCode { get; set; }
        public DateTime MeetingRequestDateTime { get; set; } 
    }
}
