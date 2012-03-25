using System.Collections.Generic;
using System.Linq;
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
                Submissions = _manager.QuerySubmissions()
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
                //if (!_manager.SubmitGitHubModule(model.URL, model.ContactName, model.ContactEmail, feedback))
                //{
                //    foreach (var item in feedback)
                //    {
                //        ModelState.AddModelError(String.Empty, item);
                //    }
                //}
                //else
                //{
                //    return RedirectToAction("Index");
                //}
            }            
            return View(model);
        }

        public ActionResult SubmitModule()
        {
            return View(new SubmitModuleModel
            {
            });
        }

        [HttpPost]
        public ActionResult SubmitModule(SubmitModuleModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new List<string>();
                var candidate = new SubmissionCandidate
                {
                    Title = model.Title,
                    Description = model.Description,
                    ProjectUrl = model.ProjectUrl,
                    ModuleID = model.ModuleID,
                    Content = new Content
                    {
                        Source = model.URL,
                        Type = model.Type
                    },
                    Author = new Contact
                    {
                        Name = model.AuthorName,
                        Email = model.AuthorEmail,
                        Uri = model.AuthorUrl
                    }
                };

                if (!_manager.SubmitModule(candidate, model.ContactName, model.ContactEmail, feedback))
                {
                    foreach (var item in feedback)
                    {
                        ModelState.AddModelError(String.Empty, item);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}