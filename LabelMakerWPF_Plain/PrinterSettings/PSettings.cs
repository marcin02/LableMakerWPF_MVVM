using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Properties;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LabelMakerWPF_Plain.PrinterSettings
{
    public class PSettings : ConvertSettings
    {
        public PSettings()
        {
            printDocument = new PrintDocument();
            printDialog = new PrintDialog();
            LoadSettings();
            PrintDialog();
        }

        PrintDocument printDocument;
        PrintDialog printDialog;

        private void LoadSettings()
        {
            printDocument.PrinterSettings = SettingFromString();
        }

        private void PrintDialog()
        {           
            printDialog.PrinterSettings = printDocument.PrinterSettings;
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                Settings.Default.printerSettings = SettingToString(printDocument.PrinterSettings);
            }
        }
    }
}
