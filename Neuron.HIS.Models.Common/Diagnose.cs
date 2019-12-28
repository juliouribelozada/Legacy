// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Diagnose.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ServiceAgreement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using Atpc.Common;

    /// <summary>
    /// This Object contains Info About the Diagnose.
    /// </summary>
    public class Diagnose : NotifiableObject
    {
        private string code;

        private string name;

        private string fullName;

        /// <summary>
        /// Gets or sets the Diagnose Code.
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
                    this.NotifyPropertyChanged("Code");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Diagnose Name.
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
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                if (value != this.fullName)
                {
                    this.fullName = value;
                    this.NotifyPropertyChanged("FullName");
                }
            }
        }

        public Diagnose Copy()
        {
            return (Diagnose)this.MemberwiseClone();
        }
    }
}