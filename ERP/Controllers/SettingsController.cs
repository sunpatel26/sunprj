using ERP.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP.Controllers
{

    public class SettingsController : Controller
    {
        public static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        public static ILoggerManager _logger => (ILoggerManager)_httpContext.RequestServices.GetService(typeof(ILoggerManager));

        private static IWebHostEnvironment _webHost = new HttpContextAccessor().HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        protected string ClientIP
        {
            get
            {
                string ip = HttpContext.Connection.RemoteIpAddress?.ToString();
                //string ip = _httpContext.GetFeature<IHttpConnectionFeature>()?.RemoteIpAddress;
                //if (string.IsNullOrEmpty(ip))
                //{
                //    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //}
                return ip;
            }
        }
        public static int USERID
        {
            get
            {
                try
                {
                    var response = 0;
                    var userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (!string.IsNullOrWhiteSpace(userId))
                        response = Convert.ToInt32(userId);
                    return response;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static int COMPANYID
        {
            get
            {
                try
                {
                    var response = 0;
                    var compnayID = _httpContext.User.FindFirstValue("CompanyID");

                    if (!string.IsNullOrWhiteSpace(compnayID))
                        response = Convert.ToInt32(compnayID);
                    return response;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static string USERNAME
        {
            get
            {
                try
                {
                    var response = "User";
                    var uname = _httpContext.User.FindFirstValue(ClaimTypes.Name);
                    if (!string.IsNullOrWhiteSpace(uname))
                        response = uname;

                    return response;
                }
                catch
                {
                    return "User";
                }
            }
        }
        public static string COMPANYNAME
        {
            get
            {
                try
                {
                    var response = "ERP";
                    var cName = _httpContext.User.FindFirstValue("CompanyName");
                    if (!string.IsNullOrWhiteSpace(cName))
                        response = cName;

                    return response;
                }
                catch
                {
                    return "ERP";
                }
            }
        }
        public static string LOGOPATH
        {
            get
            {
                try
                {
                    var filePath = "#";
                    //var fullPath = Path.Combine("companylogo");
                    var filename = _httpContext.User.FindFirstValue("LogoPath");
                    //string uploadsFolder = Path.Combine(_webHost.WebRootPath, "companylogo");
                    if (!string.IsNullOrEmpty(filename))
                    {
                        filePath = Path.Combine("/companylogo/", filename);
                    }
                    else
                    {
                        filePath = Path.Combine("/assets/images/", "Srini_Link_logo.png");
                    }
                    return filePath;
                }
                catch
                {
                    return "#";
                }
            }
        }
        public static int PAGESIZE
        {
            get { return 20; }
        }

        public string GetSafeText(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            return value;
        }

        public string GetError(Exception ex)
        {
            if (ex.InnerException != null) return ex.InnerException.Message;

            return ex.Message;
        }

        //public static List<string> Roles(this ClaimsIdentity identity)
        //{
        //    return identity.Claims
        //                   .Where(c => c.Type == ClaimTypes.Role)
        //                   .Select(c => c.Value)
        //                   .ToList();
        //}
    }
}
