using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class BusinessTypeMasterMetadata
    {
        public int BusinessTypeID { get; set; }

        [Required(ErrorMessage = "Please enter business type text")]
        public string BusinessTypeText { get; set; }
    }
}
