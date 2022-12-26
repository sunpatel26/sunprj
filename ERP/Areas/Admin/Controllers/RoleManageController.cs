using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("Role")]
    public class RoleManageController : SettingsController
    {
        private readonly RoleManager<RoleMasterMetadata> _roleManager;
        private ISiteRoleRepository _roles;
        public RoleManageController(RoleManager<RoleMasterMetadata> roleManager,ISiteRoleRepository roles)
        {
            _roleManager = roleManager;
            _roles = roles;
        }
        [DisplayName("Manage Role")]
        public IActionResult Index()
        {
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<RoleMasterMetadata>> columns = c =>
            {
                c.Add(o => o.Name, "Name")
                    .Titled(" Name")
                    .SortInitialDirection(GridSortDirection.Ascending)
                    //.ThenSortByDescending(o => o.CompanyID)
                    .SetWidth(110);

                c.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' href='Permission/Index/{o.RoleID}' ><i class='bx bx-edit'></i></a>");


            };
            PagedDataTable<RoleMasterMetadata> pds = _roles.GetAllRolesAsync(COMPANYID).Result;
            var server = new GridCoreServer<RoleMasterMetadata>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                .SetStriped(true)
                .ChangePageSize(true)
                .WithGridItemsCount()
                .WithPaging(PAGESIZE, pds.TotalItemCount)
                .ChangeSkip(false)
                ;
            return View(server.Grid);
        }        
    }
}
