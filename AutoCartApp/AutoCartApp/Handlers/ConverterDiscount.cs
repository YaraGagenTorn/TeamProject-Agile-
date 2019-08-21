using AutoCartApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AutoCartApp.Handlers
{
    class ConverterDiscount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(float) && (float)value < 1)            
                    return Color.FromHex("c2f97c"); 
            return Color.FromHex("e3ecfa");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
