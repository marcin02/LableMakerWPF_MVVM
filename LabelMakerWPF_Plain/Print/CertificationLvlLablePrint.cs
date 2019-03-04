using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace LabelMakerWPF_Plain.Print
{
    public class CertificationLvlLablePrint : ConvertSettings
    {
        #region Constructor

        public CertificationLvlLablePrint(CertificationLevelPrintModel model)
        {
            this.PaperSize = (Dictionary<string, PaperSizeModel>)SettingFromString(PrinterSettingsModel.PaperSize);
            this.model = model;
            InitialSettingsAndPrint();
        }

        #endregion

        #region Object (model)

        CertificationLevelPrintModel model;
        DrawInfoModel drawModel = new DrawInfoModel();
        Dictionary<string, PaperSizeModel> PaperSize;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(PrinterSettingsModel.PrintSettings);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", PaperSize["80x50"].PrintWidth, PaperSize["80x50"].PrintHeight);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = drawModel.header.GetHeight();
            float bodyHeight = drawModel.body.GetHeight();
            float x = 0;
            float y = 0;

            string header = "flexlean sp. z o.o.";
            string line = $"Nośność poziomu: { model.LvlWeight } kg";

            PointF posF = graphics.MeasureString(header, drawModel.header).ToPointF();
            x = (graphics.VisibleClipBounds.Width - (posF.X - x)) / 2;
            y = (graphics.VisibleClipBounds.Height - ((headerHeight*2+bodyHeight) - y)) / 2;

            graphics.DrawString(header, drawModel.header, Brushes.Black, x, y);
            y += headerHeight;
            graphics.DrawString(line, drawModel.body, Brushes.Black, x, y + 2);
        }

        #endregion
    }
}
