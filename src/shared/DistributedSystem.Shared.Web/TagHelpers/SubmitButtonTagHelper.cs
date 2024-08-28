using DistributedSystem.Shared.Utility.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DistributedSystem.Shared.Web.TagHelpers;
public class SubmitButtonTagHelper : TagHelper
{
    public ModelExpression? ForCommand { get; set; }
    public ModelExpression? ForQuery { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("type", "submit");
        if (this.ForCommand != null)
        {
            output.Attributes.SetAttribute("id", this.ForCommand.Model.GetType().SubmitButtonId());
        }
        else if (this.ForQuery != null)
        {
            output.Attributes.SetAttribute("id", this.ForQuery.Model.GetType().SubmitButtonId());
        }
        base.Process(context, output);
    }
}
