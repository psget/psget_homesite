using System.Web.Mvc;
using Newtonsoft.Json;

namespace Homesite.Views
{
    public static class LayoutExtensions
    {
        public static string ActivePart(this HtmlHelper helper, string active) 
        {
            if (helper.ViewBag.SitePart == active)
            {
                return "active";
            }
            return "";
        }   
     
        public static string ToJson(this object obj)
        {            
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,                                   
            };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);        
        }
    }
}