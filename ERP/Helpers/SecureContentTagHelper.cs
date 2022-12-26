using Kinfo.JsonStore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ERP.TagHelpers
{
    [HtmlTargetElement("secure-content")]
    public class MySecureContentTagHelper : SecureContentTagHelper
    {
        public MySecureContentTagHelper(IRoleAccessStore roleAccessStore)
            : base(roleAccessStore)
        {
        }
    }
    
    public abstract class SecureContentTagHelper : TagHelper       
    {
        private readonly IRoleAccessStore _roleAccessStore;

        protected SecureContentTagHelper(IRoleAccessStore roleAccessStore)
        {
            _roleAccessStore = roleAccessStore;
        }

        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var _user = ViewContext.HttpContext.User;


            if (!_user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            string userId = _user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                output.SuppressOutput();
                return;
            }

            var actionId = $"{Area}-{Controller}-{Action}";

            //var roles = await (
            //    from usr in _dbContext.Users
            //    join userRole in _dbContext.UserRoles on usr.Id equals userRole.UserId
            //    join role in _dbContext.Roles on userRole.RoleId equals role.Id
            //    where usr.UserName == user.Identity.Name
            //    select role.Id.ToString()
            //).ToArrayAsync();

            if (await _roleAccessStore.HasAccessToActionAsync(actionId, userId))
                return;

            output.SuppressOutput();
        }
    }

}
