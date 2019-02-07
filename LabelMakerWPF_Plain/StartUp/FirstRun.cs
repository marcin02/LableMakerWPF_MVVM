using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.PrinterSettings;
using LabelMakerWPF_Plain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.StartUp
{
    public class FirstRun
    {
        public void OnFirstRun()
        {
            if (Settings.Default.FirstRun == true)
            {
                SetPaperSize setPaperSize = new SetPaperSize();
                MessagesModel messages = new MessagesModel();
                messages.FirstRunInfo();
                Settings.Default.FirstRun = false;
                Settings.Default.Save(); 
            }
        }
    }
}
