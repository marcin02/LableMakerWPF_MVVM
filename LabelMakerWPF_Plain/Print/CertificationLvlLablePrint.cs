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
        #region Constructor

        public CertificationLvlLablePrint(CertificationLevelPrintModel model)
        {
            settings = new PrinterSettingsModel();
            this.PaperSize = (Dictionary<string, PaperSizeModel>)SettingFromString(settings.PaperSize);
            this.model = model;
            InitialSettingsAndPrint();
        }

        #endregion

        #region Object (model)

        CertificationLevelPrintModel model;
        DrawInfoModel drawModel = new DrawInfoModel();
        Dictionary<string, PaperSizeModel> PaperSize;
        PrinterSettingsModel settings;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(settings.PrintSettings);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", PaperSize["100x50"].PrintWidth, PaperSize["100x50"].PrintHeight);
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
