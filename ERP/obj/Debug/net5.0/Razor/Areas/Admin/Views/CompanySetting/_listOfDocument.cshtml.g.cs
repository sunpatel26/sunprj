#pragma checksum "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aef4511260bbfb2203332dc967d8c0992534dd14"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_CompanySetting__listOfDocument), @"mvc.1.0.view", @"/Areas/Admin/Views/CompanySetting/_listOfDocument.cshtml")]
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
#line 1 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
using Business.SQL;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aef4511260bbfb2203332dc967d8c0992534dd14", @"/Areas/Admin/Views/CompanySetting/_listOfDocument.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9944a1c594c39df98753be59f62bb100aa180b8b", @"/Areas/Admin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_CompanySetting__listOfDocument : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PagedDataTable<CompanyDocumentMetadata>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/assets/vendors/simple-datatables/style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn downdocument"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "CompanySetting", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DocumentDownload", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/assets/vendors/simple-datatables/simple-datatables.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "aef4511260bbfb2203332dc967d8c0992534dd146975", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<div id=""CompanyTable"">
    <div class=""row"">
        <div class=""col-lg-6"">
            <h6 class=""cards-title"">
                Documents
            </h6>
        </div>
        <div class=""col-lg-6 text-align-right"">
            <a onclick=""fnAddDocument(0)""
               class=""btn btn-primary px-5""
               href=""javascript:void(0)""
               data-id=""0""
               data-bs-toggle=""offcanvas""
               data-bs-target=""#canvas_document""
               aria-controls=""canvas_document"">
                Add Document
            </a>
        </div>
    </div>
    <div class=""row"">
        <table class=""table table-striped"" id=""tblDocument"">
            <thead>
                <tr>
                    <th>Sr. No.</th>
                    <th>Document Name</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 33 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                 if (Model != null)
                {
                    foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                           Write(item.SrNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 39 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                           Write(item.DocumentName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 40 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                           Write(item.DocumentDesc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                            <td>\r\n                                <a class=\'btn editdocument\'");
            BeginWriteAttribute("onclick", " onclick=\"", 1493, "\"", 1542, 3);
            WriteAttributeValue("", 1503, "fnAddDocument(", 1503, 14, true);
#nullable restore
#line 43 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
WriteAttributeValue("", 1517, item.CompanyDocumentsID, 1517, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1541, ")", 1541, 1, true);
            EndWriteAttribute();
            WriteLiteral(@"
                           href=""javascript:void(0)""
                           data-bs-toggle=""offcanvas""
                           data-bs-target=""#canvas_document""
                           aria-controls=""canvas_document""
                           data-id=""");
#nullable restore
#line 48 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                               Write(item.CompanyDocumentsID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("compnayid", "\r\n                           compnayid=\"", 1837, "\"", 1892, 1);
#nullable restore
#line 49 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
WriteAttributeValue("", 1877, item.CompanyID, 1877, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\'bx bx-edit\'></i></a>\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aef4511260bbfb2203332dc967d8c0992534dd1412001", async() => {
                WriteLiteral("<i class=\'bx bx-download\'></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 51 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                                    WriteLiteral(item.CompanyDocumentsID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                <a class=\'btn deletedocument\' href=\"#!\" data-id=\"");
#nullable restore
#line 52 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                                                                            Write(item.CompanyDocumentsID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("compnayid", " compnayid=\"", 2266, "\"", 2293, 1);
#nullable restore
#line 52 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
WriteAttributeValue("", 2278, item.CompanyID, 2278, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\'icon dripicons-trash\'></i></a>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 55 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>
<div class=""offcanvas offcanvas-end"" tabindex=""-1"" id=""canvas_document"">
    <div class=""offcanvas-header"">
        <h5 class=""offcanvas-title"">Document</h5>
        <button type=""button"" class=""btn-close te  xt-reset"" data-bs-dismiss=""offcanvas"" aria-l abel=""Close""></button>
    </div>
    <div class=""offcanvas-body pt-3 pb-5"">
        <div id=""dvInfoDoc"">
        </div>
    </div>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aef4511260bbfb2203332dc967d8c0992534dd1416151", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script>\r\n    var urlDeleteCompanyFromUser = \'");
#nullable restore
#line 74 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                               Write(Url.Action("DeleteDocument", "CompanySetting"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n    var urlAddDocument = \'");
#nullable restore
#line 75 "C:\Users\Lenovo\Documents\GitHub\ERP\ERP\Areas\Admin\Views\CompanySetting\_listOfDocument.cshtml"
                     Write(Url.Action("AddDocument", "CompanySetting"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    $(document).ready(function () {
        let table1 = document.querySelector('#tblDocument');
        let dataTable = new simpleDatatables.DataTable(table1);


    });
    function fnAddDocument(id) {
        var _id = id;//$(this).data('id');
        var _cid = $(""#CompanyID"").val();
        var _parameters = { id: _id, compnayid: _cid };
        $.ajax({
            url: urlAddDocument,
            type: ""POST"",
            data: _parameters,
            success: function (data, textStatus, jqXHR) {
                $(""#canvas_document"").show();//('hide');
                console.log(data);
                $('#dvInfoDoc').html(data);
            }
        });
    }
</script>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PagedDataTable<CompanyDocumentMetadata>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
