using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using ERP.Models;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Role")]
    public class RoleController : SettingsController
    {
        private readonly RoleManager<RoleMasterMetadata> _roleManager;
        private readonly UserManager<UserMasterMetadata> _userManager;
        private ISiteRoleRepository _roles;
        public RoleController(RoleManager<RoleMasterMetadata> roleManager, UserManager<UserMasterMetadata> userManager, ISiteRoleRepository iroleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roles = iroleManager;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        //public async Task<IActionResult> Index()
        //{
        //    var userCheck = await _userManager.FindByEmailAsync("haresh.kyada@gmail.com");
        //    var roles = await _roles.GetAllRolesAsync();
        //    return View(roles);
        //}

        [DisplayName("Role List")]
        public ActionResult Index(string gridpage = "1", string gridsearch = "", string gridcolumn = "Name", int griddir = 0)
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
                    .RenderValueAs(o => $"<a class='btn' href='Role/Edit/{o.RoleID}' ><i class='bx bx-edit'></i></a>");


            };
            PagedDataTable<RoleMasterMetadata> pds = _roles.GetAllRolesAsync().Result;
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
        [DisplayName("Role Create")]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleMasterMetadata model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new RoleMasterMetadata() { Name = model.Name, NormalizedName = model.Name });
                return RedirectToAction("Index");
            }
           
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [DisplayName("Role Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            RoleMasterMetadata role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", _roleManager.Roles);
        }
        [DisplayName("Role Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            RoleMasterMetadata role = await _roleManager.FindByIdAsync(id);

            return View("Edit", role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleMasterMetadata model)
        {

            if (ModelState.IsValid)
            {
                RoleMasterMetadata role = await _roleManager.FindByIdAsync(model.RoleID.ToString());
                role.Name = model.Name;
                role.NormalizedName = model.NormalizedName;
                await _roleManager.UpdateAsync(role);
                //    foreach (string userId in model.AddIds ?? new string[] { })
                //    {
                //        AppUser user = await userManager.FindByIdAsync(userId);
                //        if (user != null)
                //        {
                //            result = await userManager.AddToRoleAsync(user, model.RoleName);
                //            if (!result.Succeeded)
                //                Errors(result);
                //        }
                //    }
                //    foreach (string userId in model.DeleteIds ?? new string[] { })
                //    {
                //        AppUser user = await userManager.FindByIdAsync(userId);
                //        if (user != null)
                //        {
                //            result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                //            if (!result.Succeeded)
                //                Errors(result);
                //        }
                //    }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Edit(model.RoleID.ToString());
        }
    }
}