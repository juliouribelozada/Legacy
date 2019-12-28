// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullToBooleanConverter.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.Common.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool notBehavior = parameter != null;
            return notBehavior ? value == null : value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
