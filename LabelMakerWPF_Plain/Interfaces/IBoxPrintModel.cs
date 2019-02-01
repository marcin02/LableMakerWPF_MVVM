using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelMakerWPF_Plain.Models;

namespace LabelMakerWPF_Plain.Interfaces
{
    public interface IBoxPrintModel
    {
        string Company { get; }
        string Order { get; }
        int Box0 { get; }
        int Box1 { get; }
        List<string> Items { get; }
        List<int> Quantity { get; }
        Int16 Copies { get; }
    }
}
