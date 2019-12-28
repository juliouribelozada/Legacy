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

namespace Neuron.Satellite.CapturaManual.Controls
{
    using Neuron.Satelite.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.ViewModels;

    /// <summary>
    /// Interaction logic for ExamenesPendientes.xaml
    /// </summary>
    public partial class HistoricosW : Window
    {
        private readonly HistoricoViewModel viewModel = new HistoricoViewModel();

        public HistoricosW()
        {
            this.InitializeComponent();
            this.Loaded += this.ExamenesPendientes_Loaded;
            this.DataContext = this.ViewModel;
        }

        public HistoricoViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }

        private void ExamenesPendientes_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Consultar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Grafico.Visibility = Visibility.Visible;
        }
    }
}
