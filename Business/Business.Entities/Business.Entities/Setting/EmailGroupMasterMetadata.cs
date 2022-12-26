using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class EmailGroupMasterMetadata
    {
        public int EmailGroupID { get; set; }

        [Required(ErrorMessage = "Please enter email group name")]
        public string EmailGroupName { get; set; }
    }
}
