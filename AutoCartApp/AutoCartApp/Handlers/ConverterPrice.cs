using AutoCartApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AutoCartApp.Handlers
{
    class ConverterPrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(float) && (float)value < 1)            
                    return $"currently {(1 - (float)value) * 100}% off";
            return "";
        } 

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 1.0f;
        }
    }
}
