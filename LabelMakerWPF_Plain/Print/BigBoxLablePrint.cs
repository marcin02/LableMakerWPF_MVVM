﻿using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace LabelMakerWPF_Plain.Print
{
    public class BigBoxLablePrint : ConvertSettings
    {
        #region Constructor

        public BigBoxLablePrint(BigBoxPrintModel model)
        {
            this.model = model;
            this.PaperSize = (Dictionary<string, PaperSizeModel>)SettingFromString(PrinterSettingsModel.PaperSize);
            InitialSettingsAndPrint();
        }

        #endregion

        #region Objects

        private BigBoxPrintModel model;
        DrawInfoModel drawModel = new DrawInfoModel();
        Dictionary<string, PaperSizeModel> PaperSize;

        #endregion

        #region Methods

        private void InitialSettingsAndPrint()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings = (System.Drawing.Printing.PrinterSettings)SettingFromString(PrinterSettingsModel.PrintSettings);
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", PaperSize["100x100"].PrintWidth, PaperSize["100x100"].PrintHeight);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.PrinterSettings.Copies = model.Copies;
            printDocument.Print();
        }

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font header = new Font("Verdana", 12, FontStyle.Bold);
            Font body = new Font("Arial", 10);
            Font reciver2 = new Font("Arial", 9);
            Font reciver = new Font("Arial", 8);
            float header2Height = header.GetHeight();
            float bodyHeight = body.GetHeight();
            float BigLableBody = drawModel.bigLableBody.GetHeight();
            float x = (graphics.VisibleClipBounds.Width - 367)/2; // First vertical line x position
            float x_1 = x + 170;  // Second vertical line x position
            float x_2 = x + 227; // Third vertical line x position
            float x_3 = x + 367; // Last vertical line x position
            float x_4 = x + 155;
            float Margin = 10;
            float y = 6;
            string date = DateTime.Now.ToShortDateString();

            graphics.DrawString(DateTime.Now.ToShortDateString(), reciver, Brushes.Black, x + 288, y);
            y += reciver.GetHeight();

            graphics.DrawLine(drawModel.p, x, y, x_3, y);
            graphics.DrawLine(drawModel.p, x, y, x, y+5);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y+5);
            y += 5;

            graphics.DrawLine(drawModel.p, x, y, x, y + header2Height);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + header2Height);
            graphics.DrawString("Nadawca: ", header, Brushes.Black, x+2, y);
            graphics.DrawString("Odbiorca: ", header, Brushes.Black, x_4, y);
            y += header2Height;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString("flexlean sp. z o.o.", reciver, Brushes.Black, x+Margin, y);
            graphics.DrawString(model.Company, drawModel.bigLableBody, Brushes.Black, x_4+Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString($"Ul. Brodzka 10c", reciver, Brushes.Black, x+Margin, y);
            graphics.DrawString($"Ul. {model.Street}", drawModel.bigLableBody, Brushes.Black, x_4+Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString($"54-103 Wrocław", reciver, Brushes.Black, x+Margin, y);
            graphics.DrawString($"{model.ZipCode} {model.City}", drawModel.bigLableBody, Brushes.Black, x_4+Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);
            graphics.DrawString($"Osoba kontaktowa:", reciver2, Brushes.Black, x+2, y);
            graphics.DrawString($"Osoba kontaktowa:", body, Brushes.Black, x_4, y);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString("Paweł Kardis", reciver, Brushes.Black, x+Margin, y);
            graphics.DrawString(model.ContactPerson, drawModel.bigLableBody, Brushes.Black, x_4+Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);
            graphics.DrawString($"Numer telefonu:", reciver2, Brushes.Black, x+2, y);
            graphics.DrawString($"Numer telefonu:", body, Brushes.Black, x_4, y);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString("535 250 700", reciver, Brushes.Black, x+Margin, y);
            graphics.DrawString(model.PhoneNumber, drawModel.bigLableBody, Brushes.Black, x_4+Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);
            graphics.DrawString("Numer zamówienia:", body, Brushes.Black, x_4, y);
            y += bodyHeight;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            graphics.DrawString(model.Order, drawModel.bigLableBody, Brushes.Black, x_4 + Margin, y);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x, y + BigLableBody);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + BigLableBody);
            y += BigLableBody;

            graphics.DrawLine(drawModel.p, x, y, x_3, y);
            graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_1, y, x_1, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_2, y, x_2, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);
            graphics.DrawLine(drawModel.p, x, y + bodyHeight, x_3, y + bodyHeight);

            graphics.DrawString(" Komponenty", body, Brushes.Black, x, y);
            graphics.DrawString(" Szt.", body, Brushes.Black, x_1, y);
            graphics.DrawString(" Numer paczki", body, Brushes.Black, x_2, y);

            y += bodyHeight;

            float yy = y;
            float lableNumberStartY = yy;

            for (int i = 0; i < model.Items.Count ; i++)
            {
                graphics.DrawLine(drawModel.p, x, y, x_2, y);
                graphics.DrawLine(drawModel.p, x, y, x, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_1, y, x_1, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_2, y, x_2, y + bodyHeight);
                graphics.DrawLine(drawModel.p, x_3, y, x_3, y + bodyHeight);              

                if (!string.IsNullOrEmpty(model.Items[i]))
                {
                    graphics.DrawString(model.Items[i], body, Brushes.Black, x + 5, yy);
                    graphics.DrawString(model.Quantity[i].ToString(), body, Brushes.Black, x + 175, yy);
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
                float lableNumberStartX = x_2;
                

                //Checking if string fit on a lable and changing until it does
                while (posX < 0)
                {
                    size--;
                    box = new Font(drawModel.box.FontFamily, size);
                    posF = graphics.MeasureString(lableNumber, box).ToPointF();
                    posX = ((x_3-x_2) - posF.X) / 2;
                    posY = ((y-lableNumberStartY) - posF.Y) / 2;
                }

                graphics.DrawString(lableNumber, box, Brushes.Black, lableNumberStartX + posX, lableNumberStartY + posY);
            }
        }

        #endregion
    }
}
