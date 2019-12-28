// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyDBToModel.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System;
    using System.Globalization;

    public class ProxyDBToModel
    {
        public static DetalleOrdenDeServicio DetalleOrdenDeServicio(OSCDet oscDet)
        {
            var output = new DetalleOrdenDeServicio
                {
                    DocUnicoOsc = oscDet.DocUnicoOSC,
                    Codigo = oscDet.CodigoFuente,
                    Nombre = oscDet.NomManual,
                    Cantidad = oscDet.Cant,
                    ValorUnitario = oscDet.VrItem,
                    Total = oscDet.Cant * oscDet.VrItem
                };
            return output;
        }

        public static OrdenDeServicio OrdenDeServicio(OSC osc)
        {
            var output = new OrdenDeServicio
                {
                    DocUnicoOsc = osc.DocUnicoOSC,
                    NumeroUnicoDocumento = osc.NumeroUnicoDocumento,
                    FechaAnulacion = osc.FechaAnulacion,
                    Fecha = osc.Fecha,
                    FechaRegistro = osc.FechaRegistro ,
                    PrimerApellido = osc.PrimerApellido,
                    PrimerNombre = osc.PrimerNombre,
                    SegundoApellido = osc.SegundoApellido,
                    SegundoNombre = osc.SegundoNombre,
                    Prestador = osc.NomPrestador,
                    CodigoConvenio = osc.CodConvenio,
                    UsuarioCreacion = osc.UsuarioRegistro,
                    UsuarioAnulo = osc.UsuarioAnulacion,
                    Convenio = osc.NomConvenio,
                    CodigoRemitente = osc.CodRemitente,
                    Remitente = osc.NomRemitente,
                    CodigoDiagnostico = osc.CodDiag,
                    CodigoPrestador = osc.CodPrestador,
                    Diagnostico = osc.NomDiag,
                    Observaciones = osc.Observaciones
                };
            return output;
        }

        public static CentroDeTomaMuestra CentroDeTomaMuestra(CentroTomaMuestra centroTomaMuestra)
        {
            var output = new CentroDeTomaMuestra
                {
                    Nombre = centroTomaMuestra.NomCentroToma,
                    Prefijo = centroTomaMuestra.PrefijoCentroToma,
                    CodigoPrestador = centroTomaMuestra.CodPrestador
                };
            return output;
        }

        public static Cita Cita(PRO_ConsultaCitaServicio_Result proConsultaCitaServicioResult)
        {
            var output = new Cita
                {
                    Id = proConsultaCitaServicioResult.IdProgramaAgenda.ToString(CultureInfo.InvariantCulture),
                    Descripcion = proConsultaCitaServicioResult.Cita,
                    Inicio = proConsultaCitaServicioResult.FechaHoraInicio,
                    Fin = proConsultaCitaServicioResult.FechaHoraFin.HasValue ? proConsultaCitaServicioResult.FechaHoraFin.Value : proConsultaCitaServicioResult.FechaHoraInicio + new TimeSpan(0, 12, 0)
                };

            return output;
        }

        public static Modelos.Tercero Tercero(Tercero terceroExistente)
        {
            Modelos.Tercero ouput = new Modelos.Tercero
                                        {
                                            Municipio = new Modelos.Municipio
                                                    {
                                                        Codigo = terceroExistente.CodMpioDomicilio,
                                                        CodigoPais = terceroExistente.CodPaisDomicilio
                                                    },
                                            Zona = terceroExistente.Zona,
                                            Direccion = terceroExistente.DireccionDomicilio,
                                            PrimerNombre = terceroExistente.PrimerNombre,
                                            PrimerApellido = terceroExistente.PrimerApellido,
                                            SegundoNombre = terceroExistente.SegundoNombre,
                                            SegundoApellido = terceroExistente.SegundoApellido,
                                            FechaNacimiento = terceroExistente.FechaNacimiento,
                                            CorreoCorporativo = terceroExistente.CorreoCorporativo,
                                            CorreoPersonal = terceroExistente.CorreoPersonal,
                                            Barrio = terceroExistente.BarrioDomicilio,
                                            Genero = terceroExistente.Genero,
                                            Fax = terceroExistente.Fax,
                                            TelefonoMovil = terceroExistente.Movil,
                                            TelefonoAlterno = terceroExistente.TelefonoAlterno,
                                            TelefonoResidencia = terceroExistente.TelefonoDomicilio,
                                            NumeroDocumento = terceroExistente.DocumentoIdentidad,
                                            TipoDocumento = new TipoIdentificacion { TipoIdentificacionId = terceroExistente.TipoIdentificacion }
                                        };
            return ouput;
        }
    }
}
