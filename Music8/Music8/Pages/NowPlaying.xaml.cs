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
        List<Byteopia.Music.Zune.Models.ZuneImage> ImageList;

        public GoogleMusicSong Song
        {
            get;
            set;
        }

        public NowPlaying()
        {
            this.InitializeComponent();

            this.Loaded += NowPlaying_Loaded;

            PrettyBackground.ImageFadeInTime = TimeSpan.FromSeconds(5);
            PrettyBackground.ImagePanTime = TimeSpan.FromSeconds(60);
            PrettyBackground.WaitBeforePanBegin = TimeSpan.FromSeconds(5);
            PrettyBackground.ImageScale = 1.8;
            PrettyBackground.MaxImageOpacity = .8;
            PrettyBackground.ImageFadeOutTime = TimeSpan.FromSeconds(5);

            Song = App.MusicLibrary.Queue.CurrentSong;
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
            String bio = HtmlUtilities.ConvertToText(await App.ZuneAPI.GetArtistBio(App.MusicLibrary.Queue.CurrentSong.Artist));
            if (!bio.Equals(String.Empty))
            {
                artistBio.Text = bio;
            }
            else
            {
                
                artistBio.Text = "unable to locate a bio";
            }
        }
    }
}