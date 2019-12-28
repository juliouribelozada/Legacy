// -----------------------------------------------------------------------
// <copyright file="InfoPersonaViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InfoPersonaViewModel : NotifiableObject
    {
        private PRO_CargaDatosPersoMuestra_Result result;

        public PRO_CargaDatosPersoMuestra_Result Result
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
