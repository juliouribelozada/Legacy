using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Neuron.OSC.Data.Model
{
    public class PaymentDetails : NotificationObject
    {
        private ObservableCollection<OscDataModel.PRO_OSCDFLiquidaPagoRow> _liquidationItems;
        private OscDataModel.PRO_OSCDFLiquidaPagoRow _selectedSelectedAgreementToPay;
        private readonly ObservableCollection<ItemPago> _pagos= new ObservableCollection<ItemPago>();

        public PaymentDetails()
        {
            this._pagos.CollectionChanged += this.Pagos_CollectionChanged;
        }

        private void Pagos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        public ObservableCollection<OscDataModel.PRO_OSCDFLiquidaPagoRow> LiquidationItems
        {
            get { return this._liquidationItems; }
            set
            {
                if (Equals(value, this._liquidationItems)) return;
                this._liquidationItems = value;
                this.RaisePropertyChanged(nameof(this.LiquidationItems));
            }
        }

        public ObservableCollection<ItemPago> Pagos => this._pagos;
        
        public OscDataModel.PRO_OSCDFLiquidaPagoRow SelectedSelectedAgreementToPay
        {
            get { return this._selectedSelectedAgreementToPay; }
            set
            {
                if (Equals(value, this._selectedSelectedAgreementToPay)) return;
                this._selectedSelectedAgreementToPay = value;
                this.RaisePropertyChanged(nameof(this.SelectedSelectedAgreementToPay));
            }
        }
    }
}
