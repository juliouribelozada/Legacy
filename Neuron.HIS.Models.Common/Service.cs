// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Service.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   This Object contains Info About the Service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System.Collections.Generic;

    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// This Object contains Info About the Service.
    /// </summary>
    public class Service : NotificationObject
    {
        private List<Service> componentesCombo;

        private string code;

        private string name;

        private string plan;

        private string tipoPago;

        private double price;

        private float quantity = 1;

        private double total;

        private bool esCombo;

        private string serviceAgreementCode;

        private string providerCode;

        private string level;

        /// <summary>
        /// Gets or sets the Service Unit Code.
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                if (value != this.code)
                {
                    this.code = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Service Unit Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        public double Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
                this.RaisePropertyChanged("Price");
                this.RefreshTotal();
            }
        }

        public string TipoPago
        {
            get
            {
                return this.tipoPago;
            }

            set
            {
                if (value != this.tipoPago)
                {
                    this.tipoPago = value;
                    this.RaisePropertyChanged("TipoPago");
                }
            }
        }

        public string Plan
        {
            get
            {
                return this.plan;
            }

            set
            {
                if (value != this.plan)
                {
                    this.plan = value;
                    this.RaisePropertyChanged("Plan");
                }
            }
        }

        public float Quantity
        {
            get
            {
                return this.quantity;
            }

            set
            {
                if (value != this.quantity)
                {
                    this.quantity = value;
                    this.RaisePropertyChanged("Quantity");
                    this.RefreshTotal();
                }
            }
        }

        public double Total
        {
            get
            {
                return this.total;
            }

            set
            {
                this.total = value;
                this.RaisePropertyChanged("Total");
            }
        }

        public bool EsCombo
        {
            get
            {
                return this.esCombo;
            }

            set
            {
                if (value.Equals(this.esCombo))
                {
                    return;
                }

                this.esCombo = value;
                this.RaisePropertyChanged("EsCombo");
                if (this.EsCombo)
                {
                    if (this.componentesCombo == null)
                    {
                        this.componentesCombo = new List<Service>();
                    }
                }
                else
                {
                    this.componentesCombo = null;
                }
            }
        }

        public string ServiceAgreementCode
        {
            get
            {
                return this.serviceAgreementCode;
            }

            set
            {
                if (value == this.serviceAgreementCode)
                {
                    return;
                }
           
                this.serviceAgreementCode = value;
                this.RaisePropertyChanged("ServiceAgreementCode");
            }
        }

        public string ProviderCode
        {
            get
            {
                return this.providerCode;
            }

            set
            {
                if (value == this.providerCode)
                {
                    return;
                }
                
                this.providerCode = value;
                this.RaisePropertyChanged("ProviderCode");
            }
        }

        public List<Service> ComponentesCombo
        {
            get
            {
                return this.componentesCombo;
            }
        }

        public string Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value == this.level)
                {
                    return;
                }
            
                this.level = value;
                this.RaisePropertyChanged("Level");
            }
        }

        public Service Copy()
        {
            return (Service)this.MemberwiseClone();
        }

        private void RefreshTotal()
        {
            this.Total = this.Price * this.Quantity;
            if (this.EsCombo)
            {
                foreach (Service service in this.ComponentesCombo)
                {
                    service.Quantity = this.Quantity;
                }
            }
        }
    }
}