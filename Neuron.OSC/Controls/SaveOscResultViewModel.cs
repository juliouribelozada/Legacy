// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveOscResultViewModel.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the SaveOscResultViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.Practices.Prism.ViewModel;

    using Neuron.OSC.Data.Model;

    public class SaveOscResultViewModel : NotificationObject
    {
        private OscInsertaResult result;

        public OscInsertaResult Result
        {
            get
            {
                return this.result;
            }

            set
            {
                if (value == this.result)
                {
                    return;
                }

                this.result = value;
                this.RaisePropertyChanged("Result");
            }
        }
    }
}
