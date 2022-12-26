using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CityMasterMetadata
    {
        public int CityID { get; set; }
        [Required(ErrorMessage = "Please select country")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please select state")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please enter city name")]
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string StateShortName { get; set; }
        public string CountryShortName { get; set; }
    }
}
