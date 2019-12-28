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
using Neuron.Satelite.CapturaManual.Data.ViewModels;

namespace Neuron.Satellite.CapturaManual.Controls
{
    /// <summary>
    /// Interaction logic for TrackingInfoWindow.xaml
    /// </summary>
    public partial class TrackingInfoWindow : Window
    {
        public TrackingInfoViewModel ViewModel { get; } = new TrackingInfoViewModel();

        public TrackingInfoWindow()
        {
            this.InitializeComponent();
            this.DataContext = this.ViewModel;
            this.Loaded += this.TrackingInfoWindowLoaded;
        }

        private void TrackingInfoWindowLoaded(object sender, RoutedEventArgs e)
        {
           this.ViewModel.DoTracking();
        }
    }
}
