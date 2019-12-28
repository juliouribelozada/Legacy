using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NeuronCloud.Atpc.Co.WPF;

namespace Neuron.OSC.Controls
{
    /// <summary>
    /// Interaction logic for LiquidacionPagoWindow.xaml
    /// </summary>
    public partial class LiquidacionPagoWindow : NeuronMainWindow
    {
        public LiquidacionPagoWindow()
        {
            InitializeComponent();
            this.DatosPagoDg.CommandBindings.Add(new CommandBinding(LiquidacionPagoViewModel.RemovePaymentCommand, LiquidacionPagoViewModel.RemovePaymentAction));
        }

        private void ValorTextGotFocus(object sender, RoutedEventArgs e)
        {
            var text = sender as TextBox;

            if (text != null)
            {
                text.Focus();
                text.SelectAll();
            }
        }

        private void FullPagoBt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
