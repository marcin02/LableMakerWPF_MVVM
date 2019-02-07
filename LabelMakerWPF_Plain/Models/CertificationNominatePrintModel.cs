namespace LabelMakerWPF_Plain.Models
{
    public class CertificationNominatePrintModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int MaxWeight { get; set; }
        public int LvlWeight { get; set; }
        public int SelfWeight { get; set; }
        public short Copies { get; set; }
    }
}
