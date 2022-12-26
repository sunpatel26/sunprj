using Kinfo.JsonStore.Builder;
using System.Collections.Generic;

namespace Kinfo.JsonStore.Model
{
    public class MvcActionInfo
    {

        public string Id => $"{ControllerId}-{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ControllerId { get; set; }
        public bool Selected { get; set; }
        public CustomAuthorizeAttribute customAttribute { get; set; }
    }
    public class MvcControllerInfo
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
        public IList<MvcActionInfo> Actions { get; set; }

    }
}
