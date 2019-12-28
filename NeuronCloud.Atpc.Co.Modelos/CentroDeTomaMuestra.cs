// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CentroDeTomaMuestra.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the CentroDeTomaMuestra type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    public class CentroDeTomaMuestra : ObjetoValidable
    {
        private string nombre;

        private string prefijo;

        private string codigoPrestador;

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

        public string Prefijo
        {
            get
            {
                return this.prefijo;
            }

            set
            {
                if (value == this.prefijo)
                {
                    return;
                }

                this.prefijo = value;
                this.RaisePropertyChanged("Prefijo");
            }
        }

        public string CodigoPrestador
        {
            get
            {
                return this.codigoPrestador;
            }

            set
            {
                if (value == this.codigoPrestador)
                {
                    return;
                }

                this.codigoPrestador = value;
                this.RaisePropertyChanged("CodigoPrestador");
            }
        }
    }
}
