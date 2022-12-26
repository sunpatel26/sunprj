using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class CompanyDocumentMetadata
    {
        public int SrNo { get; set; }
        public int CompanyDocumentsID{ get; set; }
		public int CompanyID { get; set; }
        [Required(ErrorMessage ="Please select document type")]
        public int DocumentTypeID { get; set; }
        [Required(ErrorMessage = "Please enter document name")]
        public string DocumentName { get; set; }
        public string DocumentDesc { get; set; }
        public bool IsActive { get; set; }
        public string DocumentPath { get; set; }
        public IFormFile File { get; set; }
    }
}
