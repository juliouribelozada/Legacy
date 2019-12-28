// -----------------------------------------------------------------------
// <copyright file="SampleTakingCenterOps.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.AsyncOps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Neuron.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SampleTakingCenterOps
    {
        public static void GetAsync(OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(null, GetAction, resultadoOperacion);
        }

        private static void GetAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCapturaManualEntities context = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
            {
                List<SampleTakingCenter> output = context.CentroTomaMuestra.Select(centroTomaMuestra => new SampleTakingCenter { Name = centroTomaMuestra.NomCentroToma, Prefix = centroTomaMuestra.PrefijoCentroToma, SupplierCode = centroTomaMuestra.CodPrestador }).ToList();
                e.Result = output;
            }
        }
    }
}
