// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowControler.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web.Security;
    using System.Windows;

    using Neuron.OSC.Controls;
    using Neuron.OSC.Data.Model;
    using Neuron.OSC.NeuronDataSetTableAdapters;
    using Neuron.OSC.ViewModel;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;
    using NeuronCloud.Atpc.Co.Providers.Desktop;
    using NeuronCloud.Atpc.Co.WPF;
    using NeuronCloud.Atpc.Co.WPF.ViewModels;
    using Neuron.OSC.Data.AsyncOps;

    public class MainWindowControler
    {
        private MainWindow window;

        public MainWindowControler(MainWindow window, MainWindowViewModel viewModel)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            this.window = window;
            this.ViewModel = viewModel;
            this.ViewModel.CrearNuevoTerceroEvent += this.ViewModelCrearNuevoTerceroEvent;
            this.ViewModel.EditarTerceroEvent += this.ViewModelEditarTerceroEvent;
            this.ViewModel.BuscarServicioEvent += this.ViewModelBuscarServicioEvent;
            this.ViewModel.LiquidacionPagoEvent += this.ViewModelControllerMostrarVentanaPago;
            this.ViewModel.ConsultarExamenesEvent += this.ViewModelConsultarExamenesEvent;
            this.ViewModel.ProgramarNuevaCitaEvent += this.ViewModelProgramarNuevaCitaEvent;
            this.ViewModel.LimpiarVista += this.ViewModelLimpiarVista;
            this.ViewModel.MostrarMensajeEvent += (s, p) => MessageBox.Show(this.Window, p);
            this.ViewModel.MostrarVentanaReporteEvent += this.ViewModelMostrarVentanaReporteEvent;
            this.ViewModel.SavedOscEvent += this.ViewModelSavedOscEvent;
            this.window.ListadoOscControl.MostrarVentanaReporteEvent += this.ViewModelMostrarVentanaReporteEvent;
            this.window.ListadoOscControl.ImprimirLabelsEvent += this.ViewModelImprimirLabelsEvent;
            this.ViewModel.SolicitarCredencialesEvent += this.ViewModelSolicitarCredencialesEvent;
        }

        private void ViewModelControllerMostrarVentanaPago(object sender, string parametro1, object parametro2)
        {
            var viewModelOriginal = parametro2 as MainWindowViewModel;
            if (viewModelOriginal != null)
            {
                var pagoWindow = new LiquidacionPagoWindow() { Owner = this.Window };
                pagoWindow.ViewModel.OriginalViewModel = viewModelOriginal.PaymentDetails;

                pagoWindow.ShowDialog();

            }
        }

        public MainWindow Window
        {
            get
            {
                return this.window;
            }

            set
            {
                this.window = value;
            }
        }

        public MainWindowViewModel ViewModel { get; set; }

        private void ViewModelProgramarNuevaCitaEvent(object sender, string parametro)
        {
            var programarCitaWindow = new ControlDeCitas { Owner = this.Window };
            programarCitaWindow.Closed += (s, e) =>
                {
                    var controlDeCitas = s as ControlDeCitas;

                    if (controlDeCitas != null && controlDeCitas.DialogResult.HasValue && controlDeCitas.DialogResult.Value)
                    {
                        this.ViewModel.CitaSeleccionada = controlDeCitas.ViewModel.Cita;
                    }
                };
            programarCitaWindow.ShowDialog();
        }

        private void ViewModelSavedOscEvent(object sender, string parametro1, object parametro2)
        {
            var saveOscWindow = new SaveOscResult { Owner = this.Window };
            saveOscWindow.ViewModel.Result = parametro2 as OscInsertaResult;
            saveOscWindow.Title = parametro1;
            saveOscWindow.Closed += (ss, ee) =>
            {
                //// this.ViewModel.EstaLogeado = ConfiguracionGlobal.IPrincipalActual is NeuronCloudPrincipal;
                {
                    if (App.ImprimirDespuesDeGuardar)
                    {
                        var resultadoint = parametro2 as OscInsertaResult;

                        if (resultadoint != null)
                        {
                            this.ViewModelMostrarVentanaReporteEvent(null, resultadoint.ListaOsc, resultadoint.ListaFactura);
                        }
                    }
                }
            };

            saveOscWindow.ShowDialog();
        }

        private void ViewModelSolicitarCredencialesEvent(object sender, EventArgs e)
        {
            var autorizarWindow = new AutorizacionWindow { Owner = this.Window };
            autorizarWindow.ViewModel.AutenticarMethod = this.Autenticar;
            autorizarWindow.Closed += (ss, ee) =>
                {
                    this.ViewModel.EstaLogeado = ConfiguracionGlobal.IPrincipalActual is NeuronCloudPrincipal;
                    
                };
            autorizarWindow.ShowDialog();
        }

        private bool Autenticar(string login, string pass)
        {
            try
            {
                if (Membership.ValidateUser(login, pass))
                {
                    var usuarioActual = new NeuronCloudIdentity(login);
                    var principal = new NeuronCloudPrincipal(usuarioActual);
                    ConfiguracionGlobal.SetIPrincipalActual(principal);
                    Thread.CurrentPrincipal = ConfiguracionGlobal.IPrincipalActual;
                    StoreProceduresAsync.GetClaimsAsync(login,
                                   (sender, args) =>
                                   {
                                       if (args.Cancelled)
                                       {
                                           MessageBox.Show("Error Funciones");
                                       }
                                       else if (args.Error != null)
                                       {
                                           MessageBox.Show("Error Funciones:\n" + args.Error.Message);
                                       }
                                       else
                                       {
                                           bool resultClaims;

                                           if (args.Result is bool)
                                           {
                                               resultClaims = (bool)args.Result;
                                           }
                                           else
                                           {
                                               resultClaims = false;
                                           }

                                           if (!resultClaims)
                                           {
                                               MessageBox.Show("Error Obteniendo autorización");
                                           }

                                           this.ViewModel.UpdateClaims();
                                       }
                                       
                                   });

                    return true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error en Validacion: " + exception.Message);
            }

            return false;
        }

        private void ViewModelMostrarVentanaReporteEvent(object sender, string listaOsc, string listaFactura)
        {
            if (string.IsNullOrWhiteSpace(listaOsc))
            {
                return;
            }

            var reportesWindowAux = new VerReportes { Owner = this.Window, Title = "Impresion OSC y FACTURA" };

            reportesWindowAux.Loaded += (ss, ee) =>
            {
                var datosOsc = new Dictionary<string, DataTable>();
                var datosFactura = new Dictionary<string, DataTable>();
                var dataset = new NeuronDataSet();

                var ta = new REPO_OSCSeleccionaTableAdapter { ClearBeforeFill = true };
                ta.Fill(dataset.REPO_OSCSelecciona, listaOsc);

                if (!string.IsNullOrWhiteSpace(listaFactura))
                {
                    var ta2 = new REPO_FacturaOSCDetProceElemSeleccTableAdapter { ClearBeforeFill = true };
                    ta2.Fill(dataset.REPO_FacturaOSCDetProceElemSelecc, listaFactura);
                }
                else
                {
                    var ta2 = new REPO_FacturaOSCDetProceElemSeleccTableAdapter { ClearBeforeFill = true };
                    ta2.Fill(dataset.REPO_FacturaOSCDetProceElemSelecc, "");
                }

                datosOsc.Add("DSetOSC", dataset.REPO_OSCSelecciona);
                datosFactura.Add("DSetFactura", dataset.REPO_FacturaOSCDetProceElemSelecc);

                var reportesWindow = reportesWindowAux;

                reportesWindow.UbicacionReporteOsc = App.ReporteOsc;
                reportesWindow.UbicacionReporteFactura = App.ReporteFactura;
                reportesWindow.DatosOsc = datosOsc;
                reportesWindow.DatosFactura = datosFactura;


                if (!reportesWindow.IsReportedLoaded)
                {
                    foreach (KeyValuePair<string, DataTable> dataTable in reportesWindow.DatosOsc)
                    {
                        var reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource { Name = dataTable.Key, Value = dataTable.Value };
                        reportesWindow.ReportViewerOsc.LocalReport.DataSources.Add(reportDataSource);
                    }

                    foreach (KeyValuePair<string, DataTable> dataTable in reportesWindow.DatosFactura)
                    {
                        var reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource { Name = dataTable.Key, Value = dataTable.Value };
                        reportesWindow.ReportViewerFactura.LocalReport.DataSources.Add(reportDataSource);
                    }

                    reportesWindow.ReportViewerOsc.LocalReport.ReportPath = reportesWindow.UbicacionReporteOsc;
                    reportesWindow.ReportViewerFactura.LocalReport.ReportPath = reportesWindow.UbicacionReporteFactura;
                    reportesWindow.ReportViewerOsc.RefreshReport();
                    reportesWindow.ReportViewerFactura.RefreshReport();
                    reportesWindow.IsReportedLoaded = true;
                }
            };
            reportesWindowAux.ShowDialog();
        }
        private void ViewModelImprimirLabelsEvent(object sender, string vacio, object ordenDeServicio)
        {
            var orden = ordenDeServicio as NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.OrdenDeServicio;
            if (orden != null)
            {
                var patientInfo = new HIS.Models.Common.PatientInfo
                {
                    LastName = orden.PrimerApellido,
                    FirstName = orden.PrimerNombre,
                    IdDocumentType = orden.NumeroUnicoDocumento,
                    FullName = orden.PrimerApellido
                };
                this.ViewModel.PrintLabel(orden.DocUnicoOsc, 0, patientInfo);
            }



        }

        private void ViewModelCrearNuevoTerceroEvent(object sender, string parametro1, object parametro2)
        {
            var tipoIdentificacion = parametro2 as TipoIdentificacion;

            if (tipoIdentificacion != null)
            {
                var crearTerceroWindows = new CrearEditarTercero { Owner = this.Window };
                crearTerceroWindows.ViewModel.TerceroAEditar.NumeroDocumento = parametro1;
                crearTerceroWindows.ViewModel.TerceroAEditar.TipoDocumento = tipoIdentificacion;
                crearTerceroWindows.Closed += (s, e) =>
                {
                    var crearTerceroWindow = s as CrearEditarTercero;

                    if (crearTerceroWindow != null && crearTerceroWindow.DialogResult.HasValue
                        && crearTerceroWindows.DialogResult.Value)
                    {
                        this.ViewModel.NuevoTerceroCreado(crearTerceroWindow.ViewModel.TerceroAEditar);
                    }
                };

                crearTerceroWindows.ShowDialog();
            }
        }

        private void ViewModelEditarTerceroEvent(object sender, string parametro1, object parametro2)
        {
            var tipoIdentificacion = parametro2 as TipoIdentificacion;

            if (tipoIdentificacion != null)
            {
                var crearTerceroWindows = new CrearEditarTercero { Owner = this.Window };
                crearTerceroWindows.ViewModel.TerceroAEditar.NumeroDocumento = parametro1;
                crearTerceroWindows.ViewModel.TerceroAEditar.TipoDocumento = tipoIdentificacion;
                crearTerceroWindows.ViewModel.GetTerceroAction();
                crearTerceroWindows.Closed += (s, e) =>
                {
                    var crearTerceroWindow = s as CrearEditarTercero;

                    if (crearTerceroWindow != null && crearTerceroWindow.DialogResult.HasValue
                        && crearTerceroWindows.DialogResult.Value)
                    {
                        this.ViewModel.NuevoTerceroCreado(crearTerceroWindow.ViewModel.TerceroAEditar);
                    }
                };

                crearTerceroWindows.ShowDialog();
            }
        }

        private void ViewModelLimpiarVista(object sender, EventArgs e)
        {
            this.Window.abPersonalAsistencial.Text = string.Empty;
            this.Window.abDiagnostico.Text = string.Empty;
        }

        private void ViewModelConsultarExamenesEvent(object sender, string parametro1, object parametro2)
        {
            var examenesPrevios = parametro2 as IList<Antecedente>;

            if (examenesPrevios != null)
            {
                var buscarServicioWindow = new AlertDetailWindow(examenesPrevios) { Owner = this.Window };

                buscarServicioWindow.Closed += (s, e) =>
                {
                };

                buscarServicioWindow.ShowDialog();
            }
        }

        private void ViewModelBuscarServicioEvent(object sender, string parametro1, object parametro2)
        {
            var parametros = parametro2 as SearchServiceParameters;

            if (parametros != null)
            {
                var buscarServicioWindow = new SearchServiceWindow { Owner = this.Window };
                buscarServicioWindow.ViewModel.Parameters = parametros;
                buscarServicioWindow.Closed += (s, e) =>
                {
                    var buscarServicioWindowAux = s as SearchServiceWindow;

                    if (buscarServicioWindowAux != null && buscarServicioWindowAux.DialogResult.HasValue
                        && buscarServicioWindowAux.DialogResult.Value)
                    {
                        this.ViewModel.CodeToSearch = buscarServicioWindowAux.ViewModel.SelectedSugestion.Codigo;
                    }
                };

                buscarServicioWindow.ShowDialog();
            }
        }
    }
}
