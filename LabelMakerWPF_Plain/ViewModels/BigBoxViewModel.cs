using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using LabelMakerWPF_Plain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class BigBoxViewModel : ObservableObject, IDataErrorInfo
    {
        #region Constructor

        public BigBoxViewModel()
        {
            PrintCommand = new RelayCommand(Print);
            ClearCommand = new RelayCommand(Clear);
            NextCommand = new RelayCommand(Next);
        }

        #endregion

        #region Private propeties

        private bool _canFocus = false;
        private string _city;
        private string _contactPerson;
        private string _phoneNumber;
        private string _street;
        private string _zipCode;
        private bool _error = false;
        private bool _canValidate = false;
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
        private bool _checkBoxIsChecked = true;
        private bool _printed = false;
        private string _item_6;
        private string _item_7;
        private string _item_8;
        private int _qnt_6;

        public int Qnt_6
        {
            get { return _qnt_6; }
            set { _qnt_6 = value; OnPropertyChanged(nameof(Qnt_6)); }
        }
        private int _qnt_7;

        public int Qnt_7
        {
            get { return _qnt_7; }
            set { _qnt_7 = value; OnPropertyChanged(nameof(Qnt_7)); }
        }
        private int _qnt_8;

        public int Qnt_8
        {
            get { return _qnt_8; }
            set { _qnt_8 = value; OnPropertyChanged(nameof(Qnt_8)); }
        }


        #endregion

        #region Public propeties

        public bool CanFocus
        {
            get { return _canFocus; }
            set { _canFocus = value; OnPropertyChanged(nameof(CanFocus)); }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(City)); }
        }

        public string ContactPerson
        {
            get { return _contactPerson; }
            set { _contactPerson = value; OnPropertyChanged(nameof(ContactPerson)); }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = NumbersOnly(value); OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public string ZipCode
        {
            get { return ReturnFormattedZipCode(_zipCode); }
            set { _zipCode = CheckCode(value); OnPropertyChanged(nameof(ZipCode)); }
        }

        public string Street 
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged(nameof(Street)); }
        }
                
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
            {
                _box_0 = CheckBoxValue(value);
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
            {
                _item_0 = value;
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

        public string Item_6
        {
            get { return _item_6; }
            set { _item_6 = value; OnPropertyChanged(nameof(Item_6)); }
        }

        public string Item_7
        {
            get { return _item_7; }
            set { _item_7 = value; OnPropertyChanged(nameof(Item_7)); }
        }

        public string Item_8
        {
            get { return _item_8; }
            set { _item_8 = value; OnPropertyChanged(nameof(Item_8)); }
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
            get { return _qnt_4; ; }
            set { _qnt_4 = value; OnPropertyChanged(nameof(Qnt_4)); }
        }

        public int Qnt_5
        {
            get { return _qnt_5; }
            set { _qnt_5 = value; OnPropertyChanged(nameof(Qnt_5)); }
        }

        public short Copies
        {
            get { return _copies; }
            set { _copies = CheckIsNoZero(Convert.ToInt16(value)); OnPropertyChanged(nameof(Copies)); }
        }

        public bool CheckBoxIsChecked
        {
            get { return _checkBoxIsChecked; }
            set { _checkBoxIsChecked = value; }
        }

        public string Error { get { return null; } }

        #endregion

        #region Commands

        public RelayCommand PrintCommand {get; set;}
        public RelayCommand ClearCommand {get; set; }
        public RelayCommand NextCommand { get; private set; }

        #endregion

        #region Collections

        private List<string> _items;
        private List<int> _quantity;
        private Dictionary<string, BoxValidationModel> _validationDictionary;

        #endregion

        #region Methods

        private void AddToPrint()
        {
            BigBoxPrintModel model = new BigBoxPrintModel
            {
                Company = _company,
                Order = _order,
                ContactPerson = _contactPerson,
                PhoneNumber = _phoneNumber,
                Street = _street,
                ZipCode = _zipCode,
                City = _city,
                Box0 = _box_0,
                Box1 = _box_1,
                Items = _items,
                Quantity = _quantity,
                Copies = _copies
            };

            BigBoxLablePrint print = new BigBoxLablePrint(model);
        }
        private int CheckBoxValue(int value)
        {
            if (value > _box_1)
            {
                return _box_1;
            }
            else if (value == 0)
            {
                return 1;
            }
            return value;
        }
        private int CheckBoxValueNotZero(int value)
        {
            if (value == 0)
            {
                return 1;
            }
            return value;
        }
        private string CheckCode(string value)
        {
            if (value.Contains("-")) value = value.Remove(2, 1);
            Regex regex = new Regex("[^0-9]");

            if (!regex.IsMatch(value))
            {
                return value;
            }

            return _zipCode;
        }
        private bool CheckCanPrint(bool value)
        {
            if (_error)
            {
                value = false;
                return value;
            }
            else
            {
                return value;
            }
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
        private void Clear(object obj)
        {
            Company = default(string);
            Order = default(string);
            City = default(string);
            ContactPerson = default(string);
            PhoneNumber = "";
            Street = default(string);
            ZipCode = "";
            Box_0 = 1;
            Box_1 = 1;
            Item_0 = default(string);
            Item_1 = default(string);
            Item_2 = default(string);
            Item_3 = default(string);
            Item_4 = default(string);
            Item_5 = default(string);
            Item_6 = default(string);
            Item_7 = default(string);
            Item_8 = default(string);
            Qnt_0 = 0;
            Qnt_1 = 0;
            Qnt_2 = 0;
            Qnt_3 = 0;
            Qnt_4 = 0;
            Qnt_5 = 0;
            Qnt_6 = 0;
            Qnt_7 = 0;
            Qnt_8 = 0;
            Copies = 1;
        }
        private void FillDictionary()
        {
            _validationDictionary = new Dictionary<string, BoxValidationModel>();

            for (int i = 0; i <= _items.Count - 1; i++)
            {
                _validationDictionary.Add($"Item_{i}", new BoxValidationModel { Items = this._items[i], Quantity = this._quantity[i] });
                _validationDictionary.Add($"Qnt_{i}", new BoxValidationModel { Items = this._items[i], Quantity = this._quantity[i] });
            }

            _validationDictionary.Add("Company", new BoxValidationModel { Company = _company });
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
            _items.Add(_item_6);
            _items.Add(_item_7);
            _items.Add(_item_8);
            _quantity.Add(_qnt_0);
            _quantity.Add(_qnt_1);
            _quantity.Add(_qnt_2);
            _quantity.Add(_qnt_3);
            _quantity.Add(_qnt_4);
            _quantity.Add(_qnt_5);
            _quantity.Add(_qnt_6);
            _quantity.Add(_qnt_7);
            _quantity.Add(_qnt_8);
        }
        private void Next(object obj)
        {
            MessagesModel message = new MessagesModel();
            if (_printed == false) message.PrintError();
            else if (Box_0 + 1 > Box_1) message.BoxError();
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
            Item_6 = default(string);
            Item_7 = default(string);
            Item_8 = default(string);
            Qnt_0 = 0;
            Qnt_1 = 0;
            Qnt_2 = 0;
            Qnt_3 = 0;
            Qnt_4 = 0;
            Qnt_5 = 0;
            Qnt_6 = 0;
            Qnt_7 = 0;
            Qnt_8 = 0;
            Copies = 1;
            _printed = false;
            CanFocus = true;
            CanFocus = false;
        }
        private string NumbersOnly(string value)
        {
            Regex regex = new Regex("[^0-9 ]");

            if(!regex.IsMatch(value))
            {
                return value;
            }

            return _phoneNumber; 
        }
        private void Print(object obj)
        {
            Validate();
            if(!_error)
            {
                AddToPrint();
                _printed = true;
            }
            else
            {
                MessagesModel messages = new MessagesModel();
                messages.QuantityError();
                _error = false;
            }
        }
        private void RefreshProperty()
        {
            OnPropertyChanged(nameof(ContactPerson));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Street));
            OnPropertyChanged(nameof(ZipCode));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Company));
            OnPropertyChanged(nameof(Item_0));
            OnPropertyChanged(nameof(Item_1));
            OnPropertyChanged(nameof(Item_2));
            OnPropertyChanged(nameof(Item_3));
            OnPropertyChanged(nameof(Item_4));
            OnPropertyChanged(nameof(Item_5));
            OnPropertyChanged(nameof(Item_6));
            OnPropertyChanged(nameof(Item_7));
            OnPropertyChanged(nameof(Item_8));
            OnPropertyChanged(nameof(Qnt_0));
            OnPropertyChanged(nameof(Qnt_1));
            OnPropertyChanged(nameof(Qnt_2));
            OnPropertyChanged(nameof(Qnt_3));
            OnPropertyChanged(nameof(Qnt_4));
            OnPropertyChanged(nameof(Qnt_5));
            OnPropertyChanged(nameof(Qnt_6));
            OnPropertyChanged(nameof(Qnt_7));
            OnPropertyChanged(nameof(Qnt_8));
        }
        private string ReturnFormattedZipCode(string value)
        {
            if (value!=null)
            {
                if (value.Length > 2)
                {
                    return value.Insert(2, "-");
                } 
            }

            return _zipCode;
        }
        public string this[string columnName]
        {
            get
            {
                string resultOK = null;
                string resultNOK = "Błąd";

                if (_canValidate)
                {
                    switch (columnName)
                    {
                        case "City":
                            if(string.IsNullOrWhiteSpace(_city))
                            {
                                _error = true;
                                return resultNOK;
                            }
                            
                            return resultOK;

                        case "ContactPerson":
                            if (string.IsNullOrWhiteSpace(_contactPerson))
                            {
                                _error = true;
                                return resultNOK;
                            }
                            
                            return resultOK;

                        case "PhoneNumber":
                            if (string.IsNullOrWhiteSpace(_phoneNumber))
                            {
                                _error = true;
                                return resultNOK;
                            }
                            
                            return resultOK;

                        case "Street":
                            if (string.IsNullOrWhiteSpace(_street))
                            {
                                _error = true;
                                return resultNOK;
                            }
                            
                            return resultOK;

                        case "ZipCode":
                            if (string.IsNullOrWhiteSpace(_zipCode) || !string.IsNullOrWhiteSpace(_zipCode) && _zipCode.Length<5)
                            {
                                _error = true;
                                return resultNOK;
                            }
                            
                            return resultOK;

                        default:

                            BoxValidation vm = new BoxValidation();

                            if (_canValidate)
                            {
                                string result = vm.Validation(_validationDictionary, columnName);
                                if (string.IsNullOrEmpty(result))
                                {
                                    return result;
                                }                               
                                    _error = true;
                                    return result;                                
                            }
                            break;
                    }
                }

                return resultOK;
            }
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
