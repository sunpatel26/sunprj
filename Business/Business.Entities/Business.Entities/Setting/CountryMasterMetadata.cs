using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CountryMasterMetadata
    {
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please enter country name")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Please enter short name")]
        public string CountryShortName { get; set; }
    }
}
