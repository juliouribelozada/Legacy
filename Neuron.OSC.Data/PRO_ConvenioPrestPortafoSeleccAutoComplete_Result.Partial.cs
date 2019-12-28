// -----------------------------------------------------------------------
// <copyright file="PRO_ConvenioPrestPortafoSeleccAutoComplete_Result.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.OSC.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class PRO_ConvenioPrestPortafoSeleccAutoComplete_Result
    {
        public string FullName
        {
            get
            {
                return this.Codigo + " - " + this.Nombre;
            }
        }

        partial void OnNombreChanged()
        {
            this.ReportPropertyChanged("FullName");
        }

        partial void OnCodigoChanged()
        {
            this.ReportPropertyChanged("FullName");
        }
    }
}
