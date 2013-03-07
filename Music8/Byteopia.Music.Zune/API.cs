using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Byteopia.Music.Zune
{
    public class API
    {
        public async Task<List<Uri>> GetArtistImages(String artist){

            List<Uri> imgs = new List<Uri>();

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

            artistId = artistId.Replace("urn:uuid:", "");

            SyndicationFeed imageFeed = await client.RetrieveFeedAsync(new Uri(String.Format("http://catalog.zune.net/v3.2/en-US/music/artist/{0}/images", artistId)));

            foreach (var l in imageFeed.Items)
            {
                imgs.Add(new Uri(String.Format("http://image.catalog.zune.net/v3.2/en-US/image/{0}?width=1920&height=1080", l.Id.Replace("urn:uuid:", ""))));
            }

            return imgs;
        }
    }
}
