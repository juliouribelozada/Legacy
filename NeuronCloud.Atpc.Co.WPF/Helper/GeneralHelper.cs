// -----------------------------------------------------------------------
// <copyright file="GeneralHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GeneralHelper
    {
        internal static CultureInfo GetCulture(FrameworkElement element)
        {
            CultureInfo culture;
            if (DependencyPropertyHelper.GetValueSource(element, FrameworkElement.LanguageProperty).BaseValueSource != BaseValueSource.Default)
            {
                culture = GetCultureInfo(element);
            }
            else
            {
                culture = CultureInfo.CurrentCulture;
            }
            return culture;
        }

        internal static CultureInfo GetCultureInfo(DependencyObject element)
        {
            XmlLanguage language = (XmlLanguage)element.GetValue(FrameworkElement.LanguageProperty);
            try
            {
                return language.GetSpecificCulture();
            }
            catch (InvalidOperationException)
            {
                // We default to en-US if no part of the language tag is recognized.
                return CultureInfo.ReadOnly(new CultureInfo("en-us", false));
            }
        }

    }
}
