using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Client.Singletons;
using Microsoft.Win32;

namespace CrossyRoad2D.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Height = Settings.getInstance().getHeight();
            Width = Settings.getInstance().getWidth();

            KeyDown += Window_KeyDown;
            KeyUp += Window_KeyUp;

            Loaded += (s, e) =>
            {
                CompositionTarget.Rendering += OnRendering;
            };
        }

        private void OnRendering(object sender, EventArgs e)
        {
            KeyboardState.Instance.UpdateBeforeProcessing();
            NetworkState.Instance.UpdateBeforeProcessing();
            TimeState.Instance.UpdateBeforeProcessing();

            KeyboardController1.Instance.Update();
            KeyboardController2.Instance.Update();

            EntityCollection.Instance.FinishRemovingEntities();
            EntityCollection.Instance.UpdateEntitiesPrioritized();

            KeyboardState.Instance.UpdateAfterProcessing();
            NetworkState.Instance.UpdateAfterProcessing();
            Height = Settings.getInstance().getHeight();
            Width = Settings.getInstance().getHeight();

            DrawingCanvas.Children.Clear();
            DrawingCanvas.Background = new SolidColorBrush(Color.FromScRgb(1.0f, 0.447f, 0.549f, 0.412f));
            EntityCollection.Instance.RenderEntities(DrawingCanvas);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardState.Instance.UpdateKeyDown(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardState.Instance.UpdateKeyUp(e.Key);
        }
    }
}
