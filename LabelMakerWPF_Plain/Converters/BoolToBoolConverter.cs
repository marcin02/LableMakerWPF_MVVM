using System;
using System.Globalization;
using System.Windows.Data;

namespace LabelMakerWPF_Plain.Converters
{
    public class BoolToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var check = (bool)value;

            if (check)
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var check = (bool)value;

            if (check)
            {
                return false;
            }
            return true;
        }
    }
}
