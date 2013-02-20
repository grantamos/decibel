using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

using Byteopia.Helpers;

namespace Byteopia.Music.GoogleMusicAPI
{
    /// <summary>
    /// Wraps an HttpClient for use with Google requests
    /// </summary>
    ///
    [DataContract]
    public class GoogleHTTP
    {
        /// <summary>
        /// The HttpClient
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// This is required for each and every HTTP request
        /// </summary>
        ///
        [DataMember(Name="AuthToken")]
        private String authroizationToken;
        public System.String AuthroizationToken
        {
            get { return authroizationToken; }
            set { authroizationToken = value; }
        }

        [DataMember(Name = "AuthTokenIssueDate")]
        private DateTime authTokenIssueDate;

        public DateTime AuthTokenIssueDate
        {
            get { return authTokenIssueDate; }
            set { authTokenIssueDate = value; }
        }

        /// <summary>
        /// This is used with HttpClient to store cookies
        /// </summary>
        private CookieContainer cookieContainer;

        /// <summary>
        /// The status code from the last POST\GET request
        /// </summary>
        private HttpStatusCode lastStatusCode;

        public System.Net.HttpStatusCode LastStatusCode
        {
            get { return lastStatusCode; }
            set { lastStatusCode = value; }
        }

        private List<GoogleCookie> _cookies;

        [DataMember(Name="Cookies")]
        public List<GoogleCookie> Cookies
        {
            get { return _cookies; }
            set { _cookies = value; }
        }

        

        public GoogleHTTP()
        {
            authroizationToken = String.Empty;

            cookieContainer = new CookieContainer();

            HttpClientHandler CookieHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = cookieContainer,
            };

            client = new HttpClient(CookieHandler as HttpMessageHandler);

            _cookies = new List<GoogleCookie>();
        }

        /// <summary>
        /// Set the auth token from the login data
        /// </summary>
        /// <param name="loginData"></param>
        public void SetAuthToken(String loginData)
        {
            string CountTemplate = @"Auth=(?<AUTH>(.*?))$";
            Regex CountRegex = new Regex(CountTemplate, RegexOptions.IgnoreCase);
            string auth = CountRegex.Match(loginData).Groups["AUTH"].ToString();
            authroizationToken = auth;

            this.AuthTokenIssueDate = DateTime.Now;
        }

        /// <summary>
        /// Generic POST method that deserializes its result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T> POST<T>(Uri address, HttpContent content = null)
        {
            return JSON.Deserialize<T>(await POST(address, content));
        }

        /// <summary>
        /// Generic GET method that deserializes its result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<T> GET<T>(Uri address)
        {
            return JSON.Deserialize<T>(await GET(address));
        }

        /// <summary>
        /// POST request
        /// </summary>
        /// <param name="address">end point</param>
        /// <param name="content">content</param>
        /// <returns></returns>
        public async Task<String> POST(Uri address, HttpContent content = null)
        {
            SetAuthHeader();
            RebuildCookieContainer();

            HttpResponseMessage responseMessage = await client.PostAsync(BuildGoogleRequest(address), content);

            LastStatusCode = responseMessage.StatusCode;

            CheckForCookies(responseMessage, address);
            CheckForUpdatedAuth(responseMessage);
           

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GETResp(Uri address)
        {
            SetAuthHeader();
            RebuildCookieContainer();

            HttpResponseMessage responseMessage = await client.GetAsync(BuildGoogleRequest(address));

            LastStatusCode = responseMessage.StatusCode;

            CheckForCookies(responseMessage, address);
            CheckForUpdatedAuth(responseMessage);

            return responseMessage;
        }
        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="address">endpoint</param>
        /// <returns></returns>
        public async Task<String> GET(Uri address)
        {
            SetAuthHeader();
            RebuildCookieContainer();

            HttpResponseMessage responseMessage = await client.GetAsync(BuildGoogleRequest(address));

            LastStatusCode = responseMessage.StatusCode;

            CheckForCookies(responseMessage, address);
            CheckForUpdatedAuth(responseMessage);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        private void RebuildCookieContainer()
        {
            foreach (GoogleCookie gc in _cookies)
                cookieContainer.Add(new Uri("https://play.google.com/music/play?u=0&songid=a45bc256-75fa-3025-9119-21ee49f84bff"), 
                    new Cookie(gc.Key, gc.Value));
        }
        /// <summary>
        /// Sets Google's auth header
        /// </summary>
        private void SetAuthHeader()
        {
            if (!authroizationToken.Equals(String.Empty))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(String.Format("GoogleLogin auth={0}", authroizationToken));
        }

        public bool CheckForUpdatedAuth(HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
            {
                if (header.Key.Equals("Update-Client-Auth"))
                {
                    foreach (var v in header.Value)
                    {
                        authroizationToken = v;
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Checks response message for set-cookie, parses it, and saves
        /// This will allow us to save cookie to disk and perhaps reduce requests
        /// </summary>
        /// <param name="responseMessage"></param>
        private void CheckForCookies(HttpResponseMessage responseMessage, Uri address)
        {
            List<GoogleCookie> cookies = GoogleCookie.Parse(responseMessage);
           // _cookies.AddRange(cookies);
            //return;
            foreach (GoogleCookie c in cookies)
            {
                GoogleCookie l = null;
                if ((l = LocalCookiesContains(c.Key)) != null)
                {
                    l.Value = c.Value;
                }
                else
                    _cookies.Add(c);
            }

            CookieCollection cc = cookieContainer.GetCookies(address);

            foreach (Cookie c in cc)
            {
                c.Expired = true;
                c.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                int k = 0;
            }

            int bp = 0;
        }

        public void AgeCookies(TimeSpan span)
        {
            foreach (GoogleCookie cook in _cookies)
                cook.Expire.Add(span);
        }

        private GoogleCookie LocalCookiesContains(String key)
        {
            foreach (GoogleCookie c in _cookies)
                if (c.Key.Equals(key))
                    return c;

            return null;
        }
        /// <summary>
        /// Append xt cookie value to each request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private Uri BuildGoogleRequest(Uri uri)
        {
            String xt = GetXtCookie();
            if (xt.Equals(String.Empty))
                return uri;

            if (uri.ToString().Contains("songid"))
                return uri;

            if (uri.ToString().StartsWith("https://play.google.com/music/listen"))
                return uri;

            if (uri.ToString().StartsWith("https://www.google.com/accounts/Logout"))
                return uri;

            return new Uri(uri.OriginalString + String.Format("?u=0&xt={0}", xt));
        }

        public String GetXtCookie()
        {
            // Get the last one
            String xt = "";
            foreach (GoogleCookie cook in Cookies)
                if (cook.Key.Equals("xt"))
                    xt = cook.Value;

            return xt;
        }
        public bool CookiesHaveExpired()
        {
            foreach (GoogleCookie cook in _cookies)
            {
                if (DateTime.Now > cook.Expire)
                    return true;
            }
            return false;
        }

        public void AddGoogleCookieToContainer(GoogleCookie cookie)
        {
            cookieContainer.Add(new Uri("http://play.google.com"), new Cookie(cookie.Key, cookie.Value));
        }
    }
}