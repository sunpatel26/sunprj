#pragma checksum "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a712bbd54d49ec478f51de6c5d3407777bff3419"
// <auto-generated/>
#pragma warning disable 1591
namespace GridBlazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
using GridBlazor.Resources;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
using GridShared.Sorting;

#line default
#line hidden
#nullable disable
    public partial class GridHeaderComponent<T> : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "th");
            __builder.AddAttribute(1, "class", (
#nullable restore
#line 6 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
            _cssClass

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 6 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                       _dropClass

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "style", 
#nullable restore
#line 6 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                           _cssStyles

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(3, "ondragenter", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.DragEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                  HandleDragEnter

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "ondragleave", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.DragEventArgs>(this, 
#nullable restore
#line 8 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                  HandleDragLeave

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "ondrop", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.DragEventArgs>(this, 
#nullable restore
#line 9 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
             HandleDrop

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "ondragover", "event.preventDefault();");
            __builder.AddMarkupContent(7, "\r\n    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "grid-header-group");
            __builder.AddMarkupContent(10, "\r\n");
#nullable restore
#line 13 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
         if (GridComponent.Grid.RearrangeColumnEnabled
                && !string.IsNullOrEmpty(_dropClass))
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(11, "            ");
            __builder.AddMarkupContent(12, @"<div class=""grid-column-rearrange-insert-placeholder"">
                <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-geo-fill"" viewBox=""0 0 16 16"">
                  <path fill-rule=""evenodd"" d=""M4 4a4 4 0 1 1 4.5 3.969V13.5a.5.5 0 0 1-1 0V7.97A4 4 0 0 1 4 3.999zm2.493 8.574a.5.5 0 0 1-.411.575c-.712.118-1.28.295-1.655.493a1.319 1.319 0 0 0-.37.265.301.301 0 0 0-.057.09V14l.002.008a.147.147 0 0 0 .016.033.617.617 0 0 0 .145.15c.165.13.435.27.813.395.751.25 1.82.414 3.024.414s2.273-.163 3.024-.414c.378-.126.648-.265.813-.395a.619.619 0 0 0 .146-.15.148.148 0 0 0 .015-.033L12 14v-.004a.301.301 0 0 0-.057-.09 1.318 1.318 0 0 0-.37-.264c-.376-.198-.943-.375-1.655-.493a.5.5 0 1 1 .164-.986c.77.127 1.452.328 1.957.594C12.5 13 13 13.4 13 14c0 .426-.26.752-.544.977-.29.228-.68.413-1.116.558-.878.293-2.059.465-3.34.465-1.281 0-2.462-.172-3.34-.465-.436-.145-.826-.33-1.116-.558C3.26 14.752 3 14.426 3 14c0-.599.5-1 .961-1.243.505-.266 1.187-.467 1.957-.594a.5.5 0 0 1 .575.411z""></path>
                </svg>
            </div>   
");
#nullable restore
#line 21 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
         if (Column.HeaderCheckbox)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(13, "            ");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "grid-header-checkbox");
            __builder.AddMarkupContent(16, "\r\n");
#nullable restore
#line 25 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                 if (_allChecked == null || _showAllChecked == false)
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(17, "                    ");
            __builder.OpenElement(18, "input");
            __builder.AddAttribute(19, "class", "grid-header-checkbox-input");
            __builder.AddAttribute(20, "type", "checkbox");
            __builder.AddAttribute(21, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 27 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                         CheckboxChangeHandler

#line default
#line hidden
#nullable disable
            ));
            __builder.AddEventStopPropagationAttribute(22, "onclick", true);
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n                    <span class=\"null-checkbox\"></span>\r\n");
#nullable restore
#line 29 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }
                else
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(24, "                    ");
            __builder.OpenElement(25, "input");
            __builder.AddAttribute(26, "type", "checkbox");
            __builder.AddAttribute(27, "checked", 
#nullable restore
#line 32 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                     _allChecked

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(28, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 32 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                             CheckboxChangeHandler

#line default
#line hidden
#nullable disable
            ));
            __builder.AddEventStopPropagationAttribute(29, "onclick", true);
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n");
#nullable restore
#line 33 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(31, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n");
#nullable restore
#line 35 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
         if (Column.ParentGrid.ExtSortingEnabled || GridComponent.Grid.RearrangeColumnEnabled)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(33, "            ");
            __builder.OpenElement(34, "div");
            __builder.AddAttribute(35, "class", "grid-extsort-draggable" + " " + (
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                _cssSortingClass

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(36, "draggable", "true");
            __builder.AddAttribute(37, "data-column", 
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                 Column.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(38, "ondragstart", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.DragEventArgs>(this, 
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                            () => HandleDragStart()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(39, "onmouseover", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                                                                   DisplayTooltip

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(40, "onmouseout", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 38 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                                                                                                HideTooltip

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(41, "\r\n");
#nullable restore
#line 39 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                 if (Column.SortEnabled)
                {
                    if (Column.IsSorted)
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(42, "                        ");
            __builder.OpenElement(43, "button");
            __builder.AddAttribute(44, "type", "button");
            __builder.AddAttribute(45, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 43 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                        TitleButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(46, "data-column", 
#nullable restore
#line 43 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                          Column.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(47, "data-sorted", 
#nullable restore
#line 43 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                      Column.Direction == GridSortDirection.Ascending ? "asc" : "desc"

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 43 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(48, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n");
#nullable restore
#line 44 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(50, "                        ");
            __builder.OpenElement(51, "button");
            __builder.AddAttribute(52, "type", "button");
            __builder.AddAttribute(53, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 47 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                        TitleButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(54, "data-column", 
#nullable restore
#line 47 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                          Column.Name

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 47 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(55, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n");
#nullable restore
#line 48 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                    }
                }
                else
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(57, "                    ");
            __builder.OpenElement(58, "span");
            __builder.AddAttribute(59, "data-column", 
#nullable restore
#line 52 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                        Column.Name

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 52 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(60, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(61, "\r\n");
#nullable restore
#line 53 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                 if (Column.IsSorted)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(62, "                    <span class=\"grid-sort-arrow\"></span>\r\n");
#nullable restore
#line 57 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(63, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n");
#nullable restore
#line 59 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
        }
        else
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(65, "            ");
            __builder.OpenElement(66, "div");
            __builder.AddAttribute(67, "class", 
#nullable restore
#line 62 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                         _cssSortingClass

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(68, "onmouseover", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 62 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                         DisplayTooltip

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(69, "onmouseout", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 62 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                      HideTooltip

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(70, "\r\n");
#nullable restore
#line 63 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                 if (Column.SortEnabled)
                {
                    if (Column.IsSorted)
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(71, "                        ");
            __builder.OpenElement(72, "button");
            __builder.AddAttribute(73, "type", "button");
            __builder.AddAttribute(74, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 67 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                        TitleButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(75, "data-column", 
#nullable restore
#line 67 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                          Column.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(76, "data-sorted", 
#nullable restore
#line 67 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                      Column.Direction == GridSortDirection.Ascending ? "asc" : "desc"

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 67 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(77, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(78, "\r\n");
#nullable restore
#line 68 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(79, "                        ");
            __builder.OpenElement(80, "button");
            __builder.AddAttribute(81, "type", "button");
            __builder.AddAttribute(82, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 71 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                        TitleButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(83, "data-column", 
#nullable restore
#line 71 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                          Column.Name

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 71 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(84, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(85, "\r\n");
#nullable restore
#line 72 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                    }
                }
                else
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(86, "                    ");
            __builder.OpenElement(87, "span");
            __builder.AddAttribute(88, "data-column", 
#nullable restore
#line 76 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                        Column.Name

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 76 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(89, Column.Title);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(90, "\r\n");
#nullable restore
#line 77 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 78 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                 if (Column.IsSorted)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(91, "                    <span class=\"grid-sort-arrow\"></span>\r\n");
#nullable restore
#line 81 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(92, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(93, "\r\n");
#nullable restore
#line 83 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 84 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
         if (!Column.HeaderCheckbox && Column.FilterEnabled)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(94, "            ");
            __builder.OpenElement(95, "div");
            __builder.AddAttribute(96, "class", "grid-filter");
            __builder.AddAttribute(97, "data-type", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                 Column.FilterWidgetTypeName

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(98, "data-name", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                          Column.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(99, "data-widgetdata", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                         JsonSerializer.Serialize(Column.FilterWidgetData)

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(100, "data-filterdata", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                                                                                              JsonSerializer.Serialize(_filterSettings)

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(101, "data-url", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                                                                                                                                                    _url

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(102, "data-clearinitfilter", 
#nullable restore
#line 86 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                                                                                                                                                                                                 _clearInitFilter.ToString()

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(103, "\r\n                ");
            __builder.OpenElement(104, "span");
            __builder.AddAttribute(105, "class", 
#nullable restore
#line 87 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                              _cssFilterClass

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(106, "title", 
#nullable restore
#line 87 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                       Strings.FilterButtonTooltipText

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(107, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 87 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                                                  FilterIconClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddEventStopPropagationAttribute(108, "onclick", true);
            __builder.CloseElement();
            __builder.AddMarkupContent(109, "\r\n                ");
#nullable restore
#line 88 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
__builder.AddContent(110, FilterWidgetRender);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(111, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(112, "\r\n");
#nullable restore
#line 90 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(113, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(114, "\r\n");
#nullable restore
#line 92 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
     if (!string.IsNullOrWhiteSpace(Column.TooltipValue))
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(115, "        ");
            __builder.OpenComponent<GridBlazor.Pages.HeaderTooltipComponent<T>>(116);
            __builder.AddAttribute(117, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 94 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                               Column.TooltipValue

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(118, "Visible", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 94 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
                                                                              _isTooltipVisible

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(119, "\r\n");
#nullable restore
#line 95 "C:\Users\Lenovo\Documents\GitHub\ERP\Infrastructure\GridBlazor\Pages\GridHeaderComponent.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
