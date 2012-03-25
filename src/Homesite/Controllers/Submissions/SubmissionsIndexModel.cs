using System.Collections.Generic;
using Homesite.App.Providers.Sumission;

namespace Homesite.Controllers.Submissions
{
    public class SubmissionsIndexModel
    {
        public IList<SubmissionDoc> Submissions { get; set; }

        public SubmissionsIndexModel()
        {
            Submissions = new List<SubmissionDoc>();
        }
    }
}