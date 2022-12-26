using Business.Entities;
using Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("Home")]
    public class HomeController : Controller
    {
        private readonly IVisitorService _iVisitorDashboard;
        public HomeController(IVisitorService iVisitorDashboard)
        {
            _iVisitorDashboard = iVisitorDashboard;
        }

        [DisplayName("Admin Home Page")]
        public async Task<IActionResult> Index(int id = 0)
        {
            DashbaordCount data = new DashbaordCount();
            try
            {

                var list = await _iVisitorDashboard.GetDashboardCounts(id);
                return View(list);
            }
            catch
            {
                throw;
            }

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
