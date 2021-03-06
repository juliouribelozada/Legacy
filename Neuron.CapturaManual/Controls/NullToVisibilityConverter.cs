﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullToVisibilityConverter.cs" company="ATPC.co">
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

    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool notBehavior = parameter != null;

            return notBehavior
                       ? (value == null ? Visibility.Visible : Visibility.Collapsed)
                       : (value == null ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}