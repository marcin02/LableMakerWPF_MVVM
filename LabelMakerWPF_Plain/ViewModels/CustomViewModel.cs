using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class CustomViewModel : ObservableObject
    {
        #region Constructor

        public CustomViewModel()
        {
            ClearCommand = new RelayCommand(ClearTextBox);
            AlignmentCommand = new RelayCommand(SetContentAlignment);
            FontWeightCommand = new RelayCommand(SetFontWeight);
            FontStyleCommand = new RelayCommand(SetFontStyle);
            PrintCommand = new RelayCommand(Print);
            GetFonts();
            GetPaperSize();
            GetFontSize();
        }

        #endregion

        #region Private properties

        private short _copies = 1;
        private bool _error = false;
        private int _heightTextBox;
        private string _horizontalContentAlignment = "Left";
        private int _maxLines;
        private string _selectedFont;
        private double _selectedFontSize;
        private string _selectedFontStyle;
        private string _selectedFontWeight;
        private string _selectedPaperSize;
        private string _text;
        private int _widthTextBox;

        #endregion

        #region Public properties

        public short Copies
        {
            get { return _copies; }
            set { _copies = CheckIsNoZero(value); OnPropertyChanged(nameof(Copies)); }
        }
        public int HeightTextBox
        {
            get { return _heightTextBox; }
            set { _heightTextBox = value; OnPropertyChanged(nameof(HeightTextBox)); }
        }
        public string HorizontalContentAlignment
        {
            get { return _horizontalContentAlignment; }
            set { _horizontalContentAlignment = value; OnPropertyChanged(nameof(HorizontalContentAlignment));
            }
        }    
        public int MaxLines
        {
            get { return _maxLines; }
            set { _maxLines = value; OnPropertyChanged(nameof(MaxLines)); }
        }
        public string SelectedFont
        {
            get { return _selectedFont; }
            set { _selectedFont = value; OnPropertyChanged(nameof(SelectedFont)); }
        }
        public double SelectedFontSize
        {
            get { return _selectedFontSize; }
            set { _selectedFontSize = value; OnPropertyChanged(nameof(SelectedFontSize)); }
        }
        public string SelectedFontStyle
        {
            get { return _selectedFontStyle; }
            set { _selectedFontStyle = value; OnPropertyChanged(nameof(SelectedFontStyle)); }
        }
        public string SelectedFontWeight
        {
            get { return _selectedFontWeight; }
            set { _selectedFontWeight = value; OnPropertyChanged(nameof(SelectedFontWeight)); }
        }
        public string SelectedPaperSize
        {
            get { return _selectedPaperSize; }
            set { _selectedPaperSize = value; ChangePaperSize(value); }
        }
        public string Text
        {
            get { return _text; }
            set { _text = CheckIfFit(value); OnPropertyChanged(nameof(Text)); }
        }
        public int WidthTextBox
        {
            get { return _widthTextBox; }
            set { _widthTextBox = value; OnPropertyChanged(nameof(WidthTextBox)); }
        }

        #endregion

        #region Collections

        public List<string> Fonts { get; set; }
        public List<double> FontSize { get; set; } 
        public Dictionary<string, PaperSizeModel> PaperSize { get; set; }

        #endregion

        #region Commands

        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand AlignmentCommand { get; private set; }        
        public RelayCommand FontWeightCommand { get; private set; }
        public RelayCommand FontStyleCommand { get; private set; }

        #endregion

        #region Methods

        private void AddToPrint(object obj)
        {
            StringLenghtToPixels convert = new StringLenghtToPixels();
            CustomPrintModel model = new CustomPrintModel()
            {
                text = this._text,
                copies = this._copies,
                fontType = this._selectedFont,
                fontSize = this._selectedFontSize,
                fontStyle = this._selectedFontStyle,
                fontWeight = this._selectedFontWeight,
                paperHeight = this.PaperSize[_selectedPaperSize].Height,
                paperWidth = this.PaperSize[_selectedPaperSize].Width,
                alignment = this._horizontalContentAlignment
            };
            model.SetFont();
            CustomLablePrint print = new CustomLablePrint(model);
        }

        private void ClearTextBox(object obj)
        {
            Text = default(string);
            Copies = 1;
        }

        private void GetFonts()
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            System.Drawing.FontFamily[] fontFamily = fontCollection.Families;
            Fonts = new List<string>();

            foreach(System.Drawing.FontFamily font in fontFamily)
            {
               if(!string.IsNullOrEmpty(font.ToString())) Fonts.Add(font.Name);
            }
            Fonts.RemoveAt(0);
        }

        private void ChangePaperSize(string value)
        {
            WidthTextBox = PaperSize[value].Width;
            HeightTextBox = PaperSize[value].Height;
        }

        private void GetFontSize()
        {
            FontSize = new List<double>()
            {
                8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72
            };
        }

        private void GetPaperSize()
        {
            PaperSize = new Dictionary<string, PaperSizeModel>();

            PaperSize.Add("100x50", new PaperSizeModel { Width = 377, Height = 189 });
            PaperSize.Add("100x100", new PaperSizeModel { Width = 400, Height = 400 });
        }

        private void Print(object obj)
        {
            Validation();
            if (!_error)
            {                          
                AddToPrint(obj);
            }
        }

        private string CheckIfFit(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Font f = new Font(SelectedFont, (float)SelectedFontSize, GraphicsUnit.Inch);
                float fontHeight = f.GetHeight() / 100;
                float output = (float)_heightTextBox / fontHeight;
                int outputInt = (int)Math.Round(output);

                StringReader sr = new StringReader(text);
                string line;
                int lineCount = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    lineCount++;
                }

                if (outputInt < lineCount)
                {
                    SystemSounds.Exclamation.Play();
                    return _text;
                }

            }

            return text;
        }

        private short CheckIsNoZero(short value)
        {
            if (value >= 1)
            {
                return value;
            }
            else
            {
                value = 1;
                return value;
            }
        }

        private void SetContentAlignment(object alignment)
        {
            HorizontalContentAlignment = (string)alignment;
        }

        private void SetFontWeight(object weight)
        {
            if(SelectedFontWeight == "Bold")
            {
                SelectedFontWeight = "Normal";
            }
            else
            {
            SelectedFontWeight = "Bold";
            }
        }

        private void SetFontStyle(object style)
        {
            if(SelectedFontStyle == "Italic")
            {
                SelectedFontStyle = "Normal";
            }
            else
            {
            SelectedFontStyle = "Italic";
            }
        }

        private void Validation()
        {
            if(string.IsNullOrEmpty(_text))
            {
                _error = true;
                MessagesModel messages = new MessagesModel();
                messages.TextBoxError();
            }
            else
            {
                _error = false;
            }
        }

        #endregion
    }
}
