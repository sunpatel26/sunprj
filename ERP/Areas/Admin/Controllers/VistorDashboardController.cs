using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ERP.Areas.Admin.Controllers
{

    [Area("Admin"), Authorize]
    [DisplayName("VistorDashboard")]

    public class VistorDashboardController : Controller
    {
        //private readonly iVisitorDashboard _iVisitorDashboard;
        //public async Task<IActionResult> GetVisitorashboardCounts(int id = 0)
        //{
        //    DashbaordCount data = new DashbaordCount();
        //    try
        //    {
        //        var list = await _iVisitorDashboard.GetDashboardCounts(id);
        //        return View(list);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
    }
}
