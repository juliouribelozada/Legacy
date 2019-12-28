// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialPortConfiguratorWindow.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for SerialPortConfiguratorWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls.Configurators
{
    using System;
    using System.Collections.Generic;
    using System.IO.Ports;
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

    using Atpc.UI.Controls.Configurators.SerialPortConfiguration;

    /// <summary>
    /// Interaction logic for SerialPortConfiguratorWindow.xaml
    /// </summary>
    public partial class SerialPortConfiguratorWindow : Window
    {
        private readonly SerialPortConfigurationViewModel viewModel;

        public SerialPortConfiguratorWindow(SerialPort portToConfigure)
        {
            InitializeComponent();
            this.viewModel = new SerialPortConfigurationViewModel(this) { SerialPortToConfigure = portToConfigure };
            this.Settings = this.viewModel.Settings;
            this.DataContext = this.viewModel;
        }
    
        public SerialPortSettings Settings { get; set; }
    }
}
