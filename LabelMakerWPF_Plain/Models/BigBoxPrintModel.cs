using LabelMakerWPF_Plain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Models
{
    public class BigBoxPrintModel
    {
        public string Company  { get; set; }
        public string Order { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int Box0 { get; set; }
        public int Box1 { get; set; }
        public List<string> Items { get; set; }
        public List<int> Quantity { get; set; }
        public short Copies { get; set; }
    }
}
