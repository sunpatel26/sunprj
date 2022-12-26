using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class DepartmentMasterMetadata
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Please enter department name")]
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public int DepartmentGroupID { get; set; }
    }
}
