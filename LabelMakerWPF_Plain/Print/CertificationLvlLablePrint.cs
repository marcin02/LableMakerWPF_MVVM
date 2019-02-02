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
            this.model = model;
            InitialSettingsAndPrint();
        }

        #endregion

        #region Object (model)

        CertificationLevelPrintModel model;
        DrawInfoModel drawModel = new DrawInfoModel();

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            PrinterSettingsModel settings = new PrinterSettingsModel();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(settings.printerSettings);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 394, 197);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = drawModel.header.GetHeight();
            float bodyHeight = drawModel.body.GetHeight();
            float x = 20;
            float y = 80;

            graphics.DrawString("flexlean sp. z o.o.", drawModel.header, Brushes.Black, x, y);
            y += headerHeight;
            graphics.DrawString($"Nośność poziomu: { model.LvlWeight } kg", drawModel.body, Brushes.Black, x, y + 2);
        }

        #endregion
    }
}
