using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Music8.Common
{
    static class AnimationLibrary
    {
        static public void AnimateOpacity(FrameworkElement element, double start, double end, double duration)
        {
            element.Opacity = 0;

            Duration animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            DoubleAnimation opacityAnimation = new DoubleAnimation();

            opacityAnimation.Duration = animationDuration;
            opacityAnimation.From = start;
            opacityAnimation.To = end;

            Storyboard sb = new Storyboard();
            sb.Duration = animationDuration;

            sb.Children.Add(opacityAnimation);

            Storyboard.SetTarget(opacityAnimation, element);

            // Set the X and Y properties of the Transform to be the target properties
            // of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(opacityAnimation, "Opacity");

            // Begin the animation.
            sb.Begin();
        }

        static public void AnimateOpacity(FrameworkElement element, double end, double duration)
        {
            Duration animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            DoubleAnimation opacityAnimation = new DoubleAnimation();

            opacityAnimation.Duration = animationDuration;
            opacityAnimation.From = element.Opacity;
            opacityAnimation.To = end;

            Storyboard sb = new Storyboard();
            sb.Duration = animationDuration;

            sb.Children.Add(opacityAnimation);

            Storyboard.SetTarget(opacityAnimation, element);

            // Set the X and Y properties of the Transform to be the target properties
            // of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(opacityAnimation, "Opacity");

            // Begin the animation.
            sb.Begin();
        }

        static public void AnimateX(FrameworkElement element, double amount, double duration)
        {
            Duration animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.EnableDependentAnimation = true;

            xAnimation.Duration = animationDuration;
            xAnimation.EasingFunction = new CubicEase();
            xAnimation.From = element.RenderTransformOrigin.X;
            xAnimation.To = element.RenderTransformOrigin.X  + amount;

            Storyboard sb = new Storyboard();
            sb.Duration = animationDuration;

            sb.Children.Add(xAnimation);

            Storyboard.SetTarget(xAnimation, element.RenderTransform);
            Storyboard.SetTargetProperty(xAnimation, "X");

            // Begin the animation.
            sb.Begin();
        }

        static public void AnimateWidth(FrameworkElement element, double end, double duration)
        {
            Duration animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.EnableDependentAnimation = true;

            widthAnimation.Duration = animationDuration;
            widthAnimation.From = element.Width;
            widthAnimation.To = end;

            Storyboard sb = new Storyboard();
            sb.Duration = animationDuration;

            sb.Children.Add(widthAnimation);

            Storyboard.SetTarget(widthAnimation, element);
            Storyboard.SetTargetProperty(widthAnimation, "Width");

            // Begin the animation.
            sb.Begin();
        }
    }
}
