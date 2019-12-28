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

namespace Neuron.Satellite.CapturaManual.Controls.Windows
{
    using Neuron.Satelite.CapturaManual.Data.ViewModels;

    /// <summary>
    /// Interaction logic for TechnicianObsWindow.xaml
    /// </summary>
    public partial class TechnicianObsWindow : Window
    {
        private readonly TechnicianObsViewModel viewModel;

        public TechnicianObsWindow(string sampleId, string sourceCode,string user)
        {
            this.InitializeComponent();
            this.viewModel = new TechnicianObsViewModel(sampleId, sourceCode);
            this.viewModel.User = user;
            this.DataContext = this.viewModel;
            this.Loaded += (sender, args) => { this.viewModel.LoadObservations(); };
        }
    }
}
