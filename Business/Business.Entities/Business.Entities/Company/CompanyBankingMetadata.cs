using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CompanyBankingMetadata
    {
        public int SrNo { get; set; }
        public int CompanyBankingID { get; set; }
        public int CompanyID { get; set; }
        [Required(ErrorMessage = "Please enter bank name")]
        public string BankName { get; set; }
        
        public string BankCode { get; set; }
        [Required(ErrorMessage = "Please enter account number")]
        public string AccountNo { get; set; }
        public string BIC_SWIFTCode { get; set; }
        [Required(ErrorMessage = "Please enter  account name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Please enter IFCI code")]
        public string IFCICode { get; set; }
        [Required(ErrorMessage = "Please enter  branch name")]
        public string Branch { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public string CountryName { get; set; }
    }
}
