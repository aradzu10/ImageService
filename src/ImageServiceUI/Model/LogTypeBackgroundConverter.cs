using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageServiceUI.Model
{
    class LogTypeBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name != "Brush")
            {
                throw new InvalidOperationException("Must convert to a brush!");
            }

            string type = (string)value;
            switch (type)
            {
                case " INFO":
                    return Brushes.LightGreen;
                case " WARNING":
                    return Brushes.Yellow;
                case " ERROR":
                    return Brushes.Coral;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
