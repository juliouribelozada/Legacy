// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OscInsertaResult.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the OscInsertaResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using Microsoft.Practices.Prism.ViewModel;

    public class OscInsertaResult : NotificationObject
    {
        private string error;

        private string listaOsc;

        private string listaFactura;

        private string mensajeMuestra;

        private int? idMuestraCentro;

        private int? idNumMuestra;

        private string token;

        public string Error
        {
            get
            {
                return this.error;
            }
            
            set
            {
                if (value == this.error)
                {
                    return;
                }
                
                this.error = value;
                this.RaisePropertyChanged("Error");
            }
        }

        public string Token
        {
            get
            {
                return this.token;
            }
            
            set
            {
                if (value == this.token)
                {
                    return;
                }
                
                this.token = value;
                this.RaisePropertyChanged("Token");
            }
        }

        public int? IdNumMuestra
        {
            get
            {
                return this.idNumMuestra;
            }
            
            set
            {
                if (value == this.idNumMuestra)
                {
                    return;
                }
                
                this.idNumMuestra = value;
                this.RaisePropertyChanged("IdNumMuestra");
            }
        }

        public int? IdMuestraCentro
        {
            get
            {
                return this.idMuestraCentro;
            }
            
            set
            {
                if (value == this.idMuestraCentro)
                {
                    return;
                }
                
                this.idMuestraCentro = value;
                this.RaisePropertyChanged("IdMuestraCentro");
            }
        }

        public string MensajeMuestra
        {
            get
            {
                return this.mensajeMuestra;
            }
            
            set
            {
                if (value == this.mensajeMuestra)
                {
                    return;
                }
                
                this.mensajeMuestra = value;
                this.RaisePropertyChanged("MensajeMuestra");
            }
        }

        public string ListaFactura
        {
            get
            {
                return this.listaFactura;
            }
            
            set
            {
                if (value == this.listaFactura)
                {
                    return;
                }
                
                this.listaFactura = value;
                this.RaisePropertyChanged("ListaFactura");
            }
        }

        public string ListaOsc
        {
            get
            {
                return this.listaOsc;
            }
            
            set
            {
                if (value == this.listaOsc)
                {
                    return;
                }
                
                this.listaOsc = value;
                this.RaisePropertyChanged("ListaOsc");
            }
        }
    }
}
