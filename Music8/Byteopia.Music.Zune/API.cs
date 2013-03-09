using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;
using Windows.Web.Syndication;

namespace Byteopia.Music.Zune
{
    public class API
    {
        HttpClient client;
        public API()
        {
            client = new HttpClient();
        }


        public async Task<String> GetArtistID(String artist)
        {
            SyndicationClient client = new SyndicationClient();

            SyndicationFeed artistFeed = await client.RetrieveFeedAsync(new Uri(String.Format("http://catalog.zune.net/v3.2/en-US/music/artist?q={0}&clientType=PC/Windows", artist)));

            String artistId = "";
            if (artistFeed != null)
            {
                if (artistFeed.Items.Count > 0)
                    artistId = artistFeed.Items[0].Id;
            }

            if (artistId == "")
                return null;

            return artistId.Replace("urn:uuid:", "");
        }

        public async Task<String> GetArtistBio(String artist)
        {
            String artistID = await GetArtistID(artist);

            if (artistID == String.Empty)
                return String.Empty;

            XmlDocument d = await XmlDocument.LoadFromUriAsync(new Uri(String.Format("http://catalog.zune.net/v3.2/en-US/music/artist/{0}/biography", artistID)));
            SyndicationItem item = new SyndicationItem();
            item.LoadFromXml(d);

            if (item.Content != null && !item.Content.NodeValue.Equals(String.Empty))
                return item.Content.NodeValue;

            return string.Empty;
        }


        public async Task<List<Byteopia.Music.Zune.Models.ZuneImage>> GetArtistImages(String artist)
        {

            List<Byteopia.Music.Zune.Models.ZuneImage> imgs = new List<Byteopia.Music.Zune.Models.ZuneImage>();

            String artistID = await GetArtistID(artist);

            if (artistID == String.Empty)
                return null;

            SyndicationClient client = new SyndicationClient();
            SyndicationFeed imageFeed = await client.RetrieveFeedAsync(new Uri(String.Format("http://catalog.zune.net/v3.2/en-US/music/artist/{0}/images", artistID)));

            foreach (var item in imageFeed.Items)
            {
                foreach (var instance in item.ElementExtensions)
                {
                    foreach (var imageInstance in instance.ElementExtensions)
                    {
                        if (imageInstance.ElementExtensions.Count < 2)
                            continue;

                        imgs.Add(new Byteopia.Music.Zune.Models.ZuneImage()
                        {
                            Url = imageInstance.ElementExtensions[1].NodeValue
                        });
                    }
                }
                
            }

            return imgs;
        }
    }
}
