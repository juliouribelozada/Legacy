﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvertBooleanToVisibilityConverter.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the InvertBooleanToVisibilityConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class InvertBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }

            bool? tmp = value as bool?;
            if (tmp != null)
            {
                bValue = tmp.HasValue ? tmp.Value : false;
            }

            return bValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value != Visibility.Visible;
            }

            return true;
        }
    }
}
