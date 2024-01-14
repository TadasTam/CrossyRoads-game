using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class ExplosionXamlComponent : AnimationXamlComponent
    {
        public ExplosionXamlComponent() : base(
            animationSource: "explosion.png",
            frameWidth: 48,
            frameHeight: 48,
            animationDuration: 0.75,
            frameCount: 8,
            easingFactor: 2.5)
        {
        }
    }
}
