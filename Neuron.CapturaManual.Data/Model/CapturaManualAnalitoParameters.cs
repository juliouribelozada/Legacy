// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CapturaManualAnalitoParameters.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CapturaManualAnalitoParameters
    {
        private string identificadorMuestra;
        private string codigoFuente;
        private string analito;
        private string dato;
        private string datoLargo;
        private string observacion;
        private string formato;
        private string usuarioSistema;
        private string codPersoAsisten;

        public string CodPersoAsisten
        {
            get
            {
                return this.codPersoAsisten;
            }

            set
            {
                this.codPersoAsisten = value;
            }
        }

        public string IdentificadorMuestra
        {
            get
            {
                return this.identificadorMuestra;
            }
            set
            {
                this.identificadorMuestra = value;
            }
        }

        public string CodigoFuente
        {
            get
            {
                return codigoFuente;
            }
            set
            {
                codigoFuente = value;
            }
        }

        public string Analito
        {
            get
            {
                return analito;
            }
            set
            {
                analito = value;
            }
        }

        public string Dato
        {
            get
            {
                return dato;
            }
            set
            {
                dato = value;
            }
        }

        public string DatoLargo
        {
            get
            {
                return datoLargo;
            }
            set
            {
                datoLargo = value;
            }
        }

        public string Observacion
        {
            get
            {
                return observacion;
            }
            set
            {
                observacion = value;
            }
        }

        public string Formato
        {
            get
            {
                return formato;
            }
            set
            {
                formato = value;
            }
        }

        public string UsuarioSistema
        {
            get
            {
                return usuarioSistema;
            }
            set
            {
                usuarioSistema = value;
            }
        }
    }
}
