// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExamenesPendientesViewModel.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System.Collections.ObjectModel;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public class ExamenesPendientesViewModel : NotifiableObject
    {
        private readonly ObservableCollection<PRO_ConsultaEstudioIdenMuestra_Result> pendientes = new ObservableCollection<PRO_ConsultaEstudioIdenMuestra_Result>();

        private string idMuestra;

        private InfoPersonaViewModel infoPersona;

        private bool reload;

        private ProfesionalViewModel infoProfesional;

        public ObservableCollection<PRO_ConsultaEstudioIdenMuestra_Result> Pendientes
        {
            get
            {
                return this.pendientes;
            }
        }

        public string IdMuestra
        {
            get
            {
                return this.idMuestra;
            }

            set
            {
                if (value != this.idMuestra)
                {
                    this.idMuestra = value;
                    this.NotifyPropertyChanged(() => this.IdMuestra);
                }
            }
        }

        public InfoPersonaViewModel InfoPersona
        {
            get
            {
                return this.infoPersona;
            }

            set
            {
                if (value != this.infoPersona)
                {
                    this.infoPersona = value;
                    this.NotifyPropertyChanged("InfoPersona");
                }
            }
        }

        public bool Reload
        {
            get
            {
                return this.reload;
            }

            set
            {
                if (value != this.reload)
                {
                    this.reload = value;
                    this.NotifyPropertyChanged("Reload");
                }
            }
        }

        public ProfesionalViewModel InfoProfesional
        {
            get
            {
                return this.infoProfesional;
            }

            set
            {
                if (value != this.infoProfesional)
                {
                    this.infoProfesional = value;
                    this.NotifyPropertyChanged("InfoProfesional");
                }
            }
        }
    }
}
