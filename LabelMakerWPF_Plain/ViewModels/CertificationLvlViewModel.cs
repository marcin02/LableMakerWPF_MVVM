using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using System;
using System.ComponentModel;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class CertificationLvlViewModel : ObservableObject, IDataErrorInfo
    {
        #region Constructor

        public CertificationLvlViewModel()
        {
            PrintCommand = new RelayCommand(Print);
            ClearCommand = new RelayCommand(Clear);
        }

        #endregion

        #region Private Propeties

        private Int16 _copies = 1;
        private int _lvlWeight;
        private bool _canValidate = false;
        private bool _error = false;

        #endregion

        #region Public properites

        public Int16 Copies
        {
            get { return _copies; }
            set { _copies = CheckIsNoZero(Convert.ToInt16(value)); OnPropertyChanged(nameof(Copies)); }
        }
        public int LvlWeight
        {
            get { return _lvlWeight; }
            set { _lvlWeight = value; OnPropertyChanged(nameof(LvlWeight)); }
        }

        #endregion

        #region Commands

        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }        

        #endregion

        #region Methods

        private void AddToPrint()
        {
            CertificationLevelPrintModel model = new CertificationLevelPrintModel
            {
                Copies = _copies,
                LvlWeight = _lvlWeight
            };
            CertificationLvlLablePrint certificationLvlLablePrint = new CertificationLvlLablePrint(model);
        }

        private Int16 CheckIsNoZero(Int16 value)
        {
            if(value >= 1)
            {           
                return value;
            }
            else
            {
                value = 1;
                return value;
            }
        }

        private void Clear(object obj)
        {
            Copies = 1;
            LvlWeight = 0;
        }

        private void Print(object obj)
        {
            Validation();
            if(!_error)
            {
                AddToPrint();
            }
            else
            {
                MessagesModel messages = new MessagesModel();
                messages.BoxError();
                _error = false;                
            }         
        }

        #endregion

        #region Validation

        private void Validation()
        {
            _canValidate = true;
            OnPropertyChanged(nameof(LvlWeight));
            _canValidate = false;
        }
        public string Error { get { return null; } }
        public string this[string columnName]
        {
            get
            {
                string resultOK = null;

                if (_canValidate == true)
                {
                    string resultNOK = "Błąd";                    

                    if (_lvlWeight == 0)
                    {                        
                        _error = true;
                        return resultNOK;
                    }
                    else
                    {
                        return resultOK;
                    } 
                }

                return resultOK;
            }           
        }

        #endregion
    }
}



    

