using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class TalukaMasterMetadata
    {
        public int TalukaID { get; set; }
        [Required(ErrorMessage = "Please select country")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please select state")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please select district")]
        public int DistrictID { get; set; }
        [Required(ErrorMessage = "Please enter taluka name")]
        public string TalukaName { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string StateShortName { get; set; }
        public string CountryShortName { get; set; }
    }
}
