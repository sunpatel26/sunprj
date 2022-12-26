using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeControlScreenMappingMetadata
    {
        public int RequestTypeControlScreenID { get; set; }
        public int RequestTypeID { get; set; }
        public int RequestTypeControlID { get; set; }
        public int RoleID { get; set; }
        public string ScreenName { get; set; }
        public string RenderType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }


        public string EntityName { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestTypeControlName { get; set; }
        public string RenderTypeName
        {
            get
            {
                switch (this.RenderType)
                {
                    case "E":
                        return "Edit";
                    case "V":
                        return "Read Only";
                    case "H":
                        return "Hide";
                }

                return "";
            }
        }
    }
}
