using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Interfaces;
using LabelMakerWPF_Plain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Print
{
    public class CertificationLvlLablePrint : ConvertSettings
    {
        #region

        public CertificationLvlLablePrint(CertificationLevelPrintModel model)
        {
            FillProperties(model);
            InitialSettingsAndPrint();
        }

        #endregion

        private short _copies { get; set; }
        private int _lvlWeight { get; set; }
        private Font body { get; set; }
        private Font header { get; set; }
        private Pen p { get; set; }

        #region Methods

        private void FillProperties(CertificationLevelPrintModel model)
        {
            _copies = model.Copies;
            _lvlWeight = model.LvlWeight;
            DrawInfoModel drawModel = new DrawInfoModel();
            this.body = drawModel.body;
            this.header = drawModel.header;
            this.p = drawModel.p;
        }

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = SettingFromString();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 394, 197);
            printDocument.PrinterSettings.Copies = _copies;
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = header.GetHeight();
            float bodyHeight = body.GetHeight();
            float x = 20;
            float y = 80;

            graphics.DrawString("flexlean sp. z o.o.", header, Brushes.Black, x, y);
            y += headerHeight;
            graphics.DrawString($"Nośność poziomu: { _lvlWeight } kg", body, Brushes.Black, x, y + 2);
        }

        #endregion
    }
}
