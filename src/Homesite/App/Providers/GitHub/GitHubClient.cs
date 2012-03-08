using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Homesite.App.Providers.GitHub
{
    public class GitHubClient
    {
        readonly HttpClient _client = new HttpClient();        
        readonly IEnumerable<MediaTypeFormatter> _formatters = new List<MediaTypeFormatter>
        {
            new JsonNetFormatter()
        };

        public Task<RepositoryInfo> GetRepositoryInfo(string repoUrl)
        {
            //https://api.github.com/repos/octocat/Hello-World
            if (!repoUrl.StartsWith("https://github.com/"))
            {
                throw new ArgumentException("You should provide GitHub home URL something like: https://github.com/chaliy/psget", "repoUrl");                
            }

            var apiRepoUrl = repoUrl
                .Replace("https://github.com/", "https://api.github.com/repos/")
                .Trim('/');
            var resp = _client.GetAsync(apiRepoUrl).Result;

            return resp.Content.ReadAsOrDefaultAsync<RepositoryInfo>(_formatters);
        }

        /// <summary>
        /// Formats requests for text/json and application/json using Json.Net.
        /// </summary>
        /// <remarks>
        /// Christian Weyer is the author of this MediaTypeProcessor.
        /// <see href="http://weblogs.thinktecture.com/cweyer/2010/12/using-jsonnet-as-a-default-serializer-in-wcf-httpwebrest-vnext.html"/>
        /// Daniel Cazzulino (kzu): 
        ///		- updated to support in a single processor both binary and text Json. 
        ///		- fixed to support query composition services properly.
        /// Darrel Miller
        ///     - Converted to Preview 4 MediaTypeFormatter
        /// </remarks>
        class JsonNetFormatter : JsonMediaTypeFormatter
        {
            public JsonNetFormatter()
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
                SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            }

            protected override Task<object> OnReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext)
            {
                return Task.Factory.StartNew(() =>
                {
                    var serializer = CreateSerializer();
                    var reader = new JsonTextReader(new StreamReader(stream));

                    var result = serializer.Deserialize(reader, type);

                    return result;
                });
            }

            protected override Task OnWriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext, TransportContext transportContext)
            {
                return Task.Factory.StartNew(() =>
                {
                    var serializer = CreateSerializer();
                    // NOTE: we don't dispose or close these as they would 
                    // close the stream, which is used by the rest of the pipeline.
                    var writer = new JsonTextWriter(new StreamWriter(stream));

                    serializer.Serialize(writer, value);
                    writer.Flush();
                });
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
}