// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LiquidationParameters.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.Model
{
    using System.Collections.Generic;

    using Neuron.HIS.Models.Common;
    using Neuron.UI.Controls.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LiquidationParameters
    {
        public LiquidationParameters(IEnumerable<CRUDEntity<Service>> services)
        {
            this.Services = new List<CRUDEntity<Service>>(services);
        }

        public PatientInfo CurrentPatient { get; set; }

        public double ValorDescuento { get; set; }
        public IEnumerable<CRUDEntity<Service>> Services { get; set; }
    }
}
