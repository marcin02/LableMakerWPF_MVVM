using LabelMakerWPF_Plain.Interfaces;
using System.Collections.Generic;

namespace LabelMakerWPF_Plain.Models
{
    public class BoxPrintModel : IBoxPrintModel
    {
        public string Company { get; set; }
        public string Order { get; set; }
        public int Box0 { get; set; }
        public int Box1 { get; set; }
        public List<string> Items { get; set; }
        public List<int> Quantity { get; set; }
        public short Copies { get; set; }
    }
}
