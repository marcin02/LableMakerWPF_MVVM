using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace LabelMakerWPF_Plain.Print
{
    public class CertificationNominateLablePrint : ConvertSettings
    {
        #region Constructor

        public CertificationNominateLablePrint(CertificationNominatePrintModel model)
        {
            this.PaperSize = (Dictionary<string, PaperSizeModel>)SettingFromString(PrinterSettingsModel.PaperSize);
            this.model = model;
            _lvlWeight = $"{model.LvlWeight.ToString()} kg";
            _selfWeight = $"{model.SelfWeight.ToString()} kg";
            _maxWeight = $"{model.MaxWeight.ToString()} kg";
            InitialSettingsAndPrint();
        }

        #endregion

        #region Objects (model)

        private CertificationNominatePrintModel model;
        private DrawInfoModel drawModel = new DrawInfoModel();
        private Dictionary<string, PaperSizeModel> PaperSize;

        #endregion

        #region Private propeties

        private string _lvlWeight;
        private string _selfWeight;
        private string _maxWeight;
        
        private float x_1 = 140; // Second vertical line
        private float x_2 = 290; // Whole width of a table
        private float y_1 = 107.01236f; //Whole height of a table
        private float x;
        private float y;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(PrinterSettingsModel.PrintSettings);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", PaperSize["80x50"].PrintWidth, PaperSize["80x50"].PrintHeight);
          //  printDocument.DefaultPageSettings.Margins = new Margins(7, 7, 7, 7);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = drawModel.header.GetHeight();
            float bodyHeight = drawModel.body.GetHeight();
            
            x = (graphics.VisibleClipBounds.Width - x_2) / 2; 
            y = (graphics.VisibleClipBounds.Height - y_1) / 2;

            graphics.DrawString("flexlean sp. z o.o.", drawModel.header, Brushes.Black, x - 2, y);
            y += headerHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + headerHeight);
            graphics.DrawLine(drawModel.p, x + x_1, y, x + x_1, y + headerHeight);
            graphics.DrawLine(drawModel.p, x + x_2, y, x + x_2, y + headerHeight);

            graphics.DrawString(" Nazwa konstrukcji", drawModel.body, Brushes.Black, x, y);
            DrawAndCenterString(graphics, model.Name, drawModel.body);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_1, y, x + x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_2, y, x + x_2, y + bodyHeight);

            graphics.DrawString(" Data powstania", drawModel.body, Brushes.Black, x, y);
            DrawAndCenterString(graphics, model.Date.ToShortDateString(), drawModel.body);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_1, y, x + x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_2, y, x + x_2, y + bodyHeight);

            graphics.DrawString(" Nośność poziomu", drawModel.body, Brushes.Black, x, y);
            DrawAndCenterString(graphics, _lvlWeight, drawModel.body);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_1, y, x + x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_2, y, x + x_2, y + bodyHeight);

            graphics.DrawString(" Nośność całkowita", drawModel.body, Brushes.Black, x, y);
            DrawAndCenterString(graphics, _maxWeight, drawModel.body);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_1, y, x + x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x + x_2, y, x + x_2, y + bodyHeight);

            graphics.DrawString(" Masa własna", drawModel.body, Brushes.Black, x, y);
            DrawAndCenterString(graphics, _selfWeight, drawModel.body);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x + x_2, y);

        }

        private void DrawAndCenterString(Graphics graphics, string line, Font font)
        {
            Font body = font;
            PointF posF = graphics.MeasureString(line, body).ToPointF();
            float distX = ((x_2 - x_1) - posF.X) / 2;
            float fixedY = 0;

            if (distX < 0)
            {
                float size = body.Size;
                size--;
                body = new Font(body.FontFamily, size);
                DrawAndCenterString(graphics, line, body);                
            }
                fixedY = (((y+drawModel.body.GetHeight())-y)-posF.Y) / 2;

            float posX = x + distX + x_1;
            if(distX > 0) graphics.DrawString(line, body, Brushes.Black, posX, y+fixedY);            
        }

        #endregion
    }
}
