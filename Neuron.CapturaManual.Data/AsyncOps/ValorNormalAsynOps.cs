// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValorNormalAsynOps.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    public class ValorNormalAsynOps
    {
        public static void GetAsync(ConsultaValorNormalParameters parametros, OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(parametros, GetActionOld, resultadoOperacion);
        }

        public static void GetAsync(OperacionAsincronaEventHandler resultadoOperacion)
        {
            new AccionAsincronaGenerica(null, GetAction, resultadoOperacion);
        }

        private static void GetActionOld(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as ConsultaValorNormalParameters;

            if (parameters != null)
            {
                using (NeuronCapturaManualEntities context = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
                {
                    EstudioValorNormal output = context.EstudioValorNormal.FirstOrDefault(valorNormal => valorNormal.NomAnalizador == "MANUAL" && valorNormal.Analito == parameters.Analito && valorNormal.CodigoFuente == parameters.CodigoFuente && (valorNormal.Genero == "AMBOS" ? valorNormal.Genero == "AMBOS" : valorNormal.Genero == parameters.Genero));
                    e.Result = output;
                }
            }
        }

        private static void GetAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronCapturaManualEntities context = new NeuronCapturaManualEntities(NeuronStorage.EntityConnectionString))
            {
                List<EstudioValorNormal> output = context.EstudioValorNormal.Where(valorNormal => valorNormal.NomAnalizador == "MANUAL").ToList();
                e.Result = output;
            }
        }
    }
}
