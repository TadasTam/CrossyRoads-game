using CrossyRoad2D.Client.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class VolumeDownCommand : ICommand
    {
        public bool Execute()
        {
            var oldVolume = (int)Math.Floor(Settings.getInstance().GetVolume());
            var newVolume = Math.Clamp((int)Math.Floor(Settings.getInstance().GetVolume() - 10.0), 0, 100);

            Settings.getInstance().SetVolume(newVolume);
            return Settings.getInstance().GetVolume() != oldVolume;
        }
    }
}
