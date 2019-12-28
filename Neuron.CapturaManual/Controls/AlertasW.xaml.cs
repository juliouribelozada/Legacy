// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertasW.xaml.cs" company="Atpc.CO">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for AlertasW.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
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


    public partial class AlertasW : Window
    {
        private readonly AlertasViewModel viewModel;

        public AlertasW()
        {
            this.InitializeComponent();
            this.viewModel = new AlertasViewModel(this);
            this.DataContext = this.ViewModel;
        }

        public AlertasViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }
    }
}
