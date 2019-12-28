// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the ValidationResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValidationResult : NotifiableObject
    {
        private string name;
        private bool result;
        private string code;
        private string especialidad;

        private string idNumber;

        public string IdNumber
        {
            get
            {
                return this.idNumber;
            }

            set
            {
                if (value == this.idNumber)
                {
                    return;
                }

                this.idNumber = value;
                this.NotifyPropertyChanged(nameof(this.IdNumber));
            }
        }

        public string Especialidad
        {
            get
            {
                return this.especialidad;
            }

            set
            {
                this.especialidad = value;
            }
        }

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
                    this.NotifyPropertyChanged("Code");
                }
            }
        }

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
                    this.NotifyPropertyChanged(() => this.Name);
                }
            }
        }

        public bool Result
        {
            get
            {
                return this.result;
            }

            set
            {
                if (value != this.result)
                {
                    this.result = value;
                    this.NotifyPropertyChanged(() => this.Result);
                }
            }
        }
    }
}
