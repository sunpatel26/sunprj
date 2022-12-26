using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class DesignationMasterMetadata
    {
        public int DesignationID { get; set; }

        [Required(ErrorMessage = "Please enter designation text")]
        public string DesignationText { get; set; }
        public string Description { get; set; }
        public string DesignationLevel { get; set; }
        public int DesignationGroupID { get; set; }       
    }
}
