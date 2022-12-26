using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class IndustryTypeMasterMetadata
    {
        public int IndustryTypeID { get; set; }

        [Required(ErrorMessage = "Please enter industry type text")]
        public string IndustryTypeText { get; set; }
       
    }
}
