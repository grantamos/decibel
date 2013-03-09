using Byteopia.Music.GoogleMusicAPI;
using Music8.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Data.Html;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Music8.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NowPlaying : Music8.Common.LayoutAwarePage
    {
        List<Uri> ImageList;
        public NowPlaying()
        {
            this.InitializeComponent();

            this.Loaded += NowPlaying_Loaded;

            PrettyBackground.ImageFadeInTime = TimeSpan.FromSeconds(2);
            PrettyBackground.ImagePanTime = TimeSpan.FromSeconds(3);
            PrettyBackground.WaitBeforePanBegin = TimeSpan.FromSeconds(2);
            PrettyBackground.ImageScale = 1.8;
            PrettyBackground.MaxImageOpacity = .8;
            PrettyBackground.ImageFadeOutTime = TimeSpan.FromSeconds(2);
        }

        void NowPlaying_Loaded(object sender, RoutedEventArgs e)
        {
            SetImage();
            GetBio();
        }

        private async System.Threading.Tasks.Task SetImage()
        {
            if (App.MusicLibrary.Queue.CurrentSong != null)
            {
                PrettyBackground.ImageList = await App.ZuneAPI.GetArtistImages(App.MusicLibrary.Queue.CurrentSong.Artist);
            }

            PrettyBackground.Prettyify();
        }

        private async void GetBio()
        {
            String bioStr = HtmlUtilities.ConvertToText(await App.LastfmAPI.GetArtistBio(App.MusicLibrary.Queue.CurrentSong.Artist));
            int endIndex = bioStr.IndexOf("Read more about");

            if (bioStr == "" || endIndex == -1)
                artistBio.Text = "couldn't find bio";
            else
                artistBio.Text = bioStr.Substring(0, endIndex);
        }
    }
}