// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListadoOSC.xaml.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Interaction logic for ListadoOSC.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
{
    using System.Windows.Controls;
    using System.Windows.Input;

    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;

    /// <summary>
    /// Interaction logic for ListadoOSC.xaml
    /// </summary>
    public partial class ListadoOSC : UserControl
    {
        public ListadoOSC()
        {
            this.InitializeComponent();
            this.ViewModel.MostrarVentanaReporteEvent += (o, s) => this.OnMostrarVentanaReporteEvent(s);
            this.ViewModel.ImprimirLabelsEvent += (o, s, obj) => this.OnImprimirLabelsEvent(obj);
        }

        public event ParametroTextoTextoEventHandler MostrarVentanaReporteEvent;
        public event ParametroTextoObjetoEventHandler ImprimirLabelsEvent;

        public void OnMostrarVentanaReporteEvent(string parametro)
        {
            var handler = this.MostrarVentanaReporteEvent;
            if (handler != null)
            {
                handler(this, parametro, string.Empty);
            }
        }
        public void OnImprimirLabelsEvent(object parametro)
        {
            var handler = this.ImprimirLabelsEvent;
            if (handler != null)
            {
                handler(this, string.Empty, parametro);
            }
        }

        private void UserControlKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    this.ViewModel.MoverAlSiguiente.Execute(this);
                    e.Handled = true;
                    break;
                case Key.Left:
                    this.ViewModel.MoverAlAnterior.Execute(this);
                    e.Handled = true;
                    break;
            }
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
