#pragma checksum "C:\dev\Stark\Stark\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fe4cdd60617fe7b1d5aeaea77ad56a5308d9c811"
// <auto-generated/>
#pragma warning disable 1591
namespace Stark.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\dev\Stark\Stark\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\dev\Stark\Stark\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\dev\Stark\Stark\_Imports.razor"
using Stark;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\dev\Stark\Stark\_Imports.razor"
using Stark.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\dev\Stark\Stark\_Imports.razor"
using Stark.MVVM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\dev\Stark\Stark\_Imports.razor"
using Stark.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\dev\Stark\Stark\_Imports.razor"
using Stark.Components;

#line default
#line hidden
#nullable disable
    public class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", true);
            __builder.AddMarkupContent(2, "\r\n\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", true);
            __builder.AddMarkupContent(5, "\r\n        ");
            __builder.AddContent(6, 
#nullable restore
#line 13 "C:\dev\Stark\Stark\Shared\MainLayout.razor"
         Body

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(7, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(8, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591