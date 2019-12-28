// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronWindowViewModelBase.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   VieModel base Para ViewModels que seran atados a una Instancia de NeuronMainWindow.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.ViewModels
{
    using Microsoft.Practices.Prism.ViewModel;

    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;

    /// <summary>VieModel base Para ViewModels que seran atados a una Instancia de NeuronMainWindow.</summary>
    public class NeuronWindowViewModelBase : NotificationObject
    {
        private string tituloVentana;

        private bool closeView;

        private bool isBusy;

        public event ParametroTextoEventHandler MostrarMensajeEnUI;

        public string TituloVentana
        {
            get
            {
                return this.tituloVentana;
            }

            internal set
            {
                if (value == this.tituloVentana)
                {
                    return;
                }

                this.tituloVentana = value;
                this.RaisePropertyChanged("TituloVentana");
            }
        }

        public bool CloseView
        {
            get
            {
                return this.closeView;
            }

            set
            {
                if (value.Equals(this.closeView))
                {
                    return;
                }

                this.closeView = value;
                this.RaisePropertyChanged("CloseView");
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                if (value.Equals(this.isBusy))
                {
                    return;
                }

                this.isBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        public void OnMostrarMensaje(string parametro)
        {
            ParametroTextoEventHandler handler = this.MostrarMensajeEnUI;
            if (handler != null)
            {
                handler(this, parametro);
            }
        }
    }
}
