using LabelMakerWPF_Plain.Properties;

namespace LabelMakerWPF_Plain.Models
{
    public class PrinterSettingsModel
    {
        public string PrintSettings { get; } =  Settings.Default.printerSettings;
        public string PaperSize { get; } = Settings.Default.PaperSize;
    }
}
