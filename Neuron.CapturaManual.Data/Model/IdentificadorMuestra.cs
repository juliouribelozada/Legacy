// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentificadorMuestra.cs" company="">
//   TODO: Update copyright text.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public class IdentificadorMuestra : NotifiableObject
    {
        private readonly ObservableCollection<string> centrosDeTomaList = new ObservableCollection<string>();
        private DateTime fechaToma;
        private string centroToma = string.Empty;
        private string numeroMuestra = string.Empty;

        public IdentificadorMuestra()
        {
            this.FechaToma = DateTime.Today;
            this.CentrosDeTomaList.Add("Cargando Centros de Toma...");
        }

        public event EventHandler IdChanged;

        public ObservableCollection<string> CentrosDeTomaList
        {
            get
            {
                return this.centrosDeTomaList;
            }
        }

        public DateTime FechaToma
        {
            get
            {
                return this.fechaToma;
            }

            set
            {
                if (value != this.fechaToma)
                {
                    this.fechaToma = value;
                    this.NotifyPropertyChanged(() => this.FechaToma);
                    this.NotifyPropertyChanged(() => this.Identificador);
                    this.OnIdChanged();
                }
            }
        }

        public string CentroToma
        {
            get
            {
                return this.centroToma;
            }

            set
            {
                if (value != this.centroToma)
                {
                    this.centroToma = value;
                    this.NotifyPropertyChanged(() => this.CentroToma);
                    this.NotifyPropertyChanged(() => this.Identificador);
                    this.OnIdChanged();
                }
            }
        }

        public string NumeroMuestra
        {
            get
            {
                return this.numeroMuestra;
            }

            set
            {
                if (value != this.numeroMuestra)
                {
                    this.numeroMuestra = value;
                    this.NotifyPropertyChanged(() => this.NumeroMuestra);
                    this.NotifyPropertyChanged(() => this.Identificador);
                    this.OnIdChanged();
                }
            }
        }

        public string Identificador
        {
            get
            {
                return this.fechaToma.ToString("yyyyMMdd") + this.centroToma + this.numeroMuestra.PadLeft(3, '0');
            }
        }

        public void OnIdChanged()
        {
            EventHandler handler = this.IdChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
