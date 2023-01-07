using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vehicles;

[HtmlTargetElement("table")]
[HtmlTargetElement("p")]
public class VisibilityTagHelper : TagHelper
{
    public bool IsVisible { get; set; } = true;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if(!IsVisible)
            output.SuppressOutput();

        return base.ProcessAsync(context, output);
    }

}