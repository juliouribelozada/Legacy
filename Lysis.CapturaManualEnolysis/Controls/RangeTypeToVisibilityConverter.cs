// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeTypeToVisibilityConverter.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the NullToBooleanConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public class RangeTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool notBehavior = parameter != null;
            if (value is RangeType)
            {
                return notBehavior
                           ? ((RangeType)value == RangeType.Partial ? Visibility.Visible : Visibility.Collapsed)
                           : ((RangeType)value == RangeType.Partial ? Visibility.Collapsed : Visibility.Visible);
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}