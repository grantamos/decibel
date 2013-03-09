using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byteopia.Music.Zune
{
    public class Models
    {
        public class ZuneImage
        {
            public String Type
            {
                get;
                set;
            }
            public string Width { get; set; }

            public string Height { get; set; }

            public String Url { get; set; }

            public Uri Uri { get { return new Uri(Url); } }
        }
    }
}
