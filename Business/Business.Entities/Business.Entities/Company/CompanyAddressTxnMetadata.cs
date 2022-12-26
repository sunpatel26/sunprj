using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CompanyAddressTxnMetadata
    {
        public int SrNo { get; set; }
        //[Required(ErrorMessage = "Please enter email address")]
        //[RegularExpression(@"^[\w-+'$""]+(\.[\w-']+)*@([a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*?\.[a-zA-Z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter valid email address")]
        public int CompanyAddressTxnID { get; set; }
        public int CompanyID { get; set; }
        [Required(ErrorMessage = "Please enter plot no./name")]
        public string Address1 { get; set; }        
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please enter landmark")]
        public string Address3 { get; set; }
        public string Area { get; set; }
        
        public int ZIPCodeID { get; set; }
        [Required(ErrorMessage = "Please select country")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please select state")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please select taluka")]
        public int TalukaID { get; set; }
       [Required(ErrorMessage = "Please select district")]
        public int DistrictID { get; set; }
        public bool IsActive { get; set; }
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Please select zipcode")]
        public string ZIPCode { get; set; }
        public string TalukaName { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string StateShortName { get; set; }
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public string AddressTypeText { get; set; }
        [Required(ErrorMessage = "Please select address type")]
        public string AddressTypeID { get; set; }
        public int CreatedOrModifiedBy{ get; set; }
    }
}
