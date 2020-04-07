using System.Windows;
using System.Windows.Input;

namespace BeautifulPanel
{
    public partial class Screen
    {
        private int _directionX, _directionY;
        private double _screenHeight, _screenWidth;

        public Screen()
        {
            InitializeComponent();
        }


        private void GetSize()
        {
            _screenWidth = ActualWidth;
            _screenHeight = ActualHeight;
        }

        private void Populate()
        {
            LayerA.GenerateShape(10, 1.0, 0.9, _screenWidth, _screenHeight);
            LayerB.GenerateShape(20, 0.6, 0.5, _screenWidth, _screenHeight);
            LayerC.GenerateShape(30, 0.3, 0.2, _screenWidth, _screenHeight);
        }

        private void LayoutRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            _directionX = position.X >= 0.0 ? -1 : 1;
            _directionY = position.Y >= 0.0 ? -1 : 1;
        }

        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            var positionX = position.X / _screenWidth;
            var positionY = position.Y / _screenHeight;

            StoryboardLayerControl.BuildAnimation("LayerControls", _directionX * 1920 * positionX, _directionX * 1080 * positionY);
            StoryboardLayerA.BuildAnimation("LayerA", _directionX * 1080.0 * positionX, _directionY * 900.0 * positionY);
            StoryboardLayerB.BuildAnimation("LayerB", _directionX * 700.0 * positionX, _directionY * 450.0 * positionY);
            StoryboardLayerC.BuildAnimation("LayerC", _directionX * 400.0 * positionX, _directionY * 300.0 * positionY);

            StoryboardLayerA.Begin();
            StoryboardLayerB.Begin();
            StoryboardLayerC.Begin();
            StoryboardLayerControl.Begin();
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GetSize();
            Populate();
        }
    }
}
