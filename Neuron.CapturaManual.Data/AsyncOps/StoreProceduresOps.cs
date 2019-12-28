// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreProceduresOps.cs" company="Luis Soler">
//   Copyright © 2012-2015
// </copyright>
// <summary>
//   Defines the StoreProceduresOps type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.AsyncOps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows;

    using Neuron.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public class StoreProceduresOps
    {
        public static void PRO_CargaDatosPersoMuestraAsync(string identificadorMuestra, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(identificadorMuestra, PRO_CargaDatosPersoMuestraAction, resultadoOperacion);
        }

        public static void GetClaimsAsync(string numeroUnicoDocUsuario, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(numeroUnicoDocUsuario, ProUsuarioRolFuncionAction, resultadoOperacion);
        }

        public static void GetTechnicianObsAsync(Tuple<string, string> parameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parameters, GetTechnicianObsAction, resultadoOperacion);
        }

        public static void SaveTechnicianObsAsync(Tuple<string, string, string, string> parameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parameters, SaveTechnicianObsAction, resultadoOperacion);
        }

        public static void PRO_CargaCapturaSeccionAsync(string identificadorMuestra, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(identificadorMuestra, PRO_CargaCapturaSeccionAction, resultadoOperacion);
        }

        public static void PRO_AutorizaPersonalAsistencialAsync(ValidationParameters validationParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_AutorizaPersonalAsistencialAction, resultadoOperacion);
        }

        public static void PRO_CapturaManualAnalitolAsync(CapturaManualAnalitoParameters validationParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_CapturaManualAnalitoAction, resultadoOperacion);
        }

        public static void PRO_CargaCapturaCampoLargoAsync(CapturaManualAnalitoParameters validationParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_CargaCapturaCampoLargoAction, resultadoOperacion);
        }

        public static void PRO_CargaCapturaCuatroCamposAsync(CapturaManualAnalitoParameters queryParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(queryParameters, PRO_CargaCapturaCuatroCamposAction, resultadoOperacion);
        }
        public static void LoadPanelsAsync(CapturaManualAnalitoParameters queryParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(queryParameters, LoadPanelsAction, resultadoOperacion);
        }

        public static void PRO_CargaCapturaCuatroCamposDosBloquesAsync(CapturaManualAnalitoParameters queryParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(queryParameters, PRO_CargaCapturaCuatroCamposDosBloquesAction, resultadoOperacion);
        }

        public static void PRO_CapturaManualAnalitolAsync(CapturaManualAnalitoParameters validationParameters)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_CapturaManualAnalitoAction);
        }

        public static void PRO_CapturaManualValidaAsync(CapturaManualValidacionParameters validationParameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_CapturaManualValidaAction, resultadoOperacion);
        }

        public static void PRO_CapturaManualValidaAsync(CapturaManualValidacionParameters validationParameters)
        {
            new AccionAsincronaGenerica(validationParameters, PRO_CapturaManualValidaAction);
        }

        public static void PRO_ConsultaEstudioIdenMuestraAsync(string idMuestra, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(idMuestra, PRO_ConsultaEstudioIdenMuestraAction, resultadoOperacion);
        }

        public static void PRO_ConsultaResultadoEstudioAsync(string idResultado, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(idResultado, PRO_ConsultaResultadoEstudioAction, resultadoOperacion);
        }

        public static void PRO_CapturaManualHistoricoAsync(CapturaManualHistoricoParameters parameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parameters, PRO_CapturaManualHistoricoAction, resultadoOperacion);
        }

        public static void PRO_CapturaManualResultadoAnulaAsync(CapturaManualAnulaParameters parameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parameters, PRO_CapturaManualResultadoAnulaAction, resultadoOperacion);
        }

        public static void CargaFormulaAsync(CapturaManualFormulaParameters parameters, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parameters, PRO_CargaCapturaFormulaAction, resultadoOperacion);
        }

        public static void GetAlerts(PRO_CargaCapturaCuatroCampos_Result cuatroCamposResult)
        {
            new AccionAsincronaGenerica(cuatroCamposResult, GetAlertsAction);
        }

        public static void GetAlerts(PRO_CargaCapturaCuatroCamposDosBloques_Result cuatroCamposDosBloquesResult)
        {
            new AccionAsincronaGenerica(cuatroCamposDosBloquesResult, GetAlertsAction);
        }

        public static void GetSugestions(PRO_CargaCapturaCuatroCampos_Result cuatroCamposResult)
        {
            new AccionAsincronaGenerica(cuatroCamposResult, GetSugestionsAction);
        }

        public static void GetDatoLargoSugestions(PRO_CargaCapturaCuatroCampos_Result cuatroCamposResult, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(cuatroCamposResult, GetDatoLargoSugestionsAction, resultadoOperacion);
        }

        public static void GetDatoLargoMultiSugestions(PRO_CargaCapturaCuatroCampos_Result cuatroCamposResult, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(cuatroCamposResult, GetDatoLargoMultiSugestionsAction, resultadoOperacion);
        }

        public static void GetDatoLargoSugestions(PRO_CargaCapturaCampoLargo_Result proCargaCapturaCampoLargoResult, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(proCargaCapturaCampoLargoResult, GetDatoLargoSugestionsAction2, resultadoOperacion);
        }

        public static void GetDatoLargoSugestions(PRO_CargaCapturaSeccion_Result proCargaCapturaSeccion, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(proCargaCapturaSeccion, GetDatoLargoSugestionsAction3, resultadoOperacion);
        }

        public static void GetSampleTracking(Tuple<string, string, string> sampleParameters, OperacionAsincronaEventHandler continueAction)
        {
            new AccionAsincronaGenerica(sampleParameters, GetSampleTrackingAction, continueAction);
        }


        public static void GetSugestions(PRO_CargaCapturaCuatroCamposDosBloques_Result cuatroCamposDosBloquesResult)
        {
            new AccionAsincronaGenerica(cuatroCamposDosBloquesResult, GetSugestionsAction);
        }

        private static void PRO_CargaDatosPersoMuestraAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as string;

            if (!string.IsNullOrWhiteSpace(parameter))
            {
                using (NeuronCapturaManualEntities context = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    PRO_CargaDatosPersoMuestra_Result output = context.PRO_CargaDatosPersoMuestra(parameter).FirstOrDefault();
                    e.Result = output;
                }
            }
        }

        private static void PRO_CargaCapturaSeccionAction(object sender, DoWorkEventArgs e)
        {
            var parameter = e.Argument as string;

            if (!string.IsNullOrWhiteSpace(parameter))
            {
                using (NeuronCapturaManualEntities context = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    List<PRO_CargaCapturaSeccion_Result> output = context.PRO_CargaCapturaSeccion(parameter).ToList();
                    foreach (var proCargaCapturaSeccionResult in output)
                    {
                        proCargaCapturaSeccionResult.IsReady = true;
                    }

                    e.Result = output;
                }
            }
        }

        private static void PRO_AutorizaPersonalAsistencialAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as ValidationParameters;
            if (parameters != null)
            {
                using (var contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        PRO_AutorizaPersonalAsistencial_Result resul = contexto.PRO_AutorizaPersonalAsistencial(parameters.Password, parameters.SpecialityCode, parameters.State, parameters.UserName).FirstOrDefault();
                        if (resul != null)
                        {
                            e.Result = new ValidationResult
                            {
                                Result = true,
                                Name = resul.NombrePersoAsisten,
                                Code = resul.CodPersoAsisten,
                                Especialidad = resul.Profesion,
                                IdNumber = resul.NumeroUnicoDocumento
                            };
                        }
                        else
                        {
                            e.Result = new ValidationResult { Result = false };
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

        private static void ProUsuarioRolFuncionAction(object sender, DoWorkEventArgs e)
        {
            var numeroUnicoDocumento = e.Argument as string;
            if (!string.IsNullOrWhiteSpace(numeroUnicoDocumento))
            {
                using (var contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        var resul = contexto.PRO_UsuarioRolFuncion(numeroUnicoDocumento, "CapturaManual");
                        if (resul != null)
                        {
                            CapturaManualClaims.SetClaims(resul.Where(claim => claim.Aplicacion.ToUpperInvariant() == "CAPTURAMANUAL").Select(claim => claim.Funcion).ToArray());
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

        private static void SaveTechnicianObsAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as Tuple<string, string, string, string>;
            if (parameters != null)
            {
                using (var contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        e.Result = contexto.PRO_EstudioObservacionInserta(parameters.Item1, parameters.Item2, parameters.Item3, parameters.Item4);

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
        private static void GetTechnicianObsAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as Tuple<string, string>;
            if (parameters != null)
            {
                using (var contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        List<PRO_EstudioObservacion_Result> resul = contexto.PRO_EstudioObservacion(parameters.Item1, parameters.Item2).ToList();
                        e.Result = resul;
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

        private static void PRO_CapturaManualAnalitoAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnalitoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    var resul = contexto.PRO_CapturaManualAnalito(parameters.IdentificadorMuestra, parameters.CodigoFuente, parameters.Analito, parameters.Dato, parameters.DatoLargo, parameters.Observacion, parameters.Formato, parameters.UsuarioSistema, parameters.CodPersoAsisten);
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_CargaCapturaCampoLargoAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnalitoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        List<PRO_CargaCapturaCampoLargo_Result> resul = contexto.PRO_CargaCapturaCampoLargo(parameters.IdentificadorMuestra, parameters.CodigoFuente).OrderBy(c => c.PosicionReporte).ToList();
                        e.Result = resul;
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

        private static void PRO_CargaCapturaCuatroCamposAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnalitoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        List<PRO_CargaCapturaCuatroCampos_Result> resul = contexto.PRO_CargaCapturaCuatroCampos(parameters.IdentificadorMuestra, parameters.CodigoFuente).OrderBy(c => c.PosicionReporte).ToList();
                        e.Result = resul;
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

        private static void LoadPanelsAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnalitoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        List<PRO_CargaCapturaPanelOpcional_Result> resul = contexto.PRO_CargaCapturaPanelOpcional(parameters.CodigoFuente).ToList();
                        e.Result = resul;
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

        private static void PRO_CargaCapturaCuatroCamposDosBloquesAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnalitoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    try
                    {
                        List<PRO_CargaCapturaCuatroCampos_Result> resul = contexto.PRO_CargaCapturaCuatroCampos(parameters.IdentificadorMuestra, parameters.CodigoFuente).OrderBy(c => c.PosicionReporte).ToList();
                        e.Result = ProcesarListaCuatroCampos(resul);
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

        private static List<DatosEnColumnas> ProcesarListaCuatroCampos(List<PRO_CargaCapturaCuatroCampos_Result> list)
        {
            foreach (PRO_CargaCapturaCuatroCampos_Result proCargaCapturaCuatroCamposResult in list)
            {
                proCargaCapturaCuatroCamposResult.EsColumna = true;
            }

            var salida = new List<DatosEnColumnas>();
            int numeroDeFilas = list.Last().PosicionReporte / 2;
            for (int i = 0; i <= numeroDeFilas; i++)
            {
                var item = new DatosEnColumnas
                {
                    Columna0 = list.FirstOrDefault(it => (it.PosicionReporte - 1) / 2 == i && (it.PosicionReporte - 1) % 2 == 0),
                    Columna1 = list.FirstOrDefault(it => (it.PosicionReporte - 1) / 2 == i && (it.PosicionReporte - 1) % 2 == 1)
                };
                salida.Add(item);
            }

            return salida;
        }

        private static void PRO_CapturaManualValidaAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualValidacionParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    ObjectParameter errorParameter = new ObjectParameter("error", typeof(string));
                    ObjectParameter numRegistrosParameter = new ObjectParameter("noRegProc", typeof(int));
                    contexto.PRO_CapturaManualValida(parameters.Tipo, parameters.CodigoFuente, parameters.IdentificadorMuestra, parameters.UsuarioSistema, parameters.CodPersoAsisten, errorParameter, numRegistrosParameter);
                    //contexto.ExecuteStoreCommand()
                    PRO_CapturaManualValida_Result resul = new PRO_CapturaManualValida_Result();
                    if (errorParameter.Value != null && !(errorParameter.Value is DBNull))
                    {
                        resul.Error = (string)errorParameter.Value;
                    }

                    if (numRegistrosParameter.Value != null && !(numRegistrosParameter.Value is DBNull))
                    {
                        resul.NoRegProc = (int)numRegistrosParameter.Value;
                    }

                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_ConsultaEstudioIdenMuestraAction(object sender, DoWorkEventArgs e)
        {
            var idMuestra = e.Argument as string;
            if (!string.IsNullOrWhiteSpace(idMuestra))
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    List<PRO_ConsultaEstudioIdenMuestra_Result> resul = contexto.PRO_ConsultaEstudioIdenMuestra(idMuestra).ToList();
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_ConsultaResultadoEstudioAction(object sender, DoWorkEventArgs e)
        {
            var idResultado = e.Argument as string;
            if (!string.IsNullOrWhiteSpace(idResultado))
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    List<PRO_ConsultaResultadoEstudio_Result> resul = contexto.PRO_ConsultaResultadoEstudio(idResultado).ToList();
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_CapturaManualHistoricoAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualHistoricoParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    List<PRO_CapturaManualHistorico_Result> resul = contexto.PRO_CapturaManualHistorico(parameters.IdMuestra, parameters.CodigoFuente, parameters.Analito).ToList();
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_CapturaManualResultadoAnulaAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualAnulaParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    int resul = contexto.PRO_CapturaManualResultadoAnula(parameters.NoResultado, parameters.CodPesoAsisten);
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void PRO_CargaCapturaFormulaAction(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as CapturaManualFormulaParameters;
            if (parameters != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    var resul = contexto.PRO_CargaCapturaFormula(parameters.IdMuestra, parameters.CodigoFuente).ToList();
                    e.Result = resul;
                }
            }
            else
            {
                throw new ArgumentException("No se Enviaron Los parametros de la Consulta");
            }
        }

        private static void GetSugestionsAction(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaCuatroCampos_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<EstudioAnalitoDato> resultado =
                        contexto.EstudioAnalitoDato.Where(
                            analitoDato =>
                            analitoDato.CodigoFuente == parametro.CodigoFuente
                            && analitoDato.Analito == parametro.Analito).ToList();
                    if (Application.Current.Dispatcher != null)
                    {
                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            foreach (EstudioAnalitoDato dato in resultado)
                            {
                                parametro.Suggestions.Add(dato.Dato);
                                //// Debug.WriteLine("Añadido:" + parametro.Analito + "-" + dato.Dato);
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.BeginInvoke(
                                new Action(() =>
                                {
                                    foreach (EstudioAnalitoDato dato in resultado)
                                    {
                                        parametro.Suggestions.Add(dato.Dato);
                                        // Debug.WriteLine("Añadido:" + parametro.Analito + "-" + dato.Dato);
                                    }
                                }));
                        }
                    }
                    else
                    {
                        try
                        {
                            foreach (EstudioAnalitoDato dato in resultado)
                            {
                                parametro.Suggestions.Add(dato.Dato);
                                // Debug.WriteLine("Añadido:" + parametro.Analito + "-" + dato.Dato);
                            }
                        }
                        catch (Exception ee)
                        {
                            Debug.WriteLine("Error en GetSugestionsAction1: " + ee.Message);
                        }
                    }
                }
            }
            else
            {
                var parametro2 = e.Argument as PRO_CargaCapturaCuatroCamposDosBloques_Result;
                if (parametro2 != null)
                {
                    using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                    {
                        // Debug.WriteLine(parametro2.CodigoFuente + ":" + parametro2.Analito);
                        List<EstudioAnalitoDato> resultado = contexto.EstudioAnalitoDato.Where(analitoDato => analitoDato.CodigoFuente == parametro2.CodigoFuente && analitoDato.Analito == parametro2.Analito).ToList();
                        if (Application.Current.Dispatcher != null)
                        {
                            if (Application.Current.Dispatcher.CheckAccess())
                            {
                                foreach (EstudioAnalitoDato dato in resultado)
                                {
                                    parametro2.Suggestions.Add(dato.Dato);
                                    // Debug.WriteLine("Añadido:" + parametro2.Analito + "-" + dato.Dato);
                                }
                            }
                            else
                            {
                                Application.Current.Dispatcher.BeginInvoke(
                                    new Action(() =>
                                    {
                                        foreach (EstudioAnalitoDato dato in resultado)
                                        {
                                            parametro2.Suggestions.Add(dato.Dato);
                                            // Debug.WriteLine("Añadido:" + parametro2.Analito + "-" + dato.Dato);
                                        }
                                    }));
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (EstudioAnalitoDato dato in resultado)
                                {
                                    parametro2.Suggestions.Add(dato.Dato);
                                    // Debug.WriteLine("Añadido:" + parametro2.Analito + "-" + dato.Dato);
                                }
                            }
                            catch (Exception ee)
                            {
                                Debug.WriteLine("Error en GetSugestionsAction1: " + ee.Message);
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("No se Envió El Resultado a Guardar");
                }
            }
        }

        private static void GetDatoLargoSugestionsAction(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaCuatroCampos_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<SugerenciasAutoTexto> resultado =
                        contexto.EstudioAnalitoPlantilla.Where(
                            analitoDato =>
                            analitoDato.CodigoFuente == parametro.CodigoFuente
                            && analitoDato.Analito == parametro.Analito).Select(a => new SugerenciasAutoTexto { Etiqueta = a.Plantilla, Texto = a.PlantillaTexto }).ToList();
#if DEBUG
                    Trace.WriteLineIf(resultado.Count > 0, "SugerenciasDatoLargo: " + parametro.CodigoFuente + " TieneDatos");
#endif
                    e.Result = resultado;
                }
            }
            else
            {
                throw new ArgumentException("No se Envió El Resultado a Guardar");
            }
        }
        private static void GetDatoLargoMultiSugestionsAction(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaCuatroCampos_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<SugerenciasAutoTexto> resultado = contexto.PRO_CargaCapturaCampoLargoSelecMulti(parametro.IdentificadorMuestra, parametro.CodigoFuente, parametro.Analito).Select(a => new SugerenciasAutoTexto { Etiqueta = a.Rotulo, Texto = a.Rotulo }).ToList();


#if DEBUG
                    Trace.WriteLineIf(resultado.Count > 0, "SugerenciasDatoLargo: " + parametro.CodigoFuente + " TieneDatos");
#endif
                    e.Result = resultado;
                }
            }
            else
            {
                throw new ArgumentException("No se Envió El Resultado a Guardar");
            }
        }

        private static void GetDatoLargoSugestionsAction2(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaCampoLargo_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<SugerenciasAutoTexto> resultado =
                        contexto.EstudioAnalitoPlantilla.Where(
                            analitoDato =>
                            analitoDato.CodigoFuente == parametro.CodigoFuente
                            && analitoDato.Analito == parametro.Analito).Select(a => new SugerenciasAutoTexto { Etiqueta = a.Plantilla, Texto = a.PlantillaTexto }).ToList();
#if DEBUG
                    Trace.WriteLineIf(resultado.Count > 0, "SugerenciasDatoLargo: " + parametro.CodigoFuente + " TieneDatos");
#endif
                    e.Result = resultado;
                }
            }
            else
            {
                throw new ArgumentException("No se Envió El Resultado a Guardar");
            }

        }

        private static void GetDatoLargoSugestionsAction3(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaSeccion_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<SugerenciasAutoTexto> resultado =
                        contexto.EstudioAnalitoPlantilla.Where(
                            analitoDato =>
                            analitoDato.CodigoFuente == parametro.CodigoFuente
                            && analitoDato.Analito.ToUpper() == "OBSERVACION").Select(a => new SugerenciasAutoTexto { Etiqueta = a.Plantilla, Texto = a.PlantillaTexto }).ToList();
#if DEBUG
                    Trace.WriteLineIf(resultado.Count > 0, "SugerenciasDatoLargo: " + parametro.CodigoFuente + " TieneDatos");
#endif
                    e.Result = resultado;
                }
            }
            else
            {
                throw new ArgumentException("No se Envió El Resultado a Guardar");
            }
        }

        private static void GetSampleTrackingAction(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as Tuple<string, string, string>;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    List<TrackingItem> resultado = contexto.PRO_EstudioTrazabilidad(parametro.Item1, parametro.Item2, parametro.Item3).ToList().ConvertAll(ModelConverters.FromDbToTrackingItem);
#if DEBUG
                    Trace.WriteLineIf(resultado.Count > 0, "Tracking : " + parametro.Item1 + " Has Data");
#endif
                    e.Result = resultado;
                }
            }
            else
            {
                throw new ArgumentException("No se Envió El Resultado a Guardar");
            }
        }

        private static void GetAlertsAction(object sender, DoWorkEventArgs e)
        {
            var parametro = e.Argument as PRO_CargaCapturaCuatroCampos_Result;
            if (parametro != null)
            {
                using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    // Debug.WriteLine(parametro.CodigoFuente + ":" + parametro.Analito);
                    List<EstudioAnalitoPlantilla> resultado =
                        contexto.EstudioAnalitoPlantilla.Where(
                            analitoDato =>
                            analitoDato.CodigoFuente == parametro.CodigoFuente).ToList();
                    /////// && analitoDato.AnalitoVrCritico == parametro.Analito
                    if (Application.Current.Dispatcher != null)
                    {
                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            parametro.Alerts.Clear();
                            foreach (EstudioAnalitoPlantilla dato in resultado)
                            {
                                parametro.Alerts.Add(dato.PlantillaTexto);
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.BeginInvoke(
                                new Action(() =>
                                {
                                    parametro.Alerts.Clear();
                                    foreach (EstudioAnalitoPlantilla dato in resultado)
                                    {
                                        parametro.Alerts.Add(dato.PlantillaTexto);
                                    }
                                }));
                        }
                    }
                    else
                    {
                        try
                        {
                            parametro.Alerts.Clear();
                            foreach (EstudioAnalitoPlantilla dato in resultado)
                            {
                                parametro.Alerts.Add(dato.PlantillaTexto);
                            }
                        }
                        catch (Exception ee)
                        {
                            Debug.WriteLine("Error en GetSugestionsAction1: " + ee.Message);
                        }
                    }
                }
            }
            else
            {
                var parametro2 = e.Argument as PRO_CargaCapturaCuatroCamposDosBloques_Result;
                if (parametro2 != null)
                {
                    using (NeuronCapturaManualEntities contexto = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                    {
                        // Debug.WriteLine(parametro2.CodigoFuente + ":" + parametro2.Analito);
                        List<EstudioAnalitoPlantilla> resultado =
                            contexto.EstudioAnalitoPlantilla.Where(
                            analitoDato =>
                                analitoDato.CodigoFuente == parametro2.CodigoFuente
                                && analitoDato.Analito == parametro2.Analito).ToList();
                        if (Application.Current.Dispatcher != null)
                        {
                            if (Application.Current.Dispatcher.CheckAccess())
                            {
                                parametro2.Alerts.Clear();
                                foreach (EstudioAnalitoPlantilla dato in resultado)
                                {
                                    parametro2.Alerts.Add(dato.PlantillaTexto);
                                }
                            }
                            else
                            {
                                Application.Current.Dispatcher.BeginInvoke(
                                    new Action(() =>
                                    {
                                        parametro2.Alerts.Clear();
                                        foreach (EstudioAnalitoPlantilla dato in resultado)
                                        {
                                            parametro2.Alerts.Add(dato.PlantillaTexto);
                                        }
                                    }));
                            }
                        }
                        else
                        {
                            try
                            {
                                parametro2.Alerts.Clear();
                                foreach (EstudioAnalitoPlantilla dato in resultado)
                                {
                                    parametro2.Alerts.Add(dato.PlantillaTexto);
                                }
                            }
                            catch (Exception ee)
                            {
                                Debug.WriteLine("Error en GetSugestionsAction1: " + ee.Message);
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("No se Envió El Resultado a Guardar");
                }
            }
        }
    }
}
