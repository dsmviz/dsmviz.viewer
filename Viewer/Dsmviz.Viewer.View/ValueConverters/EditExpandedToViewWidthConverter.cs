﻿using System.Globalization;
using System.Windows.Data;

namespace Dsmviz.Viewer.View.ValueConverters
{
    public class EditExpandedToViewWidthConverter : IValueConverter
    {
        public double MaxViewWidth { get; set; }
        public double MinViewWidth { get; set; }

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? MaxViewWidth : MinViewWidth;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
