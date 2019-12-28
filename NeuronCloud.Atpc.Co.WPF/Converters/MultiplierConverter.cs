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

    public class MultiplierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double valor = value is double ? (double)value : 0;

            double multiplicador = parameter is double ? (double)parameter : 2;

            return valor * multiplicador;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return AsigNoMuestra.NONE;
        }
    }
}
