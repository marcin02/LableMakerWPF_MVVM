using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class CertificationNominateViewModel : ObservableObject, IDataErrorInfo
    {
        #region Constructor

        public CertificationNominateViewModel()
        {
            PrintCommand = new RelayCommand(Print);
            ClearCommand = new RelayCommand(Clear);
        }

        #endregion

        #region Private propeties

        private short _copies = 1;
        private DateTime _date = DateTime.Now;
        private int _lvlWeight;
        private int _maxWeight;
        private string _name;
        private int _selfWeight;
        private bool _error = true;
        private bool _canValidate = false;

        #endregion

        #region Public propeties

        public short Copies
        {
            get { return _copies; }
            set { _copies = CheckIsNoZero(value); OnPropertyChanged(nameof(Copies)); }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = IsNotTheFuture(value); OnPropertyChanged(nameof(Date)); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public int SelfWeight
        {
            get { return _selfWeight; }
            set { _selfWeight = value; OnPropertyChanged(nameof(SelfWeight)); }
        }
        public int LvlWeight
        {
            get { return _lvlWeight; }
            set { _lvlWeight = value; OnPropertyChanged(nameof(LvlWeight)); }
        }
        public int MaxWeight
        {
            get { return _maxWeight; }
            set { _maxWeight = value; OnPropertyChanged(nameof(MaxWeight)); }
        }
        public string Error { get { return null; } }

        #endregion

        #region Collections

        private List<string> _validation;

        #endregion

        #region Commands

        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }

        #endregion

        #region Methods

        private void AddToPrint()
        {
            CertificationNominatePrintModel model = new CertificationNominatePrintModel
            {
                Copies = this._copies,
                Date = this._date.ToShortDateString(),
                Name = this._name,
                SelfWeight = this._selfWeight,
                LvlWeight = this._lvlWeight,
                MaxWeight = this._maxWeight
            };
            CertificationNominateLablePrint print = new CertificationNominateLablePrint(model);
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
        private DateTime IsNotTheFuture(DateTime value)
      {
            if(value > DateTime.Now || value==null)
            {
                return _date;
            }
            else
            {
                return value;
            }
        }
        private void Clear(object obj)
        {
            Copies = 1;
            LvlWeight = 0;
            MaxWeight = 0;
            Name = default(string);
            SelfWeight = 0;
            Date = DateTime.Now;
        }
        private void FillList()
        {
            _validation = new List<string>();

            _validation.Add(_lvlWeight.ToString());
            _validation.Add(_maxWeight.ToString());
            _validation.Add(_name.ToString());
            _validation.Add(_selfWeight.ToString());
        }
        private void Print(object obj)
        {
            Validate();
            if(!_error)
            {
                AddToPrint();
                _error = true;
            }
            else
            {
                MessagesModel messages = new MessagesModel();
                messages.QuantityError();
            }
        }
        private void RefreshPropeties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(MaxWeight));
            OnPropertyChanged(nameof(LvlWeight));
            OnPropertyChanged(nameof(SelfWeight));
        }
        private void Validate()
        {
            _canValidate = true;
            RefreshPropeties();
            _canValidate = false;
        }
        public string this[string columnName]
        {
            get
            {
                string resultOK = null;

                string resulNOK = "Błąd";
                if (_canValidate)
                {
                    switch (columnName)
                    {
                        case "LvlWeight":
                            if (_lvlWeight <= 0)
                            {
                                _error = true;
                                return resulNOK;
                            }
                            _error = false;
                            return resultOK;

                        case "SelfWeight":
                            if (_selfWeight <= 0)
                            {
                                _error = true;
                                return resulNOK;
                            }
                            _error = false;
                            return resultOK;

                        case "MaxWeight":

                            if (_maxWeight <= 0)
                            {
                                _error = true;
                                return resulNOK;
                            }
                            _error = false;
                            return resultOK;

                        case "Name":
                            if (string.IsNullOrWhiteSpace(_name))
                            {
                                _error = true;
                                return resulNOK;
                            }
                            _error = false;
                            return resultOK;
                    } 
                }

                return resultOK;
            }
        }

        #endregion
    }
}
