// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrearEditarTercero.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for CrearTercero.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
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

    using NeuronCloud.Atpc.Co.WPF.ViewModels;

    public partial class CrearEditarTercero : NeuronMainWindow
    {
        public CrearEditarTercero()
        {
            this.InitializeComponent();
            this.Closing += this.CrearEditarTercero_Closing;
            this.ViewModel.MostrarMensajeEnUI += (o, s) => MessageBox.Show(this, s);
            this.Loaded += (s, e) => this.PrimerNombreCtrl.Focus();
        }

        private void CrearEditarTercero_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
