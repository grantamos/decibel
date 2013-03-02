using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music8.Common
{
    public class GoogleMusicSongInstance
    {
        public GoogleMusicSong song { get; set; }

        public GoogleMusicSongInstance(GoogleMusicSong song)
        {
            this.song = song;
        }
    }
}
