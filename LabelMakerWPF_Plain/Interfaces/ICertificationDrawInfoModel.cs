using System.Drawing;

namespace LabelMakerWPF_Plain.Interfaces
{
    public interface ICertificationDrawInfoModel : IDrawInfoModel
    {
        Font body { get; }
        Font header { get; }
        Pen p { get; }
    }
}