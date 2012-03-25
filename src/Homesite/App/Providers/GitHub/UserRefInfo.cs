using Newtonsoft.Json;

namespace Homesite.App.Providers.GitHub
{
    public class UserRefInfo
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
    }
}