using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace LabelMakerWPF_Plain.Controls
{
    [TemplatePart(Name = "PART_TextBoxNum", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_UpButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_DownButton", Type = typeof(RepeatButton))]
    public class NumericUpDown : Control
    {

        public TextBox TextBoxNum;
        protected RepeatButton UpButton;
        protected RepeatButton DownButton;

        private readonly RoutedUICommand _increaseValueCommand = new RoutedUICommand("increaseValue", nameof(UpButton), typeof(NumericUpDown));
        private readonly RoutedUICommand _decreaseValueCommand = new RoutedUICommand("decreaseValue", nameof(DownButton), typeof(NumericUpDown));

        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public NumericUpDown()
        {
            var textBox = GetTemplateChild("PART_TextBoxNum") as TextBox;
            TextBoxNum = textBox;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AttachToVisualTree();
            AttachCommand();
        }

        private void AttachToVisualTree()
        {
            AttachTextBox();
            AttachUpButton();
            AttachDownButton();
        }

        private void AttachUpButton()
        {
            var upButton = GetTemplateChild("PART_UpButton") as RepeatButton;

            if (upButton != null)
            {
                UpButton = upButton;
                UpButton.Focusable = false;
                UpButton.Command = _increaseValueCommand;
            }
        }

        private void AttachDownButton()
        {
            var downButton = GetTemplateChild("PART_DownButton") as RepeatButton;

            if(downButton != null)
            {
                DownButton = downButton;
                DownButton.Focusable = false;
                DownButton.Command = _decreaseValueCommand;
            }
        }

        private void AttachTextBox()
        {
            var textBox = GetTemplateChild("PART_TextBoxNum") as TextBox;

            if(textBox != null)
            {
                TextBoxNum = textBox;
                TextBoxNum.Text = "0";
                TextBoxNum.PreviewTextInput += TextBox_PreviewTextInput;
                TextBoxNum.LostFocus += TextBox_LostFocus;
                TextBoxNum.GotFocus += TextBoxNum_GotFocus;
                TextBoxNum.MaxLength = 5;               
            }
        } 

        private void TextBoxNum_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9]");
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxNum.Text))
            {
            Value = Convert.ToInt32(TextBoxNum.Text);
            }
        }

        private void AttachCommand()
        {
            CommandBindings.Add(new CommandBinding(_increaseValueCommand, (a, b) => IncreaseValue()));
            CommandBindings.Add(new CommandBinding(_decreaseValueCommand, (a, b) => DecreaseValue()));
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnValueChanged, CoerceValue));

        
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { 
            
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
         {
            var control = (NumericUpDown)d;
            var value = (int)baseValue;

            if (control.TextBoxNum != null)
            {
                UpdateValue(control, value);
            }
                         
            return value;
        }

        static void UpdateValue(NumericUpDown control, int value)
        {
            control.TextBoxNum.Text = value.ToString();
        }

        private void IncreaseValue()
        {
            if (!string.IsNullOrEmpty(TextBoxNum.Text))
            {
                int value = Convert.ToInt32(TextBoxNum.Text);

                value++;
                Value = value; 
            }
            else
            {
                return;
            }
        }
        
        private void DecreaseValue()
        {
            if (!string.IsNullOrEmpty(TextBoxNum.Text))
            {
                int value = Convert.ToInt32(TextBoxNum.Text);
                if (value > 0)
                {
                    value--;
                    Value = value;
                }
                else
                {
                    return;
                }           
            }
            else
            {
                return;
            }
        }
    }
}
