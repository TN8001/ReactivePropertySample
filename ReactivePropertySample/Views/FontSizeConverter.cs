using System;
using System.Globalization;
using System.Windows.Data;

namespace ReactivePropertySample.Views
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double)value;
            var fontSize = (int)(actualHeight * .7);
            return fontSize;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
