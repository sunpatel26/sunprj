using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CompanyContactTxnMetadata
    {
        public int SrNo { get; set; }
        [Required(ErrorMessage = "Please select department")]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Please select designation")]
        public int DesignationID { get; set; }
        public int CompanyID { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^[\w-+'$""]+(\.[\w-']+)*@([a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*?\.[a-zA-Z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter valid email address")]
        public string OfficeEmail { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationText { get; set; }
        public string EmailGroupName { get; set; }
        public int CompanyContactPersonsID { get; set; }
        public string Prefix { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        public string PersonalMobileNo { get; set; }
        public string OfficeMobileNo { get; set; }
        public string AlternetMobileNo { get; set; }
        public int EmailGroupID { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^[\w-+'$""]+(\.[\w-']+)*@([a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*?\.[a-zA-Z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter valid email address")]
        public string PersonEmail { get; set; }
        public bool IsActive { get; set; }
        public bool IsResigned { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Birthdate { get; set; }
        public string Religion { get; set; }
    }
}
