using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using LabelMakerWPF_Plain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace LabelMakerWPF_Plain.ViewModels
{
    public class BoxViewModel : ObservableObject, IDataErrorInfo
    {
        #region Constructor

        public BoxViewModel()
        {           
            PrintCommand = new RelayCommand(Print);
            ClearCommand = new RelayCommand(Clear);
            NextCommand = new RelayCommand(Next);
        }

        #endregion

        #region Private properties

        private string _company;
        private string _order;
        private int _box_0 = 1;
        private int _box_1 = 1;
        private string _item_0;
        private string _item_1;
        private string _item_2;
        private string _item_3;
        private string _item_4;
        private string _item_5;
        private int _qnt_0 = 0;
        private int _qnt_1 = 0;
        private int _qnt_2 = 0;
        private int _qnt_3 = 0;
        private int _qnt_4 = 0;
        private int _qnt_5 = 0;
        private short _copies = 1;
        private bool _canValidate = false;
        private bool _error = false;
        private bool _checkBoxIsChecked = true;
        private bool _printed = false;
        private bool _canFocus = false;

        #endregion

        #region Public properties

        public string Company
        {
            get { return _company; }
            set { _company = value; OnPropertyChanged(nameof(Company)); }
        }
        public string Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(nameof(Order)); }
        }
        public int Box_0
        {
            get { return _box_0; }
            set
            { _box_0 = CheckBoxValue(value);
              OnPropertyChanged(nameof(Box_0));
            }
        }
        public int Box_1
        {
            get { return _box_1; }
            set
            {
                _box_1 = CheckBoxValueNotZero(value);
                OnPropertyChanged(nameof(Box_1));
            }
        }
        public string Item_0
        {
            get { return _item_0; }
            set
            { _item_0 = value;
              OnPropertyChanged(nameof(Item_0));                        
            }
        }
        public string Item_1
        {
            get { return _item_1; }
            set { _item_1 = value; OnPropertyChanged(nameof(Item_1)); }
        }
        public string Item_2
        {
            get { return _item_2; }
            set { _item_2 = value; OnPropertyChanged(nameof(Item_2)); }
        }
        public string Item_3
        {
            get { return _item_3; }
            set { _item_3 = value; OnPropertyChanged(nameof(Item_3)); }
        }
        public string Item_4
        {
            get { return _item_4; }
            set { _item_4 = value; OnPropertyChanged(nameof(Item_4)); }
        }
        public string Item_5
        {
            get { return _item_5; }
            set { _item_5 = value; OnPropertyChanged(nameof(Item_5)); }
        }
        public int Qnt_0
        {
            get { return _qnt_0; }
            set { _qnt_0 = value; OnPropertyChanged(nameof(Qnt_0)); }
        }
        public int Qnt_1
        {
            get { return _qnt_1; }
            set { _qnt_1 = value; OnPropertyChanged(nameof(Qnt_1)); }
        }
        public int Qnt_2
        {
            get { return _qnt_2; }
            set { _qnt_2 = value; OnPropertyChanged(nameof(Qnt_2)); }
        }                
        public int Qnt_3
        {
            get { return _qnt_3; }
            set { _qnt_3 = value; OnPropertyChanged(nameof(Qnt_3)); }
        }
        public int Qnt_4
        {
            get { return _qnt_4;; }
            set { _qnt_4 = value; OnPropertyChanged(nameof(Qnt_4)); }
        }
        public int Qnt_5
        {
            get { return _qnt_5; }
            set { _qnt_5 = value; OnPropertyChanged(nameof(Qnt_5)); }
        }
        public Int16 Copies
        {
            get { return _copies; }
            set { _copies = CheckIsNoZero(Convert.ToInt16(value)); OnPropertyChanged(nameof(Copies)); }
        }      
        public bool CheckBoxIsChecked
        {
            get { return _checkBoxIsChecked; }
            set { _checkBoxIsChecked = value; }
            
        }
        public bool CanFocus
        {
            get { return _canFocus; }
            set { _canFocus = value; OnPropertyChanged(nameof(CanFocus)); }
        }

        #endregion

        #region Collections

        private List<string> _items;
        private List<int> _quantity;
        private Dictionary<string, BoxValidationModel> _validationDictionary;

        #endregion

        #region Commands

        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }

        #endregion

        #region Methods

        private void AddToPrint()
        {
            BoxPrintModel bpm = FillModel();
            SmallBoxLablePrint sblp = new SmallBoxLablePrint(bpm);
        }

        private bool CheckCanPrint(bool value)
        {
            if(_error)
            {
                value = false;
                return value;
            }
            else
            {
                return value;
            }
        }

        private void Clear(object obj)
        {
            Company = default(string);
            Order = default(string);
            Box_0 = 1;
            Box_1 = 1;
            Item_0 = default(string);
            Item_1 = default(string);
            Item_2 = default(string);
            Item_3 = default(string);
            Item_4 = default(string);
            Item_5 = default(string);
            Qnt_0 = 0; 
            Qnt_1 = 0;
            Qnt_2 = 0;
            Qnt_3 = 0;
            Qnt_4 = 0;
            Qnt_5 = 0;
            Copies = 1;
        }

        private void Next(object obj)
        {
            MessagesModel message = new MessagesModel();
            if (_printed == false) message.PrintError();
            else if(Box_0+1 > Box_1) message.BoxError();
            else if (_printed == true && Box_0+1 <= Box_1)
            {
                NextHelper();
            } 
        }

        private void NextHelper()
        {
            Box_0++;
            Item_0 = default(string);
            Item_1 = default(string);
            Item_2 = default(string);
            Item_3 = default(string);
            Item_4 = default(string);
            Item_5 = default(string);
            Qnt_0 = 0;
            Qnt_1 = 0;
            Qnt_2 = 0;
            Qnt_3 = 0;
            Qnt_4 = 0;
            Qnt_5 = 0;
            Copies = 1;
            _printed = false;
            CanFocus = true;
            CanFocus = false;
        }

        private void RefreshProperty()
        {
            OnPropertyChanged(nameof(Company));
            OnPropertyChanged(nameof(Item_0));
            OnPropertyChanged(nameof(Item_1));
            OnPropertyChanged(nameof(Item_2));
            OnPropertyChanged(nameof(Item_3));
            OnPropertyChanged(nameof(Item_4));
            OnPropertyChanged(nameof(Item_5));
            OnPropertyChanged(nameof(Qnt_0));
            OnPropertyChanged(nameof(Qnt_1));
            OnPropertyChanged(nameof(Qnt_2));
            OnPropertyChanged(nameof(Qnt_3));
            OnPropertyChanged(nameof(Qnt_4));
            OnPropertyChanged(nameof(Qnt_5));
        }
        
        private void Print(object obj)
        {            
            Validate();
            if (!_error)
            {
                AddToPrint();
                _printed = true;              
            }
            else
            {
                MessagesModel message = new MessagesModel();
                message.QuantityError();
                _error = false;
            }
        }

        //Validate UI

        public string Error { get { return null; } }

        public string this[string columnName]
        {
            get
            {
                string resultOK = null;

                BoxValidation vm = new BoxValidation();

                if (_canValidate)
                {
                    string result = vm.Validation(_validationDictionary, columnName);
                    if(string.IsNullOrEmpty(result))
                    {     
                        return result;
                    }
                    else
                    {
                        _error = true;
                        return result;
                    }
                }

                return resultOK;
            }
        }

        private int CheckBoxValueNotZero(int value)
        {
            if(value == 0)
            {
                return 1;
            }
            return value;
        }

        private int CheckBoxValue(int value)
        {
            if(value>_box_1)
            {
                return _box_1;
            }
            else if(value == 0)
            {
                return 1;
            }
            return value;
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

        private BoxPrintModel FillModel()
        {
            return new BoxPrintModel
            {
                Company = _company,
                Order = _order,
                Box0 = (_checkBoxIsChecked == true) ? _box_0 : 0, 
                Box1 = _box_1,
                Items = _items,
                Quantity = _quantity,
                Copies = _copies
            };
        }

        private void FillList()
        {
            _items = new List<string>();
            _quantity = new List<int>();

            _items.Add(_item_0);
            _items.Add(_item_1);
            _items.Add(_item_2);
            _items.Add(_item_3);
            _items.Add(_item_4);
            _items.Add(_item_5);
            _quantity.Add(_qnt_0);
            _quantity.Add(_qnt_1);
            _quantity.Add(_qnt_2);
            _quantity.Add(_qnt_3);
            _quantity.Add(_qnt_4);
            _quantity.Add(_qnt_5);
        }

        private void FillDictionary()
        {
            _validationDictionary = new Dictionary<string, BoxValidationModel>();

            for (int i = 0; i <= _items.Count - 1; i++)
            {
                _validationDictionary.Add($"Item_{i}" , new BoxValidationModel { Items = this._items[i], Quantity = this._quantity[i] });
                _validationDictionary.Add($"Qnt_{i}" , new BoxValidationModel { Items = this._items[i], Quantity = this._quantity[i] });
            }

            _validationDictionary.Add("Company", new BoxValidationModel { Company = _company });
        }

        private void Validate()
        {
            _canValidate = true;
            FillList();
            FillDictionary();
            RefreshProperty();
            _canValidate = false;
        }

        #endregion
    }
}
