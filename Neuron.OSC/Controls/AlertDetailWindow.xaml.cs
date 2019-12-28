// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertDetailWindow.xaml.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for AlertDetailWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Controls
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

    using Neuron.OSC.Data.Model;

    /// <summary>
    /// Interaction logic for AlertDetailWindow.xaml
    /// </summary>
    public partial class AlertDetailWindow
    {
        private readonly AlertDetailWindowViewModel viewModel;

        public AlertDetailWindow(IEnumerable<Antecedente> antecedentes)
        {
            this.InitializeComponent();
            if (antecedentes != null)
            {
                this.viewModel = new AlertDetailWindowViewModel(antecedentes);
                this.DataContext = this.viewModel;
            }
        }
    }
}
