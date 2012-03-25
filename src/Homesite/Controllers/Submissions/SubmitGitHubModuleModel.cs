using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homesite.Controllers.Submissions
{
    public class SubmitGitHubModuleModel
    {
        [Required]        
        public string URL { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Contact E-mail")]
        public string ContactEmail { get; set; }

        public List<string> Feedback { get; set; }

        public SubmitGitHubModuleModel()
        {
            Feedback = new List<string>();
        }
    }
}