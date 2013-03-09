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
     

        public List<Uri> ImageList
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

        DispatcherTimer shapeTimer;

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

           
            shapeTimer = new DispatcherTimer();
            shapeTimer.Tick += shapeTimer_Tick;
            shapeTimer.Interval = TimeSpan.FromSeconds(1);
            
        }

        void shapeTimer_Tick(object sender, object e)
        {
            
        }

        public void Prettyify()
        {
            SetBackground();
            SetImage();
            StartShapes();
        }

        public void SetBackground()
        {
            BackgroundColor.Fill = new SolidColorBrush(GetRandomBackgroundColor());
        }

        public void SetImage()
        {
            if (ImageList.Count == 0)
                return;

            BitmapImage img = new BitmapImage(ImageList[random.Next(0, ImageList.Count)]);

            if (img != null)
            {
                ImageBrush b = new ImageBrush();
                
                b.ImageSource = img;
                CanvasBorder.Background= b;
            }

            ImageFadeInZoom();
        }

        public void StartShapes()
        {
            if (!shapeTimer.IsEnabled)
                shapeTimer.Start();
        }

        public void ImageFadeInZoom()
        {
            Storyboard storyboard = new Storyboard();

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = scaleTransform.ScaleY = 1;
            CanvasBorder.RenderTransform = scaleTransform;

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
  
            Storyboard.SetTarget(fadeInAnimation, CanvasBorder);
            Storyboard.SetTarget(zoomInAnimationX, scaleTransform);
            Storyboard.SetTarget(zoomInAnimationY, scaleTransform);

            storyboard.BeginTime = TimeSpan.Zero;
            storyboard.Duration = WaitBeforePanBegin + ImageFadeInTime + ImageFadeOutTime;

            storyboard.Completed += (o, e) =>
            {
                FadeOut(CanvasBorder);
            };

            storyboard.Begin();
        }

        public void FadeOut(Border img)
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
            Storyboard.SetTarget(fadeOutAnimation, CanvasBorder);


            storyboard.BeginTime = TimeSpan.Zero;
            storyboard.Duration = ImageFadeOutTime;

            storyboard.Completed += (o, e) =>
            {
                Prettyify();
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

        public void StartMouseEnterAnimation(Rectangle button)
        {
            
            button.Fill = new SolidColorBrush(Colors.Blue);

            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = scale.ScaleY = 1;

            button.RenderTransformOrigin = new Point((button.Width / 2) / 100, (button.Height / 2) / 100);
            button.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(10000);
            growAnimation.From = 1;
            growAnimation.To = 3;
            storyboard.Children.Add(growAnimation);


            DoubleAnimation growY = new DoubleAnimation();
            growY.Duration = TimeSpan.FromMilliseconds(10000);
            growY.From = 1;
            growY.To = 3;
            storyboard.Children.Add(growY);

            DoubleAnimation fade = new DoubleAnimation();
            fade.BeginTime = TimeSpan.FromMilliseconds(10000);
            fade.Duration = TimeSpan.FromMilliseconds(3000);
            fade.From = 1;
            fade.To = 0;
            storyboard.Children.Add(fade);

            Storyboard.SetTargetName(growAnimation, button.Name);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("ScaleTransform.ScaleX").Path);
            Storyboard.SetTargetProperty(growY, new PropertyPath("ScaleTransform.ScaleY").Path);
            Storyboard.SetTargetProperty(fade, new PropertyPath("Opacity").Path);
            Storyboard.SetTarget(growAnimation, scale);
            Storyboard.SetTarget(growY, scale);
            Storyboard.SetTarget(fade, button);

            //storyboard.Completed += storyboard_Completed;
            storyboard.BeginTime = TimeSpan.FromSeconds(0);
            storyboard.Begin();
        }
    }
}
