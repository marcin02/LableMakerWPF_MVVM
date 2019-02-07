using System.Collections.Generic;

namespace LabelMakerWPF_Plain.Models
{
    public class BigBoxPrintModel
    {
        public string Company  { get; set; }
        public string Order { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value.Insert(2,"-"); }
        }
        public string City { get; set; }
        public int Box0 { get; set; }
        public int Box1 { get; set; }
        public List<string> Items { get; set; }
        public List<int> Quantity { get; set; }
        public short Copies { get; set; }
    }
}
