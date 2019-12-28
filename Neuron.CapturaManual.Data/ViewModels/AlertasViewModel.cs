// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertasViewModel.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the AlertasViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public class AlertasViewModel : NotifiableObject
    {
        private readonly Window ventana;

        private ObservableCollection<string> listaAlertas;

        private PRO_CargaCapturaSeccion_Result examen;

        private string selectedAlert;

        public AlertasViewModel(Window ventana)
        {
            this.ventana = ventana;
        }

        public ObservableCollection<string> ListaAlertas
        {
            get
            {
                return this.listaAlertas;
            }

            set
            {
                if (this.listaAlertas != value)
                {
                    this.listaAlertas = value;
                    this.NotifyPropertyChanged("ListaAlertas");
                }
            }
        }

        public PRO_CargaCapturaSeccion_Result Examen
        {
            get
            {
                return this.examen;
            }

            set
            {
                if (this.examen != value)
                {
                    this.examen = value;
                    this.NotifyPropertyChanged("Examen");
                }
            }
        }

        public string SelectedAlert
        {
            get
            {
                return this.selectedAlert;
            }

            set
            {
                if (this.selectedAlert != value)
                {
                    this.selectedAlert = value;
                    this.NotifyPropertyChanged("SelectedAlert");
                    this.ActualizarObservacion();
                }
            }
        }

        public void ActualizarObservacion()
        {
            if (string.IsNullOrWhiteSpace(this.SelectedAlert))
            {
                return;
            }

            if (this.examen != null)
            {
                if (string.IsNullOrEmpty(this.Examen.Observacion))
                {
                    this.Examen.Observacion = this.SelectedAlert;
                }
                else
                {
                    this.Examen.Observacion += "\n" + this.SelectedAlert;
                }
            }

            if (this.ventana != null)
            {
                this.ventana.DialogResult = true;
            }
        }
    }
}
