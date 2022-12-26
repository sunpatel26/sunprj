using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Business.Entities
{
    public  class IdentityProofTypeMetadata
    {
        public int IdentityProofTypeID { get; set; }
        public string IdentityProofTypeText { get; set; }
    }
}
