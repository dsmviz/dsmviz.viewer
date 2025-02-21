using System.Globalization;
using System.Windows.Data;

namespace Dsmviz.Viewer.View.ValueConverters
{
    public class MetricsExpandedToViewWidthConverter : IValueConverter
    {
        public double ViewWidth { get; set; }

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? ViewWidth : 0.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

