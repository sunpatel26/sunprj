#pragma checksum "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9a31c640698fe5e24284f788085f0288dea45ed"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Home_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using ERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using ERP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using Business.SQL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using ERP.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using ERP.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using ERP.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using Business.Entities.Dynamic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
using Business.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9a31c640698fe5e24284f788085f0288dea45ed", @"/Areas/Admin/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9944a1c594c39df98753be59f62bb100aa180b8b", @"/Areas/Admin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DashbaordCount>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_LayoutMaster.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div class=\"container pt-1\">\r\n            <div class=\"col-sm-9 col-md-7 col-lg-12 mx-auto\">\r\n");
            WriteLiteral(@"


                <div class=""row pt-5 px-3"">
                    <div class=""page-breadcrumb d-none d-sm-flex align-items-center pb-2 border-bottom"">
                        <ol class=""breadcrumb mb-0 p-0"">
                            <li class=""breadcrumb-item"">
                                <i class=""bx bx-home-alt""></i>
                            </li>
                            <li class=""breadcrumb-item active"" aria-current=""page"">Dashboard</li>
                        </ol>
                    </div>
");
            WriteLiteral(@"                    <div class=""row py-2"">
                        <div class=""col-sm-12 col-md-12 col-lg-4 col-xl-4"">
                            <div class=""card p-4"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">Total Visitor Count</h5>
                                    <h1 class=""card-text"">");
#nullable restore
#line 34 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
                                                     Write(Model.TotalVisitor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"                                </div>
                            </div>
                        </div>

                        <div class=""col-sm-12 col-md-12 col-lg-4 col-xl-4"">
                            <div class=""card p-4"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">Today Upcoming Visitor</h5>
                                    <h1 class=""card-text"">");
#nullable restore
#line 45 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
                                                     Write(Model.TodayUpcommingVisitor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"                                </div>
                            </div>
                        </div>

                        <div class=""col-sm-12 col-md-12 col-lg-4 col-xl-4"">
                            <div class=""card p-4"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">Today Visit Completed</h5>
");
            WriteLiteral("                                    <h1 class=\"card-text\">");
#nullable restore
#line 56 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
                                                     Write(Model.TodayVisitCompleted);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"                                </div>
                            </div>
                        </div>


                        <div class=""col-sm-12 col-md-12 col-lg-4 col-xl-4"">
                            <div class=""card p-4"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">Upcomming Visitor</h5>
");
            WriteLiteral("                                    <h1 class=\"card-text\">");
#nullable restore
#line 68 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
                                                     Write(Model.UpcommingVisitor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"                                </div>
                            </div>
                        </div>

                        <div class=""col-sm-12 col-md-12 col-lg-4 col-xl-4"">
                            <div class=""card p-4"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">Rejected Visitor</h5>
");
            WriteLiteral("                                    <h1 class=\"card-text\">");
#nullable restore
#line 79 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\Home\Index.cshtml"
                                                     Write(Model.TotalRejected);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class=""row"">
                    <h1>Bar Chart Display</h1>
                </div>
");
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DashbaordCount> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
