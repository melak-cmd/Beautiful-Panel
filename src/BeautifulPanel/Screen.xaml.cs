﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BeautifulPanel
{
    public partial class Screen
    {
        private readonly Random _random = new Random();
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
            NewMethod(LayerA, 10,1.0, 0.9);
            NewMethod(LayerB, 20,0.6, 0.5);
            NewMethod(LayerC, 30, 0.3, .2);

        }

        private void GenerateShape(Panel panel, int nb,double scale, double opacity)
        {
            int count;
            for (count = 0; count < nb; count++)
            {
                var flake = new Flake(_random.Next(0, 360), scale, opacity);
                Canvas.SetLeft(flake, _random.Next(0, Convert.ToInt32(_screenWidth)));
                Canvas.SetTop(flake, _random.Next(0, Convert.ToInt32(_screenHeight)));
                panel.Children.Add(flake);
            }
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

            BuildAnimation("LayerControls", _directionX * 1920 * positionX, _directionX * 1080 * positionY, StoryboardLayerControl);
            BuildAnimation("LayerA", _directionX * 1080.0 * positionX, _directionY * 900.0 * positionY, StoryboardLayerA);
            BuildAnimation("LayerB", _directionX * 700.0 * positionX, _directionY * 450.0 * positionY, StoryboardLayerB);
            BuildAnimation("LayerC", _directionX * 400.0 * positionX, _directionY * 300.0 * positionY, StoryboardLayerC);

            StoryboardLayerA.Begin();
            StoryboardLayerB.Begin();
            StoryboardLayerC.Begin();
            StoryboardLayerControl.Begin();
        }

        private void BuildAnimation(string targetName, double x, double y, Storyboard storyboard)
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

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GetSize();
            Populate();
        }
    }
}
