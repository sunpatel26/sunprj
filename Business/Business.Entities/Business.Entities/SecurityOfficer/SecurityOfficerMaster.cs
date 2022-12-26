using System;

namespace Business.Entities.SecurityOfficer
{
    public class SecurityOfficerMaster
    {
        public int SrNo { get; set; }
        public int SecurityOfficerID { get; set; } = 0;
        public string SecurityOfficerName { get; set; }
        public string SecurityAgencyName { get; set; }
        public string SecurityOfficerMobile { get; set; }
        public string IdentityProofTypeID { get; set; }
        public string IdentityProof { get; set; }
        public string IdentityProofNumber { get; set; }
        public int CompanyID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public DateTime CreatedOrModifiedDate { get; set; }
    }
}
