// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyListConverter.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the NullToBooleanConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class EmptyListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool notBehavior = parameter != null;

            var type = value as IList;

            if (type != null)
            {
                if (type.Count > 0)
                {
                    return notBehavior;
                }

                return !notBehavior;
            }

            return !notBehavior;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}