// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionalAnalitesWindow.xaml.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Interaction logic for OptionalAnalites.xaml
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
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using Neuron.Satelite.CapturaManual.Data.ViewModels;

    /// <summary>
    /// Interaction logic for OptionalAnalites.xaml
    /// </summary>
    public partial class OptionalAnalitesWindow : Window
    {
        private readonly OptionalAnalitesViewModel viewModel = new OptionalAnalitesViewModel();

        public OptionalAnalitesWindow()
        {
            this.InitializeComponent();
            this.DataContext = this.viewModel;
        }

        public OptionalAnalitesWindow(IList<ComplexObject> analitesList, IList<ComplexObject> panelsList)
            : this()
        {
            this.viewModel.Analites.Clear();
            foreach (var o in analitesList)
            {
                this.viewModel.Analites.Add(o);
            }

            this.viewModel.PanelsCollection.Clear();
            foreach (var o in panelsList)
            {
                this.viewModel.PanelsCollection.Add(o);
            }
            this.viewModel.UpdatePanels();
        }

        private void OnAcceptButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
