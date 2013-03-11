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
        bool isFullScreen;

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
        }

        void NowPlaying_Loaded(object sender, RoutedEventArgs e)
        {
            Song = App.MusicLibrary.Queue.CurrentSong;

            isFullScreen = false;

            SetImage();

            GetBio();

            nowPlayingList.ItemsSource = App.MusicLibrary.Queue.Songs;
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
          /* String bio = HtmlUtilities.ConvertToText(await App.ZuneAPI.GetArtistBio(App.MusicLibrary.Queue.CurrentSong.Artist));
            if (!bio.Equals(String.Empty))
            {
                artistBio.Text = "blah";
            }
            else
            {
                
                artistBio.Text = "unable to locate a bio";
            } */
        }

        private void btnFullScreenToggle_Click(object sender, RoutedEventArgs e)
        {
            if (isFullScreen == false)
            {
                //btnFullScreenToggle.NormalStateImageUriSource = new Uri("ms-appx:///Assets/Icons/NowPlaying/normalscreen.png");
            }
            else
            {
                //btnFullScreenToggle.NormalStateImageUriSource = new Uri("ms-appx:///Assets/Icons/NowPlaying/fullscreen.png");
            }

            isFullScreen = !isFullScreen;

            if (isFullScreen)
            {
                this.SideGrid.Visibility = Visibility.Collapsed;
                //Grid.SetColumn(btnFullScreenToggle, 3);
            }
            else
            {
                this.SideGrid.Visibility = Visibility.Visible;
                //Grid.SetColumn(btnFullScreenToggle, 1);
            }

        }

        private void lwl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void ScrollViewer_Loaded_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}