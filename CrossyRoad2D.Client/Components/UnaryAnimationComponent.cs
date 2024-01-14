using CrossyRoad2D.Client.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Components
{
    public class UnaryAnimationComponent
    {
        private double _lastAnimationStartTime { get; set; }
        private double _animationDuration { get; init; }
        private double _fastScaleTime { get; init; }
        private double _initialValue { get; init; }
        private double _maxValue { get; init; }

        public UnaryAnimationComponent(double animationDuration, double fastScaleTime, double initialValue, double maxValue)
        {
            _lastAnimationStartTime = -animationDuration;
            _animationDuration = animationDuration;
            _fastScaleTime = fastScaleTime;
            _initialValue = initialValue;
            _maxValue = maxValue;
        }

        public void StartAnimation()
        {
            _lastAnimationStartTime = TimeState.Instance.TimeSecondsSinceStart;
        }

        public double GetAnimatedValue()
        {
            double elapsedTime = TimeState.Instance.TimeSecondsSinceStart - _lastAnimationStartTime;

            if (elapsedTime >= _animationDuration)
            {
                return _initialValue;
            }

            if (elapsedTime < _fastScaleTime)
            {
                double progress = elapsedTime / _fastScaleTime;
                return _initialValue + progress * (_maxValue - _initialValue);
            }
            else
            {
                double progress = (elapsedTime - _fastScaleTime) / (_animationDuration - _fastScaleTime);
                return EaseOutQuad(progress, _maxValue, _initialValue - _maxValue, 1);
            }
        }

        private double EaseOutQuad(double t, double b, double c, double d)
        {
            t /= d;
            return -c * t * (t - 2) + b;
        }
    }
}
