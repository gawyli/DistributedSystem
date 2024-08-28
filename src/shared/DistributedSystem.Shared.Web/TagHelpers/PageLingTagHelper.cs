using DistributedSystem.Shared.Utility.Extensions;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DistributedSystem.Shared.Web.TagHelpers;
public class PageLinkTagHelper : AnchorTagHelper
{
    public Type? ForPage { get; set; }

    public PageLinkTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        if (this.ForPage != null)
        {
            var routeId = RouteValues.FirstOrDefault();

            if (routeId.Value == null)
            {
                output.Attributes.SetAttribute("id", this.ForPage.PageAnchorId());
            }
            else
            {
                output.Attributes.SetAttribute("id", this.ForPage.PageAnchorId(routeId.Value));
            }
        }
        base.Process(context, output);
    }
}
