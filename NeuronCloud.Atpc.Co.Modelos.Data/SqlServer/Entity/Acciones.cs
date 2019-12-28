// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Acciones.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Acciones hacia la base de Datos
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Authentication;

    using global::Atpc.Common.Async;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;

    /// <summary>
    /// Acciones hacia la base de Datos.
    /// </summary>
    public class Acciones
    {
        public static void AnularOscAsyn(OrdenDeServicioBase ordenDeServicio, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(ordenDeServicio, AnularOscAction, resultadoOperacion);
        }


        public static void GetPersonalAsistencialAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetPersonalAsistencialAction, resultadoOperacion);
        }

        public static void CentroTomaMuestraGetAsync(string codigoPrestador, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(codigoPrestador, CentroTomaMuestraGetAction, resultadoOperacion);
        }

        public static void BuscarOrdenesDeServicioAsync(ParametrosBusquedaOrdenDeServicio parametrosBusqueda, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(parametrosBusqueda, BuscarOrdenesDeServicioAction, resultadoOperacion);
        }

        public static void GuardarTerceroAsync(Modelos.Tercero terceroAGuardar, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(terceroAGuardar, GuardarTerceroAction, resultadoOperacion);
        }

        public static void GetTerceroAsync(Modelos.Tercero terceroATraer, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(terceroATraer, GetTerceroAction, resultadoOperacion);
        }

        public static void ConsultaEstadoAgendaMesAsync(DateTime fechaAConsultar, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(fechaAConsultar, ConsultaEstadoAgendaMesAction, resultadoOperacion);
        }

        public static void ConsultaEstadoAgendaDiaAsync(DateTime fechaAConsultar, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(fechaAConsultar, ConsultaEstadoAgendaDiaAction, resultadoOperacion);
        }

        private static void AnularOscAction(object sender, DoWorkEventArgs e)
        {
            var ordenDeServicioBase = e.Argument as OrdenDeServicioBase;

            if (ordenDeServicioBase != null)
            {
                if (OrdenDeServicio.PuedeEjecutarAnular())
                {
                    using (NeuronCloudBaseEntities contexto = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
                    {
                        contexto.PRO_OSCAnula(ordenDeServicioBase.DocUnicoOsc, ConfiguracionGlobal.IPrincipalActual.Identity.Name);
                    }
                }
                else
                {
                    throw new InvalidCredentialException("El usuario Antual no Tiene Permisos de Anulación");
                }
            }
        }

        private static void BuscarOrdenesDeServicioAction(object sender, DoWorkEventArgs e)
        {
            var paramentrosBusqueda = e.Argument as ParametrosBusquedaOrdenDeServicio;

            if (paramentrosBusqueda != null)
            {
                var salida = new List<OrdenDeServicio>();
                var numeroDeResultados = paramentrosBusqueda.NumeroDeResultados;
                if (paramentrosBusqueda.NumeroDeResultados < 0)
                {
                    numeroDeResultados = 100;
                }

                using (NeuronCloudBaseEntities contexto = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
                {
                    if (paramentrosBusqueda.TodasLasFechas || (!paramentrosBusqueda.FechaInicio.HasValue && !paramentrosBusqueda.FechaFin.HasValue))
                    {
                        if (string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosSinFiltros");
                            var resultadosSinFiltros = (from ordenDeServicio in contexto.OSC
                                                        join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                        select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosSinFiltros).ToTraceString());

                            foreach (var resultado in resultadosSinFiltros)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && !string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosFiltroNumeroDoc");
                            var resultadosFiltroNumeroDoc = (from ordenDeServicio in contexto.OSC
                                                             join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                             where ordenDeServicio.NumeroUnicoDocumento.Contains(paramentrosBusqueda.NumeroUnicoDocumento)
                                                             select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosFiltroNumeroDoc).ToTraceString());

                            foreach (var resultado in resultadosFiltroNumeroDoc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosFiltroOsc");
                            var resultadosFiltroOsc = (from ordenDeServicio in contexto.OSC
                                                       join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                       where ordenDeServicio.DocUnicoOSC.Contains(paramentrosBusqueda.DocUnicoOsc)
                                                       select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosFiltroOsc).ToTraceString());

                            foreach (var resultado in resultadosFiltroOsc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("resultadosFiltroNumeroDocYOsc");
                            var resultadosFiltroNumeroDocYOsc = (from ordenDeServicio in contexto.OSC
                                                                 join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                                 where ordenDeServicio.NumeroUnicoDocumento.Contains(paramentrosBusqueda.NumeroUnicoDocumento) && ordenDeServicio.DocUnicoOSC.Contains(paramentrosBusqueda.DocUnicoOsc)
                                                                 select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosFiltroNumeroDocYOsc).ToTraceString());

                            foreach (var resultado in resultadosFiltroNumeroDocYOsc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                    }
                    else if (paramentrosBusqueda.FechaInicio.HasValue && paramentrosBusqueda.FechaFin.HasValue)
                    {
                        if (string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosSinFiltros Con Fecha");
                            var resultadosSinFiltros = (from ordenDeServicio in contexto.OSC
                                                        join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                        where ordenDeServicio.Fecha >= paramentrosBusqueda.FechaInicio.Value
                                                        where ordenDeServicio.Fecha <= paramentrosBusqueda.FechaFin.Value
                                                        select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosSinFiltros).ToTraceString());

                            foreach (var resultado in resultadosSinFiltros)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && !string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosFiltroNumeroDoc Con Fecha");
                            var resultadosFiltroNumeroDoc = (from ordenDeServicio in contexto.OSC
                                                             join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                             where ordenDeServicio.NumeroUnicoDocumento.Contains(paramentrosBusqueda.NumeroUnicoDocumento)
                                                             where ordenDeServicio.Fecha >= paramentrosBusqueda.FechaInicio.Value
                                                             where ordenDeServicio.Fecha <= paramentrosBusqueda.FechaFin.Value
                                                             select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosFiltroNumeroDoc).ToTraceString());

                            foreach (var resultado in resultadosFiltroNumeroDoc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(paramentrosBusqueda.DocUnicoOsc) && string.IsNullOrWhiteSpace(paramentrosBusqueda.NumeroUnicoDocumento))
                        {
                            Debug.WriteLine("resultadosFiltroOsc Con Fecha");
                            var resultadosFiltroOsc = (from ordenDeServicio in contexto.OSC
                                                       join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                       where ordenDeServicio.DocUnicoOSC.Contains(paramentrosBusqueda.DocUnicoOsc)
                                                       where ordenDeServicio.Fecha >= paramentrosBusqueda.FechaInicio.Value
                                                       where ordenDeServicio.Fecha <= paramentrosBusqueda.FechaFin.Value
                                                       select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLine(((ObjectQuery)resultadosFiltroOsc).ToTraceString());

                            foreach (var resultado in resultadosFiltroOsc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("resultadosFiltroNumeroDocYOsc Con Fecha");
                            var resultadosFiltroNumeroDocYOsc = (from ordenDeServicio in contexto.OSC
                                                                 join det in contexto.OSCDet on ordenDeServicio.DocUnicoOSC equals det.DocUnicoOSC into detalles
                                                                 where ordenDeServicio.NumeroUnicoDocumento.Contains(paramentrosBusqueda.NumeroUnicoDocumento) && ordenDeServicio.DocUnicoOSC.Contains(paramentrosBusqueda.DocUnicoOsc)
                                                                 where ordenDeServicio.Fecha >= paramentrosBusqueda.FechaInicio.Value
                                                                 where ordenDeServicio.Fecha <= paramentrosBusqueda.FechaFin.Value
                                                                 select new { Osc = ordenDeServicio, Detalle = detalles }).Take(numeroDeResultados);

                            Debug.WriteLineIf(resultadosFiltroNumeroDocYOsc is ObjectQuery, ((ObjectQuery)resultadosFiltroNumeroDocYOsc).ToTraceString());

                            foreach (var resultado in resultadosFiltroNumeroDocYOsc)
                            {
                                var osc = ProxyDBToModel.OrdenDeServicio(resultado.Osc);
                                foreach (var oscDet in resultado.Detalle)
                                {
                                    osc.Detalle.Add(ProxyDBToModel.DetalleOrdenDeServicio(oscDet));
                                }

                                salida.Add(osc);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Se inicio una Búqueda por Rango de Fechas y una de las Fechas es Nula.");
                    }
                }

                e.Result = salida;
            }
        }

        private static void GetPersonalAsistencialAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                List<PersonalAsistencial> resultado =
                    context.V_PersonalAsistencial.Select(
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

        private static void CentroTomaMuestraGetAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                var parametro = e.Argument as string;

                List<CentroDeTomaMuestra> resultado = string.IsNullOrWhiteSpace(parametro) ?
                    context.CentroTomaMuestra.Select(ProxyDBToModel.CentroDeTomaMuestra).ToList() :
                    context.CentroTomaMuestra.Where(ctm => ctm.CodPrestador == parametro).Select(ProxyDBToModel.CentroDeTomaMuestra).ToList();

                e.Result = resultado;
            }
        }

        private static void GuardarTerceroAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                var tercero = e.Argument as Modelos.Tercero;

                if (tercero != null)
                {
                    ////    bool actualizar = false;
                    var terceroAGuardar = ProxyModelToDB.Tercero(tercero);
                    ////    var terceroExistente = context.Tercero.FirstOrDefault(terceros => terceros.NumeroUnicoDocumento == terceroAGuardar.NumeroUnicoDocumento);
                    ////    if (terceroExistente != null)
                    ////    {
                    ////        actualizar = true;
                    ////        terceroExistente.DocumentoIdentidad = terceroAGuardar.DocumentoIdentidad;
                    ////        terceroExistente.BarrioDomicilio = terceroAGuardar.BarrioDomicilio;
                    ////        terceroExistente.CodMpioDomicilio = terceroAGuardar.CodMpioDomicilio;
                    ////        terceroExistente.CodPaisDomicilio = terceroAGuardar.CodPaisDomicilio;
                    ////        terceroExistente.CorreoCorporativo = terceroAGuardar.CorreoCorporativo;
                    ////        terceroExistente.CorreoPersonal = terceroAGuardar.CorreoPersonal;
                    ////        terceroExistente.DireccionDomicilio = terceroAGuardar.DireccionDomicilio;
                    ////        terceroExistente.DocumentoIdentidad = terceroAGuardar.DocumentoIdentidad;
                    ////        terceroExistente.Fax = terceroAGuardar.Fax;
                    ////        terceroExistente.FechaNacimiento = terceroAGuardar.FechaNacimiento;
                    ////        terceroExistente.FechaRegistro = terceroAGuardar.FechaRegistro;
                    ////        terceroExistente.Genero = terceroAGuardar.Genero;
                    ////        terceroExistente.Movil = terceroAGuardar.Movil;
                    ////        terceroExistente.Nombre = terceroAGuardar.Nombre;
                    ////        terceroExistente.NumeroUnicoDocumento = terceroAGuardar.NumeroUnicoDocumento;
                    ////        terceroExistente.PrimerApellido = terceroAGuardar.PrimerApellido;
                    ////        terceroExistente.PrimerNombre = terceroAGuardar.PrimerNombre;
                    ////        terceroExistente.SegundoApellido = terceroAGuardar.SegundoApellido;
                    ////        terceroExistente.SegundoNombre = terceroAGuardar.SegundoNombre;
                    ////        terceroExistente.TelefonoAlterno = terceroAGuardar.TelefonoAlterno;
                    ////        terceroExistente.TelefonoDomicilio = terceroAGuardar.TelefonoDomicilio;
                    ////        terceroExistente.TipoIdentificacion = terceroAGuardar.TipoIdentificacion;
                    ////        terceroExistente.UsuarioRegistro = terceroAGuardar.UsuarioRegistro;
                    ////        terceroExistente.Zona = terceroAGuardar.Zona;
                    ////    }

                    ////    if (!actualizar)
                    ////    {
                    ////        context.Tercero.AddObject(terceroAGuardar);
                    ////    }

                    e.Result = context.PRO_TerceroInsertaActualiza(
                        tipoIdentificacion: terceroAGuardar.TipoIdentificacion,
                        documentoIdentidad: terceroAGuardar.DocumentoIdentidad,
                        primerNombre: terceroAGuardar.PrimerNombre,
                        segundoNombre: terceroAGuardar.SegundoNombre,
                        primerApellido: terceroAGuardar.PrimerApellido,
                        segundoApellido: terceroAGuardar.SegundoApellido,
                        codMpioDomicilio: terceroAGuardar.CodMpioDomicilio,
                        barrioDomicilio: terceroAGuardar.BarrioDomicilio,
                        direccionDomicilio: terceroAGuardar.DireccionDomicilio,
                        telefonoDomicilio: terceroAGuardar.TelefonoDomicilio,
                        telefonoAlterno: terceroAGuardar.TelefonoAlterno,
                        genero: terceroAGuardar.Genero,
                        fax: terceroAGuardar.Fax,
                        movil: terceroAGuardar.Movil,
                        nombre: terceroAGuardar.Nombre,
                        correoPersonal: terceroAGuardar.CorreoPersonal,
                        correoCorporativo: terceroAGuardar.CorreoCorporativo,
                        codPaisDomicilio: terceroAGuardar.CodPaisDomicilio,
                        zona: terceroAGuardar.Zona,
                        fechaNacimiento: terceroAGuardar.FechaNacimiento);
                }
                else
                {
                    throw new InvalidOperationException("Es Objeto no es de Tipo Modelos.Tercero o esta Vacio");
                }
            }
        }

        private static void GetTerceroAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                var tercero = e.Argument as Modelos.Tercero;

                if (tercero != null)
                {
                    bool actualizar = false;

                    var terceroExistente = context.Tercero.FirstOrDefault(terceros => terceros.NumeroUnicoDocumento == tercero.NumeroUnicoDocumento);
                    if (terceroExistente != null)
                    {
                        actualizar = true;
                        e.Result = ProxyDBToModel.Tercero(terceroExistente);
                    }

                    if (!actualizar)
                    {
                        e.Result = tercero;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Es Objeto no es de Tipo Modelos.Tercero o esta Vacio");
                }
            }
        }

        private static void ConsultaEstadoAgendaMesAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                if (e.Argument is DateTime)
                {
                    var fecha = (DateTime)e.Argument;

                    var dias = context.PRO_ConsultaCitaServicioMes("TomaMuestras", (short?)fecha.Month, (short?)fecha.Year);
                }
                else
                {
                    throw new InvalidOperationException("Es Objeto no es de Tipo Modelos.Tercero o esta Vacio");
                }
            }
        }

        private static void ConsultaEstadoAgendaDiaAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCloudBaseEntities context = new NeuronCloudBaseEntities(NeuronCloudBaseStorage.EntityConnectionString))
            {
                if (e.Argument is DateTime)
                {
                    var fecha = (DateTime)e.Argument;

                    List<Cita> dias = context.PRO_ConsultaCitaServicio("TomaMuestras", fecha).Select(ProxyDBToModel.Cita).ToList();

                    e.Result = dias;
                }
                else
                {
                    throw new InvalidOperationException("Es Objeto no es de Tipo Modelos.Tercero o esta Vacio");
                }
            }
        }
    }
}
