// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    using Neuron.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static string DBConnectionString { get; private set; }

        internal static string StoreProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            DBConnectionString = Neuron.Satellite.CapturaManual.Properties.Settings.Default.DBConnectionString;
            StoreProvider = Neuron.Satellite.CapturaManual.Properties.Settings.Default.StoreProvider;
            CapturaGlobal.ShowAlertsWindow = Neuron.Satellite.CapturaManual.Properties.Settings.Default.MostrarObsAnormal;
            try
            {
                NeuronStorage.SetConnectionString(DBConnectionString, StoreProvider);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }

            if (!Neuron.Satellite.CapturaManual.Properties.Settings.Default.UsarAceleracionHW)
            {
                RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
                Trace.WriteLine("Sin Aceleración de Hardware");
            }
        }
    }
}
