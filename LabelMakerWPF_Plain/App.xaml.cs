using System;
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
            ToggleButton btn = (ToggleButton)sender;
            if (btn.IsChecked != true)
            {
                EnterAnimation(sender);
            }
        }

        private void ToggleButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            if (btn.IsChecked != true)
            {
                ExitAnimation(sender);
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            if (btn.IsChecked != true)
            {               
                ExitUncheckedColorAnimation(sender);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            if (btn.IsChecked == true)
            {
                EnterCheckColorAnimation(sender);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        #endregion

        #region Private propeties

        private float AnimationTime = 0.3f;

        #endregion

        #region Animation method

        private async Task AnimateUIButton(object sender, DoubleAnimation doubleAnimation, ColorAnimation colorAnimation, ColorAnimation colorAnimationRectangle = null)
        {
            ToggleButton btn = (ToggleButton)sender;
            Rectangle rct = (Rectangle)btn.Template.FindName("Rct", btn);
            Grid grid = (Grid)btn.Template.FindName("Border", btn);
            Storyboard animation = new Storyboard();
            Storyboard colorFade = new Storyboard();
            Storyboard colorFadeRectangle = new Storyboard();
            DoubleAnimation rectangleExpand = doubleAnimation;
            ColorAnimation color = colorAnimation;
            ColorAnimation color2 = default(ColorAnimation);
            if(colorAnimationRectangle != null) color2 = colorAnimationRectangle;
            Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));
            Storyboard.SetTargetProperty(colorFade, new PropertyPath("Background.Color"));
            if(colorAnimationRectangle != null)Storyboard.SetTargetProperty(color2, new PropertyPath("Fill.Color"));
            if (color2 != null)colorFadeRectangle.Children.Add(color2);
            animation.Children.Add(rectangleExpand);
            colorFade.Children.Add(color);
            
            if (color2 != null) colorFadeRectangle.Begin(rct);
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

        private ColorAnimation EnterColorAnimation(string c = "#FF312E30")
        {
            Color color = (Color)ColorConverter.ConvertFromString(c);
            ColorAnimation colorAnimation = new ColorAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(AnimationTime)),
                To = color
            };

            return colorAnimation;
        }

        private ColorAnimation ExitColorAnimation(string c = "#FF292728")
        {
            Color color = (Color)ColorConverter.ConvertFromString(c);
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
            await AnimateUIButton(sender, EnterDoubleAnimation(), EnterColorAnimation());  
        }

        private async void ExitAnimation(object sender)
        {
            await AnimateUIButton(sender, ExitDoubleAnimation(), ExitColorAnimation());
        }

        private async void EnterCheckColorAnimation(object sender)
        {
            await AnimateUIButton(sender, EnterDoubleAnimation(), EnterColorAnimation(), EnterColorAnimation("#E83C3C"));
        }

        private async void ExitUncheckedColorAnimation(object sender)
        {
            await AnimateUIButton(sender, ExitDoubleAnimation(), ExitColorAnimation(), ExitColorAnimation("RoyalBlue"));
        }

        #endregion

    }
}
