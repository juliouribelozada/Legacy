// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceAgreement.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ServiceAgreement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using Atpc.Common;

    /// <summary>
    /// This Object contains Info About the Service Agreement.
    /// </summary>
    public class ServiceAgreement : NotifiableObject
    {
        private string code;

        private string name;

        private string estadoAfiliacion;

        private string nivel;

        private string copago;

        /// <summary>
        /// Gets or sets the Agreement Code.
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                if (value != this.code)
                {
                    this.code = value;
                    this.NotifyPropertyChanged("Code");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Agreement Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public string EstadoAfiliacion
        {
            get
            {
                return this.estadoAfiliacion;
            }

            set
            {
                if (value != this.estadoAfiliacion)
                {
                    this.estadoAfiliacion = value;
                    this.NotifyPropertyChanged("EstadoAfiliacion");
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
                if (value != this.nivel)
                {
                    this.nivel = value;
                    this.NotifyPropertyChanged("Nivel");
                }
            }
        }
        public string Copago
        {
            get
            {
                return this.copago;
            }

            set
            {
                if (value != this.copago)
                {
                    this.copago = value;
                    this.NotifyPropertyChanged("Copago");
                }
            }
        }
    }
}