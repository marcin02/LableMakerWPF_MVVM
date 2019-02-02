using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Models
{
    public class PaperSizeModel
    {
        private const float Converter = 0.9568527918781726f;
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int OnScreenHeight { get {return (int)(Height * Converter); } }
        public int OnScreenWidth { get {return (int)(Width * Converter); } }
    }
}
