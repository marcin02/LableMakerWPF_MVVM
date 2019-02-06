
using LabelMakerWPF_Plain.Interfaces;
using System.Drawing;


namespace LabelMakerWPF_Plain.Models
{
    public class DrawInfoModel : IDrawInfoModel
    {
        public Font header { get; private set; } 
        public Font header2 { get; private set; }
        public Font body { get; private set; }
        public Font bigLableBody { get; private set; }
        public Font box { get; private set; }
        public Pen p { get; private set; }

        public DrawInfoModel()
        {
            header = new Font("Verdana", 14, FontStyle.Bold);
            header2 = new Font("Arial", 12);
            body = new Font("Arial", 11);
            bigLableBody = new Font("Arial", 9);
            box = new Font("Arial", 26);
            p = new Pen(Color.Black);
        }
    }
}
