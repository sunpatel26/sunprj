using Business.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class RoleMasterMetadata : BaseEntity
    {

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Please enter role name")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        /// <summary>
        /// Describe the roles purpose etc...
        /// </summary>
        public string Description { get; set; }
        public bool IsSelectedRole { get; set; }
        public IList<PermissionControllerInfo> SelectedControllers { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }

    }
}
