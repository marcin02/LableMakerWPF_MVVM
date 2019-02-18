using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace LabelMakerWPF_Plain.Print
{
    public class SmallBoxLablePrint : ConvertSettings
    {
        #region Constructor

        public SmallBoxLablePrint(BoxPrintModel model)
        {
            settings = new PrinterSettingsModel();
            this.PaperSize = (Dictionary<string, PaperSizeModel>)SettingFromString(settings.PaperSize);
            this.model = model;
            InitialSettingsAndPrint();
        }

        #endregion

        #region Objects (model)

        BoxPrintModel model;
        DrawInfoModel drawModel = new DrawInfoModel();
        Dictionary<string, PaperSizeModel> PaperSize;
        PrinterSettingsModel settings;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(settings.PrintSettings);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", PaperSize["100x50"].PrintWidth, PaperSize["100x50"].PrintHeight);
 //           printDocument.DefaultPageSettings.Margins = new Margins(7, 7, 7, 7);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = drawModel.header.GetHeight();
            float bodyHeight = drawModel.body.GetHeight();
            float x = 6; // First vertical line x position
            float x_1 = x + 170;  // Second vertical line x position
            float x_2 = x + 227; // Third vertical line x position
            float x_3 = x + 367; // Last vertical line x position
            float y = 6;
            string date = DateTime.Now.ToShortDateString();

            graphics.DrawString("flexlean sp. z o.o.", drawModel.header, Brushes.Black, x, y);
            graphics.DrawString(date, drawModel.body, Brushes.Black, x + 288, y + 5);
            graphics.DrawString($"Firma: { model.Company }", drawModel.header2, Brushes.Black, x, y + headerHeight);
            y = y + headerHeight;
            graphics.DrawString($"Nr zamówienia: {  model.Order  }", drawModel.header2, Brushes.Black, x, y + bodyHeight);
            y = 68;

            graphics.DrawLine(drawModel.p, x, y, x_3, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_1, y, x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_2, y, x_2, y + bodyHeight); 
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight); 
            graphics.DrawLine(drawModel.p, x, y + bodyHeight, x_3, y + bodyHeight); 

            graphics.DrawString(" Komponenty", drawModel.body, Brushes.Black, x, y);
            graphics.DrawString(" Szt.", drawModel.body, Brushes.Black, x_1, y);
            graphics.DrawString(" Numer paczki", drawModel.body, Brushes.Black, x_2, y);

            y += bodyHeight;

            float yy = y;

            for (int i = 0; i < 6; i++)
            {
                graphics.DrawLine(drawModel.p, x, y, x_2, y);
                graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_1, y, x_1, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_2, y, x_2, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);

                string label = model.Items[i];

                if (!string.IsNullOrEmpty(label))
                {
                    graphics.DrawString(label, drawModel.body, Brushes.Black, x + 5, yy);
                    graphics.DrawString(model.Quantity[i].ToString(), drawModel.body, Brushes.Black, x + 175, yy);
                    yy += bodyHeight;
                }

                y += bodyHeight;
            }

            graphics.DrawLine(drawModel.p, x, y, x_3, y);

            if (model.Box0 != 0)
            {
                string lableNumber = $"{model.Box0} z { model.Box1}";
                Font box = drawModel.box;
                float size = drawModel.box.Size;
                PointF posF; 
                float posX = -1f;
                float posY = 0; 
                int lableNumberStartX = 243;
                float lableNumberStartY = 84.8652344f;

                //Checking if string fit on a lable and changing until it does
                while (posX < 0)
                {
                    size--;
                    box = new Font(drawModel.box.FontFamily, size);
                    posF = graphics.MeasureString(lableNumber, box).ToPointF();
                    posX = (140 - posF.X) / 2;
                    posY = (101.1914066f - posF.Y) / 2;
                }

                graphics.DrawString(lableNumber, box, Brushes.Black, lableNumberStartX + posX, lableNumberStartY + posY);
            }
        }       

        #endregion
    }
}
