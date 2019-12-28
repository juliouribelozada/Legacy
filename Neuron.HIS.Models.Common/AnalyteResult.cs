// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnalyteResult.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;

    using Atpc.Common;

    public class AnalyteResult<T> : NotifiableObject, IAnalyteResult
        where T : class
    {
        private Analyte analyte;

        private T value;
        private T alternateValue;

        private string status;

        public Analyte Analyte
        {
            get
            {
                return this.analyte;
            }

            set
            {
                if (value != this.analyte)
                {
                    this.analyte = value;
                    this.NotifyPropertyChanged("Analyte");
                }
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                this.NotifyPropertyChanged("Status");
            }
        }

        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value != this.value)
                {
                    this.value = value;
                    this.NotifyPropertyChanged("Value");
                }
            }
        }

        public T AlternateValue
        {
            get
            {
                return this.alternateValue;
            }

            set
            {
                if (value != this.alternateValue)
                {
                    this.alternateValue = value;
                    this.NotifyPropertyChanged("AlternateValue");
                }
            }
        }

        public void SetValue(object newValue)
        {
            var castedValue = newValue as T;

            if (castedValue != null)
            {
                this.Value = castedValue;
            }
        }

        public void SetAlternateValue(object newValue)
        {
            var castedValue = newValue as T;

            if (castedValue != null)
            {
                this.AlternateValue = castedValue;
            }
        }

        public void SetStatus(string newValue)
        {
            this.Status = newValue;
        }
    }
}
