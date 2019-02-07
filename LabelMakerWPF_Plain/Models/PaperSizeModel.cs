using System;

namespace LabelMakerWPF_Plain.Models
{
    [Serializable]
    public class PaperSizeModel
    {
        private const float MMtoPrintConverter = MMtoScreenConverter / 0.9568527918781726f;
        private const float MMtoScreenConverter = 9.6f / 2.54f;
        public string Name { get { return $"{Width.ToString()}x{Height.ToString()}"; } }
        public int Height { private get; set; }
        public int Width { private get; set; }
        public int PrintHeight { get { return (int)(Height * MMtoPrintConverter); } }
        public int PrintWidth { get { return (int)(Width * MMtoPrintConverter); } }
        public int OnScreenHeight { get {return (int)(Height * MMtoScreenConverter); } }
        public int OnScreenWidth { get {return (int)(Width * MMtoScreenConverter); } }
    }
}
