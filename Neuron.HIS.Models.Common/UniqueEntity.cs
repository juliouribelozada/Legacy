// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqueEntity.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Atpc.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UniqueEntity : NotifiableObject
    {
        private Guid uniqueId;

        //[Key]
        public Guid UniqueId
        {
            get
            {
                return this.uniqueId;
            }

            set
            {
                if (value != this.uniqueId)
                {
                    this.uniqueId = value;
                    this.NotifyPropertyChanged("UniqueId");
                }
            }
        }
    }
}
