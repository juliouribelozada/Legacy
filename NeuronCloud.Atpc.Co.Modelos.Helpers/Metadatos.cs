// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Metadatos.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the Metadatos type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Helpers
{
    using Microsoft.Practices.Prism.ViewModel;

    public class Metadatos : NotificationObject
    {
        private string nombre;

        private string descripcion;

        private bool esRequerido;

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

        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }

            set
            {
                if (value == this.descripcion)
                {
                    return;
                }
            
                this.descripcion = value;
                this.RaisePropertyChanged("Descripcion");
            }
        }

        public bool EsRequerido
        {
            get
            {
                return this.esRequerido;
            }

            set
            {
                if (value.Equals(this.esRequerido))
                {
                    return;
                }
            
                this.esRequerido = value;
                this.RaisePropertyChanged("EsRequerido");
            }
        }
    }
}