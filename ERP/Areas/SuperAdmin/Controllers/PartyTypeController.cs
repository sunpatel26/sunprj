using Business.Entities.PartyType;
using Business.Interface.IPartyTypeService;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("PartyType")]
    public class PartyTypeController : SettingsController
    {
        private readonly IPartyTypeService iPartyType;
        public PartyTypeController(IPartyTypeService iPartyType)
        {
            this.iPartyType = iPartyType;
        }

        /* Party Type Listing Page Start */
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<PartyType>> columns = c =>
            {
                c.Add(o => o.SrNo)
                    .Titled("SrNo")
                    .SetWidth(10);

                c.Add(o => o.PartyTypeText)
                    .Titled("Party Type Name")
                    .SetWidth(70);

                c.Add(o => o.Remark)
                    .Titled("Remark")
                    .SetWidth(160);

             /*c.Add()
                 .Titled("Edit")
                 .Encoded(false)
                 .Sanitized(false)
                 .SetWidth(60)
                 .Css("hidden-xs") //hide on phones
                 .RenderValueAs(o => $"<a class='btn' href='PartyType/Create/{o.PartyTypeID}' ><i class='bx bx-edit'></i></a>");*/

            c.Add()
                .Titled("Edit")
                .Encoded(false)
                .Sanitized(false)
                .SetWidth(60)
                .Css("hidden-xs") //hide on phones
                .RenderValueAs(o => $"<a class='btn' onclick='fnPartyType(this)' href='javascript:void(0)' data-id='{o.PartyTypeID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_PartyType' aria-controls='canvas_masterentity' ><i class='bx bx-edit'></i></a>");

                 



            };
            PagedDataTable<PartyType> pds = iPartyType.GetAllPartyTypeAsync(gridpage.ToInt(),
                PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<PartyType>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                //.Filterable()
                //.WithMultipleFilters()
                .Searchable(true, false)
                //.Groupable(true)
                .ClearFiltersButton(true)
                .Selectable(true)
                //.SetStriped(true)
                //.ChangePageSize(true)
                .WithGridItemsCount()
                //.WithPaging(PAGESIZE, pds.TotalItemCount)
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false);
            return View("Index",server.Grid);
        }
        /* Party Type Listing Page End */

        /* Party Type silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                PartyType model = new PartyType();
                model.PartyTypeID = id;
                if (Convert.ToInt32(id) > 0)
                {
                    model = iPartyType.GetPartyTypeAsync(id).Result;
                    /*model = new MarketingFeedback() 
                     * { 
                     * MarketingFeedbackID = key 
                     * };*/
                    return PartialView("CreateOrUpdatePartyType", model);
                }
                return PartialView("CreateOrUpdatePartyType", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /* Party Type silder End*/


        /* Party Type Create or Update Page Start*/
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePartyType(PartyType model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _partyTypeID = await iPartyType.PartyTypeCreateOrUpdateAsync(model);

            if (_partyTypeID > 0)
            {
                model.PartyTypeID = _partyTypeID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
            // If we got this far, something failed, redisplay form
        }
        /* Party Type Create or Update Page End*/

    }
}
