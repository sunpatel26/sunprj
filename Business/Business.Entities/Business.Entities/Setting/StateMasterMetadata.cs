using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class StateMasterMetadata
    {
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please select country")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please enter state name")]
        public string StateName { get; set; }
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Please enter short name")]
        public string StateShortName { get; set; }
        public string CountryShortName { get; set; }
    }
}
