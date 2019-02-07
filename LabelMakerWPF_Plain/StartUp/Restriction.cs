using LabelMakerWPF_Plain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabelMakerWPF_Plain.StartUp
{
    public class Restriction
    {
        public void CheckRestriction()
        {
            DateTime dateTime = new DateTime(2019, 9, 1);
            if(DateTime.Now>dateTime)
            {
                MessagesModel messages = new MessagesModel();
                messages.TrialTimeUp();
                Application.Current.Shutdown();
            }
        }
    }
}
