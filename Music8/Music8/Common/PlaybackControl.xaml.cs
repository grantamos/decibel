using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Music8.Common
{
    public sealed partial class PlaybackControl : UserControl
    {
        Image backgroundImage = null;

        public PlaybackControl()
        {
            this.InitializeComponent();
            this.mediaElement.CurrentStateChanged += mediaElement_CurrentStateChanged;
            songList.ItemsSource = App.GoogleAPI.Tracks;
            //SetBackground("http://blog.rhapsody.com/kse%20album%20art.jpg");
        }

        private void mediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if(mediaElement.CurrentState == MediaElementState.Paused)
                playButton.Style = (Style)Application.Current.Resources["MyPlayAppBarButtonStyle"];
            else
                playButton.Style = (Style)Application.Current.Resources["MyPauseAppBarButtonStyle"];
        }

        public MediaElement GetMediaElement()
        {
            return this.mediaElement;
        }

        public void Expand()
        {
            AnimationLibrary.AnimateOpacity(smallDetailsTitle, 0, 1);
            smallDetailsTitle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AnimationLibrary.AnimateOpacity(smallDetailsArtist, 0, 1);
            smallDetailsArtist.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Collapse()
        {
            AnimationLibrary.AnimateOpacity(smallDetailsTitle, 1, 1);
            smallDetailsTitle.Visibility = Windows.UI.Xaml.Visibility.Visible;
            AnimationLibrary.AnimateOpacity(smallDetailsArtist, 1, 1);
            smallDetailsArtist.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public void SetBackground(string url){
            if (backgroundImage != null)
                AnimationLibrary.AnimateOpacity(backgroundImage as FrameworkElement, .3, 0, 1);
            
            backgroundImage = new Image();
            backgroundImage.Stretch = Stretch.UniformToFill;

            BitmapImage src = new BitmapImage(new Uri(url));
            backgroundImage.Source = src;

            rootGrid.Children.Insert(0, backgroundImage);
            AnimationLibrary.AnimateOpacity(backgroundImage as FrameworkElement, 0, 0.3, 1);
        }

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                //App.SongQueue.Pause();
                playButton.Style = (Style)Application.Current.Resources["MyPlayAppBarButtonStyle"];
            }
            else
            {
               // bool didPlay = await App.SongQueue.Play();
                //if (didPlay)
                    //playButton.Style = (Style)Application.Current.Resources["MyPauseAppBarButtonStyle"];
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //App.SongQueue.NextSong();
        }

        private void Queue_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
