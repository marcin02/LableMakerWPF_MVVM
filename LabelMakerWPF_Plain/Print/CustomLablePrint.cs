using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace LabelMakerWPF_Plain.Print
{
    public class CustomLablePrint : ConvertSettings
    {
        #region Constructor

        public CustomLablePrint(CustomPrintModel model)
        {
            settings = new PrinterSettingsModel();
            this.model = model;           
            InitialSettingsAndPrint();
        }

        #endregion

        #region Object

        private CustomPrintModel model;
        private PrinterSettingsModel settings;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();            
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(settings.PrintSettings);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", model.PaperWidth, model.PaperHeight);
           // printDocument.DefaultPageSettings.Margins = new Margins(7, 7, 7, 7); 
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;          
            StringReader sr = new StringReader(model.Text);
            string line;
            float Y = 0;
            float interline = 0;
            float X = 5;

            while ((line = sr.ReadLine()) != null) //Breakes multiline string into lines
            {
                if (model.VerticalAlignment == "Center") Y = SetPositionVerticalCenter(graphics, line) + interline;
                if (model.HorizontalAlignment == "Center") X = SetPositionCenter(graphics, line);
                else if (model.HorizontalAlignment == "Right") X = SetPositionRight(graphics, line) - X;

                graphics.DrawString(line, model.Body, Brushes.Black, X, Y);
                interline += model.Body.GetHeight();
            }
        }

        private float SetPositionCenter(Graphics gr, string line)
        {
            PointF posF = gr.MeasureString(line, model.Body).ToPointF();              // Gets printed width of a string
            float pos = (gr.VisibleClipBounds.Width - posF.X) / 2;                    //Calculate X for center string position on page

            return pos;
        }

        private float SetPositionRight(Graphics gr, string line)
        {
            PointF posF = gr.MeasureString(line, model.Body).ToPointF();
            float pos = (gr.VisibleClipBounds.Width - posF.X);

            return pos;
        }

        private float SetPositionVerticalCenter(Graphics gr, string line)
        {
            PointF posF = gr.MeasureString(line, model.Body).ToPointF();
            float pos = (gr.VisibleClipBounds.Height - posF.Y)/2;                     //Calculate Y for center string position on page

            return pos;
        }
        #endregion
    }
}
