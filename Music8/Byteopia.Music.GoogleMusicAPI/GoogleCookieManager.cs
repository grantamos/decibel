using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Byteopia.Music.GoogleMusicAPI
{
    class GoogleCookieManager
    {
        public static String URI = "https://play.google.com/music/";

        private CookieContainer cookieContainer;
        public GoogleCookieManager()
        {
            cookieContainer = new CookieContainer();
        }

        public void HandleResponse(HttpResponseMessage msg)
        {
            IEnumerable<String> cookies;
            if(msg.Headers.TryGetValues("Set-Cookie", out cookies))
            {
                foreach (String cookie in cookies)
                {
                    cookieContainer.SetCookies(new Uri(URI), cookie) ;
                }
            }
        }

        public void SetCookiesFromString(String str)
        {
            cookieContainer.SetCookies(new Uri(URI), str);
        }

        public String GetCookies()
        {
            return cookieContainer.GetCookieHeader(new Uri(URI));
        }

        public List<Cookie> GetCookiesList()
        {
            List<Cookie> cookies = new List<Cookie>();
            foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(URI)))
            {
                cookies.Add(cookie);
            }

            return cookies;
        }
    }
}
