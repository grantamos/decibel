using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

using Byteopia.Helpers;

namespace Byteopia.Music.Lastfm
{
    public class API
    {
        HttpClient client;

        public static string API_KEY = "e1b2f6c44daea9036a81c6770df58cac";
        public static string ALBUM_BASE = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key={0}&artist={1}&album={2}&format=json";
        public static string ARTIST_BASE  = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={0}&api_key={1}&format=json";
        public API()
        {
            client = new HttpClient();
        }

        public async Task<T> GET<T>(Uri address)
        {
            return JSON.Deserialize<T>(await GET(address));
        }

        public async Task<String> GET(Uri address)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(address);
            return  await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<Byteopia.Music.Lastfm.Models.Album> GetAlbumInfo(String album, String artist)
        {
            return await GET<Byteopia.Music.Lastfm.Models.Album>(new Uri(String.Format(API.ALBUM_BASE, API_KEY, artist, album)));
        }

        public async Task<Byteopia.Music.Lastfm.Models.Artist> GetArtistInfo(String artist)
        {
            return await GET<Byteopia.Music.Lastfm.Models.Artist>(new Uri(String.Format(API.ARTIST_BASE, artist, API_KEY)));
        }

        public async Task<String> GetArtistImage(String artist, Models.ImageSize size)
        {
            Models.Artist art = await GetArtistInfo(artist);

            if (art != null)
            {
                if( (int) size <= art.Data.Images.Count )
                {
                    return art.Data.Images[(int)size].URL;
                }
            }

            return String.Empty;
        }
    }
}
