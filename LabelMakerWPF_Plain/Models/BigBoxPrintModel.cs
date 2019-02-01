using LabelMakerWPF_Plain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Models
{
    public class BigBoxPrintModel : IBoxPrintModel
    {
        public string Company  { get; set; }
        public string Order { get; set; }
        public string contactPerson { get; set; }
        public string phoneNumber { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public int Box0 { get; set; }
        public int Box1 { get; set; }
        public List<string> Items { get; set; }
        public List<int> Quantity { get; set; }
        public short Copies { get; set; }
    }
}
