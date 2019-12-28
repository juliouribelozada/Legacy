// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreProceduresAsync.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the StoreProceduresAsync type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NeuronCloud.Atpc.Co.Modelos;

namespace Neuron.OSC.Data.AsyncOps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;

    using Atpc.Common.Async;

    using Neuron.HIS.Models.Common;
    using Neuron.OSC.Data.Model;
    using Neuron.OSC.Data.Model.OscDataModelTableAdapters;
    using Neuron.UI.Controls.Models;

    using NeuronCloud.Atpc.Co.Modelos.Helpers;

    public class StoreProceduresAsync
    {

        public static void GetClaimsAsync(string numeroUnicoDocUsuario, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(numeroUnicoDocUsuario, ProUsuarioRolFuncionAction, resultadoOperacion);
        }

        public static void SetFingerprintAsync(Tuple<string, byte[]> parametros, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parametros, SetFingerprintAction, resultadoOperacion);
        }


        public static void GetPatientInfo(string documentUniqueNumber, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(documentUniqueNumber, GetAction, resultadoOperacion);
        }

        public static void GetBookingSearch(Tuple<string, string> parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetBookingSearch, resultadoOperacion);
        }

        public static void GuardarFechaNacimiento(PatientInfo parametro, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parametro, GuardarFechaNacimientoAction, resultadoOperacion);
        }

        public static void SaveAddressAsync(PatientInfo parametro, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parametro, SaveAddressAction, resultadoOperacion);
        }

        public static void SavePhoneNumberAsync(PatientInfo parametro, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parametro, SavePhoneNumberAction, resultadoOperacion);
        }

        public static void GetServiceAgreements(PatientInfo patient, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(patient, GetServiceAgreementsAction, resultadoOperacion);
        }

        public static void GetServiceProviders(ServiceAgreement agreement, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(agreement, GetServiceProvidersAction, resultadoOperacion);
        }

        public static void GetServiceRequesters(ServiceAgreement selectedAgreement, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(selectedAgreement, GetServiceRequestersAction, resultadoOperacion);
        }

        public static void GetServiceUnits(ServiceUnitParameters parameter, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameter, GetServiceUnitsAction, resultadoOperacion);
        }

        public static void GetServiceByCode(SearchServiceParameters parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetServiceByCodeAction, resultadoOperacion);
        }

        public static void GetServiceCodeByName(SearchServiceParameters parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetServiceCodeByNameAction, resultadoOperacion);
        }

        public static void GetDiagnoseAutoCompleteAsync(Tuple<string, PatientInfo> parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetDiagnoseAutoCompleteAction, resultadoOperacion);
        }

        public static void GetExamenesRecientesAsync(Tuple<string, string, string> parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            Debug.WriteLine("GetExamenesRecientesAction:\t " + DateTime.Now);
            new AsyncTask(parameters, GetExamenesRecientesAction, resultadoOperacion);
        }

        public static void SaveOscAsync(SaveOSCParameters parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, SaveOscAction, resultadoOperacion);
        }

        public static void GetLiquidationAsync(LiquidationParameters parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetLiquidationAction, resultadoOperacion);
        }

        public static void GetLabelsAsync(string idNumMuestra, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(idNumMuestra, GetLabelsAction, resultadoOperacion);
        }
        public static void GetLabelsOSCAsync(string docUnicoOSC, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(docUnicoOSC, GetLabelsOSCAction, resultadoOperacion);
        }

        private static void GetServiceAgreementsAction(object sender, DoWorkEventArgs e)
        {
            var patientInfo = e.Argument as PatientInfo;

            if (patientInfo != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<ServiceAgreement> output = context.PRO_ConvenioSeleccPaciOSC(patientInfo.IdDocument, patientInfo.IdDocumentType, null).Select(ProxyConverters.FromPRO_ConvenioSeleccPaciOSC_ResultToServiceAgreement).ToList();
                    e.Result = output;
                }
            }
        }

        private static void GetServiceProvidersAction(object sender, DoWorkEventArgs e)
        {
            var agreement = e.Argument as ServiceAgreement;

            if (agreement != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<ServiceProvider> output = context.PRO_ConvenioPrestSelecc(agreement.Code).Select(ProxyConverters.PRO_ConvenioPrestSeleccToServiceProvider).ToList();
                    e.Result = output;
                }
            }
        }

        private static void GetServiceRequestersAction(object sender, DoWorkEventArgs e)
        {
            var agreement = e.Argument as ServiceAgreement;

            if (agreement != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<ServiceRequester> output = context.PRO_ConvenioRemitenteSelecc(agreement.Code).Select(ProxyConverters.PRO_ConvenioRemitenteSelecc_ResultToServiceRequester).ToList();
                    e.Result = output;
                }
            }
        }

        private static void GetServiceUnitsAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as ServiceUnitParameters;

            if (parameters != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<ServiceUnit> output = context.PRO_ConvenioRemiteServSelecc(parameters.CodigoConvenio, parameters.CodigoRemitente).Select(ProxyConverters.PRO_ConvenioRemiteServSelecc_ResultToServiceUnit).ToList();
                    e.Result = output;
                }
            }
        }

        private static void GetDiagnoseAutoCompleteAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as Tuple<string, PatientInfo>;

            if (parameters != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<Diagnose> output = context.PRO_DiagnosticoSeleccAutocomplete(parameters.Item1, parameters.Item2.GenderAsString).Select(ProxyConverters.PRO_DiagnosticoSeleccAutocomplete_ResultToDiagnose).ToList();
                    e.Result = output;
                }
            }
        }

        ////private static void GetPersonalAsistencialAction(object sender, DoWorkEventArgs e)
        ////{
        ////    var parameters = e.Argument as string;
        ////    Debug.WriteLine("GetPersonalAsistencialAction Inicio:\t " + DateTime.Now);
        ////    if (!string.IsNullOrWhiteSpace(parameters))
        ////    {
        ////        using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
        ////        {
        ////            List<CRUD_PersonalAsistencialSelecciona_Result> output = context.CRUD_PersonalAsistencialSelecciona(parameters).ToList();
        ////            e.Result = output;
        ////        }
        ////    }

        ////    Debug.WriteLine("GetPersonalAsistencialAction Fin:\t " + DateTime.Now);
        ////}

        private static void GetServiceByCodeAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as SearchServiceParameters;

            if (parameters != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<Service> output = context.PRO_ConvenioPrestPortafoSelecc(parameters.ServiceAgreement.Code, parameters.Provider.Code, parameters.ServiceRequester.Code, null, parameters.Code, string.Empty, parameters.GenderAsString).Select(ProxyConverters.PRO_ConvenioPrestPortafoSelecc_ResultToService).ToList();
                    Service mainService = output.FirstOrDefault(servicio => servicio.EsCombo);
                    if (mainService != null)
                    {
                        if (mainService.ComponentesCombo != null)
                        {
                            mainService.ComponentesCombo.Clear();
                            mainService.ComponentesCombo.AddRange(output.Where(servicio => !servicio.EsCombo));
                            if (!(mainService.Price > 0))
                            {
                                mainService.Price = mainService.ComponentesCombo.Sum(servicio => servicio.Price);
                            }

                            foreach (Service service in mainService.ComponentesCombo)
                            {
                                service.ServiceAgreementCode = parameters.ServiceAgreement.Code;
                                service.ProviderCode = parameters.Provider.Code;
                                service.Level = parameters.ServiceAgreement.Nivel;
                            }
                        }
                    }
                    else
                    {
                        mainService = output.FirstOrDefault();
                    }

                    if (mainService != null)
                    {
                        mainService.ServiceAgreementCode = parameters.ServiceAgreement.Code;
                        mainService.ProviderCode = parameters.Provider.Code;
                        mainService.Level = parameters.ServiceAgreement.Nivel;
                    }

                    e.Result = mainService;
                }
            }
        }

        private static void GetServiceCodeByNameAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as SearchServiceParameters;

            if (parameters != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<PRO_ConvenioPrestPortafoSeleccAutoComplete_Result> output = context.PRO_ConvenioPrestPortafoSeleccAutoComplete(parameters.ServiceAgreement.Code, parameters.Provider.Code, parameters.ServiceRequester.Code, null, parameters.SearchChars, parameters.GenderAsString).ToList();
                    e.Result = output;
                }
            }
        }

        private static void SaveOscAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as SaveOSCParameters;

            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            if (parameters != null)
            {
                var paciente = parameters.CurrentPatient.OriginalObject as CRUD_TerceroSeleccionaExacto_Result
                               ?? new CRUD_TerceroSeleccionaExacto_Result();

                ////var detalle = new DataTable();
                ////detalle.Columns.Add("Cant", typeof(decimal));
                //////// detalle.Columns.Add("CodigoManual");
                ////detalle.Columns.Add("CodigoFuente");
                ////detalle.Columns.Add("VrItem", typeof(decimal));

                var detalleServicios = new OscDataModel.OscDfLiquidaDataTable();

                foreach (CRUDEntity<Service> selectedService in parameters.SelectedServices)
                {
                    ////var row = detalle.NewRow();
                    ////row["VrItem"] = selectedService.BaseEntity.Price;
                    ////row["CodigoFuente"] = selectedService.BaseEntity.Code;
                    //////// row["CodigoManual"] = selectedService.BaseEntity.Plan;
                    ////row["Cant"] = selectedService.BaseEntity.Quantity;
                    ////detalle.Rows.Add(row);

                    var rowServicio = detalleServicios.NewOscDfLiquidaRow();

                    rowServicio.VrItem = (decimal)selectedService.BaseEntity.Price;
                    rowServicio.CodigoFuente = selectedService.BaseEntity.Code;
                    rowServicio.Cant = (decimal)selectedService.BaseEntity.Quantity;
                    rowServicio.CodConvenio = selectedService.BaseEntity.ServiceAgreementCode;
                    rowServicio.CodPrestador = selectedService.BaseEntity.ProviderCode;
                    rowServicio.Nivel  = selectedService.BaseEntity.Level;
                    decimal number;
                    if (Decimal.TryParse(selectedService.BaseEntity.Level, out number))
                    {
                        rowServicio.VrAporteComple = decimal.Parse(selectedService.BaseEntity.Level);
                    }

                    detalleServicios.Rows.Add(rowServicio);
                }

                var detallePagos = new OscDataModel.OSCTipoPagoDataTable();
                foreach (ItemPago pago in parameters.Pagos)
                {
                    var rowPago = detallePagos.NewOSCTipoPagoRow();

                    rowPago.TipoPago = pago.TipoPago;
                    rowPago.CodConvenio = pago.CodConvenio;
                    rowPago.VrTipoPago = pago.VrTipoPago;
                    rowPago.NoDocumento = pago.NoDocumento;

                    detallePagos.Rows.Add(rowPago);
                }

                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = new SqlConnection(((System.Data.EntityClient.EntityConnection)context.Connection).StoreConnection.ConnectionString),
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "Caja.PRO_OSCInserta"
                    };

                    //var command = new SqlCommand
                    //{
                    //    Connection = new SqlConnection(((System.Data.EntityClient.EntityConnection)context.Connection).StoreConnection.ConnectionString),
                    //    CommandType = CommandType.StoredProcedure,
                    //    CommandText = "[CAJA].[PRO_OSCWEBInserta]"
                    //};

                    command.Parameters.Add(new SqlParameter("@PrefijoNoOSC", SqlDbType.VarChar) { Value = parameters.OSCPrefix });
                    command.Parameters.Add(new SqlParameter("@Barrio", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.BarrioResidencia) ? string.Empty : paciente.BarrioResidencia });
                    command.Parameters.Add(new SqlParameter("@CodDiag", SqlDbType.VarChar) { Value = parameters.CurrentDiagnosis.Code });
                    command.Parameters.Add(new SqlParameter("@CodPersoAsisten", SqlDbType.VarChar) { Value = parameters.PersonalAsistencial == null ? string.Empty : parameters.PersonalAsistencial.Codigo });
                    command.Parameters.Add(new SqlParameter("@CodRemitente", SqlDbType.VarChar) { Value = parameters.CurrentServiceRequester.Code });
                    command.Parameters.Add(new SqlParameter("@VrDcto", SqlDbType.Decimal) { Value = string.IsNullOrWhiteSpace(parameters.VrDcto) ? string.Empty : parameters.VrDcto });
                    command.Parameters.Add(new SqlParameter("@DireccionResidencia", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.DireccionResidencia) ? string.Empty : paciente.DireccionResidencia });
                    command.Parameters.Add(new SqlParameter("@FechaUltimaRegla", SqlDbType.Date) { Value = parameters.FUR ?? default(DateTime) });
                    command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.Date) { Value = DateTime.Now });
                    command.Parameters.Add(new SqlParameter("@IdSistema", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.IdSistema) ? string.Empty : parameters.IdSistema });
                    command.Parameters.Add(new SqlParameter("@NumeroAutorizacion", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.NumeroAutorizacion) ? string.Empty : parameters.NumeroAutorizacion });
                    command.Parameters.Add(new SqlParameter("@NumeroUnicoDocumento", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.NumeroUnicoDocumento) ? string.Empty : paciente.NumeroUnicoDocumento });
                    command.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.Observaciones) ? string.Empty : parameters.Observaciones });
                    command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.TelefonoResidencia) ? string.Empty : paciente.TelefonoResidencia });
                    command.Parameters.Add(new SqlParameter("@EnviarPDF", SqlDbType.Bit) { Value = parameters.EnviarResultadoPorCorreo });
                    command.Parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.CorreoElectronico) ? string.Empty : parameters.CorreoElectronico });
                    command.Parameters.Add(new SqlParameter("@IdProgramaAgenda", SqlDbType.VarChar) { Value = parameters.IdProgramaAgenda });
                    command.Parameters.Add(new SqlParameter("@UsuarioRegistro", SqlDbType.VarChar) { Value = ConfiguracionGlobal.IPrincipalActual.Identity.Name });
                    command.Parameters.Add(new SqlParameter("@VrPagar", SqlDbType.Decimal) { Value = parameters.Total });
                    command.Parameters.Add(new SqlParameter("@NoOrdenMedica", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.NoOrdenMedica) ? string.Empty : parameters.NoOrdenMedica });
                    command.Parameters.Add(new SqlParameter("@AsigNoMuestra", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.AsigNoMuestra.ToString()) ? string.Empty : parameters.AsigNoMuestra.ToString() });
                    command.Parameters.Add(new SqlParameter("@IdMuestraCentroAsig", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.NumeroDeMuestraAsignado.NumeroAsignado) ? string.Empty : parameters.NumeroDeMuestraAsignado.NumeroAsignado });
                    command.Parameters.Add(new SqlParameter("@PrefijoCentroToma", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.NumeroDeMuestraAsignado.CentroDeToma.Prefijo) ? string.Empty : parameters.NumeroDeMuestraAsignado.CentroDeToma.Prefijo });
                    command.Parameters.Add(new SqlParameter("@ServicioUbicacion", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.ServicioUbicacion) ? string.Empty : parameters.ServicioUbicacion });
                    command.Parameters.Add(new SqlParameter("@Cama", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.Cama) ? string.Empty : parameters.Cama });
                    command.Parameters.Add(new SqlParameter("@SedeAtencion", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.Sede) ? string.Empty : parameters.Sede });
                    command.Parameters.Add(new SqlParameter("@NoCita", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(parameters.BookingNumber) ? string.Empty : parameters.BookingNumber });

                    ////command.Parameters.Add(new SqlParameter("@Edad", SqlDbType.VarChar) { Value = paciente.Edad });
                    ////command.Parameters.Add(new SqlParameter("@EdadDias", SqlDbType.Int) { Value = paciente.EdadDias });
                    ////command.Parameters.Add(new SqlParameter("@Genero", SqlDbType.VarChar) { Value = paciente.Genero });
                    ////command.Parameters.Add(new SqlParameter("@EstadoAfiliacion", SqlDbType.VarChar) { Value = parameters.CurrentServiceAgreement.EstadoAfiliacion });
                    ////command.Parameters.Add(new SqlParameter("@Nivel", SqlDbType.VarChar) { Value = parameters.Nivel });
                    ////command.Parameters.Add(new SqlParameter("@NomTarifario", SqlDbType.VarChar) { Value = "Testo" });
                    ////command.Parameters.Add(new SqlParameter("@NomPlanBeneficio", SqlDbType.VarChar) { Value = "Testo" });
                    ////command.Parameters.Add(new SqlParameter("@PrimerApellido", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.PrimerApellido) ? string.Empty : paciente.PrimerApellido });
                    ////command.Parameters.Add(new SqlParameter("@PrimerNombre", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.PrimerNombre) ? string.Empty : paciente.PrimerNombre });
                    ////command.Parameters.Add(new SqlParameter("@SegundoApellido", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.SegundoApellido) ? string.Empty : paciente.SegundoApellido });
                    ////command.Parameters.Add(new SqlParameter("@SegundoNombre", SqlDbType.VarChar) { Value = string.IsNullOrWhiteSpace(paciente.SegundoNombre) ? string.Empty : paciente.SegundoNombre });
                    ////command.Parameters.Add(new SqlParameter("@TipoAutorizacion", SqlDbType.VarChar) { Value = "Testo" });
                    ////command.Parameters.Add(new SqlParameter("@TipoUsuario", SqlDbType.VarChar) { Value = "Testo" });
                    ////command.Parameters.Add(new SqlParameter("@ZonaResidencia", SqlDbType.VarChar) { Value = paciente.Zona });
                    ////command.Parameters.Add(new SqlParameter("@CodConvenio", SqlDbType.VarChar) { Value = parameters.CurrentServiceAgreement.Code });
                    ////command.Parameters.Add(new SqlParameter("@CodMPio", SqlDbType.VarChar) { Value = paciente.CodMpioResidencia });
                    ////command.Parameters.Add(new SqlParameter("@CodPrestador", SqlDbType.VarChar) { Value = parameters.CurrentProvider.Code });

                    command.Parameters.Add(new SqlParameter("@OSCDet", SqlDbType.Structured) { Value = detalleServicios });
                    command.Parameters.Add(new SqlParameter("@OSCTipoPago", SqlDbType.Structured) { Value = detallePagos });

                    // Output Parameters:
                    ////command.Parameters.Add(new SqlParameter("@DocUnicoOSC", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output });
                    ////command.Parameters.Add(new SqlParameter("@NoMuestra", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output });

                    command.Parameters.Add(new SqlParameter("@Error", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@Token", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@IdNumMuestra", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@IdMuestraCentro", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@MensajeMuestra", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@ListaFactura", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@ListaOSC", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });

                    command.Connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    finally
                    {
                        command.Connection.Close();
                    }

                    //// e.Result = Tuple.Create(
                    //// command.Parameters["@DocUnicoOSC"].Value,
                    //// command.Parameters["@Token"].Value, 
                    //// command.Parameters["@NoMuestra"].Value, 
                    //// command.Parameters["@IdNumMuestra"].Value,
                    //// command.Parameters["@IdMuestraCentro"].Value);

                    e.Result = new OscInsertaResult
                    {
                        Error = command.Parameters["@Error"].Value as string,
                        IdMuestraCentro = command.Parameters["@IdMuestraCentro"].Value as int?,
                        IdNumMuestra = command.Parameters["@IdNumMuestra"].Value as int?,
                        ListaFactura = command.Parameters["@ListaFactura"].Value as string,
                        ListaOsc = command.Parameters["@ListaOSC"].Value as string,
                        MensajeMuestra = command.Parameters["@MensajeMuestra"].Value as string,
                        Token = command.Parameters["@Token"].Value as string
                    };
                }
            }
        }

        private static void GetLiquidationAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as LiquidationParameters;

            if (parameters != null)
            {
                var paciente = parameters.CurrentPatient.OriginalObject as CRUD_TerceroSeleccionaExacto_Result
                               ?? new CRUD_TerceroSeleccionaExacto_Result();

                var detalle = new OscDataModel.OscDfLiquidaDataTable();

                foreach (CRUDEntity<Service> service in parameters.Services)
                {
                    var row = detalle.NewOscDfLiquidaRow();
                    row.Cant = (decimal)service.BaseEntity.Quantity;
                    row.CodConvenio = service.BaseEntity.ServiceAgreementCode;
                    row.CodPrestador = service.BaseEntity.ProviderCode;
                    row.CodigoFuente = service.BaseEntity.Code;
                    row.Nivel = service.BaseEntity.Level;
                    row.VrItem = (decimal)service.BaseEntity.Price;
                    decimal number;
                    if (Decimal.TryParse(service.BaseEntity.Level, out number))
                    {
                        row.VrAporteComple = decimal.Parse(service.BaseEntity.Level);
                    }

                    detalle.Rows.Add(row);
                }

                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    var descuento = parameters.ValorDescuento;
                    var ta = new PRO_OSCDFLiquidaPagoTableAdapter();
                    ta.Connection.ConnectionString = ((System.Data.EntityClient.EntityConnection)context.Connection).StoreConnection.ConnectionString;

                    var nud = string.IsNullOrWhiteSpace(paciente.NumeroUnicoDocumento)
                                  ? string.Empty
                                  : paciente.NumeroUnicoDocumento;
                    var result = ta.GetData(nud, detalle, (decimal)descuento);

                    e.Result = result.ToList();
                }
            }
        }

        private static void ProUsuarioRolFuncionAction(object sender, DoWorkEventArgs e)
        {
            var numeroUnicoDocumento = e.Argument as string;
            if (!string.IsNullOrWhiteSpace(numeroUnicoDocumento))
            {
                using (var contexto = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    try
                    {
                        var resul = contexto.PRO_UsuarioRolFuncion(numeroUnicoDocumento, "CapturaManual");
                        if (resul != null)
                        {
                            OscClaims.SetClaims(resul.Where(claim => claim.Aplicacion.ToUpperInvariant() == "OSC").Select(claim => claim.Funcion).ToArray());
                            e.Result = true;
                        }
                        else
                        {
                            e.Result = false;
                        }
                    }
                    catch (Exception)
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }
        private static void SetFingerprintAction(object sender, DoWorkEventArgs e)
        {
            var parametros = e.Argument as Tuple<string, byte[]>;
            if (parametros != null)
            {
                using (var contexto = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    try
                    {
                        var usuario = contexto.FingerPrintRegister.FirstOrDefault((fp) => (fp.NumeroUnicoDocumento == parametros.Item1));
                        if (usuario == null)
                        {
                            contexto.AddToFingerPrintRegister(new FingerPrintRegister() { FingerPrint = parametros.Item2, NumeroUnicoDocumento = parametros.Item1 });

                        }
                        else
                        {
                            usuario.FingerPrint = parametros.Item2;
                        }
                        contexto.SaveChanges();
                    }
                    catch (Exception eee)
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }


        private static void GetAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as string;

            if (!string.IsNullOrEmpty(parameter))
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    PatientInfo output = context.CRUD_TerceroSeleccionaExacto(parameter).FirstOrDefault().ToPatientInfo();
                    e.Result = output;
                }
            }
        }

        private static void GetBookingSearch(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as Tuple<string, string>;

            if (parameters != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    NoCita output = context.PRO_SearchBooking(parameters.Item1, parameters.Item2).FirstOrDefault().ToNoCitaInfo();
                    e.Result = output;
                }
            }
        }

        private static void GetLabelsAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as string;

            if (!string.IsNullOrEmpty(parameter))
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    int idNumMuestra;
                    if (int.TryParse(parameter, out idNumMuestra))
                    {
                        List<PRO_TipoMuestraIdNum_Result> output = context.PRO_TipoMuestraIdNum(idNumMuestra).ToList();
                        e.Result = output;
                    }
                    else
                    {
                        throw new ArgumentException("El Numero de Muestra no es un Entero");
                    }
                }
            }
        }
        private static void GetLabelsOSCAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as string;

            if (!string.IsNullOrEmpty(parameter))
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {

                    List<PRO_PrinterCodeBar_Result> output = context.PRO_PrinterCodeBar(parameter).ToList();
                    e.Result = output;
                }
            }
        }

        private static void GuardarFechaNacimientoAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as PatientInfo;

            if (parameter != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    Tercero output = context.Tercero.FirstOrDefault(tercero => tercero.NumeroUnicoDocumento == parameter.UniqueDocumentId);
                    if (output != null)
                    {
                        output.FechaNacimiento = parameter.BirthDate;
                        e.Result = context.SaveChanges();
                    }
                }
            }
        }

        private static void SaveAddressAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as PatientInfo;

            if (parameter != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    Tercero output = context.Tercero.FirstOrDefault(tercero => tercero.NumeroUnicoDocumento == parameter.UniqueDocumentId);
                    if (output != null)
                    {
                        output.DireccionDomicilio = parameter.FullName;
                        e.Result = context.SaveChanges();
                    }
                }
            }
        }

        private static void SavePhoneNumberAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as PatientInfo;

            if (parameter != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    Tercero output = context.Tercero.FirstOrDefault(tercero => tercero.NumeroUnicoDocumento == parameter.UniqueDocumentId);
                    if (output != null)
                    {
                        output.TelefonoDomicilio = parameter.FullName;
                        e.Result = context.SaveChanges();
                    }
                }
            }
        }

        private static void GetExamenesRecientesAction(object sender, DoWorkEventArgs e)
        {
            var agreement = e.Argument as Tuple<string, string, string>;

            if (agreement != null)
            {
                using (var context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<Antecedente> output = context.PRO_ConsultaResultadoPaciente(agreement.Item1, agreement.Item2, agreement.Item3).Select(ProxyConverters.FromPRO_ConsultaResultadoPacienteToAntecedente).ToList();
                    e.Result = output;
                }
            }
        }

        public static void GetPersonalAsistencialNewAsync(Tuple<string, string> parameters, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parameters, GetPersonalAsistencialAction, resultadoOperacion);
        }

        private static void GetPersonalAsistencialAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                var parametros = e.Argument as Tuple<string, string>;
                if (parametros == null)
                {
                    e.Result = new List<PersonalAsistencial>();
                    return;
                }


                List<PersonalAsistencial> resultado =
                    context.PRO_SenderPersonSelecAutoComplete(parametros.Item1, parametros.Item2).Select(
                        personalAsistencial =>
                            new PersonalAsistencial
                            {
                                Codigo = personalAsistencial.CodPersoAsisten,
                                NombreCompleto = personalAsistencial.NombrePersoAsisten,
                                Profesion = personalAsistencial.Profesion
                            }).ToList();
                e.Result = resultado;
            }
        }
    }
}