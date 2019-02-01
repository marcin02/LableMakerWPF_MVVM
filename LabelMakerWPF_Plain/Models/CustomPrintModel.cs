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
        //Need rework
        public string text { get; set; }
        public short copies { get; set; }
        public Font body { get; private set; }
        public string fontType { get; set; }
        public double fontSize { get; set; }
        public string fontStyle { get; set; }
        public string fontWeight { get; set; }
        public int paperHeight { get; set; }
        public int paperWidth { get; set; }
        public string horizontalAlignment { get; set; }
        public string verticalAlignment { get; set; }

        public void SetFont()
        {
            FontStyle style = FontStyle.Regular;
            FontStyle weight = FontStyle.Regular;
            Font font;
            if (fontStyle == "Italic") style = FontStyle.Italic;
            if (fontWeight == "Bold")weight = FontStyle.Bold;
            FontFamily fontFamily = new FontFamily(fontType);
            font = new Font(fontFamily, (float)fontSize, style | weight, GraphicsUnit.Pixel);
            body = font;                   
        }
    }
}
