#pragma checksum "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a27bde34e82c899f863d6f018e0c8e901df3320b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_UploadProductImages_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/UploadProductImages/Index.cshtml")]
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
#line 3 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\_ViewImports.cshtml"
using Business.Entities;

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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a27bde34e82c899f863d6f018e0c8e901df3320b", @"/Areas/Admin/Views/UploadProductImages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9944a1c594c39df98753be59f62bb100aa180b8b", @"/Areas/Admin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_UploadProductImages_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Business.Entities.ProductPhotoPath.ProductPhotoPath>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "UploadProductImages", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddOrEditProductImages", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", new global::Microsoft.AspNetCore.Html.HtmlString("GET"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
  
    ViewData["Title"] = "Upload Product Images";
    Layout = "~/Views/Shared/_LayoutMaster.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container"">
    <div class=""row"">
        <div class=""col-sm-12 col-md-12 col-lg-12 mx-auto"">
            <div class=""card border-0 shadow rounded-3"">
                <div class=""card-body"">
                    <div class=""row ProductImageUpload"">
                        <div class=""ps-3 col-md-6"">
                            <nav aria-label=""breadcrumb"">
                                <ol class=""breadcrumb mb-0 p-0"">
                                    <li class=""breadcrumb-item"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a27bde34e82c899f863d6f018e0c8e901df3320b7861", async() => {
                WriteLiteral("<i class=\"bx bx-home-alt\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    </li>
                                    <li class=""breadcrumb-item active"" aria-current=""page"">Product Images and Description</li>
                                </ol>
                            </nav>
                        </div>
                        <div class=""col-md-6 text-align-right"">
                            <!--<a class=""btn btn-primary px-5"" onclick=""fnUploadProductImage(this)""
                               href=""javascript:void(0)""
                               data-key=""0""
                               data-bs-toggle=""offcanvas""
                               data-bs-target=""#canvas_UploadProductImage""
                               aria-controls=""canvas_UploadProductImage"">
                                Add
                            </a>-->
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a27bde34e82c899f863d6f018e0c8e901df3320b10327", async() => {
                WriteLiteral("Add");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                        </div>\r\n\r\n                    </div>\r\n\r\n                    <div class=\"row pt-1\">\r\n                        <div class=\"col-md-12\">\r\n");
            WriteLiteral("\r\n                            <div class=\"row mb-5\" id=\"imagesLoop\">\r\n");
#nullable restore
#line 47 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
                                 foreach (var items in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"col-sl-12 col-md-4 col-lg-4 col-xl-4 my-3\">\r\n                                        <div class=\"card\">\r\n                                            <img id=\"img01\"");
            BeginWriteAttribute("src", " src=\"", 2509, "\"", 2561, 1);
#nullable restore
#line 51 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
WriteAttributeValue("", 2515, Html.DisplayFor(modelItem => items.ImagePath), 2515, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top rounded mx-auto d-block\" alt=\"Solar Cable\" style=\"width:150px; height:150px;\">\r\n                                            <div class=\"card-body text-center\">\r\n");
            WriteLiteral("                                                <h4 class=\"card-title\">");
#nullable restore
#line 54 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
                                                                  Write(Html.DisplayFor(modelItem => items.ProductImageText));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                                <p class=\"card-text\">");
#nullable restore
#line 55 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
                                                                Write(Html.DisplayFor(modelItem => items.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a27bde34e82c899f863d6f018e0c8e901df3320b14277", async() => {
                WriteLiteral("<i class=\'bx bx-edit\'></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
                                                                                                                                                                        WriteLiteral(items.ProductImageID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </div>\r\n                                    </div>\r\n");
#nullable restore
#line 60 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!--<div class=""offcanvas offcanvas-end"" tabindex=""-1"" id=""canvas_UploadProductImage"">
    <div class=""offcanvas-header"">
        <h5 class=""offcanvas-title"">Add Product Images and Description</h5>
        <button type=""button"" class=""btn-close te  xt-reset"" data-bs-dismiss=""offcanvas"" aria-l abel=""Close""></button>
    </div>
    <div class=""offcanvas-body pt-3 pb-5"">
        <div id=""dvInfo"">
        </div>
    </div>
</div>-->
<!--<script type=""text/javascript"">
    var urladd = '");
#nullable restore
#line 82 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\UploadProductImages\Index.cshtml"
             Write(Url.Action("Get", "UploadProductImages"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    function fnUploadProductImage(obj) {
        var _key = $(obj).data('key');
        var _id = $(obj).data('id');
        var _parameters = { id: _id, key: _key };
        $.ajax({
            url: urladd,
            type: ""POST"",
            data: _parameters,
            success: function(data, textStatus, jqXHR) {
                $('#dvInfo').html(data);
                $(""#canvas_UploadProductImage"").show();//('hide');
            }
        });
    }
</script>-->
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Business.Entities.ProductPhotoPath.ProductPhotoPath>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591