using Kinfo.JsonStore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace Kinfo.JsonStore.Builder
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public FilterConstraint _someFilterParameter=FilterConstraint.UserOnline;

        public CustomAuthorizeAttribute(FilterConstraint someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IRoleAccessStore _roleAccessStore = (IRoleAccessStore)context.HttpContext.RequestServices.GetService(typeof(IRoleAccessStore));
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            string actionId = "Super-Home-Index";
            if (!_roleAccessStore.HasAccessToActionAsync(actionId, "4").Result)
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                { { "area", "" },
                     { "controller", "Unauthorized" },
                     { "action", "Index" }
                });

            var isAuthorized = true;// someService.IsUserAuthorized(user.Identity.Name, _someFilterParameter);
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}

