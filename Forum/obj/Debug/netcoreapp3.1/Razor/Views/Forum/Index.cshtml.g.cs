#pragma checksum "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d024b1bebb29725208d8bcfabc4e124212ccbfb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Forum_Index), @"mvc.1.0.view", @"/Views/Forum/Index.cshtml")]
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
#line 1 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\_ViewImports.cshtml"
using Forum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\_ViewImports.cshtml"
using Forum.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d024b1bebb29725208d8bcfabc4e124212ccbfb", @"/Views/Forum/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ac609fd15eba99a48942b04c8579a10a24406fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Forum_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Forum.ViewModels.Forum.ForumIndexModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration:none;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Forum", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Topic", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
   
    ViewBag.Title = "Forum";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table table-bordered\">\r\n    <tbody>\r\n");
#nullable restore
#line 9 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
         foreach (var forum in Model.ForumList)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr class=\"forum-row \">\r\n                    <td class=\"forum-logo-card\">\r\n                        <div class=\"card\" style=\"border:none\">\r\n\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d024b1bebb29725208d8bcfabc4e124212ccbfb4811", async() => {
                WriteLiteral("\r\n                            <div class=\"card-body text-center\" style=\"padding:0px;\">\r\n                                    ");
#nullable restore
#line 18 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
                               Write(forum.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </div>\r\n                            <img class=\"card-img-bottom mx-auto\" id=\"forumLogo\"");
                BeginWriteAttribute("src", " src=\"", 739, "\"", 763, 1);
#nullable restore
#line 20 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
WriteAttributeValue("", 745, forum.ForumImgUrl, 745, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            <div class=\"card-footer\">\r\n                                Posts:\r\n                                <p class=\"text-center mb-0\">\r\n                                    ");
#nullable restore
#line 24 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
                               Write(forum.PostsCount);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </p>\r\n                            </div>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
                                                                                                             WriteLiteral(forum.Id);

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
            WriteLiteral("\r\n                        </div>\r\n                    </td>\r\n                    <td style=\"text-align:justify; padding-right:20px;\">\r\n                        ");
#nullable restore
#line 31 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
                   Write(forum.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n            </tr>\r\n");
#nullable restore
#line 34 "C:\Users\Mortiner\source\repos\MortinerAzohen\Forum\Forum\Views\Forum\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Forum.ViewModels.Forum.ForumIndexModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
