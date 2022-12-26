
using Kinfo.JsonStore.Builder;
using Kinfo.JsonStore.Enums;
using Kinfo.JsonStore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Kinfo.JsonStore
{
    public class MvcControllerDiscovery : IMvcControllerDiscovery
    {
        private List<MvcControllerInfo> _mvcControllers;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public MvcControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IList<MvcControllerInfo> GetControllers()
        {
           

            _mvcControllers = new List<MvcControllerInfo>();
            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .OfType<ControllerActionDescriptor>()
                .Select(descriptor => descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                var currentController = new MvcControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                    IsAnonymous=controllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null
                };

                var actions = new List<MvcActionInfo>();
                foreach (var descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First()))
                {
                    if (!currentController.IsAnonymous)
                    {
                        var methodInfo = descriptor.MethodInfo;
                        if (IsProtectedAction(controllerTypeInfo, methodInfo))
                        {
                            MvcActionInfo mvcInfo = new MvcActionInfo
                            {
                                ControllerId = currentController.Id,
                                Name = descriptor.ActionName,
                                DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                                customAttribute = methodInfo.GetCustomAttribute<CustomAuthorizeAttribute>()
                            };
                            if (mvcInfo.customAttribute != null)
                            {
                                if (mvcInfo.customAttribute._someFilterParameter != FilterConstraint.Ignore)
                                {
                                    actions.Add(mvcInfo);
                                }
                            }
                            else
                            {
                                actions.Add(mvcInfo);
                            }
                        }
                    }
                }

                if (actions.Any())
                {
                    currentController.Actions = actions;
                    _mvcControllers.Add(currentController);
                }
            }

            return _mvcControllers;
        }

        private static bool IsProtectedAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            if (actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
                return false;

            if (controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                return true;

            if (actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                return true;

            return false;
        }
    }
}