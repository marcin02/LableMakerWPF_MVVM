using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabelMakerWPF_Plain.Models
{
    public class CustomPrintModel
    {
        public string Text { get; set; }
        public short Copies { get; set; }
        public Font Body { get { return SetFont(); } }
        public string FontType { get; set; }
        public double FontSize { get; set; }
        public string FontStyle { get; set; }
        public string FontWeight { get; set; }
        public int PaperHeight { get; set; }
        public int PaperWidth { get; set; }
        public string HorizontalAlignment { get; set; }
        public string VerticalAlignment { get; set; }
        
        public Font SetFont()
        {
            FontStyle style = System.Drawing.FontStyle.Regular;
            FontStyle weight = System.Drawing.FontStyle.Regular;
            Font font;
            if (FontStyle == "Italic") style = System.Drawing.FontStyle.Italic;
            if (FontWeight == "Bold") weight = System.Drawing.FontStyle.Bold;
            FontFamily fontFamily = new FontFamily(FontType);
            font = new Font(fontFamily, (float)FontSize, style | weight, GraphicsUnit.Pixel);
            return font;                   
        }
    }
}
