using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Interfaces;
using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.Print
{
    public class SmallBoxLablePrint : ConvertSettings, IBoxDrawInfoModel, IBoxPrintModel
    {
        public SmallBoxLablePrint(BoxPrintModel model)
        {
            FillProperties(model);
            InitialSettingsAndPrint();
        }

        public Font body { get; private set; }
        public Font box { get; private set; }
        public Font header { get; private set; }
        public Font header2 { get; private set; }
        public Pen p { get; private set; }
        public string Company { get; private set; }
        public string Order { get; private set; }
        public int Box0 { get; private set; }
        public int Box1 { get; private set; }
        public List<string> Items { get; private set; }
        public List<int> Quantity { get; private set; }
        public Int16 Copies { get; private set; }

        private void FillProperties(BoxPrintModel model)
        {
            this.Company = model.Company;
            this.Order = model.Order;
            this.Box0 = model.Box0;
            this.Box1 = model.Box1;
            this.Items = model.Items;
            this.Quantity = model.Quantity;
            this.Copies = model.Copies;
            DrawInfoModel drawModel = new DrawInfoModel();
            body = drawModel.body;
            box = drawModel.box;
            header = drawModel.header;
            header2 = drawModel.header2;
            p = drawModel.p;
        }

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = SettingFromString();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 394, 197);
            printDocument.DefaultPageSettings.Margins = new Margins(7, 7, 7, 7);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.PrinterSettings.Copies = Copies;
            printDocument.Print();
        }

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float headerHeight = header.GetHeight();
            float bodyHeight = body.GetHeight();
            float x = 16;
            float y = 6;
            float nr = 0;
            string d = DateTime.Now.ToShortDateString();

            graphics.DrawString("flexlean sp. z o.o.", header, Brushes.Black, x, y);
            graphics.DrawString(d, body, Brushes.Black, x + 288, y + 5);
            graphics.DrawString("Firma: " + Company + "", header2, Brushes.Black, x, y + headerHeight);
            y = y + headerHeight;
            graphics.DrawString("Nr zamówienia: " + Order + "", header2, Brushes.Black, x, y + bodyHeight);
            y = 68;

            graphics.DrawLine(p, x, y, x + 367, y);
            graphics.DrawLine(p, x, y, x, y + bodyHeight);
            graphics.DrawLine(p, x + 170, y, x + 170, y + bodyHeight);
            graphics.DrawLine(p, x + 227, y, x + 227, y + bodyHeight);
            graphics.DrawLine(p, x + 367, y, x + 367, y + bodyHeight);
            graphics.DrawLine(p, x, y + bodyHeight, x + 367, y + bodyHeight);

            graphics.DrawString(" Komponenty", body, Brushes.Black, x, y);
            graphics.DrawString(" Szt.", body, Brushes.Black, x + 170, y);
            graphics.DrawString(" Numer paczki", body, Brushes.Black, x + 227, y);

            y += bodyHeight;
            nr = y + bodyHeight;
            float yy = y;

            for (int i = 0; i < 6; i++)
            {
                graphics.DrawLine(p, x, y, x + 227, y);
                graphics.DrawLine(p, x, y, x, y + bodyHeight);
                graphics.DrawLine(p, x + 170, y, x + 170, y + bodyHeight);
                graphics.DrawLine(p, x + 227, y, x + 227, y + bodyHeight);
                graphics.DrawLine(p, x + 367, y, x + 367, y + bodyHeight);

                string label = Items[i];

                if (!string.IsNullOrEmpty(label))
                {
                    graphics.DrawString(label, body, Brushes.Black, x + 5, yy);
                    graphics.DrawString(Quantity[i].ToString(), body, Brushes.Black, x + 175, yy);
                    yy += bodyHeight;
                }

                y += bodyHeight;
            }

            graphics.DrawLine(p, x, y, x + 367, y);

            if (Box0 != 0)
            {
                int Box0_Lenght = Box0.ToString().Length;
                int Box1_Lenght = Box1.ToString().Length;

                if (Box0_Lenght == 1 && Box1_Lenght == 1)
                {
                    graphics.DrawString(Box0 + " z " + Box1, box, Brushes.Black, x + 250, nr + 10);
                }
                if (Box0_Lenght == 1 && Box1_Lenght > 1)
                {
                    graphics.DrawString(Box0 + " z " + Box1, box, Brushes.Black, x + 240, nr + 10);
                }
                if (Box0_Lenght > 1 && Box1_Lenght > 1)
                {
                    graphics.DrawString(Box0 + " z " + Box1, box, Brushes.Black, x + 230, nr + 10);
                }
            }
        }
    }
}
