using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.PrinterSettings
{
    public class SetPaperSize : ConvertSettings
    {
        public SetPaperSize()
        {
            SetSize();
            Save();
        }

        List<PaperSizeModel> PaperSizeList = new List<PaperSizeModel>();
        Dictionary<string, PaperSizeModel> PaperSize = new Dictionary<string, PaperSizeModel>();

        private void SetSize()
        {
            PaperSizeList.Add(new PaperSizeModel { Height = 50, Width = 100 });
            PaperSizeList.Add(new PaperSizeModel { Height = 100, Width = 100 });
            PaperSizeList.Add(new PaperSizeModel { Height = 50, Width = 80 });

            foreach (PaperSizeModel item in PaperSizeList)
            {
                PaperSize.Add(item.Name, item);
            }
        }

        private void Save()
        {
            Settings.Default.PaperSize = SettingToString(PaperSize);
            Settings.Default.Save();
        }
    }
}
