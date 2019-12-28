// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Analyte.cs" company="">
//   TODO: Update copyright text.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Atpc.Common;

    public class Analyte : NotifiableObject
    {
        private string code;

        private string range;

        private string units;

        private string abnormalFlags;

        /// <summary>
        /// Gets or sets Code.
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                if (this.code != value)
                {
                    this.code = value;
                    this.NotifyPropertyChanged("Code");
                }
            }
        }

        /// <summary>
        /// Gets or sets Range.
        /// </summary>
        public string Range
        {
            get
            {
                return this.range;
            }

            set
            {
                if (this.range != value)
                {
                    this.range = value;
                    this.NotifyPropertyChanged("Range");
                }
            }
        }

        /// <summary>
        /// Gets or sets Units.
        /// </summary>
        public string Units
        {
            get
            {
                return this.units;
            }

            set
            {
                if (this.units != value)
                {
                    this.units = value;
                    this.NotifyPropertyChanged("Units");
                }
            }
        }

        public string AbnormalFlags
        {
            get
            {
                return this.abnormalFlags;
            }

            set
            {
                if (this.abnormalFlags != value)
                {
                    this.abnormalFlags = value;
                    this.NotifyPropertyChanged("AbnormalFlags");
                }
            }
        }

        ////public override string ToString()
        ////{
        ////    return this.Value + " " + this.Units + (string.IsNullOrWhiteSpace(this.AbnormalFlags) ? string.Empty : (": " + this.AbnormalFlags));
        ////}
    }
}
