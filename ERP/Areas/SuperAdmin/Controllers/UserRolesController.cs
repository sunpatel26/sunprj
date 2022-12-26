using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Controllers;
using Microsoft.AspNetCore.Http;
using GridShared;
using Business.Entities;
using GridShared.Sorting;
using Business.SQL;
using GridCore.Server;
using Microsoft.AspNetCore.Authorization;
using Business.Interface;
using System.ComponentModel;

namespace ERP.Areas.Admin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Manage Users")]
    public class UserRolesController : SettingsController
    {
        private readonly RoleManager<RoleMasterMetadata> _roleManager;
        private readonly UserManager<UserMasterMetadata> _userManager;
        private ISiteUserService _users;
        private ISiteRoleRepository _roles;
        public UserRolesController(RoleManager<RoleMasterMetadata> roleManager, UserManager<UserMasterMetadata> userManager, ISiteUserService iusermanage,ISiteRoleRepository roles)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _users = iusermanage;
            _roles = roles;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    var userRolesViewModel = new List<UserRolesViewModel>();
        //    foreach (ApplicationUser user in users)
        //    {
        //        var thisViewModel = new UserRolesViewModel();
        //        thisViewModel.UserId = user.Id;
        //        thisViewModel.Email = user.Email;
        //        thisViewModel.FirstName = user.FirstName;
        //        thisViewModel.LastName = user.LastName;
        //        thisViewModel.Roles = await GetUserRoles(user);
        //        userRolesViewModel.Add(thisViewModel);
        //    }
        //    return View(userRolesViewModel);
        //}

        public async Task<IActionResult> Index(string gridpage = "1", string gridsearch = "", string gridcolumn = "Name", int griddir = 0)
        {

            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<UserRolesViewModel>> columns = c =>
            {
                c.Add(o => o.FirstName, "Name")
                    .Titled(" Name")
                    .SortInitialDirection(GridSortDirection.Ascending)
                    //.ThenSortByDescending(o => o.CompanyID)
                    .SetWidth(110);

                c.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .RenderValueAs(o => $"<a class='btn' href='UserRoles/Manage/{o.UserId}' ><i class='bx bx-edit'></i></a>");


            };
            PagedDataTable<UserRolesViewModel> userRolesViewModel = new PagedDataTable<UserRolesViewModel>();

            PagedDataTable<UserMasterMetadata>  pds = _users.GetAllUser(1,gridpage.ToInt(),PAGESIZE);//.ToListAsync();
            //var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (UserMasterMetadata user in pds)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.UserID.ToString();
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.Forename;
                thisViewModel.LastName = user.Surname;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }

            var server = new GridCoreServer<UserRolesViewModel>(userRolesViewModel, query, false, "ordersGrid",
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


        public async Task<IActionResult> Manage(string id)
        {
            ViewBag.userId = id;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.Username;
            var model = new List<ManageUserRolesViewModel>();
            PagedDataTable<RoleMasterMetadata> pdsRoles = _roles.GetAllRolesAsync().Result;
            foreach (var role in pdsRoles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.RoleID.ToString(),
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                foreach (var item in roles)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, item);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot remove user existing roles");
                        return View(model);
                    }
                }               
            }
            var userresult = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!userresult.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        private async Task<List<string>> GetUserRoles(UserMasterMetadata user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}