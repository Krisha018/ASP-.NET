using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;
using System.Threading.Tasks;
namespace Student_Registration
{
    [HtmlTargetElement("cute")]
    public class EmailTagHelper: TagHelper

    {
        public  string ImageLink {  get; set; } 
        public string AlternativeText   { get; set; }

        // Can be passed via <email mail-to="..." />. 
        // PascalCase gets translated into kebab-case.
       
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           output.TagName="img";
            output.TagMode = TagMode.StartTagOnly;
            output.Attributes.SetAttribute("src", ImageLink);
            output.Attributes.SetAttribute("alt", AlternativeText);



        }
    }
}
