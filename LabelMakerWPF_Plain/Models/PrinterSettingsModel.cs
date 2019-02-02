using LabelMakerWPF_Plain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Models
{
    public class PrinterSettingsModel
    {
        public string printerSettings { get; } =  Settings.Default.printerSettings;
    }
}
