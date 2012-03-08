using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Homesite.App.Providers.GitHub
{
    public class GitHubClient : IGitHubClient
    {
        static  readonly JsonSerializer Serializer = CreateSerializer();
       
        public RepositoryInfo GetRepositoryInfo(string repoUrl)
        {
            if (!repoUrl.StartsWith("https://api.github.com/repos/"))
            {
                throw new ArgumentException("You should provide GitHub home URL something like: https://api.github.com/repos/chaliy/psget", "repoUrl");                
            }
            
            return Get<RepositoryInfo>(repoUrl);
        }

        private static T Get<T>(string url) where T : class 
        {
            var client = new WebClient();            

            try
            {
                var restult = client.DownloadString(url);
                return Serializer.Deserialize<T>(new JsonTextReader(new StringReader(restult)));
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
            
        }

        private static JsonSerializer CreateSerializer()
        {
            var serializer = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            serializer.Converters.Add(new StringEnumConverter());
            serializer.Converters.Add(new IsoDateTimeConverter());
            return serializer;
        }        
    }
}