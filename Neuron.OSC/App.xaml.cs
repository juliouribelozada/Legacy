// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Neuron.OSC
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    using Neuron.OSC.Data;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using Neuron.OSC.Data.Model;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static string DBConnectionString { get; private set; }

        internal static string StoreProvider { get; private set; }

        internal static string ReporteOsc { get; private set; }

        internal static string ReporteFactura { get; private set; }

        internal static AsigNoMuestra AsigNoMuestra { get; private set; }

        internal static bool NoOrdenMedicaRequired { get; private set; }

        internal static string PrefijoCentroTomaDefault { get; private set; }

        internal static string PrefijosCentroToma { get; private set; }

        internal static bool ImprimirDespuesDeGuardar { get; private set; }

        internal static bool TotalModificable { get; private set; }

        internal static bool RemitenteDesdeConfig { get; private set; }

        internal static bool BuscarServiciosPrevios { get; private set; }

        internal static bool CambiarValorItem { get; private set; }
        internal static bool CanChangeValueItem { get; private set; }

        internal static bool PersonalAsistencialEsRequerido { get; private set; }

        internal static bool PersonalAsistencialVisible { get; private set; }

        internal static bool ImprimirCodigosDeBarras { get; private set; }

		internal static bool PrintServiceLabel { get; private set; }

		internal static string NombreImpresoraCodigoDeBarras { get; private set; }

        internal static bool Hl7Enabled { get; private set; }

        internal static Uri Hl7Url { get; private set; }

        internal static bool EnableEditTercero { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            Atpc.Common.Settings.Provider.SetDefaultProvider(typeof(LocalFileSettingsProvider));
			Hl7Enabled = Neuron.OSC.Properties.Settings.Default.HL7Enable;
			//Hl7Url = new Uri(Neuron.OSC.Properties.Settings.Default.HL7Url, UriKind.RelativeOrAbsolute);
			////Hl7Url = new Uri(" http://localhost:3794/hl7proxy/DataTransfer.svc", UriKind.RelativeOrAbsolute);

			DBConnectionString = Neuron.OSC.Properties.Settings.Default.DBConnectionString;
            StoreProvider = Neuron.OSC.Properties.Settings.Default.StoreProvider;
            ReporteOsc = Neuron.OSC.Properties.Settings.Default.ReporteOsc;
            ReporteFactura = Neuron.OSC.Properties.Settings.Default.ReporteFactura;
            AsigNoMuestra = Neuron.OSC.Properties.Settings.Default.AsigNoMuestra;
            PrefijoCentroTomaDefault = Neuron.OSC.Properties.Settings.Default.PrefijoCentroDeTomaDefault;
            ImprimirDespuesDeGuardar = Neuron.OSC.Properties.Settings.Default.ImprimirDespuesDeGuardar;
            NoOrdenMedicaRequired = Neuron.OSC.Properties.Settings.Default.NumeroOrdenRequerido;
            PrefijosCentroToma = Neuron.OSC.Properties.Settings.Default.PrefijosCentroDeToma;
            TotalModificable = Neuron.OSC.Properties.Settings.Default.TotalModificable;
            RemitenteDesdeConfig = Neuron.OSC.Properties.Settings.Default.RemitenteDesdeConfig;
            BuscarServiciosPrevios = Neuron.OSC.Properties.Settings.Default.BuscarServiciosPrevios;
            CambiarValorItem = Neuron.OSC.Properties.Settings.Default.CambiarValorItem;
            //CanChangeValueItem = Neuron.OSC.Properties.Settings.Default.CanChangeValueItem;
            PersonalAsistencialEsRequerido = Neuron.OSC.Properties.Settings.Default.PersonalAsistencialEsRequerido && Neuron.OSC.Properties.Settings.Default.PersonalAsistencialVisible;
            PersonalAsistencialVisible = Neuron.OSC.Properties.Settings.Default.PersonalAsistencialVisible;
            ImprimirCodigosDeBarras = Neuron.OSC.Properties.Settings.Default.ImprimirCodigosDeBarras;
			PrintServiceLabel = Neuron.OSC.Properties.Settings.Default.PrintServiceLabel;
			NombreImpresoraCodigoDeBarras = Neuron.OSC.Properties.Settings.Default.NombreImpresoraCodigoDeBarras;
            EnableEditTercero = Neuron.OSC.Properties.Settings.Default.EnableEditTercero;
            OscGlobalConfig.SetNomSede(Neuron.OSC.Properties.Settings.Default.NomSede);

            ConfiguracionGlobal.SetUsuarioActual("TestOsc");

            // ToDo: Traer el Usuario del Proveedor.
            ////NeuronCloudIdentity usuarioActual = new NeuronCloudIdentity("TestOsc");
            ////NeuronCloudPrincipal principal = new NeuronCloudPrincipal(usuarioActual);
            ////ConfiguracionGlobal.SetIPrincipalActual(principal);

            try
            {
                NeuronOSCStorage.SetConnectionString(DBConnectionString, StoreProvider);
                NeuronCloudBaseStorage.SetConnectionString(DBConnectionString, StoreProvider);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }

            if (!Neuron.OSC.Properties.Settings.Default.UsarAceleracionHW)
            {
                RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
                Trace.WriteLine("Sin Aceleración de Hardware");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Neuron.OSC.Properties.Settings.Default.Save();
            }
            catch (Exception exc)
            {
                Trace.WriteLine("Error al Guardar Settings" + exc.Message);
            }

            base.OnExit(e);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message,"Error");
        }
    }
}
