using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Byteopia.Music.Lastfm
{
    public class Models
    {
        public enum ImageSize : int
        {
            small = 0,
            medium = 1,
            large = 2,
            extrallarge = 3,
            mega=4
        };

        [DataContract]
        public class AlbumImages
        {
            [DataMember(Name = "#text")]
            public String URL { get; set; }

            [DataMember(Name = "size")]
            public String Size { get; set; }
        }

        [DataContract]
        public class Wiki
        {
            [DataMember(Name = "summary")]
            public String Summary { get; set; }

            [DataMember(Name = "content")]
            public String Content { get; set; }
        }

        [DataContract]
        public class Bio : Wiki
        {
            [DataMember(Name = "yearformed")]
            public String YearFormed { get; set; }
        }

         [DataContract]
        public class Album
        {
            [DataMember(Name = "album")]
            public AlbumInfo Data { get; set; }
        }

         [DataContract]
         public class Artist
         {
             [DataMember(Name = "artist")]
             public ArtistInfo Data { get; set; }
         }


        [DataContract]
        public class AlbumInfo
        {
            [DataMember(Name = "name")]
            public String Name { get; set; }

            [DataMember(Name = "artist")]
            public String Artist { get; set; }

            [DataMember(Name = "image")]
            public List<AlbumImages> Images { get; set; }

            [DataMember(Name = "wiki")]
            public Wiki WikiData { get; set; }
            
        }

        [DataContract]
        public class Track
        {
            [DataMember(Name = "name")]
            public String Name { get; set; }

            [DataMember(Name = "duration")]
            public String Duration { get; set; }

            [DataMember(Name = "artist")]
            public Artist ArtistInfo { get; set; }

            [DataMember(Name = "album")]
            public Album AlbumInfo { get; set; }

            [DataMember(Name = "wiki")]
            public Wiki WikiData { get; set; }
        }

        [DataContract]
        public class ArtistInfo
        {
            [DataMember(Name = "name")]
            public String Name { get; set; }

            [DataMember(Name = "image")]
            public List<AlbumImages> Images { get; set; }

            [DataMember(Name = "bio")]
            public Bio Biography { get; set; }
        }
    }
}
