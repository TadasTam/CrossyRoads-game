using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using CrossyRoad2D.Client.Extensions;
using System.Windows.Controls;
using System.Windows.Media;
using CrossyRoad2D.Client.Singletons;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public abstract class AnimationXamlComponent : XamlComponent
    {
        private string _animationSource;
        private int _frameWidth;
        private int _frameHeight;
        private double _animationDuration;
        private int _frameCount;
        private double _easingFactor;

        private int _currentFrame;
        private double _startSeconds;

        protected AnimationXamlComponent(string animationSource, 
            int frameWidth, int frameHeight, 
            double animationDuration, int frameCount, double easingFactor = 1.0) : base("animation.xaml")
        {
            _animationSource = animationSource;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _animationDuration = animationDuration;
            _frameCount = frameCount;
            _easingFactor = easingFactor;

            _currentFrame = 0;
            _startSeconds = TimeState.Instance.TimeSecondsSinceStart;
        }

        public override void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            ChangeFrameIfNeeded();
            base.Render(canvas, convertGridToRendered);
        }

        private void ChangeFrameIfNeeded()
        {
            double passedTime = TimeState.Instance.TimeSecondsSinceStart - _startSeconds;
            double progress = passedTime / _animationDuration;

            double easedProgress = 1 - Math.Pow(1 - progress, _easingFactor);
            easedProgress = Math.Min(Math.Max(easedProgress, 0), 1);

            int newFrame = (int)Math.Floor(easedProgress * _frameCount);
            if(newFrame >= _frameCount)
            {
                newFrame = 0;
                _startSeconds = TimeState.Instance.TimeSecondsSinceStart;
            }

            if(_currentFrame != newFrame)
            {
                _currentFrame = newFrame;

                BitmapImage spriteSheet = new BitmapImage(new Uri($"pack://application:,,,/Resources/{_animationSource}"));
                Int32Rect cropArea = new Int32Rect(_frameWidth * _currentFrame, 0, _frameWidth, _frameHeight);
                CroppedBitmap croppedBitmap = new CroppedBitmap(spriteSheet, cropArea);

                if (_uiElement is FrameworkElement frameworkElement)
                {
                    var image = frameworkElement.FindName("frameImage") as Image;
                    if (image != null)
                    {
                        image.Source = croppedBitmap;
                    }
                }
            }
        }
    }
}
