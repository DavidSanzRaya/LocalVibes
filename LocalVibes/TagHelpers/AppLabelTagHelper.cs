using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LocalVibes.TagHelpers
{
    public class AppLabelTagHelper : TagHelper
    {
        public string DefaultStyle { get; set; } = "all: unset; background-color: #FFF; border-radius: 999em; display: flex; justify-content: center; align-items: center; text-align: center; width: 100px; height: 40px;";
        public string Text { get; set; }

        public string BorderStyle { get; set; }

        public string Class { get; set; }
        public string OnClick { get; set; }
        public string Style { get; set; } 
        public string Color { get; set; } 

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string mergedStyle = DefaultStyle;
            output.TagName = "div";

            output.Attributes.SetAttribute("class", Class);

            if (!string.IsNullOrEmpty(OnClick))
            {
                output.Attributes.SetAttribute("onclick", OnClick);
            }

            if (!string.IsNullOrEmpty(BorderStyle))
            {
                mergedStyle += " " + BorderStyle;
            }

            if (!string.IsNullOrEmpty(Color))
            {
                mergedStyle += $"background-color:{Color};";
            }

            
            
            
            if (!string.IsNullOrEmpty(Style))
            {
                mergedStyle += " " + Style; 
            }
            output.Attributes.SetAttribute("style", mergedStyle);

            output.Content.SetContent(Text.ToUpper());
        }
    }

}

