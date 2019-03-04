using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Properties;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LabelMakerWPF_Plain.PrinterSettings
{
    public class PSettings : ConvertSettings
    {
        public PSettings()
        {
            if(Settings.Default.FirstRun!=true)LoadSettings();
            PrintDialog();
        }

        PrintDocument printDocument = new PrintDocument();
        PrintDialog printDialog = new PrintDialog();

        private void LoadSettings()
        {
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(Settings.Default.printerSettings);
        }

        private void PrintDialog()
        {           
            printDialog.PrinterSettings = printDocument.PrinterSettings;
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                Settings.Default.printerSettings = SettingToString(printDocument.PrinterSettings);
                if (Settings.Default.FirstRun == true) Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
            else if (Settings.Default.FirstRun == true && result != DialogResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }         
        }
    }
}
