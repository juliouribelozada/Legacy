// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsigNoMuestraToVisibilityConverter.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the AsigNoMuestraToVisibilityConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;

    public class AsigNoMuestraToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var asigNoMuestra = value is AsigNoMuestra ? (AsigNoMuestra)value : AsigNoMuestra.NONE;

            switch (asigNoMuestra)
            {
                case AsigNoMuestra.NONE:
                    return Visibility.Collapsed;

                case AsigNoMuestra.AUTO:
                    return Visibility.Collapsed;

                case AsigNoMuestra.DIA:
                    return Visibility.Visible;

                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return AsigNoMuestra.NONE;
        }
    }
}
