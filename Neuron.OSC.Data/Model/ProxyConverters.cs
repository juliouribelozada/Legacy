// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyConverters.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ProxyConverters type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using System;

    using Neuron.HIS.Models.Common;

    public static class ProxyConverters
    {
        internal static PatientInfo ToPatientInfo(this CRUD_TerceroSeleccionaExacto_Result result)
        {
            return result != null ? FromCRUD_TerceroSeleccionaExacto_ResultToPatientInfo(result) : null;
        }

        internal static NoCita  ToNoCitaInfo(this PRO_SearchBooking_Result  result)
        {
            return result != null ? FromPRO_SearchBooking_ResultToNoCitaInfo(result) : null;
        }

        internal static ServiceAgreement ToServiceAgreement(this PRO_ConvenioSeleccPaciOSC_Result result)
        {
            return result != null ? FromPRO_ConvenioSeleccPaciOSC_ResultToServiceAgreement(result) : null;
        }

        internal static PatientInfo FromCRUD_TerceroSeleccionaExacto_ResultToPatientInfo(CRUD_TerceroSeleccionaExacto_Result result)
        {
            var output = new PatientInfo
                {
                    FullName = result.Nombre,
                    FirstName = result.PrimerNombre,
                    MiddleName = result.SegundoNombre,
                    LastName = result.PrimerApellido,
                    AdditionalLastName = result.SegundoApellido,
                    IdDocument = result.DocumentoIdentidad,
                    IdDocumentType = result.TipoIdentificacion,
                    UniqueDocumentId = result.NumeroUnicoDocumento,
                    OriginalObject = result
                };

            if (result.Genero != null)
            {
                switch (result.Genero.ToUpper())
                {
                    case "MASCULINO":
                        output.Gender = Gender.Male;
                        break;

                    case "FEMENINO":
                        output.Gender = Gender.Female;
                        break;
                }
            }

            return output;
        }
        internal static NoCita  FromPRO_SearchBooking_ResultToNoCitaInfo(PRO_SearchBooking_Result result)
        {
            var output = new NoCita 
            {

                FullName = result.Nombre,
                FirstName = result.PrimerNombre,
                MiddleName = result.SegundoNombre,
                LastName = result.PrimerApellido,
                AdditionalLastName = result.SegundoApellido,
                IdDocument = result.DocumentoIdentidad,
                IdDocumentType = result.TipoIdentificacion,
                UniqueDocumentId = result.NumeroUnicoDocumento,
                BookingNumber = result.NoCita,
                BookingDate = result.FechaHoraDesde,
                OriginalObject = result
            };


            return output;
        }

        internal static ServiceAgreement FromPRO_ConvenioSeleccPaciOSC_ResultToServiceAgreement(PRO_ConvenioSeleccPaciOSC_Result result)
        {
            var output = new ServiceAgreement
                {
                    Name = result.NomConvenio,
                    Code = result.CodConvenio,
                    EstadoAfiliacion = result.EstadoAfiliacion,
                    Nivel = result.Nivel,
                    Copago = result.Copago
                };
            return output;
        }

        internal static ServiceProvider PRO_ConvenioPrestSeleccToServiceProvider(PRO_ConvenioPrestSelecc_Result result)
        {
            var output = new ServiceProvider
            {
                Name = result.NomPrestador,
                Code = result.CodPrestador
            };
            return output;
        }

        internal static ServiceRequester PRO_ConvenioRemitenteSelecc_ResultToServiceRequester(PRO_ConvenioRemitenteSelecc_Result result)
        {
            var output = new ServiceRequester
            {
                Name = result.NomRemitente,
                Code = result.CodRemitente
            };
            return output;
        }

        internal static ServiceUnit PRO_ConvenioRemiteServSelecc_ResultToServiceUnit(PRO_ConvenioRemiteServSelecc_Result result)
        {
            var output = new ServiceUnit
            {
                Name = result.NomServicio
            };
            return output;
        }

        internal static Service PRO_ConvenioPrestPortafoSelecc_ResultToService(PRO_ConvenioPrestPortafoSelecc_Result result)
        {
            var output = new Service
            {
                Name = result.NombreCodigoFuente,
                Code = result.CodigoFuente,
                Price = result.Valor.HasValue ? Convert.ToDouble(result.Valor.Value) : 0,
                Plan = result.GrupoPlanBene,
                TipoPago = result.NomTipoPagoComp,
                EsCombo = result.Combo == 1
            };
            return output;
        }

        internal static Diagnose PRO_DiagnosticoSeleccAutocomplete_ResultToDiagnose(PRO_DiagnosticoSeleccAutocomplete_Result result)
        {
            var output = new Diagnose
            {
                Name = result.NomDiag,
                Code = result.CodDiag,
                FullName = string.Concat(result.CodDiag, " - ", result.NomDiag)
            };
            return output;
        }

        internal static NeuronCloud.Atpc.Co.Modelos.TipoIdentificacion DbTipoIdentificacionToModel(TipoIdentificacion result)
        {
            var output = new NeuronCloud.Atpc.Co.Modelos.TipoIdentificacion()
                {
                    Nombre = result.NomTipoIdentificacion,
                    TipoIdentificacionId = result.TipoIdentificacion1,
                    NecesitaRazonSocial = result.RazonSocialCHK.ToUpper() != "NO"
                };
            return output;
        }

        internal static Antecedente FromPRO_ConsultaResultadoPacienteToAntecedente(PRO_ConsultaResultadoPaciente_Result queryResult)
        {
            var output = new Antecedente();
            if (queryResult != null)
            {
                output.Alarma = queryResult.Alarma;
                output.CodigoFuente = queryResult.CodigoFuente;
                output.FechaHoraValidacion = queryResult.FechaHoraValidacion;
                output.LapsoPertinenteDia = queryResult.LapsoPertinenteDia;
            }
            else
            {
                output.IsEmpty = true;
            }

            return output;
        }
    }
}