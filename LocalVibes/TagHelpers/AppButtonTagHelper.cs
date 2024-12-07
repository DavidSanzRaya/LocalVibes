using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LocalVibes.TagHelpers
{
    public class AppButtonTagHelper : TagHelper
    {
        public string DefaultStyle { get; set; } = "all: unset; background-color: #FFF; border-radius: 999em; display: flex; justify-content: center; align-items: center; text-align: center; width: 200px; height: 40px;";
        public string ButtonText { get; set; }

        public string BorderStyle { get; set; }

        public string ButtonType { get; set; }
        public string Class { get; set; }
        public string OnClick { get; set; }
        public string Style { get; set; } 

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string mergedStyle = DefaultStyle;
            output.TagName = "button";

            output.Attributes.SetAttribute("class", Class);

            if (!string.IsNullOrEmpty(OnClick))
            {
                output.Attributes.SetAttribute("onclick", OnClick);
            }

            if (!string.IsNullOrEmpty(BorderStyle))
            {
                mergedStyle += " " + BorderStyle;
            }

            if (!string.IsNullOrEmpty(ButtonType))
            {
                switch (ButtonType.ToLower())  
                {
                    case "primary":
                        
                        mergedStyle += "background-color:#FFF;";
                        break;
                    case "secondary":
                        mergedStyle += "background-color:#B336B3;color:#FFF";
                        break;
                    default:                      
                        mergedStyle += "background-color:#FFF;";
                        break;
                }
            }

            
            
            
            if (!string.IsNullOrEmpty(Style))
            {
                mergedStyle += " " + Style; 
            }
            output.Attributes.SetAttribute("style", mergedStyle);

            output.Content.SetContent(ButtonText);
        }
    }

}

