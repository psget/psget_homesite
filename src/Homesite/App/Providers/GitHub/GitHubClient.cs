using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using Ionic.Zip;
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

        public UserInfo GetUserInfo(string userUrl)
        {
            if (!userUrl.StartsWith("https://api.github.com/users/"))
            {
                throw new ArgumentException("You should provide GitHub home URL something like: https://api.github.com/users/chaliy/", "userUrl");
            }

            return Get<UserInfo>(userUrl);
        }

        public ReadOnlyCollection<string> GetRepositoryFileNames(string repoHtmlUrl)
        {
            var downloadUrl = repoHtmlUrl + "/zipball/master";
            var downloadedData = new WebClient().DownloadData(downloadUrl);
            using (var zip = ZipFile.Read(new MemoryStream(downloadedData)))
            {
                return zip.EntryFileNames.ToList().AsReadOnly();
            }
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