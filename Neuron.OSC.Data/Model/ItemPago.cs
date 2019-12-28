using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Neuron.OSC.Data.Model
{
    public class ItemPago : NotificationObject
    {
        private string _codConvenio;
        private decimal _vrTipoPago;
        private string _tipoPago;
        private string _noDocumento;

        public string CodConvenio
        {
            get { return this._codConvenio; }
            set
            {
                if (value == this._codConvenio) return;
                this._codConvenio = value;
                this.RaisePropertyChanged(nameof(this.CodConvenio));
            }
        }

        public string TipoPago
        {
            get { return this._tipoPago; }
            set
            {
                if (value == this._tipoPago) return;
                this._tipoPago = value;
                this.RaisePropertyChanged(nameof(this.TipoPago));
            }
        }

        public decimal VrTipoPago
        {
            get { return this._vrTipoPago; }
            set
            {
                if (value == this._vrTipoPago) return;
                this._vrTipoPago = value;
                this.RaisePropertyChanged(nameof(this.VrTipoPago));
            }
        }

        public string NoDocumento
        {
            get { return this._noDocumento; }
            set
            {
                if (value == this._noDocumento) return;
                this._noDocumento = value;
                this.RaisePropertyChanged(nameof(this.NoDocumento));
            }
        }
    }
}
