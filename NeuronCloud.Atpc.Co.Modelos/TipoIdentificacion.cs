// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TipoIdentificacion.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// Tipo de Identificación.
    /// </summary>
    public class TipoIdentificacion : NotificationObject
    {
        private bool necesitaRazonSocial;

        private string nombre;

        private string tipoIdentificacionId;

        public string Nombre
        {
            get
            {
                return this.nombre;
            }

            set
            {
                if (value == this.nombre)
                {
                    return;
                }

                this.nombre = value;
                this.RaisePropertyChanged("Nombre");
            }
        }

        public string TipoIdentificacionId
        {
            get
            {
                return this.tipoIdentificacionId;
            }

            set
            {
                if (value == this.tipoIdentificacionId)
                {
                    return;
                }

                this.tipoIdentificacionId = value;
                this.RaisePropertyChanged("TipoIdentificacionId");
            }
        }

        public bool NecesitaRazonSocial
        {
            get
            {
                return this.necesitaRazonSocial;
            }

            set
            {
                if (value.Equals(this.necesitaRazonSocial))
                {
                    return;
                }

                this.necesitaRazonSocial = value;
                this.RaisePropertyChanged("NecesitaRazonSocial");
            }
        }
    }
}
