// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnaliteTemplateSelector.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the AnaliteTemplateSelector type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using Neuron.Satelite.CapturaManual.Data;

    public class AnaliteTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Window win = Application.Current.MainWindow;

            if (item is PRO_CargaCapturaCuatroCampos_Result)
            {
                return (DataTemplate)win.TryFindResource("OptionalSelector");
            }

            return base.SelectTemplate(item, container);
        }
    }
}
