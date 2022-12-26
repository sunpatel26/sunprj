using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class PermissionActionInfo
    {

        public string Id => $"{ControllerId}-{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ControllerId { get; set; }
        public bool Selected { get; set; }
        
    }
    public class PermissionControllerInfo
    {
        public string Id => $"{AreaName}-{Name}";
        public string IDS
        {
            get
            {
                if (!string.IsNullOrEmpty(AreaName))
                {
                    return string.Format("{0}-{1}", AreaName, Name);
                }
                return string.Format("{0}", Name);
            }

        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string AreaName { get; set; }
        public bool IsAnonymous { get; set; }
        public IList<PermissionActionInfo> Actions { get; set; }

    }
}
