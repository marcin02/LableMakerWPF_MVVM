using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LabelMakerWPF_Plain.Converters
{
    public class StringLenghtToPixels
    {
        public float Convert(object obj, string text)
        {
            TextBox textBox = new TextBox();
            textBox = (TextBox)obj;
            Rect rect = textBox.GetRectFromCharacterIndex(text.Length);

            double position = rect.Left;

            return (float)position;
        }
    }
}
