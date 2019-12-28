// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjetoValidable.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ObjetoValidable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml.Serialization;

    using Microsoft.Practices.Prism.ViewModel;

    public abstract class ObjetoValidable : NotificationObject, IDataErrorInfo
    {
        private readonly Dictionary<string, List<ValidationResult>> erroresPorPropiedad = new Dictionary<string, List<ValidationResult>>();

        private bool isBusy;

        public bool IsValid
        {
            get
            {
                if (this.ErroresPorPropiedad.Values.Any(validationResults => validationResults.Count > 0))
                {
                    return false;
                }

                return true;
            }
        }

        [XmlIgnore]
        public Dictionary<string, List<ValidationResult>> ErroresPorPropiedad
        {
            get
            {
                return this.erroresPorPropiedad;
            }
        }

        public ObservableCollection<string> Errores
        {
            get
            {
                Debug.WriteLine("Errores: " + this.GetType());
                return new ObservableCollection<string>(this.ErroresPorPropiedad.SelectMany(val => val.Value).Select(a => a.ErrorMessage));
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

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return this.OnValidate(propertyName);
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            Debug.WriteLine(this.GetType() + " => " + propertyName);
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            if (!this.erroresPorPropiedad.ContainsKey(propertyName))
            {
                this.erroresPorPropiedad.Add(propertyName, new List<ValidationResult>());
            }

            string error = string.Empty;
            var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>(1);
            var result = Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName }, results);
            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }

            this.erroresPorPropiedad[propertyName] = results;
            this.RaisePropertyChanged("IsValid");
            this.RaisePropertyChanged("Errores");

            return error;
        }
    }
}
