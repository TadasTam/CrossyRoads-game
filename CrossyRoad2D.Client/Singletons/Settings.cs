using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CrossyRoad2D.Client.Singletons
{
    public class Settings
    {
        private static Settings Instance { get; } = new Settings();
        private int Width { get; set; }
        private int Height { get; set; }
        private MediaPlayer _musicPlayer;

        private Settings()
        {
            Width = 1280;
            Height = 720;
            _musicPlayer = new MediaPlayer();
            _musicPlayer.Open(new Uri("waltz.mp3", UriKind.Relative));
            _musicPlayer.Volume = 1 / 100.0f;
            _musicPlayer.Play();
        }
        public static Settings getInstance()
        {
            Console.WriteLine("Returning instance");
            return Instance;
        }
        public int getHeight()
        {
            return this.Height;
        }
        public void setHeight(int newHeight)
        {
            this.Height = newHeight;
        }
        public int getWidth()
        {
            return this.Width;
        }
        public void setWidth(int newWidth)
        {
            this.Width = newWidth;
        }

        public void SetVolume(int volume)
        {
            _musicPlayer.Volume = volume / 100.0f;
            Console.WriteLine("Volume changed!");
        }

        public double GetVolume()
        {
            return _musicPlayer.Volume;
        }

        public void ChangeScreenSize()
        {
            int newWidth = 0;
            int newHeight = 0;

            if (Width == 1280)
            {
                newWidth = 640;
                newHeight = 360;
            }
            else if (Width == 640)
            {
                newWidth = 1600;
                newHeight = 1000;
            }
            else if (Width == 1600)
            {
                newWidth = 1280;
                newHeight = 720;
            }

            Width = newWidth; 
            Height = newHeight;
        }
    }
}
