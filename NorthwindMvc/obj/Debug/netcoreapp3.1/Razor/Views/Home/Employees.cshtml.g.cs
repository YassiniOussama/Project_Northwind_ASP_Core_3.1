#pragma checksum "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "88dbf93eff2590a3ba3047c2acd0d0eec9099cb5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Employees), @"mvc.1.0.view", @"/Views/Home/Employees.cshtml")]
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
#line 1 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\_ViewImports.cshtml"
using NorthwindMvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\_ViewImports.cshtml"
using NorthwindMvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88dbf93eff2590a3ba3047c2acd0d0eec9099cb5", @"/Views/Home/Employees.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c10324db967624bce2cebb2dbfaf1321bdbfb158", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Employees : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NorthwinDB.Employee>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 3 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<table class=\"table\">\r\n\t<tr>\r\n        <th>First Name</th>\r\n\t\t<th>Last Name</th>\r\n\t\t<th>Address</th>\r\n\t\t<th>Home Phone</th>\r\n\t\t<th>BirthDate</th>\r\n\t</tr>\r\n");
#nullable restore
#line 12 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
     foreach(var item in Model)
	{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<tr>\r\n\t\t\t<td>");
#nullable restore
#line 15 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t<td>");
#nullable restore
#line 16 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t<td>\r\n\t\t\t\t");
#nullable restore
#line 18 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t");
#nullable restore
#line 19 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.City));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t");
#nullable restore
#line 20 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.Region));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t");
#nullable restore
#line 21 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.Country));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t");
#nullable restore
#line 22 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.PostalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t</td>\r\n\t\t\t<td>");
#nullable restore
#line 24 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.HomePhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 25 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
           Write(Html.DisplayFor(modelItem => item.BirthDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t</tr>\r\n");
#nullable restore
#line 27 "C:\Users\Oussama\Documents\Projects VS_Code\PracticalApps\NorthwindMvc\Views\Home\Employees.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<NorthwinDB.Employee>> Html { get; private set; }
    }
}
#pragma warning restore 1591
