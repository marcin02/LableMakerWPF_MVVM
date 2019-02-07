using LabelMakerWPF_Plain.Converters;
using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Media;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class CustomViewModel : ObservableObject
    {
        #region Constructor

        public CustomViewModel()
        {
            ClearCommand = new RelayCommand(ClearTextBox);
            HorizontalAlignmentCommand = new RelayCommand(SetHorizontalContentAlignment);
            VerticalAlignmentCommand = new RelayCommand(SetVerticalContentAlignment);
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
        private string _verticalContentAlignment = "Top";
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
        public string VerticalContentAlignment
        { 
            get { return _verticalContentAlignment; }
            set { _verticalContentAlignment = value; OnPropertyChanged(nameof(VerticalContentAlignment)); }
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
        public RelayCommand HorizontalAlignmentCommand { get; private set; }        
        public RelayCommand VerticalAlignmentCommand { get; private set; }
        public RelayCommand FontWeightCommand { get; private set; }
        public RelayCommand FontStyleCommand { get; private set; }

        #endregion

        #region Methods

        private void AddToPrint()
        {
            
            CustomPrintModel model = new CustomPrintModel
            {
                Text = this._text,
                Copies = this._copies,
                FontType = this._selectedFont,
                FontSize = this._selectedFontSize,
                FontStyle = this._selectedFontStyle,
                FontWeight = this._selectedFontWeight,
                PaperHeight = this.PaperSize[_selectedPaperSize].PrintHeight,
                PaperWidth = this.PaperSize[_selectedPaperSize].PrintWidth,
                HorizontalAlignment = this._horizontalContentAlignment,
                VerticalAlignment = this._verticalContentAlignment
            };
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
            WidthTextBox = PaperSize[value].OnScreenWidth;
            HeightTextBox = PaperSize[value].OnScreenHeight;
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
            ConvertSettings cs = new ConvertSettings();
            PrinterSettingsModel settingsModel = new PrinterSettingsModel();
            this.PaperSize = (Dictionary<string, PaperSizeModel>)cs.SettingFromString(settingsModel.PaperSize);
        }

        private void Print(object obj)
        {
            Validation();
            if (!_error)
            {                          
                AddToPrint();
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

        private void SetHorizontalContentAlignment(object alignment)
        {
            HorizontalContentAlignment = (string)alignment;
        }

        private void SetVerticalContentAlignment(object obj)
        {
            if(_verticalContentAlignment == "Top")
            {
                VerticalContentAlignment = "Center";
            }
            else
            {
                VerticalContentAlignment = "Top";
            }
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
