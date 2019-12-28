// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyContext.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DummyContext : NotifiableObject
    {
        private string texto1;
        private string texto2;
        private string mensaje;

        public string Texto1
        {
            get
            {
                return this.texto1;
            }

            set
            {
                if (value != this.texto1)
                {
                    this.texto1 = value;
                    NotifyPropertyChanged("Texto1");
                }
            }
        }
        public string Texto2
        {
            get
            {
                return this.texto2;
            }

            set
            {
                if (value != this.texto2)
                {
                    this.texto2 = value;
                    NotifyPropertyChanged("Texto2");
                }
            }
        }

        public string Mensaje
        {
            get
            {
                return this.mensaje;
            }

            set
            {
                if (value != this.mensaje)
                {
                    this.mensaje = value;
                    NotifyPropertyChanged("Mensaje");
                }
            }
        }
    }
}
