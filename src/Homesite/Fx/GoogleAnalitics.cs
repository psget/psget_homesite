/*
 * Bit modified code from https://gist.github.com/1972785 by Gregory Young
 */
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Homesite.Fx
{
    public class GoogleAnalytics
    {
        // Tracker version.
        private const string Version = "4.4sa";

        // Get a random number string.
        private static String GetRandomNumber()
        {
            var randomClass = new Random();
            return randomClass.Next(0x7fffffff).ToString();
        }

        // Make a tracking request to Google Analytics from this server.
        // Copies the headers from the original request to the new one.
        // If request containg utmdebug parameter, exceptions encountered
        // communicating with Google Analytics are thown.
        private static void SendRequestToGoogleAnalytics(string utmUrl)
        {
            try
            {
                var connection = WebRequest.Create(utmUrl);

                ((HttpWebRequest)connection).UserAgent = "";
                connection.Headers.Add("Accept-Language",
                                       "EN-US");

                using (connection.GetResponse())
                {
                    // Ignore response
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error contacting Google Analytics\r\n" + ex);
            }
        }

        // Track a page view, updates all the cookies and campaign tracker,
        // makes a server side request to Google Analytics and writes the transparent
        // gif byte data to the response.
        private static void TrackPageView(string path)
        {
            var utmGifLocation = "http://www.google-analytics.com/__utm.gif";
            var visitor = Guid.NewGuid().GetHashCode();

            // Construct the gif hit url.
            var utmUrl = utmGifLocation + "?" +
                            "utmwv=" + Version +
                            "&utmn=" + GetRandomNumber() +
                            "&utmhn=" + "psget.net" +
                            "&utmr=" + "-" +
                            "&utmp=" + path.Replace("/", "%2F") +
                            "&utmac=" + "UA-28961030-1" +
                            //"&utmcc=__utma%3D999.999.999.999.999.1%3B" +
                            "&utmcc=__utma%3D999.999.999." + visitor + "." + DateTime.Now.GetHashCode() + ".1%3B" +
                            "&utmvid=" + (visitor - DateTime.Now.GetHashCode());

            SendRequestToGoogleAnalytics(utmUrl);
        }        
        
        public static void SendEvent(string path)
        {
            Task.Factory.StartNew(() =>
            {
                TrackPageView(path);
            });
        }
    }
}
