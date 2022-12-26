using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Business.Entities
{
    public class VisitorMetaData
    {
        public int SrNo { get; set; }
        public int VisitorMeetingRequestID { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression("^[a-zA-Z\\-'_]+$", ErrorMessage = "Please enter a valid first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression("^[a-zA-Z\\-'_]+$", ErrorMessage = "Please enter a valid last name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter your Mobile No")]
        [RegularExpression("^(\\+91[\\-\\s]?)?[0]?(91)?[789]\\d{9}$", ErrorMessage = "Please enter a valid mobile number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Area { get; set; }
        [Required(ErrorMessage = "Please enter your pin code")]
        public string ZipCode { get; set; }
        public string ZipCodeID { get; set; }
        public int AddressID { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int IdentityProofTypeID { get; set; }
        public string IdentityProofNumber { get; set; }
        public int? VehicleTypeID { get; set; }
        public string VehicleNo { get; set; }
        [Required(ErrorMessage = "Please select meeting request date and time")]
        public DateTime MeetingRequestDateTime { get; set; }
        [Required(ErrorMessage = "Please enter meeting request title")]
        public string MeetingRequestTitle { get; set; }
        [Required(ErrorMessage = "Please enter reason for meeting")]
        public string PurposeofMeeting { get; set; }
        [Required(ErrorMessage = "Please enter name of person to whom you want to meet")]
        public string MeetToWhomPersonName { get; set; }
        public string MeetToWhomPersonMobile { get; set; }

        [Required(ErrorMessage = "Please enter email of person to whom you want to meet")]
        public string MeetToWhomPersonEmail { get; set; }
        public int VisitorTypeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SecurityOfficerName { get; set; }
        public string SecurityOfficerMobile { get; set; }

        public SelectList IdentityProofTypeSelectList { get; set; }

        public string VisitorName { get; set; }

        public string IdentityProofTypeText { get; set; }

        public string VehicleTypeText { get; set; }

        public int IsApproved { get; set; }
        public string VisitorMeetingRequestCode { get; set; }

        public string IdentityProofFilePath { get; set; }

        public VisitorMeetingRequestFile ProofFile { get; set; }

        public string IdentityProofFileName { get; set; }
    }

    
}
