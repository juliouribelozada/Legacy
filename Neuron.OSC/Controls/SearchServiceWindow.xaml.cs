// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchServiceWindow.xaml.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for SearchServiceWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Controls
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Windows;
    using System.Windows.Controls;

    using Neuron.OSC.Data;

    using NeuronCloud.Atpc.Co.WPF;

    /// <summary>
    /// Interaction logic for SearchServiceWindow.xaml
    /// </summary>
    public partial class SearchServiceWindow : NeuronMainWindow
    {
        public SearchServiceWindow()
        {
            this.InitializeComponent();
            this.ViewModel.ListaCargada += (sender, args) => this.acNombreServicio.PopulateComplete();
        }

        private void AcNombreServicioSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                this.ViewModel.SelectedSugestion = autoCompleteBox.SelectedItem as PRO_ConvenioPrestPortafoSeleccAutoComplete_Result;
                Debug.WriteLine("Seleccion Realizada");
            }
        }

        private void AcNombreServicioPopulating(object sender, PopulatingEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                Debug.WriteLine("Iniciando Consulta...");
                this.ViewModel.LoadSugestions(autoCompleteBox.Text);
            }

            e.Cancel = true;
        }

        private void NeuronMainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.acNombreServicio.FocusTextBox();
        }

        private void AcNombreServicio_OnSeleccionadoDeLista(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.SeleccionarCommand.CanExecute())
            {
                this.ViewModel.SeleccionarCommand.Execute(); 
                
            }
        }
    }
}
