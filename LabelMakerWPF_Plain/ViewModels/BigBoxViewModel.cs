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
            set { _phoneNumber = PhoneNumberKeyCheck(value); OnPropertyChanged(nameof(PhoneNumber)); }
        }
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; OnPropertyChanged(nameof(ZipCode)); }
        }

        #endregion

        #region Methods

        private string PhoneNumberKeyCheck(string value)
        {
            Regex regex = new Regex("[^0-9]");

            if(!regex.IsMatch(value))
            {
                return value;
            }

            return _phoneNumber; 
        }



        #endregion
    }
}
