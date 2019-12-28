// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValorGraficaFecha.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;

    public class ValorGraficaFecha : NotifiableObject
    {
        private DateTime fecha;
        private double valor;

        public DateTime Fecha
        {
            get
            {
                return this.fecha;
            }

            set
            {
                if (value != this.fecha)
                {
                    this.fecha = value;
                    NotifyPropertyChanged("Fecha");
                }
            }
        }

        public double Valor
        {
            get
            {
                return this.valor;
            }

            set
            {
                if (value != this.valor)
                {
                    this.valor = value;
                    NotifyPropertyChanged("Valor");
                }
            }
        }
    }
}
