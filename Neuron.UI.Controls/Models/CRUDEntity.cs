// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRUDEntity.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.UI.Controls.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;

    using Atpc.Common;

    public class CRUDEntity<T> : NotifiableObject where T : class
    {
        private T baseEntity;

        private bool editing;
        private bool isValid;
        private bool isBusy;

        public CRUDEntity(T baseEntity, bool isValid)
        {
            this.BaseEntity = baseEntity;
            this.IsValid = isValid;
        }

        public CRUDEntity()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            // throw new Exception("No se Puede Vacio");
        }

        public T BaseEntity
        {
            get
            {
                return this.baseEntity;
            }

            set
            {
                if (value != this.baseEntity)
                {
                    this.baseEntity = value;
                    this.NotifyPropertyChanged("BaseEntity");
                }
            }
        }

        public bool Editing
        {
            get
            {
                return this.editing;
            }

            set
            {
                if (value != this.editing)
                {
                    this.editing = value;
                    this.NotifyPropertyChanged("Editing");
                }
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
                if (value != this.isBusy)
                {
                    this.isBusy = value;
                    this.NotifyPropertyChanged("IsBusy");
                    this.NotifyPropertyChanged("Clear");
                }
            }
        }

        public bool Clear
        {
            get
            {
                return this.isBusy || this.isValid;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }

            set
            {
                if (value != this.isValid)
                {
                    this.isValid = value;
                    this.NotifyPropertyChanged("IsValid"); 
                    this.NotifyPropertyChanged("Clear");
                }
            }
        }

        public void ForceNotify()
        {
            this.NotifyPropertyChanged("IsValid");
        }
    }
}
