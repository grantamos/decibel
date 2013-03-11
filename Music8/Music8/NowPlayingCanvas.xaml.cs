using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Music8
{
    public sealed partial class NowPlayingCanvas : UserControl
    {
        Random random;
        ShapeManager shapes;

        public List<Byteopia.Music.Zune.Models.ZuneImage> ImageList
        {
            get;
            set;
        }

        public TimeSpan ImageFadeInTime
        {
            get;
            set;
        }

        public TimeSpan ImagePanTime
        {
            get;
            set;
        }

        public TimeSpan WaitBeforePanBegin
        {
            get;
            set;
        }

        public double ImageScale
        {
            get;
            set;
        }

        public double MaxImageOpacity
        {
            get;
            set;
        }

        public TimeSpan ImageFadeOutTime
        {
            get;
            set;

        }

        public Windows.UI.Xaml.Shapes.Path Mypath
        {
            get;
            set;
        }

        Color[,] AcceptablePallete = new Color[,]
        {
            {Color.FromArgb(0xff, 0xe1, 0x5b, 0x14), Color.FromArgb(0xff, 0xf5, 0xb8, 0x98)},
            {Color.FromArgb(0xFF, 0xfc, 0x01, 0x64), Color.FromArgb(0xFF,0xfe,0x97,0xc0)},
            {Color.FromArgb(0xFF,0x58,0x78,0xcd),Color.FromArgb(0xFF,0xcd,0xd7,0xf0)},
            {Color.FromArgb(0xFF,0xbc, 0x09,0x9e),Color.FromArgb( 0xFF,0xf7, 0x66,0xdf)},
            {Color.FromArgb(0xFF,0x94,0xc5,0x35),Color.FromArgb( 0xFF,0xd3,0xe8,0xaa)},
            {Color.FromArgb(0xFF,0xE2,0x68,0x72),Color.FromArgb( 0xFF,0xfa,0xe8,0xe9)},
            {Color.FromArgb( 0xFF,0xd5, 0x91, 0xfd),Color.FromArgb(0xFF,0xfa,0xe8,0xe9)}
        };

        public NowPlayingCanvas()
        {
            this.InitializeComponent();
            random = new Random(DateTime.Now.Millisecond);

            ImageFadeInTime = TimeSpan.FromSeconds(5);
            ImagePanTime = TimeSpan.FromSeconds(90);
            WaitBeforePanBegin = TimeSpan.FromSeconds(3);
            ImageScale = 1.8;
            MaxImageOpacity = .8;
            ImageFadeOutTime = TimeSpan.FromSeconds(5);

           

            //shapes.Good = Test;
            //shapes.Canvas = this;
            this.Loaded += NowPlayingCanvas_Loaded;
        }

        void NowPlayingCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            shapes = new ShapeManager();
            shapes.Container = ShapeContainer;
            shapes.ContainerSize = new Point(ShapeContainer.ActualWidth - (ShapeContainer.ActualWidth * .26), ShapeContainer.ActualHeight);
            shapes.Load();
        }

        public void Prettyify()
        {
            SetBackground();
            SetImage();
            StartShapes();
        }

        public void SetBackground()
        {
            BackgroundColor.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x77, 0x77, 0xd9));
        }

        public void SetImage()
        {
            if (ImageList.Count == 0)
                return;

            BitmapImage img = new BitmapImage(ImageList[random.Next(0, ImageList.Count)].Uri);

            if (img != null)
            {
                ImageBrush b = new ImageBrush();
                
                b.ImageSource = img;
                ImageContainer.Background= b;
            }

            ImageFadeInZoom();
        }

        public void StartShapes()
        {
            
        }

        public void ImageFadeInZoom()
        {
            Storyboard storyboard = new Storyboard();

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = scaleTransform.ScaleY = 1;
            ImageContainer.RenderTransform = scaleTransform;

            DoubleAnimationUsingKeyFrames fadeInAnimation = new DoubleAnimationUsingKeyFrames();
            fadeInAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
                Value = 0
            });
            fadeInAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(ImageFadeInTime),
                Value = MaxImageOpacity
            });

            DoubleAnimationUsingKeyFrames zoomInAnimationX = new DoubleAnimationUsingKeyFrames();
            zoomInAnimationX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
                Value = 1
            });
            zoomInAnimationX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin),
                Value = 1
            });
            zoomInAnimationX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin),
                Value = 1
            });
            zoomInAnimationX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin + ImagePanTime),
                Value = ImageScale
            });


            DoubleAnimationUsingKeyFrames zoomInAnimationY = new DoubleAnimationUsingKeyFrames();
            zoomInAnimationY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
                Value = 1
            });
            zoomInAnimationY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin),
                Value = 1
            });
            zoomInAnimationY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin),
                Value = 1
            });
            zoomInAnimationY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(WaitBeforePanBegin + ImagePanTime),
                Value = ImageScale
            });

       
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Children.Add(zoomInAnimationX);
            storyboard.Children.Add(zoomInAnimationY);
           

            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity").Path);
            Storyboard.SetTargetProperty(zoomInAnimationX, new PropertyPath("ScaleTransform.ScaleX").Path);
            Storyboard.SetTargetProperty(zoomInAnimationY, new PropertyPath("ScaleTransform.ScaleY").Path);

            Storyboard.SetTarget(fadeInAnimation, ImageContainer);
            Storyboard.SetTarget(zoomInAnimationX, scaleTransform);
            Storyboard.SetTarget(zoomInAnimationY, scaleTransform);

            storyboard.BeginTime = TimeSpan.Zero;
            storyboard.Duration = WaitBeforePanBegin + ImageFadeInTime + ImageFadeOutTime;

            storyboard.Completed += (o, e) =>
            {
                FadeOut(ImageContainer);
            };

            storyboard.Begin();
        }

        public void FadeOut(Grid img)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimationUsingKeyFrames fadeOutAnimation = new DoubleAnimationUsingKeyFrames();
            fadeOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
                Value = MaxImageOpacity
            });
            fadeOutAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(ImageFadeOutTime),
                Value = 0
            });

            storyboard.Children.Add(fadeOutAnimation);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity").Path);
            Storyboard.SetTarget(fadeOutAnimation, ImageContainer);


            storyboard.BeginTime = TimeSpan.Zero;
            storyboard.Duration = ImageFadeOutTime;

            storyboard.Completed += (o, e) =>
            {
                SetImage();
                //Prettyify();
            };

            storyboard.Begin();
        }

        private Color GetRandomForegroundColor()
        {
            return AcceptablePallete[random.Next(0, AcceptablePallete.Length / 2), 1];
        }
        private Color GetRandomBackgroundColor()
        {
            return AcceptablePallete[random.Next(0, AcceptablePallete.Length / 2), 0];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin();
        }
    }
}
