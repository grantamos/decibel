using Byteopia.Music.GoogleMusicAPI;
using Music8.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Music8.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NowPlaying : Music8.Common.LayoutAwarePage
    {
        List<Uri> ImageList;
        Random random;

        public NowPlaying()
        {
            this.InitializeComponent();

            this.Loaded += NowPlaying_Loaded;

            random = new Random(DateTime.Now.Millisecond);

            ImageFadeIn.Completed += ImageFadeIn_Completed;
            ImageZoomIn.Completed += ImageZoomIn_Completed;
         
            ImageFadeOut.Completed += ImageFadeOut_Completed;
        }

        void ImageFadeOut_Completed(object sender, object e)
        {
            scale.ScaleX = scale.ScaleY = 1;
            GetImage();
        }

        void ImageZoomIn_Completed(object sender, object e)
        {
            ImageFadeOut.BeginTime = TimeSpan.FromSeconds(0);
            ImageFadeOut.Begin();
        }

        void ImageFadeIn_Completed(object sender, object e)
        {
            ImageZoomIn.BeginTime = TimeSpan.FromSeconds(0);
            ImageZoomIn.Begin();
        }

        void NowPlaying_Loaded(object sender, RoutedEventArgs e)
        {
            nowPlayingList.ItemsSource = App.MusicLibrary.Queue.Songs;
            SetImage();
        }

        private async System.Threading.Tasks.Task SetImage()
        {
            if (App.MusicLibrary.Queue.CurrentSong != null)
            {
                ImageList = await App.ZuneAPI.GetArtistImages(App.MusicLibrary.Queue.CurrentSong.Artist);
                GetImage();
            }
        }

        private void GetImage()
        {
            if (ImageList.Count > 0)
            {
                BitmapImage img = (ImageList != null && ImageList.Count > 0) ? new BitmapImage(ImageList[random.Next(0, ImageList.Count)]) : null;
                if (img != null)
                    artistBackground.Source = img;
                else
                {

                }
               
                ImageFadeIn.BeginTime = TimeSpan.FromSeconds(0);
                ImageFadeIn.Begin();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ImageFadeIn.Begin();
            ImageZoomIn.Begin();
        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
