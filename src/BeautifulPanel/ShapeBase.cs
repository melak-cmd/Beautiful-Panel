using System.Windows.Controls;
using System.Windows.Media;

namespace BeautifulPanel
{
    public abstract class ShapeBase : Control
    {
        private readonly int _angle;
        private readonly double _scale;
        private readonly double _opacity;
        private RotateTransform _rotateTransform;
        private ScaleTransform _scaleTransform;

        protected ShapeBase(int angle, double scale, double opacity) : this()
        {
            _angle = angle;
            _scale = scale;
            _opacity = opacity;
        }

        private ShapeBase()
        {
            DefaultStyleKey = this.GetType();
        }

        public override void OnApplyTemplate()
        {
            _rotateTransform = GetTemplateChild("MyRotateTransform") as RotateTransform;
            _scaleTransform = GetTemplateChild("MyScaleTransform") as ScaleTransform;
            Transform(_angle, _scale);
            this.Opacity = _opacity;
            base.OnApplyTemplate();
        }

        private void Transform(int angle, double scale)
        {
            _rotateTransform.Angle = angle;
            _scaleTransform.ScaleX = _scale;
            _scaleTransform.ScaleY = _scale;
        }
    }
}