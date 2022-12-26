using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class VisitorRequestApproveMetaData
    {
        public int VisitorMeetingRequestID { get; set; }
        public int CreatedBy { get; set; }

        public bool IsApproved { get; set; }
        public DateTime MeetingDateAndTime { get; set; }
        public string Note { get; set; }
        public string VisitorMeetingRequestCode { get; set; }
    }
}
