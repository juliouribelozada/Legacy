// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EstudioValorNormal.cs" company="Luis Soler">
//   Copyright © 2012-2015
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public partial class EstudioValorNormal
    {
        public RangeType Tipo
        {
            get
            {
                if (this.VrAlarmaDesde == this.VrAlarmaHasta)
                {
                    return RangeType.Partial;
                }
                
                return RangeType.Full;
            }
        }
    }
}
