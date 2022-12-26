using Business.Entities.Dynamic;
using Business.Entities.SecurityOfficer;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SecurityOfficerController : SettingsController
    {
        private readonly ISecurityOfficerService _securityOfficerService;
        private readonly IMasterService _masterService;
        public SecurityOfficerController(ISecurityOfficerService securityOfficerService, IMasterService masterService)
        {
            _securityOfficerService = securityOfficerService;
            _masterService = masterService;
        }
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<SecurityOfficerMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.");
                c.Add(o => o.SecurityOfficerName).Titled("Security Officer Name").Sortable(true);
                c.Add(o => o.SecurityAgencyName).Titled("Security Agency Name").Sortable(true);
                c.Add(o => o.SecurityOfficerMobile).Titled("Security Officer Mobile").Sortable(false);
                c.Add(o => o.IdentityProof).Titled("Identity Proof").Sortable(true);
                c.Add(o => o.IdentityProofNumber).Titled("Identity Proof number").Sortable(false);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);

                c.Add().Titled("Update").Encoded(false).Sanitized(false).RenderValueAs(o => string.Format("<a class='btn' onclick = 'fnSecurityOfficer(this)' href = 'javascript:void(0)' data-id='{0}' data-bs-toggle='offcanvas' data-bs-target='#canvas_securityOfficer' aria-controls='canvas_securityOfficer'><i class='bx bx-edit'></i></a>", o.SecurityOfficerID));
    };
            PagedDataTable<SecurityOfficerMaster> pds = _securityOfficerService.GetAllSecurityOfficerAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<SecurityOfficerMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }

        //Method for right side slide panel(To add update security officer)
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public PartialViewResult GetSecurityOfficer(int id)
        {
            try
            {
                SecurityOfficerMaster securityOfficerMaster = new SecurityOfficerMaster();
                if (id > 0)
                {
                    securityOfficerMaster.SecurityOfficerID = id;
                    securityOfficerMaster = _securityOfficerService.GetSecurityOfficerAsync(id).Result;

                }
                var idProofTypes = _masterService.GetIdentityProofTypeAsync();
                ViewData["IdentityProofType"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(idProofTypes, "IdentityProofTypeID", "IdentityProofTypeText");
                return PartialView("AddUpdateSecurityOfficer", securityOfficerMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        //Method for right side slide panel(To add update security officer)

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult AddUpdateSecurityOfficer(SecurityOfficerMaster securityOfficerMaster)
        {
            securityOfficerMaster.CompanyID = COMPANYID;
            securityOfficerMaster.CreatedOrModifiedBy = USERID;
            var id = _securityOfficerService.AddUpdateSecurityOfficer(securityOfficerMaster).Result;
            if (id > 0)
                return Json(new { status = true, message = MessageHelper.Added });
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
    }
}
