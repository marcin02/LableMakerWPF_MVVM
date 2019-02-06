using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.Print;
using LabelMakerWPF_Plain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.ViewModels
{
    public class BigBoxViewModel : BoxViewModel
    {
        #region Constructor

        public BigBoxViewModel()
        {
            PrintBigCommand = new RelayCommand(Print);
        }

        #endregion
        #region Private propeties

        private string _city;
        private string _conteacPerson;
        private string _phoneNumber;
        private string _street;
        private string _zipCode;

        #endregion

        #region Public propeties

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string ContactPerson
        {
            get { return _conteacPerson; }
            set { _conteacPerson = value; }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = NumbersOnly(value); OnPropertyChanged(nameof(PhoneNumber)); }
        }
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = CheckCode(value); OnPropertyChanged(nameof(ZipCode)); }
        }

        #endregion

        #region Commands

        public RelayCommand PrintBigCommand {get; set;}

        #endregion

        #region Methods

        private string NumbersOnly(string value)
        {
            Regex regex = new Regex("[^0-9]");

            if(!regex.IsMatch(value))
            {
                return value;
            }

            return _phoneNumber; 
        }

        private string CheckCode(string value)
        {
            Regex regex = new Regex("[^0-9]");

            if (!regex.IsMatch(value))
            {
                return value;
            }

            return _zipCode;
        }

        private void Print(object obj)
        {
            FillList();
            BigBoxPrintModel model = new BigBoxPrintModel
            {
                Company = _company,
                Order = _order,
                ContactPerson = _conteacPerson,
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


        #endregion
    }
}
