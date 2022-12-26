using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class VisitorFeedbackMetaData
    {
        public int VisitorFeedbackID { get; set; }
        public int VisitorMeetingRequestID { get; set; }
        public int FeedbackQuestionID { get; set; }
        public string Answer { get; set; }

        public List<FeedbackQuestionMasterMetadata> feedbackQuestions { get; set; }
        public string hdfRatingValue { get; set; }
    }
}
