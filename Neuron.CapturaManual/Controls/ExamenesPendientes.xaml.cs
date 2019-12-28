// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExamenesPendientes.xaml.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for ExamenesPendientes.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using Neuron.Satelite.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;
    using Neuron.Satelite.CapturaManual.Data.ViewModels;

    /// <summary>
    /// Interaction logic for ExamenesPendientes.xaml
    /// </summary>
    public partial class ExamenesPendientes : Window
    {
        private readonly ExamenesPendientesViewModel viewModel = new ExamenesPendientesViewModel();

        public ExamenesPendientes()
        {
            this.InitializeComponent();
            this.Loaded += this.ExamenesPendientes_Loaded;
            this.DataContext = this.ViewModel;
        }

        public ExamenesPendientesViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }

        private void ExamenesPendientes_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ViewModel.IdMuestra))
            {
                StoreProceduresOps.PRO_ConsultaEstudioIdenMuestraAsync(
                    this.ViewModel.IdMuestra,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var results = ee.Result as List<PRO_ConsultaEstudioIdenMuestra_Result>;

                            if (results != null)
                            {
                                this.ViewModel.Pendientes.Clear();
                                foreach (var result in results)
                                {
                                    this.ViewModel.Pendientes.Add(result);
                                }
                            }
                        }
                    });
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.ResultadosPorTomaList.SelectedItem != null)
            {
                var result = this.ResultadosPorTomaList.SelectedItem as PRO_ConsultaEstudioIdenMuestra_Result;

                if (result != null)
                {
                    e.CanExecute = !string.IsNullOrWhiteSpace(result.NoResultado);
                }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.ResultadosPorTomaList.SelectedItem != null)
            {
                var result = this.ResultadosPorTomaList.SelectedItem as PRO_ConsultaEstudioIdenMuestra_Result;

                if (result != null)
                {
                    if (this.ViewModel.InfoPersona != null)
                    {
                        var baseUrl = Properties.Settings.Default.BaseUrl;
                        Process.Start(new ProcessStartInfo($@"{baseUrl}NumeroUnicoDoc={this.ViewModel.InfoPersona.Result.NumeroUnicoDoc}&sNoOrdenMedica={this.ViewModel.InfoPersona.Result.NoOrdenMedica}"));
                        Thread.Sleep(1000);
                        this.Close();
                    }
                }
            }
        }

        private void AnulaCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var image = e.OriginalSource as Image;

            if (image != null)
            {
                var muestraResult = image.DataContext as PRO_ConsultaEstudioIdenMuestra_Result;

                if (muestraResult != null)
                {
                    if (MessageBox.Show("Desea Anular este Resultado?", "Anular Resultado", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        StoreProceduresOps.PRO_CapturaManualResultadoAnulaAsync(
                            new CapturaManualAnulaParameters { NoResultado = muestraResult.NoResultado, CodPesoAsisten = this.ViewModel.InfoProfesional.Codigo },
                            (ss, ee) =>
                            {
                                this.ViewModel.Reload = true;
                                this.DialogResult = true;
                            });
                    }
                }
            }
        }

        private void AnulaCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Anular);
        }
    }
}
