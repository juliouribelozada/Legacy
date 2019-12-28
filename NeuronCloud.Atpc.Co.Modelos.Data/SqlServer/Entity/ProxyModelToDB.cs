// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyModelToDB.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Convierte entidades del Negocio en su equivalente en la base de Datos.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NeuronCloud.Atpc.Co.Modelos.Helpers;

    /// <summary>
    /// Convierte entidades del Negocio en su equivalente en la base de Datos.
    /// </summary>
    public static class ProxyModelToDB
    {
        public static Tercero Tercero(Modelos.Tercero modelTercero)
        {
            var salida = new Tercero
                             {
                                 PrimerNombre = modelTercero.PrimerNombre,
                                 PrimerApellido = modelTercero.PrimerApellido,
                                 SegundoNombre = modelTercero.SegundoNombre,
                                 SegundoApellido = modelTercero.SegundoApellido,
                                 FechaNacimiento = modelTercero.FechaNacimiento,
                                 CorreoCorporativo = modelTercero.CorreoCorporativo,
                                 CorreoPersonal = modelTercero.CorreoPersonal,
                                 BarrioDomicilio = modelTercero.Barrio,
                                 Genero = modelTercero.Genero,
                                 Fax = modelTercero.Fax,
                                 Movil = modelTercero.TelefonoMovil,
                                 TelefonoAlterno = modelTercero.TelefonoAlterno,
                                 TelefonoDomicilio = modelTercero.TelefonoResidencia,
                                 TipoIdentificacion = modelTercero.TipoDocumento.TipoIdentificacionId,
                                 DocumentoIdentidad = modelTercero.NumeroDocumento,
                                 NumeroUnicoDocumento = modelTercero.NumeroUnicoDocumento,
                                 CodMpioDomicilio = modelTercero.Municipio.Codigo,
                                 DireccionDomicilio = modelTercero.Direccion,
                                 Nombre = modelTercero.NombreCompleto,
                                 CodPaisDomicilio = modelTercero.Municipio.CodigoPais,
                                 Zona = modelTercero.Zona,
                                 FechaRegistro = DateTime.Now,
                                 UsuarioRegistro = ConfiguracionGlobal.UsuarioActual
                             };

            return salida;
        }
    }
}
