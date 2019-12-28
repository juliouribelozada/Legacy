// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceProvider.cs" company="ATPC.co">
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
    /// This Object contains Info About the Service Agreement.
    /// </summary>
    public class ServiceProvider : NotifiableObject
    {
        private string code;

        private string name;

        /// <summary>
        /// Gets or sets the Provider Code.
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
        /// Gets or sets the Provider Name.
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
    }
}