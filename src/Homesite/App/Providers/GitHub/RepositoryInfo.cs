using Newtonsoft.Json;

namespace Homesite.App.Providers.GitHub
{
    public class RepositoryInfo
    {
        /// <summary>
        /// "url": "https://api.github.com/repos/octocat/Hello-World"
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// "html_url": "https://github.com/octocat/Hello-World",
        /// </summary>
        [JsonProperty(PropertyName = "html_url")]
        public string HtmlUrl { get; set; }  

        /// <summary>
        /// "owner": {
        ///     "login": "octocat",
        ///     "id": 1,
        ///     "avatar_url": "https://github.com/images/error/octocat_happy.gif",
        ///     "gravatar_id": "somehexcode",
        ///     "url": "https://api.github.com/users/octocat"
        /// },
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public UserInfo Owner { get; set; }  

        /// <summary>
        /// "name": "Hello-World",
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }  

        /// <summary>
        /// "description": "This your first repo!",
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// "homepage": "https://github.com",
        /// </summary>
        [JsonProperty(PropertyName = "homepage")]
        public string Homepage { get; set; }

        /// <summary>
        /// "private": false,
        /// </summary>
        [JsonProperty(PropertyName = "private")]
        public bool IsPrivate { get; set; }

        public RepositoryInfo()
        {
            Owner = new UserInfo();
        }
    }
}