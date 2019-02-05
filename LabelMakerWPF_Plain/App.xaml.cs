using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace LabelMakerWPF_Plain
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Events

        private void ToggleButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            EnterAnimation(sender);
        }

        private void ToggleButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExitAnimation(sender);
        }

        #endregion

        #region Private propeties

        private float AnimationTime = 0.3f;

        #endregion

        #region Animation method

        private async Task AnimateUIButton(object sender, DoubleAnimation doubleAnimation, ColorAnimation colorAnimation)
        {
            ToggleButton btn = (ToggleButton)sender;
            Rectangle rct = (Rectangle)btn.Template.FindName("Rct", btn);
            Grid grid = (Grid)btn.Template.FindName("Border", btn);
            Storyboard animation = new Storyboard();
            Storyboard colorFade = new Storyboard();
            DoubleAnimation rectangleExpand = doubleAnimation;
            ColorAnimation color = colorAnimation;
            Storyboard.SetTargetName(rct, "Rct");
            Storyboard.SetTargetName(grid, "Border");
            Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
            Storyboard.SetTargetProperty(colorFade, new PropertyPath("Background.Color"));
            animation.Children.Add(rectangleExpand);
            colorFade.Children.Add(color);

            colorFade.Begin(grid);
            animation.Begin(rct);

            await Task.Delay((int)AnimationTime * 1000);
        }

        #endregion

        #region Animation helper methods

        private DoubleAnimation EnterDoubleAnimation()
        {
            DoubleAnimation enter = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(AnimationTime)),
                To = 10
            };

            return enter;
        }

        private DoubleAnimation ExitDoubleAnimation()
        {
            DoubleAnimation exit = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(AnimationTime)),
                To = 0
            };

            return exit;
        }

        private ColorAnimation EnterColorAnimation()
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF312E30");
            ColorAnimation colorAnimation = new ColorAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(AnimationTime)),
                To = color
            };

            return colorAnimation;
        }

        private ColorAnimation ExitColorAnimation()
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF292728");
            ColorAnimation colorAnimation = new ColorAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(AnimationTime)),
                To = color
            };

            return colorAnimation;
        }

        #endregion

        #region Event helper methods

        private async void EnterAnimation(object sender)
        {
            ToggleButton btn = (ToggleButton)sender;

            if (btn.IsChecked != true)
            {
                await AnimateUIButton(sender, EnterDoubleAnimation(), EnterColorAnimation());
            }
        }

        private async void ExitAnimation(object sender)
        {
            ToggleButton btn = (ToggleButton)sender;

            if (btn.IsChecked != true)
            {
                await AnimateUIButton(sender, ExitDoubleAnimation(), ExitColorAnimation());
            }
        }

        #endregion

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ExitAnimation(sender);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            btn.IsChecked = true;
        }
    }
}
