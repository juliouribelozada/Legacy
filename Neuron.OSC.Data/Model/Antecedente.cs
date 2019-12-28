// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Antecedente.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using System;

    using Microsoft.Practices.Prism.ViewModel;

    public class Antecedente : NotificationObject
    {
        private bool isEmpty;

        private int alarma;

        private int? lapsoPertinenteDia;

        private DateTime? fechaHoraValidacion;

        private string codigoFuente;

        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }

            set
            {
                if (value.Equals(this.isEmpty))
                {
                    return;
                }

                this.isEmpty = value;
                this.RaisePropertyChanged("IsEmpty");
            }
        }

        public int Alarma
        {
            get
            {
                return this.alarma;
            }

            set
            {
                if (value == this.alarma)
                {
                    return;
                }

                this.alarma = value;
                this.RaisePropertyChanged("Alarma");
            }
        }

        public string CodigoFuente
        {
            get
            {
                return this.codigoFuente;
            }

            set
            {
                if (value == this.codigoFuente)
                {
                    return;
                }

                this.codigoFuente = value;
                this.RaisePropertyChanged("CodigoFuente");
            }
        }

        public DateTime? FechaHoraValidacion
        {
            get
            {
                return this.fechaHoraValidacion;
            }

            set
            {
                if (value.Equals(this.fechaHoraValidacion))
                {
                    return;
                }

                this.fechaHoraValidacion = value;
                this.RaisePropertyChanged("FechaHoraValidacion");
            }
        }

        public int? LapsoPertinenteDia
        {
            get
            {
                return this.lapsoPertinenteDia;
            }

            set
            {
                if (value == this.lapsoPertinenteDia)
                {
                    return;
                }

                this.lapsoPertinenteDia = value;
                this.RaisePropertyChanged("LapsoPertinenteDia");
            }
        }
    }
}
