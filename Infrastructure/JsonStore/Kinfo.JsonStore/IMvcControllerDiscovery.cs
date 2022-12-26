using Kinfo.JsonStore.Model;
using System.Collections.Generic;

namespace Kinfo.JsonStore
{
    public interface IMvcControllerDiscovery
    {
        IList<MvcControllerInfo> GetControllers();
    }
}