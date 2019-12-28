// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParametrosBusquedaOrdenDeServicio.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ParametrosBusquedaOrdenDeServicio type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Auxiliares
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Practices.Prism.ViewModel;

    public class ParametrosBusquedaOrdenDeServicio : NotificationObject
    {
        private string docUnicoOsc;

        private string numeroUnicoDocumento;

        private DateTime? fechaInicio;

        private DateTime? fechaFin;

        private bool todasLasFechas;

        private int numeroDeResultados;

        public ParametrosBusquedaOrdenDeServicio()
        {
            this.FechaFin = DateTime.Today;
            this.FechaInicio = DateTime.Today - new TimeSpan(8, 0, 0, 0);
            this.TodasLasFechas = true;
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "ParametrosBusqueda_NumeroOsc_Caption")]
        public string DocUnicoOsc
        {
            get
            {
                return this.docUnicoOsc;
            }

            set
            {
                if (value == this.docUnicoOsc)
                {
                    return;
                }

                this.docUnicoOsc = value;
                this.RaisePropertyChanged("DocUnicoOsc");
            }
        }
        
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "ParametrosBusqueda_NumeroUnicoDocumento_Caption")]
        public string NumeroUnicoDocumento
        {
            get
            {
                return this.numeroUnicoDocumento;
            }

            set
            {
                if (value == this.numeroUnicoDocumento)
                {
                    return;
                }

                this.numeroUnicoDocumento = value;
                this.RaisePropertyChanged("NumeroUnicoDocumento");
            }
        }

        public DateTime? FechaInicio
        {
            get
            {
                return this.fechaInicio;
            }

            set
            {
                if (value.Equals(this.fechaInicio))
                {
                    return;
                }

                this.fechaInicio = value;
                this.RaisePropertyChanged("FechaInicio");
            }
        }

        public DateTime? FechaFin
        {
            get
            {
                return this.fechaFin;
            }

            set
            {
                if (value.Equals(this.fechaFin))
                {
                    return;
                }

                this.fechaFin = value;
                this.RaisePropertyChanged("FechaFin");
            }
        }

        public bool TodasLasFechas
        {
            get
            {
                return this.todasLasFechas;
            }

            set
            {
                if (value.Equals(this.todasLasFechas))
                {
                    return;
                }

                this.todasLasFechas = value;
                this.RaisePropertyChanged("TodasLasFechas");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "ParametrosBusqueda_NumeroDeResultados_Caption")]
        public int NumeroDeResultados
        {
            get
            {
                return this.numeroDeResultados;
            }

            set
            {
                if (value == this.numeroDeResultados)
                {
                    return;
                }

                this.numeroDeResultados = value;
                this.RaisePropertyChanged("NumeroDeResultados");
            }
        }
    }
}
