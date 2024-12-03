using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LocalVibes.TagHelpers
{
    public class AppInputTagHelper : TagHelper
    {
        public string DefaultStyle { get; set; } = "all: unset; background-color: #FFF; border-radius: 999em; text-align: left; width: 200px; height: 40px;padding-left:0.5em; ";
        public string Placeholder { get; set; }
        public string BorderStyle { get; set; }
        public string Class { get; set; } = "form-control input-placeholder"; 
        public string Style { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "input";

            string mergedStyle = DefaultStyle;
            if (!string.IsNullOrEmpty(BorderStyle))
            {
                mergedStyle += " " + BorderStyle;
            }

            if (!string.IsNullOrEmpty(Style))
            {
                mergedStyle += " " + Style;
            }


            output.Attributes.SetAttribute("class", Class);
            output.Attributes.SetAttribute("style", mergedStyle);


            if (!string.IsNullOrEmpty(Placeholder))
            {
                output.Attributes.SetAttribute("placeholder", Placeholder);
            }
        }
    }
}
