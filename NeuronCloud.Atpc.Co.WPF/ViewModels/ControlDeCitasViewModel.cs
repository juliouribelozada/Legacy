// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlDeCitasViewModel.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Microsoft.Practices.Prism.Commands;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ControlDeCitasViewModel : NeuronWindowViewModelBase
    {
        private readonly ObservableCollection<Cita> citas = new ObservableCollection<Cita>();

        private readonly DelegateCommand<Cita> onCitaSeleccionada;

        private DateTime? fechaSeleccionada;

        public ControlDeCitasViewModel()
        {
            this.onCitaSeleccionada = new DelegateCommand<Cita>(this.ExecuteOnCitaSeleccionada);
            this.TituloVentana = "Disponibilidad Cita:";
            this.FechaSeleccionada = null;
        }

        public enum ModoVentana
        {
            Programar, Modificar, SoloLectura
        }

        public Cita Cita { get; set; }

        public DelegateCommand<Cita> OnCitaSeleccionada
        {
            get
            {
                return this.onCitaSeleccionada;
            }
        }

        public DateTime? FechaSeleccionada
        {
            get
            {
                return this.fechaSeleccionada;
            }

            set
            {
                if (value.Equals(this.fechaSeleccionada))
                {
                    return;
                }

                this.fechaSeleccionada = value;
                this.RaisePropertyChanged("FechaSeleccionada");
                this.OnFechaSeleccionadaChanged();
            }
        }

        public ObservableCollection<Cita> Citas
        {
            get
            {
                return this.citas;
            }
        }

        private void ExecuteOnCitaSeleccionada(Cita cita)
        {
            this.Cita = cita;
            this.CloseView = true;
        }

        private void OnFechaSeleccionadaChanged()
        {
            if (this.FechaSeleccionada.HasValue)
            {
                this.IsBusy = true;
                this.Citas.Clear();
                Acciones.ConsultaEstadoAgendaDiaAsync(
                    this.FechaSeleccionada.Value,
                    (ss, ee) =>
                    {
                        if (ee.Cancelled)
                        {
                        }
                        else if (ee.Error != null)
                        {
                            this.OnMostrarMensaje("Error: " + ee.Error.Message);
                        }
                        else
                        {
                            var citasEncontradas = ee.Result as List<Cita>;

                            if (citasEncontradas != null)
                            {
                                foreach (var citasEncontrada in citasEncontradas)
                                {
                                    this.Citas.Add(citasEncontrada);
                                }
                            }
                        }

                        this.IsBusy = false;
                    });
            }
        }
    }
}
