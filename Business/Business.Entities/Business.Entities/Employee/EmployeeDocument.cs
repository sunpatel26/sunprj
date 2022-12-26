using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Entities.Employee
{
    public class EmployeeDocument
    {
        public int SrNo { get; set; }
        public int EmployeeDocumentID { get; set; }
        public int EmployeeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentExtension { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public string DocumentPath { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public IFormFile DocumentFile { get; set; }
    }
}
