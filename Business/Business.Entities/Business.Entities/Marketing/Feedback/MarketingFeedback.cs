using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Marketing.Feedback
{
    public class MarketingFeedback
    {
        public int MarketingFeedbackID { get; set; }

        [Required(ErrorMessage = "Please Select Date")]
        public DateTime FeedbackDate { get; set; }

        [Required(ErrorMessage = "Please Enter the Party Name")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Please Select the Party Type")]
        public int PartyTypeID { get; set; }
        public string PartyTypeText { get; set; }

        [Required(ErrorMessage = "Please Enter the Party Email ID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the Party Mobile Number")]
        public string MobileNo { get; set; }

        /*[Required(ErrorMessage = "Please Select This Check Box")]*/
        public bool IsReceivedDocument { get; set; } = true;

        /*[Required(ErrorMessage = "Please Witer the Note")]*/
        public string Note { get; set; }

        public int CreatedOrModifiedBy { get; set; }

        public object SrNo { get; set; }
    }
}
