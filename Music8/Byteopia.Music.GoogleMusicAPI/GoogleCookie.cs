using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Byteopia.Music.GoogleMusicAPI
{
    public class GoogleCookie
    {
        private String _key, _value;

        public System.String Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public System.String Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private DateTime _expire;

        public DateTime Expire
        {
            get { return _expire; }
            set { _expire = value; }
        }

        private bool _secure;

        public bool Secure
        {
            get { return _secure; }
            set { _secure = value; }
        }

        public GoogleCookie()
        {
            Expire = DateTime.Now.AddDays(1.0);
        }

        public static List<GoogleCookie> Parse(HttpResponseMessage msg)
        {
            List<GoogleCookie> list = new List<GoogleCookie>();
            foreach (var header in msg.Headers)
            {
                if (header.Key.Equals("Set-Cookie"))
                {
                    foreach (var cookie in header.Value)
                    {
                        int i = cookie.IndexOf('=');
                        int j = cookie.IndexOf(';');

                        if (i == -1 || j == -1)
                            throw new Exception("Invalid cookie format");

                        String value = cookie.Substring(i + 1, j - i - 1);
                        String key = cookie.Substring(0, i);

                        list.Add(new GoogleCookie()
                        {
                            Key = key,
                            Value = value
                        });
                    }
                }
            }
            return list;
        }
    }
}