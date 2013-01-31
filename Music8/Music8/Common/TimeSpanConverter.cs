using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Music8.Common
{
    class TimeSpanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan)
                return ((TimeSpan)value).TotalSeconds;
            else if(value is Duration)
                return ((Duration)value).TimeSpan.TotalSeconds;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }
}
