// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoCompleteBoxAux.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Controls
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoCompleteBoxAux : AutoCompleteBox
    {
        public event RoutedEventHandler SeleccionadoDeLista;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.SelectionAdapter != null)
            {
                this.SelectionAdapter.Commit += (sender, args) =>
                    {
                        Trace.WriteLine("Seleccion Realizada desde la lista");
                        this.OnSeleccionadoDeLista(new RoutedEventArgs());
                    };
            }
        }

        protected internal void FocusTextBox()
        {
            var tb = this.GetTemplateChild("Text") as TextBox;
            if (tb != null)
            {
                tb.Focus();
            }
        }

        protected virtual void OnSeleccionadoDeLista(RoutedEventArgs e)
        {
            RoutedEventHandler handler = this.SeleccionadoDeLista;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
