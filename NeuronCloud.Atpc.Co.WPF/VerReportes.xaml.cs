// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerReportes.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for VerReportes.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Data;
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

    /// <summary>
    /// Interaction logic for VerReportes.xaml
    /// </summary>
    public partial class VerReportes : Window
    {
        public VerReportes()
        {
            this.InitializeComponent();
            this.ReportViewerOsc.RenderingComplete += this.ReportViewerOsc_RenderingComplete;
            this.ReportViewerFactura.RenderingComplete += this.ReportViewerFactura_RenderingComplete;
        }

        public bool IsReportedLoaded { get; set; }

        public string UbicacionReporteOsc { get; set; }

        public string UbicacionReporteFactura { get; set; }

        public Dictionary<string, DataTable> DatosFactura { get; set; }
        public Dictionary<string, DataTable> DatosOsc { get; set; }

        private void ReportViewerOsc_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {
            if (this.IsReportedLoaded)
            {
               //z this.ReportViewerOsc.PrintDialog();
            }
        }
        private void ReportViewerFactura_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {
            if (this.IsReportedLoaded)
            {
               // this.ReportViewerFactura.PrintDialog();
            }
        }
    }
}
