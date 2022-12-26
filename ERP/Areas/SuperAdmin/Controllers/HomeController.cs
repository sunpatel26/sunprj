using Business.Entities;
using Business.Interface;

using Kinfo.JsonStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ERP.Areas.SuperAdmin.Controllers
{

    //[Authorize]
    //[CustomAuthorize(FilterConstraint.Ignore)]
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Home")]
    public class HomeController : Controller
    {


        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;
        ISuperAdminService _superAadmin;
        public HomeController(ISuperAdminService superAdminService, IMvcControllerDiscovery mvcControllerDiscovery)
        {
            _superAadmin = superAdminService;
            _mvcControllerDiscovery = mvcControllerDiscovery;
        }

        [DisplayName("Welcome Super Admin")]
        public ActionResult Index()
        {
            //IList<PermissionInfo> actionsList = _mvcControllerDiscovery.GetControllers()
            //       .Where(s => !s.Name.Contains("Account"))
            //      .ToList()
            //          .OrderBy(s => s.AreaName).ThenBy(s => s.Name).ToList();
            //foreach (var controllers in actionsList)
            //{
            //    foreach (var actions in controllers.Actions)
            //    {
            //        PermissionMasterMetadata permisison = new PermissionMasterMetadata();
            //        permisison.Controller = controllers.Name;
            //        permisison.Area = controllers.AreaName;
            //        permisison.Action = actions.Name;
            //        permisison.PermissionDesc = actions.Id;
            //        _superAadmin.AddPermission(permisison);
            //    }
            //}

            return View();
        }
    }
}
