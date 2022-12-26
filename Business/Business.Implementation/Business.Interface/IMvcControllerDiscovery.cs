using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface
{
    public interface IMvcControllerDiscovery
    {
        IList<MvcControllerInfo> GetControllers();
    }
}
