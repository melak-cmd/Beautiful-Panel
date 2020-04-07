using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BeautifulPanel
{
    public static class Utils
    {
        internal static void GenerateShape(this Panel panel, int nb, double scale, double opacity, double width, double height)
        {
            Random random = new Random();
            int count;
            for (count = 0; count < nb; count++)
            {
                ShapeBase shape;
                shape = new Flake(random.Next(0, 360), scale, opacity);
                Canvas.SetLeft(shape, random.Next(0, Convert.ToInt32(width)));
                Canvas.SetTop(shape, random.Next(0, Convert.ToInt32(height)));
                panel.Children.Add(shape);
            }
        }

        internal static void BuildAnimation(this Storyboard storyboard, string targetName, double x, double y)
        {
            DoubleAnimationUsingKeyFrames animation1, animation2;

            if (storyboard.Children.Any())
            {
                animation1 = storyboard.Children[0] as DoubleAnimationUsingKeyFrames;
                animation2 = storyboard.Children[1] as DoubleAnimationUsingKeyFrames;
                animation1.KeyFrames[0].Value = x;
                animation2.KeyFrames[0].Value = y;
            }
            else
            {
                animation1 = new DoubleAnimationUsingKeyFrames
                {
                    BeginTime = new TimeSpan(0, 0, 0)
                };
                animation2 = new DoubleAnimationUsingKeyFrames
                {
                    BeginTime = new TimeSpan(0, 0, 0)
                };

                Storyboard.SetTargetName(animation1, targetName);
                Storyboard.SetTargetName(animation2, targetName);

                Storyboard.SetTargetProperty(animation1,
                    new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
                Storyboard.SetTargetProperty(animation2,
                    new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));

                var translate1ControlX = new SplineDoubleKeyFrame
                {
                    Value = x,
                    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500)),
                    KeySpline = new KeySpline { ControlPoint1 = new Point(0, 0), ControlPoint2 = new Point(0.5, 1) }
                };

                var translate1ControlY = new SplineDoubleKeyFrame
                {
                    Value = y,
                    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500)),
                    KeySpline = new KeySpline { ControlPoint1 = new Point(0, 0), ControlPoint2 = new Point(0.5, 1) }
                };

                animation1.KeyFrames.Add(translate1ControlX);
                animation2.KeyFrames.Add(translate1ControlY);

                storyboard.Children.Add(animation1);
                storyboard.Children.Add(animation2);
            }
        }
    }
}
