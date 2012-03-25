using Newtonsoft.Json;

namespace Homesite.App.Providers.GitHub
{
    public class UserInfo
    {
        /// <summary>
        /// "login": "octocat",
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        /// <summary>
        /// "id": 1,
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// "avatar_url": "https://github.com/images/error/octocat_happy.gif",
        /// </summary>
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// "gravatar_id": "somehexcode",
        /// </summary>
        [JsonProperty(PropertyName = "gravatar_id")]
        public string GravatarId { get; set; }
        
        /// <summary>
        /// "url": "https://api.github.com/users/octocat"
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// "name": "monalisa octocat"
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        /// <summary>
        /// "company": "GitHub"
        /// </summary>
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        /// <summary>
        /// "blog": "https://github.com/blog"
        /// </summary>
        [JsonProperty(PropertyName = "blog")]
        public string Blog { get; set; }

        /// <summary>
        /// "location": "San Francisco"
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// "email": "octocat@github.com"
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}