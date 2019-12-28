// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   View Model for the Main Window
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.ViewModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Xml;
    using System.Xml.Serialization;

    using Atpc.Common;

    using Lysis.Commom.BarCode;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using Neuron.HIS.Models.Common;
    using Neuron.OSC.Data;
    using Neuron.OSC.Data.AsyncOps;
    using Neuron.OSC.Data.Model;
    using Neuron.OSC.Ws.LysisService;
    using Neuron.UI.Controls.Models;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;

    using Tercero = NeuronCloud.Atpc.Co.Modelos.Tercero;
    using TipoIdentificacion = NeuronCloud.Atpc.Co.Modelos.TipoIdentificacion;
    using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

    /// <summary>
    /// View Model for the Main Window
    /// </summary>
    public sealed class MainWindowViewModel : NotificationObject, IDataErrorInfo
    {
        ////public static readonly DependencyProperty MainWindowProperty = DependencyProperty.Register("MainWindow", typeof(Window), typeof(MainWindowViewModel), new PropertyMetadata(default(Window), MainWindowsChanged));
        private static readonly object SyncLock = new object();

        private readonly ObservableCollection<TipoIdentificacion> tipoIdentificacionList = new ObservableCollection<TipoIdentificacion>();

        private readonly ObservableCollection<ServiceAgreement> agreementsByPatient = new ObservableCollection<ServiceAgreement>();

        private readonly ObservableCollection<string> alertsAndWarningsCollection = new ObservableCollection<string>();

        private readonly ObservableCollection<ServiceProvider> providersByAgreement = new ObservableCollection<ServiceProvider>();

        private readonly ObservableCollection<ServiceRequester> requestersByAgreement = new ObservableCollection<ServiceRequester>();

        private readonly ObservableCollection<OscDataModel.PRO_OSCDFLiquidaPagoRow> liquidationValues = new ObservableCollection<OscDataModel.PRO_OSCDFLiquidaPagoRow>();

        private readonly ListCollectionView requestersByAgreementView;

        private readonly ObservableCollection<CentroDeTomaMuestra> centrosDeToma = new ObservableCollection<CentroDeTomaMuestra>();

        private readonly ObservableCollection<Antecedente> antecedentes = new ObservableCollection<Antecedente>();

        private readonly ObservableCollection<ServiceUnit> serviceUnitsByRequester = new ObservableCollection<ServiceUnit>();

        private readonly ObservableCollection<Service> servicesByRequester = new ObservableCollection<Service>();

        private readonly ObservableCollection<Diagnose> sugestedDiagnoses = new ObservableCollection<Diagnose>();

        private readonly ObservableCollection<string> prefijosOSCDisponibles = new ObservableCollection<string>();

        private readonly ObservableCollection<CRUDEntity<Service>> selectedServices = new ObservableCollection<CRUDEntity<Service>>();

        ////private readonly ObservableCollection<CRUD_PersonalAsistencialSelecciona_Result> listaPersonalAsistencial = new ObservableCollection<CRUD_PersonalAsistencialSelecciona_Result>();

        private readonly ObservableCollection<PersonalAsistencial> listaPersonalAsistencial;

        ////private readonly CentroDeTomaMuestra defaultCentroDeToma;

        private readonly AtpcBindableCommand saveCommand;

        private readonly AtpcBindableCommand saveFingerPrint;

        private readonly DelegateCommand guardarEdad;

        private readonly DelegateCommand programarNuevaCita;

        private readonly DelegateCommand recalcularConNuevoConvenio;

        private readonly DelegateCommand solicitarCredenciales;

        private readonly DelegateCommand buscarServicioPorNombreCommand;

        private readonly DelegateCommand _liquidacionPagoCommand;

        private readonly DelegateCommand consultarExamenesCommand;

        private readonly DelegateCommand editarTerceroCommand;

        private PatientInfo currentPatient = new PatientInfo();

        private ServiceAgreement selectedAgreement = new ServiceAgreement();

        private ServiceProvider selectedProvider;

        private ServiceRequester selectedRequester;

        private ServiceUnit selectedServiceUnit;

        private CRUDEntity<Service> selectedService;

        private bool currentPatientIsBusy;

        private TipoIdentificacion typeDocIdSearch;

        private string docIdSearch;

        private string correoElectronico;

        private bool enviarPDF;

        private bool programarCita;

        private bool searchingAgreements;

        private bool searchingRequesters;

        internal void UpdateClaims()
        {
            this.CambioDescuento = PuedeCambiarTipoDescuento();
            this.RaisePropertyChanged("CambioDescuento");
            this.ChangeValueItem = CanChangeValueItem();
            this.RaisePropertyChanged("ChangeValueItem");
        }

        private bool searchingProviders;

        private bool searchingServiceUnits;

        private bool isAutenticated;

        private string codeToSearch;

        private double total;

        private double totalManual;

        private Window mainWindow;

        private bool imprimirOGuardar;

        private bool cambiarValorItem;

        private Diagnose selectedDiagnose;

        private bool sugestedDiagnosesLoaded;

        private bool sugestedDiagnosesIsLoading;

        private string diagnoseParameter;

        private PersonalAsistencial selectedPersonalAsistencial;

        private string personalAsistencialParameter;

        private string idSistema;

        private string numeroAutorizacion;

        private string observaciones;

        private double vrDcto;

        private double porcentajeDcto;

        private string noOrdenMedica;

        private bool documentoEncontrado;

        private string noCitaSearch;

        private bool mostrarControlesAsignacionNumeroMuestra;

        private NumeroDeMuestra numeroDeMuestraAsignado = new NumeroDeMuestra();

        private string oscPrefix;

        private bool enEmbarazo;

        private DateTime? fur;

        private string nivel;

        private DateTime? edadAGuardar;

        private DateTime? fechaHoraCita;

        private Cita citaSeleccionada;

        private bool estaLogeado;

        private string busquedaRemitente;

        private bool totalModificable;

        private bool totalCambio;

        private bool selectedServiceHasDetails;

        private bool buscarServiciosPrevios;

        private bool hayAlerta;

        private bool idle;

        private bool savingAddress;

        private bool savingPhoneNumber;

        private bool firstLoad;

        private string direccion;

        private string telefono;

        private decimal totalLiquidacion;

        private string totalItem;

        private bool enableEditTercero;

        private ServiceProvider tempProvider;

        private ServiceRequester tempRequester;
        private bool _esNumerico;
        private string _descuentolabel;
        private readonly PaymentDetails _paymentDetails = new PaymentDetails();

        public MainWindowViewModel()
        {
            this.VrDcto = 0.0;
            this.EsNumerico = false;
            this.Idle = true;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }


            EnableCollectionSynchronization(this.alertsAndWarningsCollection, SyncLock);
            this.PrefijosOscDisponibles.CollectionChanged += (sender, args) => this.RaisePropertyChanged("SeleccionarPrefijoOSC");
            this.ServiceUnitsByRequester.CollectionChanged += (sender, args) => this.RaisePropertyChanged("MostrarServicios");

            this.CargarCentrosDeTomaManuales();

            if (App.AsigNoMuestra == AsigNoMuestra.DIA || App.AsigNoMuestra == AsigNoMuestra.AUTO)
            {
                this.MostrarControlesAsignacionNumeroMuestra = true;
            }

            var fecha = DateTime.Today;
            this.NumeroDeMuestraAsignado.TipoAsignacion = App.AsigNoMuestra;
            if (!string.IsNullOrWhiteSpace(App.PrefijoCentroTomaDefault))
            {
                this.NumeroDeMuestraAsignado.CentroDeToma = this.CentrosDeToma.FirstOrDefault(ctm => ctm.Prefijo == App.PrefijoCentroTomaDefault)
                                                            ?? this.CentrosDeToma.FirstOrDefault();
            }

            this.NumeroDeMuestraAsignado.Dia = fecha.Day;
            this.NumeroDeMuestraAsignado.Mes = fecha.Month;

            this.Imprimir = App.ImprimirDespuesDeGuardar;
            this.CambiarValorItem = App.CambiarValorItem;

            
            this.SelectedServices.CollectionChanged += (sss, eee) =>
                {
                    this.TotalCambio = true;
                    this.RaisePropertyChanged("Total");
                    this.RaisePropertyChanged("TotalAPagar");
                    this.RaisePropertyChanged("TotalManual");
                };

            this.LiquidationValues.CollectionChanged += (sender, args) =>
                {
                    this.RaisePropertyChanged("TotalLiquidacion");
                    this.RaisePropertyChanged("TotalAPagar");
                    this.RaisePropertyChanged("TotalItem");
                    this.LiquidacionPagoCommand.RaiseCanExecuteChanged();
                };

            this.SelectedService = new CRUDEntity<Service>(new Service { Name = string.Empty }, false);
            this.saveCommand = new AtpcBindableCommand(this.SaveOSC);
            this.saveFingerPrint = new AtpcBindableCommand(this.SaveFinger);
            this.guardarEdad = new DelegateCommand(this.GuardarEdadExecute, this.GuardarEdadCanExecute);
            this.programarNuevaCita = new DelegateCommand(this.ProgramarNuevaCitaExecute, this.ProgramarNuevaCitaCanExecute);
            this.recalcularConNuevoConvenio = new DelegateCommand(this.RecalcularConNuevoConvenioExecute, this.RecalcularConNuevoConvenioCanExecute);

            this.solicitarCredenciales = new DelegateCommand(this.SolicitarCredencialesExecute);
            this.buscarServicioPorNombreCommand = new DelegateCommand(this.BuscarServicioExecute, this.BuscarServicioCanExecute);
            this._liquidacionPagoCommand = new DelegateCommand(this.LiquidacionPagoExecute, this.LiquidacionPagoCanExecute);
            this.consultarExamenesCommand = new DelegateCommand(this.ConsultarExamenesExecuted, this.ConsultarExamenesCanExecuted);
            this.editarTerceroCommand = new DelegateCommand(this.EditarTerceroExecuted, this.EditarTerceroCanExecuted);
            this.EnableEditTercero = App.EnableEditTercero;


            if (!GlobalModelosCache.PersonalAsistencialListaInicializada)
            {
                this.LoadPersonalAsistencial();
            }

            this.listaPersonalAsistencial = GlobalModelosCache.PersonalAsistencialList;

            GeneralOps.GetAsync(
                (s, e) =>
                {
                    if (e.Cancelled)
                    {
                    }
                    else if (e.Error != null)
                    {
                    }
                    else
                    {
                        this.tipoIdentificacionList.Clear();
                        var list = e.Result as List<TipoIdentificacion>;

                        if (list != null)
                        {
                            foreach (var s1 in list)
                            {
                                this.tipoIdentificacionList.Add(s1);
                            }
                        }

                        GlobalModelosCache.RefreshTipoDeIdentifiacionList(this.TipoIdentificacionList);
                    }
                });

            GeneralOps.GetPaisAsync(
                       (s, e) =>
                       {
                           if (e.Cancelled)
                           {
                           }
                           else if (e.Error != null)
                           {
                           }
                           else
                           {
                               var paises = e.Result as List<NeuronCloud.Atpc.Co.Modelos.Pais>;

                               if (paises != null)
                               {
                                   GlobalModelosCache.RefreshPaisList(paises);
                               }
                           }
                       });

            GeneralOps.GetDepartamentosAsync(
                       (s, e) =>
                       {
                           if (e.Cancelled)
                           {
                           }
                           else if (e.Error != null)
                           {
                           }
                           else
                           {
                               var departamentos = e.Result as List<NeuronCloud.Atpc.Co.Modelos.Departamento>;

                               if (departamentos != null)
                               {
                                   GlobalModelosCache.RefreshDepartamentoList(departamentos);
                               }
                           }
                       });

            GeneralOps.GetMunicipiosAsync(
                       (s, e) =>
                       {
                           if (e.Cancelled)
                           {
                           }
                           else if (e.Error != null)
                           {
                           }
                           else
                           {
                               var municipios = e.Result as List<NeuronCloud.Atpc.Co.Modelos.Municipio>;

                               if (municipios != null)
                               {
                                   GlobalModelosCache.RefreshMunicipiosList(municipios);
                               }
                           }
                       });

            GeneralOps.GetFormasDePagoAsync(
            (s, e) =>
            {
                if (e.Cancelled)
                {
                }
                else if (e.Error != null)
                {
                    Trace.TraceError("Error Cargando Formas de Pago:{0}", e.Error.Message);
                }
                else
                {
                    var formasDePago = e.Result as List<FormaDePago>;
                    if (formasDePago != null)
                    {
                        foreach (var formaDePago in formasDePago)
                        {
                            if (formaDePago.MedioDePago.GetValueOrDefault(false))
                            {
                                GeneralOps.GetTipoPagoAsync(
                                    formaDePago.TipoPago,
                                    (ss, ee) =>
                                    {
                                        if (ee.Cancelled)
                                        {
                                        }
                                        else if (ee.Error != null)
                                        {
                                            Trace.TraceError("Error Cargando Tipos de Pago:{0}", ee.Error.Message);
                                        }
                                        else
                                        {
                                            var tiposDePago = ee.Result as List<TipoPago>;
                                            if (tiposDePago != null)
                                            {
                                                formaDePago.MediosDePago = tiposDePago;
                                            }
                                        }

                                    });
                            }
                        }

                        GlobalModelosCache.RefreshFormasDePagoList(formasDePago);
                    }
                }
            });

            this.SugestedDiagnoses.Add(new Diagnose { Code = "1", FullName = "1212", Name = "test" });
            this.SugestedDiagnoses.Add(new Diagnose { Code = "1", FullName = "121w2", Name = "test" });
            this.SugestedDiagnoses.Add(new Diagnose { Code = "1", FullName = "12wqe12", Name = "test" });
            this.SugestedDiagnoses.Add(new Diagnose { Code = "1", FullName = "12weqw12", Name = "test" });
            this.SugestedDiagnoses.Add(new Diagnose { Code = "1", FullName = "1qwe212", Name = "test" });
            this.requestersByAgreementView = CollectionViewSource.GetDefaultView(this.RequestersByAgreement) as ListCollectionView;
            if (this.requestersByAgreementView != null)
            {
                this.requestersByAgreementView.Filter = this.FiltrarRemitentes;
            }

            this.RaisePropertyChanged("MostrarPersonalAsistencial");
            this._paymentDetails = new PaymentDetails();
            this.PaymentDetails.LiquidationItems = this.LiquidationValues;
        }
        public byte[] HuellaActual { get; set; }
        private void RecalcularConNuevoConvenioExecute()
        {
            var temporal = new ObservableCollection<CRUDEntity<Service>>();
            foreach (var servicio in this.SelectedServices)
            {
                temporal.Add(servicio);
            }

            var listaTareas = new Dictionary<Guid, Boolean>();


            foreach (var servicioAReemplazar in temporal)
            {
                if (this.SelectedAgreement != null && this.SelectedProvider != null && this.SelectedRequester != null && !string.IsNullOrWhiteSpace(servicioAReemplazar.BaseEntity.Code))
                {


                    ////this.SearchingServiceUnits = true;
                    var ID = Guid.NewGuid();
                    listaTareas.Add(ID, false);
                    this.SelectedService = new CRUDEntity<Service>(new Service { Name = "Buscando..." }, false) { IsBusy = true };
                    StoreProceduresAsync.GetServiceByCode(
                    new SearchServiceParameters
                    {
                        Provider = this.SelectedProvider,
                        Code = servicioAReemplazar.BaseEntity.Code,
                        ServiceAgreement = this.SelectedAgreement,
                        ServiceRequester = this.SelectedRequester,
                        Gender = this.CurrentPatient.Gender
                    },
                    (ss, ee) =>
                    {
                        if (listaTareas.ContainsKey(ID))
                        {
                            listaTareas[ID] = true;
                        }

                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var service = ee.Result as Service;
                            if (service != null)
                            {
                                var servicioEncontrado = new CRUDEntity<Service>(service, true);
                                if (this.Antecedentes.Any(ant => ant.CodigoFuente == this.selectedService.BaseEntity.Code))
                                {
                                    this.SelectedServiceHasDetails = true;
                                }
                                servicioEncontrado.BaseEntity.Quantity = servicioAReemplazar.BaseEntity.Quantity;
                                this.SelectedServices.Add(new CRUDEntity<Service>(servicioEncontrado.BaseEntity.Copy(), true));
                                this.SelectedServices.Remove(servicioAReemplazar);
                            }
                            else
                            {
                                this.SelectedServices.Remove(servicioAReemplazar);
                            }
                        }
                        if (validarLista(listaTareas))
                        {
                            this.UpdateLiquidation();
                        }
                        ////this.SearchingServiceUnits = false;
                    });
                }
            }
        }

        private bool validarLista(Dictionary<Guid, Boolean> lista)
        {
            foreach (var item in lista)
            {
                if (item.Value == false)
                {
                    return false;
                }
            }
            return true;
        }

        private bool RecalcularConNuevoConvenioCanExecute()
        {
            return true;
        }

        public event ParametroTextoObjetoEventHandler CrearNuevoTerceroEvent;

        public event ParametroTextoObjetoEventHandler EditarTerceroEvent;

        public event ParametroTextoObjetoEventHandler SavedOscEvent;

        public event ParametroTextoEventHandler ProgramarNuevaCitaEvent;

        public event ParametroTextoEventHandler MostrarMensajeEvent;

        public event ParametroTextoTextoEventHandler MostrarVentanaReporteEvent;

        public event EventHandler LimpiarVista;

        public event EventHandler DiagnosticosCargados;

        public event EventHandler PersonalAsistencialEvento;


        public event ParametroTextoObjetoEventHandler BuscarServicioEvent;

        public event ParametroTextoObjetoEventHandler LiquidacionPagoEvent;

        public event ParametroTextoObjetoEventHandler ConsultarExamenesEvent;

        public event EventHandler SolicitarCredencialesEvent;

        public bool MostrarControlesAsignacionNumeroMuestra
        {
            get
            {
                return this.mostrarControlesAsignacionNumeroMuestra;
            }

            set
            {
                if (value.Equals(this.mostrarControlesAsignacionNumeroMuestra))
                {
                    return;
                }

                this.mostrarControlesAsignacionNumeroMuestra = value;
                this.RaisePropertyChanged("MostrarControlesAsignacionNumeroMuestra");
            }
        }

        public Window MainWindow
        {
            get
            {
                return this.mainWindow;
            }

            set
            {
                if (value != this.mainWindow)
                {
                    this.mainWindow = value;
                    this.RaisePropertyChanged("MainWindow");
                    this.MainWindowChanged();
                }
            }
        }

        public string BusquedaRemitente
        {
            get
            {
                return this.busquedaRemitente;
            }

            set
            {
                if (value == this.busquedaRemitente)
                {
                    return;
                }

                this.busquedaRemitente = value;
                this.RaisePropertyChanged("BusquedaRemitente");
                this.RequestersByAgreementView.Refresh();
                if (this.RequestersByAgreementView.Count == 1)
                {
                    this.SeleccionarRemitente(NeuronCloud.Atpc.Co.Modelos.Auxiliares.Direccion.Ninguna);
                }
            }
        }

        public AtpcBindableCommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
        }
        public AtpcBindableCommand SaveFingerPrint
        {
            get
            {
                return this.saveFingerPrint;
            }
        }

        public DelegateCommand GuardarEdad
        {
            get
            {
                return this.guardarEdad;
            }
        }

        public DelegateCommand BuscarServicioPorNombreCommand
        {
            get
            {
                return this.buscarServicioPorNombreCommand;
            }
        }

        public DelegateCommand LiquidacionPagoCommand => this._liquidacionPagoCommand;

        public DelegateCommand ConsultarExamenesCommand
        {
            get
            {
                return this.consultarExamenesCommand;
            }
        }

        public DelegateCommand EditarTerceroCommand
        {
            get
            {
                return this.editarTerceroCommand;
            }
        }

        public DelegateCommand ProgramarNuevaCita
        {
            get
            {
                return this.programarNuevaCita;
            }
        }

        public DelegateCommand RecalcularConNuevoConvenio
        {
            get
            {
                return this.recalcularConNuevoConvenio;
            }
        }

        public DelegateCommand SolicitarCredencialesCommand
        {
            get
            {
                return this.solicitarCredenciales;
            }
        }

        public double Total
        {
            get
            {
                return this.SelectedServices.Sum(service => service.BaseEntity.Total);
            }

            set
            {
                this.total = value;
            }
        }

        public decimal TotalLiquidacion
        {
            get
            {
                return this.LiquidationValues.Sum(service => service.Valor);
            }

            set
            {
                this.totalLiquidacion = value;
            }
        }
        public string TotalItem
        {
            get
            {
                //return this.SelectedServices.Count;
                return string.Format("{0} {1}", "Items: ", Convert.ToString(this.SelectedServices.Count));
            }

            set
            {
                this.totalItem = value;
            }
        }

        public bool TotalModificable
        {
            get
            {
                return this.totalModificable;
            }

            set
            {
                if (value.Equals(this.totalModificable))
                {
                    return;
                }

                this.totalModificable = value;
                this.RaisePropertyChanged("TotalModificable");
                this.RaisePropertyChanged("TotalAPagar");
            }
        }

        public bool EnableEditTercero
        {
            get
            {
                return this.enableEditTercero;
            }

            set
            {
                if (value.Equals(this.enableEditTercero))
                {
                    return;
                }

                this.enableEditTercero = value;
                this.RaisePropertyChanged("EnableEditTercero");
            }
        }

        public bool TotalCambio
        {
            get
            {
                return this.totalCambio;
            }

            set
            {
                if (value.Equals(this.totalCambio))
                {
                    return;
                }

                this.totalCambio = value;
                this.RaisePropertyChanged("TotalCambio");
            }
        }

        public bool SavingAddress
        {
            get
            {
                return this.savingAddress;
            }
            set
            {
                if (value.Equals(this.savingAddress))
                {
                    return;
                }
                this.savingAddress = value;
                this.RaisePropertyChanged("SavingAddress");
            }
        }

        public bool SavingPhoneNumber
        {
            get
            {
                return this.savingPhoneNumber;
            }

            set
            {
                if (value.Equals(this.savingPhoneNumber))
                {
                    return;
                }

                this.savingPhoneNumber = value;
                this.RaisePropertyChanged("SavingPhoneNumber");
            }
        }

        public bool Idle
        {
            get
            {
                return this.idle;
            }

            set
            {
                if (value.Equals(this.idle))
                {
                    return;
                }

                this.idle = value;
                this.RaisePropertyChanged("Idle");
            }
        }

        public double TotalManual
        {
            get
            {
                if (this.TotalCambio)
                {
                    this.totalManual = this.Total - this.VrDcto;
                    this.TotalCambio = false;
                }

                return this.totalManual;
            }

            set
            {
                if (value.Equals(this.totalManual))
                {
                    return;
                }

                this.totalManual = value;
                this.RaisePropertyChanged("TotalManual");
                this.RaisePropertyChanged("TotalAPagar");
            }
        }

        public PatientInfo CurrentPatient
        {
            get
            {
                return this.currentPatient;
            }

            set
            {
                if (value != this.currentPatient)
                {
                    this.currentPatient = value;
                    this.RaisePropertyChanged("CurrentPatient");
                    this.RaisePropertyChanged("TerceroExacto");
                    this.RaisePropertyChanged("SolicitarEstadoEmbarazo");
                    this.RaisePropertyChanged("SolicitarFUR");
                    this.RaisePropertyChanged("PermitirEditarEdad");
                    this.EditarTerceroCommand.RaiseCanExecuteChanged();
                    this.SearchAgreements();
                }
            }
        }

        public CRUD_TerceroSeleccionaExacto_Result TerceroExacto
        {
            get
            {
                var patientInfo = this.CurrentPatient;
                if (patientInfo != null)
                {
                    var exactoResult = patientInfo.OriginalObject as CRUD_TerceroSeleccionaExacto_Result;

                    if (exactoResult != null)
                    {
                        return exactoResult;
                    }
                }

                return null;
            }
        }

        public PRO_SearchBooking_Result Booking
        {
            get
            {
                var bookingInfo = this.CurrentPatient;
                if (bookingInfo != null)
                {
                    var exactoResult = bookingInfo.OriginalObject as PRO_SearchBooking_Result;

                    if (exactoResult != null)
                    {
                        return exactoResult;
                    }
                }

                return null;
            }
        }

        public string OscPrefix
        {
            get
            {
                return this.oscPrefix;
            }

            set
            {
                if (value == this.oscPrefix)
                {
                    return;
                }

                this.oscPrefix = value;
                this.RaisePropertyChanged("OscPrefix");
            }
        }

        public ObservableCollection<string> PrefijosOscDisponibles
        {
            get
            {
                return this.prefijosOSCDisponibles;
            }
        }

        public bool DocumentoEncontrado
        {
            get
            {
                return this.documentoEncontrado;
            }

            set
            {
                if (value.Equals(this.documentoEncontrado))
                {
                    return;
                }

                this.documentoEncontrado = value;
                this.RaisePropertyChanged("DocumentoEncontrado");
            }
        }
        public string NoCitaSearch
        {
            get
            {
                return this.noCitaSearch;
            }

            set
            {
                if (value.Equals(this.noCitaSearch))
                {
                    return;
                }

                this.noCitaSearch = value;
                this.RaisePropertyChanged("NoCitaSearch");
                this.VerifyNoCita();
            }
        }

        public string DocIdSearch
        {
            get
            {
                return this.docIdSearch;
            }

            set
            {
                if (value != this.docIdSearch)
                {
                    this.docIdSearch = value;
                    this.RaisePropertyChanged("DocIdSearch");
                    this.VerifyPatienId();
                }
            }
        }

        public TipoIdentificacion TypeDocIdSearch
        {
            get
            {
                return this.typeDocIdSearch;
            }

            set
            {
                if (value != this.typeDocIdSearch)
                {
                    this.typeDocIdSearch = value;
                    this.RaisePropertyChanged("TypeDocIdSearch");
                    this.VerifyPatienId();
                }
            }
        }

        public string CorreoElectronico
        {
            get
            {
                return this.correoElectronico;
            }

            set
            {
                if (value != this.correoElectronico)
                {
                    this.correoElectronico = value;
                    this.RaisePropertyChanged("CorreoElectronico");
                }
            }
        }

        public string Direccion
        {
            get
            {
                return this.direccion;
            }

            set
            {
                if (value == this.direccion)
                {
                    return;
                }
                this.direccion = value;
                this.RaisePropertyChanged("Direccion");
                if (!firstLoad)
                {
                    this.SaveAddress();
                }
            }
        }

        public string Telefono
        {
            get
            {
                return this.telefono;
            }

            set
            {
                if (value == this.telefono)
                {
                    return;
                }
                this.telefono = value;
                this.RaisePropertyChanged("Telefono");
                if (!firstLoad)
                {
                    this.SavePhoneNumber();
                }
            }
        }

        public string IdSistema
        {
            get
            {
                return this.idSistema;
            }

            set
            {
                if (value != this.idSistema)
                {
                    this.idSistema = value;
                    this.RaisePropertyChanged("IdSistema");
                }
            }
        }

        public string NoOrdenMedica
        {
            get
            {
                return this.noOrdenMedica;
            }

            set
            {
                if (value == this.noOrdenMedica)
                {
                    return;
                }

                this.noOrdenMedica = value;
                this.RaisePropertyChanged("NoOrdenMedica");
            }
        }

        public string CodeToSearch
        {
            get
            {
                return this.codeToSearch;
            }

            set
            {
                if (value != this.codeToSearch)
                {
                    this.codeToSearch = value;
                    this.RaisePropertyChanged("CodeToSearch");
                    this.SearchService();
                }
            }
        }
        public bool EsNumerico
        {
            get
            {
                return _esNumerico;
            }
            set
            {
                _esNumerico = value;
                this.RaisePropertyChanged("EsNumerico");

                this.TotalCambio = true;
                this.RaisePropertyChanged("VrDcto");
                this.RaisePropertyChanged("TotalAPagar");
                this.RaisePropertyChanged("TotalManual");
                this.Descuentolabel = this.EsNumerico ? "Dcto" : "% Dcto";
                this.UpdateLiquidation();

            }
        }
        public double VrDcto
        {
            get
            {
                return this.vrDcto;
            }

            set
            {
                this.vrDcto = value;
                this.TotalCambio = true;
                this.RaisePropertyChanged("VrDcto");
                this.RaisePropertyChanged("TotalAPagar");
                this.RaisePropertyChanged("TotalManual");
                this.UpdateLiquidation();
            }
        }

        public decimal TotalAPagar
        {
            get
            {
                if (this.TotalModificable)
                {
                    return (decimal)this.TotalManual;
                }

                return this.TotalLiquidacion;
                    //- (this.EsNumerico ? (decimal)this.VrDcto : ((this.TotalLiquidacion * (decimal)this.VrDcto) / 100));
            }
        }

        public string Observaciones
        {
            get
            {
                return this.observaciones;
            }

            set
            {
                if (value != this.observaciones)
                {
                    this.observaciones = value;
                    this.RaisePropertyChanged("Observaciones");
                }
            }
        }

        public string NumeroAutorizacion
        {
            get
            {
                return this.numeroAutorizacion;
            }

            set
            {
                if (value != this.numeroAutorizacion)
                {
                    this.numeroAutorizacion = value;
                    this.RaisePropertyChanged("NumeroAutorizacion");
                }
            }
        }

        public string Nivel
        {
            get
            {
                return this.nivel;
            }

            set
            {
                if (value == this.nivel)
                {
                    return;
                }

                this.nivel = value;
                this.RaisePropertyChanged("Nivel");
                this.RaisePropertyChanged("NivelEditable");
                this.OnNivelChanged();
            }
        }

        private void OnNivelChanged()
        {
            if (this.SelectedAgreement != null)
            {
                this.SelectedAgreement.Nivel = this.Nivel;
            }
        }

        public bool HayAlerta
        {
            get
            {
                return this.hayAlerta;
            }

            set
            {
                if (value.Equals(this.hayAlerta))
                {
                    return;
                }

                this.hayAlerta = value;
                this.RaisePropertyChanged("HayAlerta");
            }
        }

        public static bool PuedeCambiarTipoDescuento()
        {
            var currentPrincipal = ConfiguracionGlobal.IPrincipalActual as NeuronCloud.Atpc.Co.Providers.Desktop.NeuronCloudPrincipal;

            if (currentPrincipal != null)
            {
                if (currentPrincipal.PermisosOsc(new Func<bool>(() => OscClaims.ValidateClaims("DESCUENTO"))))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CanChangeValueItem()
        {
            var currentPrincipal = ConfiguracionGlobal.IPrincipalActual as NeuronCloud.Atpc.Co.Providers.Desktop.NeuronCloudPrincipal;

            if (currentPrincipal != null)
            {
                if (currentPrincipal.PermisosOsc(new Func<bool>(() => OscClaims.ValidateClaims("MODVALUE"))))
                {
                    return true;
                }
            }

            return false;
        }


        public PaymentDetails PaymentDetails => this._paymentDetails;

        public ObservableCollection<TipoIdentificacion> TipoIdentificacionList
        {
            get
            {
                return this.tipoIdentificacionList;
            }
        }

        public ObservableCollection<ServiceAgreement> AgreementsByPatient
        {
            get
            {
                return this.agreementsByPatient;
            }
        }

        public ObservableCollection<string> AlertsAndWarningsCollection
        {
            get
            {
                return this.alertsAndWarningsCollection;
            }
        }

        public ObservableCollection<ServiceProvider> ProvidersByAgreement
        {
            get
            {
                return this.providersByAgreement;
            }
        }

        public ObservableCollection<ServiceRequester> RequestersByAgreement
        {
            get
            {
                return this.requestersByAgreement;
            }
        }

        public ObservableCollection<OscDataModel.PRO_OSCDFLiquidaPagoRow> LiquidationValues
        {
            get
            {
                return this.liquidationValues;
            }
        }

        public ListCollectionView RequestersByAgreementView
        {
            get
            {
                return this.requestersByAgreementView;
            }
        }

        public ObservableCollection<ServiceUnit> ServiceUnitsByRequester
        {
            get
            {
                return this.serviceUnitsByRequester;
            }
        }

        public ObservableCollection<Service> ServicesByRequester
        {
            get
            {
                return this.servicesByRequester;
            }
        }

        public ObservableCollection<CentroDeTomaMuestra> CentrosDeToma
        {
            get
            {
                return this.centrosDeToma;
            }
        }

        public ObservableCollection<Antecedente> Antecedentes
        {
            get
            {
                return this.antecedentes;
            }
        }

        public ObservableCollection<Diagnose> SugestedDiagnoses
        {
            get
            {
                return this.sugestedDiagnoses;
            }
        }

        public ObservableCollection<PersonalAsistencial> ListaPersonalAsistencial
        {
            get
            {
                return this.listaPersonalAsistencial;
            }
        }

        public ObservableCollection<CRUDEntity<Service>> SelectedServices
        {
            get
            {
                return this.selectedServices;
            }
        }

        public Diagnose SelectedDiagnose
        {
            get
            {
                return this.selectedDiagnose;
            }

            set
            {
                if (value != this.selectedDiagnose)
                {
                    this.selectedDiagnose = value;
                    this.RaisePropertyChanged("SelectedDiagnose");
                }
            }
        }

        public ServiceAgreement SelectedAgreement
        {
            get
            {
                return this.selectedAgreement;
            }

            set
            {
                if (value != this.selectedAgreement)
                {
                    this.selectedAgreement = value;
                    this.RaisePropertyChanged("SelectedAgreement");
                    this.BuscarServicioPorNombreCommand.RaiseCanExecuteChanged();
                    this.OnSelectedAgreementChanged();
                }
            }
        }

        public ServiceProvider SelectedProvider
        {
            get
            {
                return this.selectedProvider;
            }

            set
            {
                if (value != this.selectedProvider)
                {
                    this.selectedProvider = value;
                    this.RaisePropertyChanged("SelectedProvider");
                    this.BuscarServicioPorNombreCommand.RaiseCanExecuteChanged();
                    this.OnSelectedProviderChanged();
                }
            }
        }

        public ServiceRequester SelectedRequester
        {
            get
            {
                return this.selectedRequester;
            }

            set
            {
                if (value != this.selectedRequester)
                {
                    this.selectedRequester = value;
                    this.RaisePropertyChanged("SelectedRequester");
                    this.BuscarServicioPorNombreCommand.RaiseCanExecuteChanged();
                    this.OnSelectedRequesterChanged();
                }
            }
        }

        public ServiceUnit SelectedServiceUnit
        {
            get
            {
                return this.selectedServiceUnit;
            }

            set
            {
                if (value != this.selectedServiceUnit)
                {
                    this.selectedServiceUnit = value;
                    this.RaisePropertyChanged("SelectedServiceUnit");
                    this.OnSelectedServiceUnitChanged();
                }
            }
        }

        public PersonalAsistencial SelectedPersonalAsistencial
        {
            get
            {
                return this.selectedPersonalAsistencial;
            }

            set
            {
                if (value != this.selectedPersonalAsistencial)
                {
                    this.selectedPersonalAsistencial = value;
                    this.RaisePropertyChanged("SelectedPersonalAsistencial");
                }
            }
        }

        public CRUDEntity<Service> SelectedService
        {
            get
            {
                return this.selectedService;
            }

            set
            {
                if (value != this.selectedService)
                {
                    this.selectedService = value;
                    this.RaisePropertyChanged("SelectedService");
                    ////this.OnSelectedRequesterChanged();
                }
            }
        }

        public bool Imprimir
        {
            get
            {
                return this.imprimirOGuardar;
            }

            private set
            {
                if (value.Equals(this.imprimirOGuardar))
                {
                    return;
                }

                this.imprimirOGuardar = value;
                this.RaisePropertyChanged("Imprimir");
                this.RaisePropertyChanged("Guardar");
            }
        }

        public bool CambiarValorItem
        {
            get
            {
                return this.cambiarValorItem;
            }

            private set
            {
                if (value.Equals(this.cambiarValorItem))
                {
                    return;
                }

                this.cambiarValorItem = value;
                this.RaisePropertyChanged("CambiarValorItem");
            }
        }

        public bool CanChangeVaueItem()
        {
            var currentPrincipal = ConfiguracionGlobal.IPrincipalActual as NeuronCloud.Atpc.Co.Providers.Desktop.NeuronCloudPrincipal;

            if (currentPrincipal != null)
            {
                if (currentPrincipal.PermisosOsc(new Func<bool>(() => OscClaims.ValidateClaims("MODVALUE"))))
                {
                    return true;
                }
            }

            return false;
        }


        public bool EnviarPDF
        {
            get
            {
                return this.enviarPDF;
            }

            set
            {
                if (value.Equals(this.enviarPDF))
                {
                    return;
                }

                this.enviarPDF = value;
                this.RaisePropertyChanged("EnviarPDF");
            }
        }

        public bool ProgramarCita
        {
            get
            {
                return this.programarCita;
            }

            set
            {
                if (value.Equals(this.programarCita))
                {
                    return;
                }

                this.programarCita = value;
                this.RaisePropertyChanged("ProgramarCita");
                this.ProgramarNuevaCita.RaiseCanExecuteChanged();
                if (!this.ProgramarCita)
                {
                    this.CitaSeleccionada = null;
                }
            }
        }

        public bool SeleccionarPrefijoOSC
        {
            get
            {
                return this.PrefijosOscDisponibles.Count > 1;
            }
        }

        public bool MostrarServicios
        {
            get
            {
                return this.ServiceUnitsByRequester.Count > 0;
            }
        }

        public bool MostrarPersonalAsistencial
        {
            get
            {
                return App.PersonalAsistencialVisible;
            }
        }

        public bool Guardar
        {
            get
            {
                return !this.imprimirOGuardar;
            }
        }

        public bool SearchingAgreements
        {
            get
            {
                return this.searchingAgreements;
            }

            set
            {
                if (value != this.searchingAgreements)
                {
                    this.searchingAgreements = value;
                    this.RaisePropertyChanged("SearchingAgreements");
                }
            }
        }

        public bool IsAutenticated
        {
            get
            {
                return this.isAutenticated;
            }

            set
            {
                if (value != this.isAutenticated)
                {
                    this.isAutenticated = value;
                    this.RaisePropertyChanged("IsAutenticated");
                }
            }
        }

        public bool SearchingRequesters
        {
            get
            {
                return this.searchingRequesters;
            }

            set
            {
                if (value != this.searchingRequesters)
                {
                    this.searchingRequesters = value;
                    this.RaisePropertyChanged("SearchingRequesters");
                }
            }
        }

        public bool SearchingProviders
        {
            get
            {
                return this.searchingProviders;
            }

            set
            {
                if (value != this.searchingProviders)
                {
                    this.searchingProviders = value;
                    this.RaisePropertyChanged("SearchingProviders");
                }
            }
        }

        public bool SolicitarFUR
        {
            get
            {
                if (this.CurrentPatient != null && this.CurrentPatient.Gender == Gender.Female && this.EnEmbarazo)
                {
                    return true;
                }

                return false;
            }
        }

        public bool SolicitarEstadoEmbarazo
        {
            get
            {
                if (this.CurrentPatient != null && this.CurrentPatient.Gender == Gender.Female)
                {
                    return true;
                }

                return false;
            }
        }

        public bool PermitirEditarEdad
        {
            get
            {
                if (this.TerceroExacto != null && string.IsNullOrWhiteSpace(this.TerceroExacto.Edad))
                {
                    return true;
                }

                return false;
            }
        }

        public bool EnEmbarazo
        {
            get
            {
                return this.enEmbarazo;
            }

            set
            {
                if (value.Equals(this.enEmbarazo))
                {
                    return;
                }

                this.enEmbarazo = value;
                this.RaisePropertyChanged("EnEmbarazo");
                this.RaisePropertyChanged("SolicitarFUR");
            }
        }

        public bool NivelEditable
        {
            get
            {
                return this.SelectedAgreement != null && !string.IsNullOrWhiteSpace(this.SelectedAgreement.Nivel) && (this.SelectedAgreement.Nivel.ToUpper() == "NA" || this.SelectedAgreement.Nivel.ToUpper() == "LIBRE" || this.SelectedAgreement.Nivel.ToUpper() == "MANU");
            }
        }

        public bool EstaLogeado
        {
            get
            {
                return this.estaLogeado;
            }

            set
            {
                if (value.Equals(this.estaLogeado))
                {
                    return;
                }

                this.estaLogeado = value;
                this.RaisePropertyChanged("EstaLogeado");
            }
        }

        public bool SelectedServiceHasDetails
        {
            get
            {
                return this.selectedServiceHasDetails;
            }

            set
            {
                if (value != this.selectedServiceHasDetails)
                {
                    this.selectedServiceHasDetails = value;
                    this.RaisePropertyChanged("SelectedServiceHasDetails");
                    this.ConsultarExamenesCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool BuscarServiciosPrevios
        {
            get
            {
                return this.buscarServiciosPrevios;
            }

            set
            {
                if (value != this.buscarServiciosPrevios)
                {
                    this.buscarServiciosPrevios = value;
                    this.RaisePropertyChanged("BuscarServiciosPrevios");
                }
            }
        }

        public NumeroDeMuestra NumeroDeMuestraAsignado
        {
            get
            {
                return this.numeroDeMuestraAsignado;
            }

            set
            {
                if (Equals(value, this.numeroDeMuestraAsignado))
                {
                    return;
                }

                this.numeroDeMuestraAsignado = value;
                this.RaisePropertyChanged("NumeroDeMuestraAsignado");
            }
        }

        public bool SearchingServiceUnits
        {
            get
            {
                return this.searchingServiceUnits;
            }

            set
            {
                if (value != this.searchingServiceUnits)
                {
                    this.searchingServiceUnits = value;
                    this.RaisePropertyChanged("SearchingServiceUnits");
                }
            }
        }

        public bool CurrentPatientIsBusy
        {
            get
            {
                return this.currentPatientIsBusy;
            }

            set
            {
                if (value != this.currentPatientIsBusy)
                {
                    this.currentPatientIsBusy = value;
                    this.RaisePropertyChanged("CurrentPatientIsBusy");
                }
            }
        }

        public DateTime? FUR
        {
            get
            {
                return this.fur;
            }

            set
            {
                if (value.Equals(this.fur))
                {
                    return;
                }

                this.fur = value;
                this.RaisePropertyChanged("FUR");
            }
        }

        public DateTime? FechaHoraCita
        {
            get
            {
                return this.fechaHoraCita;
            }

            set
            {
                if (value.Equals(this.fechaHoraCita))
                {
                    return;
                }

                this.fechaHoraCita = value;
                this.RaisePropertyChanged("FechaHoraCita");
            }
        }

        public DateTime? EdadAGuardar
        {
            get
            {
                return this.edadAGuardar;
            }

            set
            {
                if (value.Equals(this.edadAGuardar))
                {
                    return;
                }

                this.edadAGuardar = value;
                this.RaisePropertyChanged("EdadAGuardar");
                this.GuardarEdad.RaiseCanExecuteChanged();
            }
        }

        public Cita CitaSeleccionada
        {
            get
            {
                return this.citaSeleccionada;
            }

            set
            {
                if (object.Equals(value, this.citaSeleccionada))
                {
                    return;
                }

                this.citaSeleccionada = value;
                this.RaisePropertyChanged("CitaSeleccionada");
            }
        }

        public string Error { get; private set; }
        public bool CambioDescuento { get; private set; }
        private bool applyDiscount;
        public bool ApplyDiscount
        {
            get { return applyDiscount; }
            set {
                this.applyDiscount = value;
                this.RaisePropertyChanged("ApplyDiscount");
            }
        }
         

        public bool ChangeValueItem { get; private set; }
        public string Descuentolabel
        {
            get => _descuentolabel;
            private set
            {
                _descuentolabel = value;
                this.RaisePropertyChanged("Descuentolabel");
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "NoOrdenMedica" && App.NoOrdenMedicaRequired)
                {
                    return string.IsNullOrWhiteSpace(this.NoOrdenMedica) ? "El Numero de Orden Medica es Requerido" : string.Empty;
                }

                return string.Empty;
            }
        }

        public void SearchDiagnoses(string parameter)
        {
            this.diagnoseParameter = parameter;
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                this.sugestedDiagnosesIsLoading = true;

                StoreProceduresAsync.GetDiagnoseAutoCompleteAsync(
                    Tuple.Create(parameter, this.CurrentPatient),
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var diagnoses = ee.Result as List<Diagnose>;

                            if (diagnoses != null && this.diagnoseParameter == parameter)
                            {
                                this.SugestedDiagnoses.Clear();
                                foreach (var diagnosis in diagnoses)
                                {
                                    this.SugestedDiagnoses.Add(diagnosis);
                                }
                            }

                            this.sugestedDiagnosesLoaded = true;
                            this.OnDiagnosticosCargados();
                        }

                        this.sugestedDiagnosesIsLoading = false;
                    });
            }
        }

        public void NuevoTerceroCreado(Tercero tercero)
        {
            this.DocIdSearch = string.Empty;
            this.TypeDocIdSearch = null;
            this.DocIdSearch = tercero.NumeroDocumento;
            this.TypeDocIdSearch = tercero.TipoDocumento;
        }

        internal void CargarPrefijosOSC<T>(OrigenDatos origenDatos, bool limpiarAntesDeCargar, [Optional]string csv, [Optional]IList<T> lista, [Optional]T valorPorDefecto)
        {
            if (limpiarAntesDeCargar)
            {
                this.PrefijosOscDisponibles.Clear();
            }

            switch (origenDatos)
            {
                case OrigenDatos.DesdeBD:
                    break;
                case OrigenDatos.DesdeCSV:
                    if (!string.IsNullOrWhiteSpace(csv))
                    {
                        var valores = csv.Split(',');
                        foreach (string valor in valores)
                        {
                            if (!string.IsNullOrWhiteSpace(valor))
                            {
                                this.PrefijosOscDisponibles.Add(valor);
                            }
                        }
                    }

                    break;
                case OrigenDatos.DesdeLista:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("origenDatos");
            }

            if (this.PrefijosOscDisponibles.Count == 1)
            {
                this.OscPrefix = this.PrefijosOscDisponibles.First();
            }

            var defaultV = valorPorDefecto as string;

            if (defaultV != null)
            {
                if (this.PrefijosOscDisponibles.Contains(defaultV))
                {
                    this.OscPrefix = defaultV;
                }
            }
        }

        internal void SeleccionarRemitente(NeuronCloud.Atpc.Co.Modelos.Auxiliares.Direccion direccionCursor)
        {
            if (this.RequestersByAgreementView.CurrentItem == null)
            {
                this.RequestersByAgreementView.MoveCurrentToFirst();
            }
            else
            {
                switch (direccionCursor)
                {
                    case NeuronCloud.Atpc.Co.Modelos.Auxiliares.Direccion.Arriba:
                        this.RequestersByAgreementView.MoveCurrentToPrevious();
                        if (this.RequestersByAgreementView.IsCurrentBeforeFirst)
                        {
                            this.RequestersByAgreementView.MoveCurrentToLast();
                        }

                        break;

                    case NeuronCloud.Atpc.Co.Modelos.Auxiliares.Direccion.Abajo:
                        this.RequestersByAgreementView.MoveCurrentToNext();
                        if (this.RequestersByAgreementView.IsCurrentAfterLast)
                        {
                            this.RequestersByAgreementView.MoveCurrentToFirst();
                        }

                        break;

                    case NeuronCloud.Atpc.Co.Modelos.Auxiliares.Direccion.Ninguna:
                        if (this.RequestersByAgreementView.Count == 1)
                        {
                            this.RequestersByAgreementView.MoveCurrentToFirst();
                        }

                        break;
                }
            }

            var requester = this.RequestersByAgreementView.CurrentItem as ServiceRequester;

            if (requester != null)
            {
                this.SelectedRequester = requester;
            }
        }

        private void OnSelectedProviderChanged()
        {
            if (this.MostrarControlesAsignacionNumeroMuestra)
            {
                this.BuscarCentrosDeToma();
            }
        }

        private bool FiltrarRemitentes(object o)
        {
            if (string.IsNullOrWhiteSpace(this.BusquedaRemitente) || this.BusquedaRemitente.Length < 0)
            {
                return true;
            }

            var serviceRequester = o as ServiceRequester;

            if (serviceRequester != null)
            {
                return serviceRequester.Name.IndexOf(this.BusquedaRemitente, StringComparison.OrdinalIgnoreCase) > -1 || serviceRequester.Code.IndexOf(this.BusquedaRemitente, StringComparison.OrdinalIgnoreCase) > -1;
            }

            return true;
        }

        private void OnMostrarMensajeEvent(string parametro)
        {
            ParametroTextoEventHandler handler = this.MostrarMensajeEvent;
            handler?.Invoke(this, parametro);
        }

        private void SearchAgreements()
        {
            if (!string.IsNullOrEmpty(this.CurrentPatient?.IdDocument))
            {
                this.SearchingAgreements = true;
                this.AgreementsByPatient.Clear();
                StoreProceduresAsync.GetServiceAgreements(
                    this.CurrentPatient,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            this.AgreementsByPatient.Clear();
                            var serviceAgreements = ee.Result as List<ServiceAgreement>;

                            if (serviceAgreements != null)
                            {
                                foreach (var serviceAgreement in serviceAgreements)
                                {
                                    this.AgreementsByPatient.Add(serviceAgreement);
                                }
                            }
                        }

                        this.SearchingAgreements = false;
                    });
            }
        }

        private void MainWindowChanged()
        {
            if (this.MainWindow != null)
            {
                this.MainWindow.CommandBindings.Add(new CommandBinding(CRUDCommands.AddCommand, this.AddCommandExecuted, this.AddCommandCanExecuted));
                this.MainWindow.CommandBindings.Add(new CommandBinding(CRUDCommands.RemoveCommand, this.RemoveCommandExecuted, this.RemoveCommandCanExecuted));
            }
        }

        private void RemoveCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RemoveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;

            if (button != null)
            {
                var crudEntity = button.DataContext as CRUDEntity<Service>;

                if (crudEntity != null)
                {
                    this.SelectedServices.Remove(crudEntity);
                    this.UpdateLiquidation();
                }
            }
        }

        private void AddCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            //// = this.SelectedService != null && this.SelectedService.IsValid;
        }

        private void AddCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectedServiceHasDetails = false;
            if (this.SelectedService.BaseEntity.EsCombo)
            {
                foreach (Service servicioToAdd in this.selectedService.BaseEntity.ComponentesCombo)
                {
                    CRUDEntity<Service> element = this.SelectedServices.FirstOrDefault(crud => crud.BaseEntity != null && servicioToAdd != null && crud.BaseEntity.Code == servicioToAdd.Code);
                    if (element != null)
                    {
                        element.BaseEntity = servicioToAdd.Copy();
                        this.RaisePropertyChanged("Total");
                    }
                    else
                    {
                        this.SelectedServices.Add(new CRUDEntity<Service>(servicioToAdd.Copy(), true));
                        this.RecalcularConNuevoConvenio.RaiseCanExecuteChanged();
                    }
                }

            }
            else
            {
                CRUDEntity<Service> element = this.SelectedServices.FirstOrDefault(crud => crud.BaseEntity != null && this.SelectedService.BaseEntity != null && crud.BaseEntity.Code == this.SelectedService.BaseEntity.Code);
                if (element != null)
                {
                    element.BaseEntity = this.SelectedService.BaseEntity.Copy();
                    this.RaisePropertyChanged("Total");
                }
                else
                {
                    this.SelectedServices.Add(new CRUDEntity<Service>(this.SelectedService.BaseEntity.Copy(), true));
                    this.RecalcularConNuevoConvenio.RaiseCanExecuteChanged();
                }
            }
            this.UpdateLiquidation();

            this.SelectedService = new CRUDEntity<Service>(new Service { Name = string.Empty }, false);
        }

        private void OnSelectedRequesterChanged()
        {
            this.SearchServiceUnits();
            if (this.CurrentPatient != null && this.SelectedAgreement != null && this.SelectedRequester != null)
            {
                if (this.BuscarServiciosPrevios)
                {
                    StoreProceduresAsync.GetExamenesRecientesAsync(
                        Tuple.Create(this.CurrentPatient.UniqueDocumentId, this.SelectedRequester.Code, string.Empty),
                        (sender, args) =>
                        {
                            if (args.Cancelled)
                            {
                            }
                            else if (args.Error != null)
                            {
                            }
                            else
                            {
                                this.Antecedentes.Clear();
                                var listaAntecedentes = args.Result as IList<Antecedente>;

                                if (listaAntecedentes != null)
                                {
                                    foreach (var antecedente in listaAntecedentes)
                                    {
                                        this.Antecedentes.Add(antecedente);
                                    }
                                }
                            }
                        });
                }
            }
        }

        private void OnSelectedServiceUnitChanged()
        {
            if (this.CurrentPatient != null && this.SelectedAgreement != null && this.SelectedRequester != null && this.SelectedServiceUnit != null)
            {
                if (this.BuscarServiciosPrevios)
                {
                    StoreProceduresAsync.GetExamenesRecientesAsync(
                        Tuple.Create(this.CurrentPatient.UniqueDocumentId, this.SelectedRequester.Code, this.SelectedServiceUnit.Name),
                        (sender, args) =>
                        {
                            if (args.Cancelled)
                            {
                            }
                            else if (args.Error != null)
                            {
                            }
                            else
                            {
                                this.Antecedentes.Clear();
                                var listaAntecedentes = args.Result as IList<Antecedente>;

                                if (listaAntecedentes != null)
                                {
                                    foreach (var antecedente in listaAntecedentes)
                                    {
                                        this.Antecedentes.Add(antecedente);
                                    }
                                }
                            }
                        });
                }
            }
        }

        private void VerifyPatienId()
        {
            if (!string.IsNullOrWhiteSpace(this.DocIdSearch) && this.TypeDocIdSearch != null && !string.IsNullOrWhiteSpace(this.TypeDocIdSearch.TipoIdentificacionId))
            {
                this.Limpiar();
                this.CurrentPatientIsBusy = true;
                StoreProceduresAsync.GetPatientInfo(
                    string.Concat(this.DocIdSearch, this.TypeDocIdSearch.TipoIdentificacionId),
                    (ss, ee) =>
                    {
                        this.CurrentPatientIsBusy = false;

                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var patientInfo = ee.Result as PatientInfo;
                            if (patientInfo != null)
                            {
                                this.firstLoad = true;
                                this.CurrentPatient = patientInfo;
                                this.DocumentoEncontrado = true;
                                if (this.TerceroExacto != null)
                                {
                                    this.CorreoElectronico = this.TerceroExacto.CorreoPersonal;
                                    this.Direccion = this.TerceroExacto.DireccionResidencia;
                                    this.Telefono = this.TerceroExacto.TelefonoResidencia;
                                }

                                this.firstLoad = false;
                            }
                            else
                            {
                                this.NotifyNoUser();
                            }
                        }
                    });
            }
        }
        private void VerifyNoCita()
        {
            if (!string.IsNullOrWhiteSpace(this.DocIdSearch) && this.TypeDocIdSearch != null && !string.IsNullOrWhiteSpace(this.TypeDocIdSearch.TipoIdentificacionId))
            {
                StoreProceduresAsync.GetBookingSearch(Tuple.Create(string.Concat(this.DocIdSearch, this.TypeDocIdSearch.TipoIdentificacionId), this.NoCitaSearch),
                    (ss, ee) =>
                    {

                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var noCita = ee.Result as NoCita;
                            if (noCita != null)
                            {
                                this.DocumentoEncontrado = true;
                                this.NoCitaSearch = Convert.ToString(noCita.BookingNumber);

                                this.firstLoad = false;
                            }
                            else
                            {
                                this.NoCitaSearch = string.Empty;
                                MessageBox.Show("No existe ese número de cita para este paciente", "Cita no existe");
                            }
                        }
                    });
            }
        }

        private void NotifyNoUser()
        {
            var s = MessageBox.Show("No se ha Encotrado El Documento.\nDesea Ingresar Nuevo Paciente?", "No se Encontro Paciente...", MessageBoxButton.YesNo, MessageBoxImage.Information);
            switch (s)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    this.OnCrearNuevoTerceroEvent();
                    break;
                case MessageBoxResult.No:
                    this.DocIdSearch = string.Empty;
                    this.Limpiar();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SavePhoneNumber()
        {
            this.SavingPhoneNumber = true;
            if (!string.IsNullOrWhiteSpace(this.CurrentPatient.UniqueDocumentId))
            {
                StoreProceduresAsync.SavePhoneNumberAsync(
                    new PatientInfo
                    {
                        UniqueDocumentId = this.CurrentPatient.UniqueDocumentId,
                        FullName = this.Telefono
                    },
                    (ss, ee) =>
                    {
                        this.SavingPhoneNumber = false;
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            MessageBox.Show("Error:\n" + ee.Error.Message, "Error al Guardar la Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            ////this.VerifyPatienId();
                        }

                    });
            }

        }

        private void SaveAddress()
        {
            if (!string.IsNullOrWhiteSpace(this.CurrentPatient.UniqueDocumentId))
            {
                this.SavingAddress = true;
                StoreProceduresAsync.SaveAddressAsync(
                        new PatientInfo
                        {
                            UniqueDocumentId = this.CurrentPatient.UniqueDocumentId,
                            FullName = this.Direccion
                        },
                        (ss, ee) =>
                        {
                            this.SavingAddress = false;
                            if (ee.Cancelled)
                            {
                            }
                            else if (ee.Error != null)
                            {
                                MessageBox.Show("Error:\n" + ee.Error.Message, "Error al Guardar la Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            else
                            {
                                ////this.VerifyPatienId();
                            }
                        });
            }
        }

        private void OnSelectedAgreementChanged()
        {
            this.BusquedaRemitente = string.Empty;
            
            if (this.SelectedAgreement != null)
            {
                if (this.SelectedProvider != null)
                {
                    this.tempProvider = this.SelectedProvider;
                }

                if (this.SelectedRequester != null)
                {
                    this.tempRequester = this.SelectedRequester;
                }

                this.SearchRequesters();
                this.SearchProviders();
                this.Nivel = this.SelectedAgreement.Nivel;
                if (this.SelectedAgreement.Copago == "MANUAL")
                {
                    this.ApplyDiscount = false;
                }
                else
                {
                    this.ApplyDiscount = true;
                }
                if (this.SelectedService != null)
                {
                    this.SelectedService = new CRUDEntity<Service>(new Service { Name = string.Empty }, false);
                }
            }
            else
            {
                this.ProvidersByAgreement.Clear();
                this.RequestersByAgreement.Clear();
                this.ServiceUnitsByRequester.Clear();
                this.Nivel = string.Empty;
            }
        }

        private void BuscarCentrosDeToma()
        {
            this.CentrosDeToma.Clear();
            if (this.SelectedProvider != null)
            {
                Acciones.CentroTomaMuestraGetAsync(
                    this.SelectedProvider.Code,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var centroDeTomaMuestras = ee.Result as List<CentroDeTomaMuestra>;

                            if (centroDeTomaMuestras != null)
                            {
                                foreach (CentroDeTomaMuestra centroDeTomaMuestra in centroDeTomaMuestras)
                                {
                                    this.CentrosDeToma.Add(centroDeTomaMuestra);
                                }
                            }
                        }

                        this.CargarCentrosDeTomaManuales();

                        Debug.WriteLine("Fin Carga Centros de Toma");
                    });
            }
        }

        private void CargarCentrosDeTomaManuales()
        {
            if (!string.IsNullOrWhiteSpace(App.PrefijosCentroToma))
            {
                var centrosManuales = App.PrefijosCentroToma.Split(',');
                foreach (var centroManual in from centroManual in centrosManuales.Where(centroManual => !string.IsNullOrWhiteSpace(centroManual)) let ct = this.CentrosDeToma.FirstOrDefault(centrosDeTomaCargados => centrosDeTomaCargados.Prefijo == centroManual) where ct == null select centroManual)
                {
                    this.CentrosDeToma.Add(new CentroDeTomaMuestra { Prefijo = centroManual });
                }
            }

            if (!string.IsNullOrWhiteSpace(App.PrefijoCentroTomaDefault))
            {
                this.NumeroDeMuestraAsignado.CentroDeToma = this.CentrosDeToma.FirstOrDefault(ctm => ctm.Prefijo == App.PrefijoCentroTomaDefault)
                                                            ?? this.CentrosDeToma.FirstOrDefault();
            }
        }

        private void SearchProviders()
        {
            this.SearchingProviders = true;
            this.ProvidersByAgreement.Clear();
            StoreProceduresAsync.GetServiceProviders(
                this.SelectedAgreement,
                (ss, ee) =>
                {
                    if (ee.Cancelled)
                    {
                    }
                    else if (ee.Error != null)
                    {
                    }
                    else
                    {
                        var serviceProviders = ee.Result as List<ServiceProvider>;

                        if (serviceProviders != null)
                        {
                            foreach (var serviceProvider in serviceProviders)
                            {
                                this.ProvidersByAgreement.Add(serviceProvider);
                            }

                            if (this.tempProvider != null)
                            {
                                var providerd = this.ProvidersByAgreement.FirstOrDefault(provider => provider.Code == this.tempProvider.Code);
                                if (providerd != null)
                                {
                                    this.SelectedProvider = providerd;
                                }
                            }
                        }
                    }

                    this.SearchingProviders = false;
                });
        }

        private void SearchRequesters()
        {
            this.SearchingRequesters = true;
            this.RequestersByAgreement.Clear();
            StoreProceduresAsync.GetServiceRequesters(
                this.SelectedAgreement,
                (ss, ee) =>
                {
                    if (ee.Cancelled)
                    {
                    }
                    else if (ee.Error != null)
                    {
                        this.RequestersByAgreement.Add(new ServiceRequester { Code = "999", Name = "Error" });
                    }
                    else
                    {
                        var serviceRequesters = ee.Result as List<ServiceRequester>;

                        if (serviceRequesters != null)
                        {
                            foreach (var serviceRequester in serviceRequesters)
                            {
                                this.RequestersByAgreement.Add(serviceRequester);
                            }

                            if (this.tempRequester != null)
                            {
                                var requestd = this.RequestersByAgreement.FirstOrDefault(request => request.Code == this.tempRequester.Code);
                                if (requestd != null)
                                {
                                    this.SelectedRequester = requestd;
                                }
                            }
                        }
                    }

                    this.SearchingRequesters = false;
                });
        }

        private void SearchServiceUnits()
        {
            if (this.SelectedAgreement != null && this.SelectedRequester != null)
            {
                this.SearchingServiceUnits = true;
                this.ServiceUnitsByRequester.Clear();
                StoreProceduresAsync.GetServiceUnits(
                    new ServiceUnitParameters { CodigoConvenio = this.SelectedAgreement.Code, CodigoRemitente = this.SelectedRequester.Code },
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var serviceUnits = ee.Result as List<ServiceUnit>;

                            if (serviceUnits != null)
                            {
                                foreach (var serviceUnit in serviceUnits)
                                {
                                    this.ServiceUnitsByRequester.Add(serviceUnit);
                                }
                            }
                        }

                        this.SearchingServiceUnits = false;
                    });
            }
        }

        private void SearchService()
        {
            this.SelectedServiceHasDetails = false;
            if (this.SelectedAgreement != null && this.SelectedProvider != null && this.SelectedRequester != null && !string.IsNullOrWhiteSpace(this.CodeToSearch))
            {
                ////this.SearchingServiceUnits = true;
                this.SelectedService = new CRUDEntity<Service>(new Service { Name = "Buscando..." }, false) { IsBusy = true };
                StoreProceduresAsync.GetServiceByCode(
                    new SearchServiceParameters
                    {
                        Provider = this.SelectedProvider,
                        Code = this.CodeToSearch,
                        ServiceAgreement = this.SelectedAgreement,
                        ServiceRequester = this.SelectedRequester,
                        Gender = this.CurrentPatient.Gender
                    },
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                        }
                        else
                        {
                            var service = ee.Result as Service;

                            if (service != null)
                            {
                                this.SelectedService = new CRUDEntity<Service>(service, true);
                                if (this.Antecedentes.Any(ant => ant.CodigoFuente == this.selectedService.BaseEntity.Code))
                                {
                                    this.SelectedServiceHasDetails = true;
                                }
                            }
                            else
                            {
                                this.SelectedService = new CRUDEntity<Service>(
                                    new Service { Name = "No Encontrado" }, false);
                            }
                        }

                        ////this.SearchingServiceUnits = false;
                    });
            }
        }

        private void SaveOSC(object obj)
        {
            if (!this.ValidarDatos())
            {
                return;
            }

            if (this.CurrentPatient != null)
            {
                var patient = this.CurrentPatient.Copy();
                var numeroDeMuestraAsignadoAux = this.NumeroDeMuestraAsignado;
                this.Idle = false;
                if (App.Hl7Enabled)
                {
                    var parametro = new SaveOSCParameters
                    {
                        SelectedServices = this.SelectedServices.ToList(),
                        CurrentPatient = this.CurrentPatient,
                        CurrentDiagnosis = this.SelectedDiagnose,
                        PersonalAsistencial = this.SelectedPersonalAsistencial,
                        OSCPrefix = this.OscPrefix,
                        CurrentServiceAgreement = this.SelectedAgreement,
                        CurrentProvider = this.selectedProvider,
                        CurrentServiceRequester = this.SelectedRequester,
                        CurrentServiceUnit = this.SelectedServiceUnit,
                        IdSistema = this.IdSistema,
                        NoOrdenMedica = this.NoOrdenMedica,
                        NumeroAutorizacion = this.NumeroAutorizacion,
                        Observaciones = this.Observaciones,
                        VrDcto = this.VrDcto.ToString(),
                        Total = Convert.ToDecimal(this.TotalAPagar),
                        AsigNoMuestra = App.AsigNoMuestra,
                        NumeroDeMuestraAsignado = this.NumeroDeMuestraAsignado,
                        FUR = this.FUR,
                        Nivel = this.Nivel,
                        CorreoElectronico = this.CorreoElectronico,
                        EnviarResultadoPorCorreo = this.EnviarPDF,
                        ProgramarCita = this.ProgramarCita,
                        FechaHoraCita = this.FechaHoraCita,
                        Sede = OscGlobalConfig.NomSede,
                        BookingNumber = this.NoCitaSearch,
                        IdProgramaAgenda =
                                                this.CitaSeleccionada != null
                                                    ? this.CitaSeleccionada.Id
                                                    : null,
                        ServicioUbicacion =
                                                this.selectedServiceUnit != null
                                                    ? this.SelectedServiceUnit.Name
                                                    : null
                    };
                    var dataTransferClient = new DataTransferClient(new WSHttpBinding(SecurityMode.None), new EndpointAddress(App.Hl7Url));
                    dataTransferClient.HL7DataRawCompleted += (sender, args) =>
                        {
                            if (args.Cancelled)
                            {
                            }
                            else if (args.Error != null)
                            {
                                MessageBox.Show("Error:\n" + args.Error.Message, "Error en la Ejecución del WS", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            else
                            {
                                OscInsertaResult resultado;

                                using (TextReader reader = new StringReader(args.Result))
                                {
                                    var xmlSerial = new XmlSerializer(typeof(OscInsertaResult));
                                    resultado = (OscInsertaResult)xmlSerial.Deserialize(reader);
                                }


                                if (resultado != null)
                                {
                                    this.OnSavedOscEvent("Osc Guardada", resultado);


                                    ////string token = string.Empty;
                                    ////if (!string.IsNullOrWhiteSpace(resultado.Token))
                                    ////{
                                    ////    token = "\n\nToken : " + resultado.Token;
                                    ////}

                                    string noMuestra = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(resultado.IdNumMuestra.ToString()))
                                    {
                                        //// noMuestra = "\n\nNo. de Muestra : " + resultado.Item3;
                                        if (App.ImprimirCodigosDeBarras)
                                        {
                                            Application.Current.Dispatcher.Invoke((Action)(() => this.ImprimirCodigosDeBarras(resultado.IdNumMuestra.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), resultado.IdMuestraCentro.GetValueOrDefault(), patient, numeroDeMuestraAsignadoAux)));
                                        }
                                    }
                                }

                                this.DocIdSearch = string.Empty;
                                this.TypeDocIdSearch = null;
                                this.Limpiar();
                            }

                            this.Idle = true;
                        };

                    var x = new XmlSerializer(parametro.GetType());
                    using (var sww = new StringWriter())
                    {
                        XmlWriter writer = XmlWriter.Create(sww);
                        x.Serialize(writer, parametro);
                        dataTransferClient.HL7DataRawAsync(sww.ToString());
                    }
                }
                else
                {


                    StoreProceduresAsync.SaveOscAsync(
                        new SaveOSCParameters
                        {
                            SelectedServices = this.SelectedServices.ToList(),
                            Pagos = this.PaymentDetails.Pagos.ToList(),
                            CurrentPatient = this.CurrentPatient,
                            CurrentDiagnosis = this.SelectedDiagnose,
                            PersonalAsistencial = this.SelectedPersonalAsistencial,
                            OSCPrefix = this.OscPrefix,
                            CurrentServiceAgreement = this.SelectedAgreement,
                            CurrentProvider = this.selectedProvider,
                            CurrentServiceRequester = this.SelectedRequester,
                            CurrentServiceUnit = this.SelectedServiceUnit,
                            IdSistema = this.IdSistema,
                            NoOrdenMedica = this.NoOrdenMedica,
                            NumeroAutorizacion = this.NumeroAutorizacion,
                            Observaciones = this.Observaciones,
                            VrDcto = this.VrDcto.ToString(),
                            Total = Convert.ToDecimal(this.TotalAPagar),
                            AsigNoMuestra = App.AsigNoMuestra,
                            NumeroDeMuestraAsignado = this.NumeroDeMuestraAsignado,
                            FUR = this.FUR,
                            Nivel = this.Nivel,
                            CorreoElectronico = this.CorreoElectronico,
                            EnviarResultadoPorCorreo = this.EnviarPDF,
                            ProgramarCita = this.ProgramarCita,
                            FechaHoraCita = this.FechaHoraCita,
                            IdProgramaAgenda = this.CitaSeleccionada != null ? this.CitaSeleccionada.Id : null,
                            ServicioUbicacion = this.selectedServiceUnit != null ? this.SelectedServiceUnit.Name : null,
                            Sede = OscGlobalConfig.NomSede,
                            BookingNumber = this.NoCitaSearch,
                        },
                        (ss, ee) =>
                        {
                            if (ee.Cancelled)
                            {
                            }
                            else if (ee.Error != null)
                            {
                                MessageBox.Show("Error:\n" + ee.Error.Message, "Error en la Ejecución del Procedimiento ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            else
                            {
                                var resultado = ee.Result as OscInsertaResult;

                                if (resultado != null)
                                {
                                    this.OnSavedOscEvent("Osc Guardada", resultado);


                                    ////string token = string.Empty;
                                    ////if (!string.IsNullOrWhiteSpace(resultado.Token))
                                    ////{
                                    ////    token = "\n\nToken : " + resultado.Token;
                                    ////}

                                    string noMuestra = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(resultado.IdNumMuestra.ToString()))
                                    {
                                        //// noMuestra = "\n\nNo. de Muestra : " + resultado.Item3;
                                        if (App.ImprimirCodigosDeBarras)
                                        {
                                            Application.Current.Dispatcher.Invoke((Action)(() => this.ImprimirCodigosDeBarras(resultado.IdNumMuestra.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), resultado.IdMuestraCentro.GetValueOrDefault(), patient, numeroDeMuestraAsignadoAux)));
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(resultado.ListaOsc.ToString()))
                                    {
                                        //// noMuestra = "\n\nNo. de Muestra : " + resultado.Item3;
                                        if (App.PrintServiceLabel)
                                        {
                                            Application.Current.Dispatcher.Invoke((Action)(() => this.PrintLabel(resultado.ListaOsc.ToString(CultureInfo.InvariantCulture), resultado.IdMuestraCentro.GetValueOrDefault(), patient)));
                                        }
                                    }

                                    ////MessageBox.Show("Guardada con el Numero: " + resultado.ListaOSC + token + noMuestra);
                                    ////if (!string.IsNullOrWhiteSpace(resultado.ListaOSC))
                                    ////{
                                    ////    if (App.ImprimirDespuesDeGuardar)
                                    ////    {
                                    ////        this.OnMostrarVentanaReporteEvent(resultado.ListaOSC, resultado.ListaFactura);
                                    ////    }
                                    ////}
                                }

                                this.DocIdSearch = string.Empty;
                                this.TypeDocIdSearch = null;
                                this.Limpiar();
                            }

                            this.Idle = true;
                        });
                }
            }
        }

        private void SaveFinger(object obj)
        {
            //if (!this.ValidarDatos())
            //{
            //    return;
            //}

            if (this.CurrentPatient != null && this.HuellaActual != null)
            {
                var patient = this.CurrentPatient.Copy();
                this.Idle = false;

                StoreProceduresAsync.SetFingerprintAsync(Tuple.Create(currentPatient.IdDocument,this.HuellaActual)
                    ,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            MessageBox.Show("Error:\n" + ee.Error.Message, "Error en la Ejecución del Procedimiento ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                           
                        }

                        this.Idle = true;
                    });
            }
        }


        private void ImprimirCodigosDeBarras(string idNumMuestra, int idMuestraCentro, PatientInfo patient, NumeroDeMuestra numeroDeMuestraAsignadoAux)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(idNumMuestra))
                {
                    return;
                }

                StoreProceduresAsync.GetLabelsAsync(
                    idNumMuestra,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            Application.Current.Dispatcher.BeginInvoke((Action)(() => this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + ee.Error.Message)));
                        }
                        else
                        {
                            var results = ee.Result as List<PRO_TipoMuestraIdNum_Result>;

                            if (results != null)
                            {
                                foreach (var result in results)
                                {
                                    double? minutosRecepcion = null;
                                    double? minutosRadicacion = null;

                                    if (result.FechaToma.HasValue && result.FechaRecep.HasValue)
                                    {
                                        minutosRecepcion = (result.FechaRecep.Value - result.FechaToma.Value).TotalMinutes;
                                    }

                                    if (result.FechaToma.HasValue)
                                    {
                                        minutosRadicacion = (DateTime.Now - result.FechaToma.Value).TotalMinutes;
                                    }

                                    string lineaHora = DateTime.Now.ToString("yyyy/M/d h:mm");
                                    string lineaHora2 = "MinRecep:" + (minutosRecepcion.HasValue ? Convert.ToInt32(minutosRecepcion.Value).ToString(CultureInfo.InvariantCulture) : " ") + " MinRadic:" + (minutosRadicacion.HasValue ? Convert.ToInt32(minutosRadicacion.Value).ToString(CultureInfo.InvariantCulture) : " ");
                                    string documentoEdad = patient.IdDocumentType + " " + patient.IdDocument + " Edad " + result.Edad + " " + (string.IsNullOrWhiteSpace(numeroDeMuestraAsignadoAux.CentroDeToma.Prefijo) ? string.Empty : numeroDeMuestraAsignadoAux.CentroDeToma.Prefijo) + idMuestraCentro;
                                    var label = new List<string>();
                                    label.Add("N");
                                    label.Add("q480");
                                    label.Add("Q240");
                                    label.Add("A40,20,0,1,1,1,N,\"" + lineaHora + "\"");
                                    label.Add("A40,40,0,1,1,1,N,\"" + lineaHora2 + "\"");
                                    label.Add("A40,60,0,2,1,1,N,\"" + patient.FullName + "\"");
                                    label.Add("A40,85,0,1,1,1,N,\"" + documentoEdad + "\"");
                                    label.Add("B70,100,0,1,2,5,65,B,\"" + result.IdentificadorMuestra + "\"");
                                    label.Add("A40,195,0,2,1,1,N,\"" + result.TipoMuestra + ":" + result.Abreviatura + "\"");
                                    label.Add("P1");

                                    Task<Task> resultTask =
                                        Utils.PrintLabelAsync(label, App.NombreImpresoraCodigoDeBarras, "Test Lysis: " + DateTime.Now)
                                        .ContinueWith(
                                        except => Task.Factory.StartNew(
                                            () =>
                                            {
                                                if (except.IsFaulted)
                                                {
                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                                                    {
                                                        if (except.Exception != null)
                                                        {
                                                            this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + except.Exception.Message);
                                                            if (except.Exception.InnerException != null)
                                                            {
                                                                this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas Inner: " + except.Exception.InnerException.Message);
                                                            }
                                                        }

                                                        this.HayAlerta = true;
                                                    }));
                                                }
                                                else if (except != null && except.Result != null)
                                                {
                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                                                    {
                                                        this.AlertsAndWarningsCollection.Add("Error: " + except.Result.Message);
                                                        this.HayAlerta = true;
                                                    }));
                                                }
                                                else
                                                {
                                                    if (Debugger.IsAttached)
                                                    {
                                                        Debugger.Break();
                                                    }

                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() => this.AlertsAndWarningsCollection.Add("Código de Barras enviado a impresora \"" + App.NombreImpresoraCodigoDeBarras + "\"")));
                                                }
                                            }));

                                }
                            }
                        }
                    });

            }
            catch (Exception labelException)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + labelException.Message);
                        if (labelException.InnerException != null)
                        {
                            this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas Inner: " + labelException.InnerException.Message);
                        }

                        this.HayAlerta = true;
                    }));

            }

        }
        internal void PrintLabel(string ListaOsc, int idMuestraCentro, PatientInfo patient)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ListaOsc))
                {
                    return;
                }

                StoreProceduresAsync.GetLabelsOSCAsync(
                    ListaOsc,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            Application.Current.Dispatcher.BeginInvoke((Action)(() => this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + ee.Error.Message)));
                        }
                        else
                        {
                            var results = ee.Result as List<PRO_PrinterCodeBar_Result>;

                            if (results != null)
                            {
                                foreach (var result in results)
                                {

                                    string lineaHora = DateTime.Now.ToString("yyyy/M/d h:mm");
                                    string lineaDoc = result.TipoIdent + " " + result.DocIdent + "      " + result.DocUnicoOSC;
                                    string lineaHora3 = result.NomConvenio + "-" + result.NomRemitente;
                                    string documentoEdad = "Edad " + result.Edad;
                                    var label = new List<string>();
                                    label.Add("N");
                                    label.Add("q480");
                                    label.Add("Q240");
                                    label.Add("A20,15,0,1,1,1,N,\"" + lineaHora + "\"");
                                    label.Add("A20,35,0,2,1,1,N,\"" + lineaDoc + "\"");
                                    label.Add("A20,60,0,3,1,2,N,\"" + result.Nombre + "\"");
                                    label.Add("A20,110,0,2,1,1,N,\"" + documentoEdad + "\"");
                                    label.Add("A20,135,0,2,1,1,N,\"" + lineaHora3 + "\"");
                                    label.Add("A20,152,0,2,1,1,N,\"" + result.Abreviatura + "\"");
                                    label.Add("P1");

                                    Task<Task> resultTask =
                                        Utils.PrintLabelAsync(label, App.NombreImpresoraCodigoDeBarras, "Test Lysis: " + DateTime.Now)
                                        .ContinueWith(
                                        except => Task.Factory.StartNew(
                                            () =>
                                            {
                                                if (except.IsFaulted)
                                                {
                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                                                    {
                                                        if (except.Exception != null)
                                                        {
                                                            this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + except.Exception.Message);
                                                            if (except.Exception.InnerException != null)
                                                            {
                                                                this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas Inner: " + except.Exception.InnerException.Message);
                                                            }
                                                        }

                                                        this.HayAlerta = true;
                                                    }));
                                                }
                                                else if (except != null && except.Result != null)
                                                {
                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                                                    {
                                                        this.AlertsAndWarningsCollection.Add("Error: " + except.Result.Message);
                                                        this.HayAlerta = true;
                                                    }));
                                                }
                                                else
                                                {
                                                    if (Debugger.IsAttached)
                                                    {

                                                        Debugger.Break();
                                                    }

                                                    Application.Current.Dispatcher.BeginInvoke((Action)(() => this.AlertsAndWarningsCollection.Add("Código de Barras enviado a impresora \"" + App.NombreImpresoraCodigoDeBarras + "\"")));
                                                }
                                            }));

                                }
                            }
                        }
                    });

            }
            catch (Exception labelException)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas: " + labelException.Message);
                    if (labelException.InnerException != null)
                    {
                        this.AlertsAndWarningsCollection.Add("Error al Procesar las Etiquetas Inner: " + labelException.InnerException.Message);
                    }

                    this.HayAlerta = true;
                }));

            }

        }

        private void Limpiar()
        {
            this.CurrentPatient = new PatientInfo();
            this.DocumentoEncontrado = false;
            this.SelectedServices.Clear();
            this.SelectedPersonalAsistencial = null;
            this.Observaciones = string.Empty;
            this.SelectedDiagnose = null;
            this.CodeToSearch = string.Empty;
            this.VrDcto = 0.0;
            this.NoOrdenMedica = string.Empty;
            this.NumeroAutorizacion = string.Empty;
            this.NumeroDeMuestraAsignado.Consecutivo = string.Empty;
            this.CargarCentrosDeTomaManuales();
            this.EnviarPDF = false;
            this.CorreoElectronico = string.Empty;
            this.BusquedaRemitente = string.Empty;
            this.OnLimpiarVista(new EventArgs());
            this.Direccion = string.Empty;
            this.Telefono = string.Empty;
        }

        private void OnLimpiarVista(EventArgs e)
        {
            EventHandler handler = this.LimpiarVista;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnSolicitarCredencialesEvent()
        {
            EventHandler handler = this.SolicitarCredencialesEvent;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnPersonalAsistencial()
        {
            EventHandler handler = this.PersonalAsistencialEvento;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnDiagnosticosCargados()
        {
            EventHandler handler = this.DiagnosticosCargados;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private bool ValidarDatos()
        {
            bool hayError = false;

            if (this.SelectedServices.Count > 0 && this.PaymentDetails.Pagos.Count <= 0 && this.TotalAPagar > 0 && GlobalModelosCache.FormasDePagoList.Count == 1)
            {
                foreach (var convenioAPagar in this.PaymentDetails.LiquidationItems)
                {
                    var itemPago = new ItemPago
                    {
                        NoDocumento = string.Empty,
                        VrTipoPago = convenioAPagar.Valor,
                        CodConvenio = convenioAPagar.CodConvenio,
                        TipoPago = GlobalModelosCache.FormasDePagoList.First().TipoPago
                    };
                    this.PaymentDetails.Pagos.Add(itemPago);

                }
            }
            if (this.SelectedAgreement.Copago == "MANUAL")
            {
                int DatoNivel;
                if (!int.TryParse(this.Nivel, out DatoNivel))
                {
                    this.OnMostrarMensajeEvent("Debe Ingresar un Vr de pago compartido");
                    hayError = true;
                }
            }

            if (string.IsNullOrWhiteSpace(this.OscPrefix))
            {
                this.OnMostrarMensajeEvent("Debe seleccionar Un Prefijo Para la Orden");
                hayError = true;
            }

            if (this.TerceroExacto == null)
            {
                this.OnMostrarMensajeEvent("Debe seleccionar Un Paciente");
                hayError = true;
            }

            if (App.NoOrdenMedicaRequired && string.IsNullOrWhiteSpace(this.NoOrdenMedica))
            {
                this.OnMostrarMensajeEvent("El Numero de Orden Medica es Requerido");
                hayError = true;
            }

            if (this.TerceroExacto != null && this.EnEmbarazo && !this.FUR.HasValue)
            {
                this.OnMostrarMensajeEvent("Debe Indicar la Fecha de la ultima Regla.");
                hayError = true;
            }

            if (this.TerceroExacto != null && this.EnviarPDF && string.IsNullOrWhiteSpace(this.CorreoElectronico))
            {
                this.OnMostrarMensajeEvent("Debe Indicar una Dirección de Correo Electrónico.");
                hayError = true;
            }

            if (this.SelectedRequester == null)
            {
                this.OnMostrarMensajeEvent("Debe seleccionar Un Remitente");
                hayError = true;
            }

            if (this.ProgramarCita && this.CitaSeleccionada == null)
            {
                this.OnMostrarMensajeEvent("Selecciono Programar Cita pero no la ha programado.");
                hayError = true;
            }

            if (this.SelectedProvider == null)
            {
                this.OnMostrarMensajeEvent("Debe seleccionar Un Prestador");
                hayError = true;
            }

            if (this.SelectedDiagnose == null)
            {
                this.OnMostrarMensajeEvent("No se ha seleccionado Diagnostico o el código no Corresponde.");
                hayError = true;
            }

            if (App.PersonalAsistencialEsRequerido && this.SelectedPersonalAsistencial == null)
            {
                this.OnMostrarMensajeEvent("No se ha seleccionado Personal Remitente  o el código no es Válido.");
                hayError = true;
            }

            if (this.SelectedServices.Count <= 0)
            {
                this.OnMostrarMensajeEvent("No se ha Adicionado Ningun Item");
                hayError = true;
            }

            if (this.SelectedServices.Count > 0 && this.PaymentDetails.Pagos.Count <= 0 && this.TotalAPagar > 0)
            {
                this.OnMostrarMensajeEvent("No se ha Adicionado Ningun Pago");
                hayError = true;
            }

            if (this.NumeroDeMuestraAsignado.TipoAsignacion == AsigNoMuestra.DIA && !this.numeroDeMuestraAsignado.IsValid)
            {
                var error = new StringBuilder();
                error.AppendLine("El Número de Muestra Asignado Tiene Errores:");
                foreach (KeyValuePair<string, List<ValidationResult>> keyValuePair in this.NumeroDeMuestraAsignado.ErroresPorPropiedad)
                {
                    foreach (ValidationResult validationResult in keyValuePair.Value)
                    {
                        error.AppendLine("- " + validationResult.ErrorMessage);
                    }
                }

                this.OnMostrarMensajeEvent(error.ToString());
                hayError = true;
            }

            return !hayError;
        }

        private void OnCrearNuevoTerceroEvent()
        {
            ParametroTextoObjetoEventHandler handler = this.CrearNuevoTerceroEvent;
            if (handler != null)
            {
                handler(this, this.DocIdSearch, this.TypeDocIdSearch);
            }
        }

        private void OnEditarTerceroEvent()
        {
            ParametroTextoObjetoEventHandler handler = this.EditarTerceroEvent;
            if (handler != null)
            {
                handler(this, this.DocIdSearch, this.TypeDocIdSearch);
            }
        }

        private void OnSavedOscEvent(string title, OscInsertaResult saveResult)
        {
            ParametroTextoObjetoEventHandler handler = this.SavedOscEvent;
            if (handler != null)
            {
                handler(this, title, saveResult);
            }
        }

        private void OnProgramarNuevaCitaEvent()
        {
            ParametroTextoEventHandler handler = this.ProgramarNuevaCitaEvent;
            if (handler != null)
            {
                handler(this, this.TerceroExacto.NumeroUnicoDocumento);
            }
        }

        private void OnMostrarVentanaReporteEvent(string listaOsc, string listaFactura)
        {
            ParametroTextoTextoEventHandler handler = this.MostrarVentanaReporteEvent;
            if (handler != null)
            {
                handler(this, listaOsc, listaFactura);
            }
        }

        public void LoadPersonalAsistencial(Tuple<string, string> parameters = null)
        {
            // ToDo: Tratar de Hacerlo Por Interfaces
            StoreProceduresAsync.GetPersonalAsistencialNewAsync(parameters,
                (ss, ee) =>
                {
                    if (ee.Cancelled)
                    {
                        Trace.WriteLine("GetPersonalAsistencialAsync Cancelado");
                    }
                    else if (ee.Error != null)
                    {
                        Trace.WriteLine("GetPersonalAsistencialAsync Error: " + ee.Error.Message);
                    }
                    else
                    {
                        var personalAsistencials = ee.Result as List<PersonalAsistencial>;

                        if (personalAsistencials != null)
                        {
                            GlobalModelosCache.RefreshPersonalAsistencialList(personalAsistencials);
                        }
                        this.OnPersonalAsistencial();
                    }
                });
        }

        private void GuardarEdadExecute()
        {
            if (this.EdadAGuardar.HasValue)
            {
                this.CurrentPatientIsBusy = true;
                StoreProceduresAsync.GuardarFechaNacimiento(
                    new PatientInfo
                    {
                        UniqueDocumentId = this.CurrentPatient.UniqueDocumentId,
                        BirthDate = this.EdadAGuardar
                    },
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            MessageBox.Show("Error:\n" + ee.Error.Message, "Error al Guardar la Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            this.VerifyPatienId();
                        }
                    });
            }
        }

        private bool GuardarEdadCanExecute()
        {
            return this.EdadAGuardar.HasValue;
        }

        private void ProgramarNuevaCitaExecute()
        {
            if (this.ProgramarCita && this.TerceroExacto != null)
            {
                this.OnProgramarNuevaCitaEvent();
            }
        }

        private void SolicitarCredencialesExecute()
        {
            this.OnSolicitarCredencialesEvent();
        }

        private void OnConsultarExamenesEvent(string parametro1, object parametro2)
        {
            ParametroTextoObjetoEventHandler handler = this.ConsultarExamenesEvent;
            if (handler != null)
            {
                handler(this, parametro1, parametro2);
            }
        }

        private void ConsultarExamenesExecuted()
        {
            this.OnConsultarExamenesEvent(this.SelectedService.BaseEntity.Code, this.Antecedentes.Where(ant => ant.CodigoFuente == this.SelectedService.BaseEntity.Code).ToList());
        }

        private void EditarTerceroExecuted()
        {
            this.OnEditarTerceroEvent();
        }

        private bool ConsultarExamenesCanExecuted()
        {
            return this.SelectedService.IsValid && this.SelectedServiceHasDetails;
        }

        private bool EditarTerceroCanExecuted()
        {
            return this.CurrentPatient != null && this.TerceroExacto != null && !string.IsNullOrWhiteSpace(this.CurrentPatient.IdDocumentType) && !string.IsNullOrWhiteSpace(this.CurrentPatient.IdDocument);
        }

        private void OnBuscarServicioEvent(string parametro1, object parametro2)
        {
            ParametroTextoObjetoEventHandler handler = this.BuscarServicioEvent;
            handler?.Invoke(this, parametro1, parametro2);
        }

        private void BuscarServicioExecute()
        {
            this.OnBuscarServicioEvent(
                string.Empty,
                new SearchServiceParameters
                {
                    Provider = this.SelectedProvider,
                    Code = string.Empty,
                    ServiceAgreement = this.SelectedAgreement,
                    ServiceRequester = this.SelectedRequester,
                    Gender = this.CurrentPatient.Gender
                });
        }

        private void LiquidacionPagoExecute()
        {
            this.OnLiquidacionPagoEvent(string.Empty, this);
        }

        private bool BuscarServicioCanExecute()
        {
            return this.SelectedAgreement != null && this.SelectedProvider != null && this.SelectedRequester != null;
        }

        private bool LiquidacionPagoCanExecute()
        {
            return this.LiquidationValues.Count > 0;
        }

        private bool ProgramarNuevaCitaCanExecute()
        {
            return this.ProgramarCita;
        }

        private void UpdateLiquidation()
        {
            if (this.SelectedServices.Count > 0)
            {
                StoreProceduresAsync.GetLiquidationAsync(
                    new LiquidationParameters(this.SelectedServices) { CurrentPatient = this.CurrentPatient, ValorDescuento = (this.EsNumerico ? this.VrDcto : (double)((this.TotalLiquidacion * (decimal)this.VrDcto) / 100)) },
                    (s, e) =>
                    {
                        this.LiquidationValues.Clear();
                        this.PaymentDetails.Pagos.Clear();

                        if (e.Cancelled)
                        {
                        }
                        else if (e.Error != null)
                        {
                        }
                        else
                        {
                            var oscDfLiquidaRows = e.Result as List<OscDataModel.PRO_OSCDFLiquidaPagoRow>;

                            if (oscDfLiquidaRows != null)
                            {
                                foreach (var oscDfLiquidaRow in oscDfLiquidaRows)
                                {
                                    this.LiquidationValues.Add(oscDfLiquidaRow);
                                }
                            }

                        }
                    });
            }
            else
            {
                this.LiquidationValues.Clear();
                this.PaymentDetails.Pagos.Clear();
            }
        }

        public static void EnableCollectionSynchronization(IEnumerable collection, object lockObject)
        {
            // Equivalent to .NET 4.5:
            // BindingOperations.EnableCollectionSynchronization(collection, lockObject);
            MethodInfo method = typeof(BindingOperations).GetMethod("EnableCollectionSynchronization", new Type[] { typeof(IEnumerable), typeof(object) });
            if (method != null)
            {
                method.Invoke(null, new object[] { collection, lockObject });
                Trace.WriteLine("EnableCollectionSynchronization called");
            }
        }

        private void OnLiquidacionPagoEvent(string parametro1, object parametro2)
        {
            this.LiquidacionPagoEvent?.Invoke(this, parametro1, parametro2);
        }

    }
}
