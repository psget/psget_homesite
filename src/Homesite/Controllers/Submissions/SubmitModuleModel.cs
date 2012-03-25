using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Homesite.Controllers.Submissions
{
    public class SubmitModuleModel
    {
        [Required]
        [Display(Name = "ID")]
        public string ModuleID { get; set; }

        [Required]
        public string URL { get; set; }
        
        public string Type { get; set; }

        [Required]
        [Display(Name = "Project URL")]
        public string ProjectUrl { get; set; }
        
        public string Title { get; set; }        
        public string Description { get; set; }

        // Author of the module
        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        
        [Display(Name = "Author URL")]
        public string AuthorUrl { get; set; }

        [Required]
        [Display(Name = "Author E-mail")]
        public string AuthorEmail { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Contact E-mail")]
        public string ContactEmail { get; set; }

        public List<string> Feedback { get; set; }

        public IEnumerable<SelectListItem> ModuleTypes 
        { 
            get
            {
                yield return new SelectListItem
                {
                    Selected = Type == "text/plain",
                    Text = "PSM1",
                    Value = "text/plain"
                };
                yield return new SelectListItem
                {
                    Selected = Type == "application/zip",
                    Text = "ZIP",
                    Value = "application/zip"
                };
            }
        }
        

        public SubmitModuleModel()
        {
            Feedback = new List<string>();
        }
    }
}