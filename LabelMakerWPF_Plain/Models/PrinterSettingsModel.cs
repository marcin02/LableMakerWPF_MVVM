using LabelMakerWPF_Plain.Properties;

namespace LabelMakerWPF_Plain.Models
{
    public static class PrinterSettingsModel
    {
        public static string PrintSettings { get; } =  Settings.Default.printerSettings;
        public static string PaperSize { get; } = Settings.Default.PaperSize;
    }
}
