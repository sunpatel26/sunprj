using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class DocumentTypeMasterMetadata
    {
        public int DocumentTypeID { get; set; }
        [Required(ErrorMessage = "Please enter document type")]
        public string DocumentTypeName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
