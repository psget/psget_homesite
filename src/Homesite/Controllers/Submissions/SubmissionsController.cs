using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Homesite.App.Providers.Sumission;
using System;

namespace Homesite.Controllers.Submissions
{
    public class SubmissionsController : Controller
    {
        readonly SubmissionManager _manager = new SubmissionManager();
        
        public ActionResult Index()
        {
            return View(new SubmissionsIndexModel
            {
                Submissions = _manager.QeurySubmissions()
            });
        }

        public ActionResult SubmitGitHubModule()
        {
            return View(new SubmitGitHubModuleModel
            {
            });
        }

        [HttpPost]
        public ActionResult SubmitGitHubModule(SubmitGitHubModuleModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new List<string>();
                if (!_manager.SubmitGitHubModule(model.URL, model.ContactName, model.ContactEmail, feedback))
                {
                    foreach (var item in feedback)
                    {
                        ModelState.AddModelError(String.Empty, item);
                    }
                }   
            }            
            return View(model);
        }
    }

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

    public class SubmissionsIndexModel
    {
        public IList<SubmissionDoc> Submissions { get; set; }

        public SubmissionsIndexModel()
        {
            Submissions = new List<SubmissionDoc>();
        }
    }
}