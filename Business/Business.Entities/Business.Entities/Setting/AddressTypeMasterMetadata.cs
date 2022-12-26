using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class AddressTypeMasterMetadata
    {
        public int AddressTypeID { get; set; }

        [Required(ErrorMessage = "Please enter address type text")]
        public string AddressTypeText { get; set; }
    }
}
