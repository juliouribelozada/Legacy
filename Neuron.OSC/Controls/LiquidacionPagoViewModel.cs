using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Neuron.OSC.Data.Model;

using NeuronCloud.Atpc.Co.Modelos;
using NeuronCloud.Atpc.Co.Modelos.Helpers;

namespace Neuron.OSC.Controls
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    public class LiquidacionPagoViewModel : NotificationObject
    {
        private PaymentDetails originalViewModel;

        public LiquidacionPagoViewModel()
        {
            this.FormasDePago = GlobalModelosCache.FormasDePagoList;
            this.AddPaymentCommand = new DelegateCommand(this.AddPaymentActionHelper);
            this.DoFullPaymentCommand = new DelegateCommand(this.DoFullPaymentAction);
        }

        public static RoutedCommand RemovePaymentCommand { get; } = new RoutedCommand();

        public static void RemovePaymentAction(object sender, ExecutedRoutedEventArgs e)
        {
            var controlDataGrid = e.Source as DataGrid;

            var liquidacionPagoViewModel = controlDataGrid?.DataContext as LiquidacionPagoViewModel;

            if (liquidacionPagoViewModel != null)
            {
                var itemPago = controlDataGrid.CurrentItem as ItemPago;

                if (itemPago != null)
                {
                    liquidacionPagoViewModel.OriginalViewModel.Pagos.Remove(itemPago);
                    liquidacionPagoViewModel.RaisePropertyChanged(nameof(liquidacionPagoViewModel.CurrentValorPendientePorConvenio));
                }
            }
        }

        private void AddPaymentActionHelper()
        {
            this.AddPaymentAction(false);
        }

        private void AddPaymentAction(bool closeview)
        {

            if (this.OriginalViewModel.SelectedSelectedAgreementToPay == null)
            {
                MessageBox.Show("Seleccione un Convenio", "Error");
                return;
            }

            if (this.SelectedFormaDePago == null)
            {
                MessageBox.Show("Seleccione un Tipo de Pago", "Error");
                return;
            }

            if (this.CurrentValorParcial > this.CurrentValorPendientePorConvenio)
            {
                MessageBox.Show("El Valor no puede ser Mayor al Saldo", "Error");
                return;
            }

            if (this.CurrentValorParcial > 0)
            {
                var itemPago = new ItemPago
                {
                    NoDocumento = this.CurrenNoDocPago,
                    VrTipoPago = this.CurrentValorParcial,
                    CodConvenio = this.OriginalViewModel.SelectedSelectedAgreementToPay.CodConvenio,
                    TipoPago = this.SelectedFormaDePago.TipoPago
                };

                this.OriginalViewModel.Pagos.Add(itemPago);

            }


            RaisePropertyChanged(nameof(CurrentValorPendientePorConvenio));

            this.CurrenNoDocPago = string.Empty;
            this.CurrentValorParcial = 0;
            this.SelectedFormaDePago = null;
            if (closeview)
            {
                this.CloseView = true;
            }
        }

        private void DoFullPaymentAction()
        {
            this.CurrentValorParcial = this.CurrentValorPendientePorConvenio;
            this.AddPaymentAction(true);
        }

        public ObservableCollection<FormaDePago> FormasDePago { get; private set; }

        public FormaDePago SelectedFormaDePago
        {
            get { return this.selectedFormaDePago; }
            set
            {
                if (Equals(value, this.selectedFormaDePago)) return;
                this.selectedFormaDePago = value;
                this.RaisePropertyChanged(nameof(this.SelectedFormaDePago));
            }
        }

        public string CurrenNoDocPago
        {
            get
            {
                return currenNoDocPago;
            }
            set
            {
                if (value == currenNoDocPago) return;
                currenNoDocPago = value;
                RaisePropertyChanged(nameof(CurrenNoDocPago));
            }
        }

        public decimal CurrentValorParcial
        {
            get
            {
                return currentValorParcial;
            }
            set
            {
                if (value == currentValorParcial) return;
                currentValorParcial = value;
                RaisePropertyChanged(nameof(CurrentValorParcial));
            }
        }

        public decimal CurrentValorPendientePorConvenio
        {
            get
            {
                if (this.OriginalViewModel != null)
                {
                    if (OriginalViewModel.SelectedSelectedAgreementToPay != null)
                    {
                        decimal sum = this.OriginalViewModel.Pagos.Where(p => p.CodConvenio == OriginalViewModel.SelectedSelectedAgreementToPay.CodConvenio).Sum(p => p.VrTipoPago);
                        return OriginalViewModel.SelectedSelectedAgreementToPay.Valor - sum;
                    }
                    return 0;

                }
                return 0;
            }

        }

        private bool closeView;
        private FormaDePago selectedFormaDePago;

        private TipoPago selectedMedioPago;

        private string currenNoDocPago;

        private decimal currentValorParcial;

        public PaymentDetails OriginalViewModel
        {
            get { return this.originalViewModel; }
            set
            {
                if (Equals(value, this.originalViewModel)) return;
                this.originalViewModel = value;
                this.RaisePropertyChanged(nameof(this.OriginalViewModel));
                this.ConveniosView = CollectionViewSource.GetDefaultView(this.OriginalViewModel.LiquidationItems);
                this.ConveniosView.CurrentChanged += (sender, args) =>
                {
                    RaisePropertyChanged(nameof(CurrentValorPendientePorConvenio));
                };
                if (this.OriginalViewModel.LiquidationItems.Count == 1)
                {
                    this.OriginalViewModel.SelectedSelectedAgreementToPay = this.OriginalViewModel.LiquidationItems.First();
                }

            }
        }

        public ICollectionView ConveniosView { get; set; }

        public bool CloseView
        {
            get { return this.closeView; }
            set
            {
                if (value == this.closeView) return;
                this.closeView = value;
                this.RaisePropertyChanged(nameof(this.CloseView));
            }
        }

        public DelegateCommand AddPaymentCommand { get; }

        public DelegateCommand DoFullPaymentCommand { get; }



        public TipoPago SelectedMedioPago
        {
            get
            {
                return selectedMedioPago;
            }
            set
            {
                if (Equals(value, selectedMedioPago)) return;
                selectedMedioPago = value;
                RaisePropertyChanged(nameof(SelectedMedioPago));
            }
        }
    }
}
