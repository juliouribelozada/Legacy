// -----------------------------------------------------------------------
// <copyright file="ListaAnalitosTemplateSelector.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using Neuron.Satelite.CapturaManual.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ListaAnalitosTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Window win = Application.Current.MainWindow;

            var list = item as IList<ComplexObject>;

            if (list != null)
            {
                if (list.FirstOrDefault() is PRO_CargaCapturaCampoLargo_Result)
                {
                    return win.FindResource("AnalitosLista") as DataTemplate;
                }
                else if (list.FirstOrDefault() is PRO_CargaCapturaCuatroCampos_Result)
                {
                    return win.FindResource("AnalitosGridView") as DataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
