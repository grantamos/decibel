using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Byteopia.Music.GoogleMusicAPI;

namespace Music8.Common
{
    public class DistinctItemComparers
    {
        public class DistinctItemComparerSong : IEqualityComparer<GoogleMusicSong>
        {

            public bool Equals(GoogleMusicSong x, GoogleMusicSong y)
            {
                return x.Title == y.Title;
            }

            public int GetHashCode(GoogleMusicSong y)
            {
                return 1;
            }

        }

        public class DistinctItemComparerArtist : IEqualityComparer<GoogleMusicSong>
        {

            public bool Equals(GoogleMusicSong x, GoogleMusicSong y)
            {
                return x.ArtistNorm.Equals(y.ArtistNorm);
            }

            public int GetHashCode(GoogleMusicSong y)
            {
                return 1;
            }

        }

        public class DistinctItemComparerAlbum : IEqualityComparer<GoogleMusicSong>
        {

            public bool Equals(GoogleMusicSong x, GoogleMusicSong y)
            {
                return x.Album == y.Album;
            }

            public int GetHashCode(GoogleMusicSong y)
            {
                return 1;
            }
        }
    }
}
