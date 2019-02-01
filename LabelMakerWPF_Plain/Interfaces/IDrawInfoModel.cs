using System.Drawing;

namespace LabelMakerWPF_Plain.Interfaces
{
    public interface IDrawInfoModel
    {
        Font body { get; }
        Font box { get;}
        Font header { get; }
        Font header2 { get; }
        Pen p { get; }
    }
}