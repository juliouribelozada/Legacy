// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneralOps.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Data.AsyncOps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using Atpc.Common.Async;

    using Neuron.OSC.Data.Model;

    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;

    public class GeneralOps
    {
        public static void GetDepartamentosAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetDepartamentosAction, resultadoOperacion);
        }

        public static void GetMunicipiosAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetGetMunicipiosAction, resultadoOperacion);
        }

        public static void GetFormasDePagoAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetFormasDePagoAction, resultadoOperacion);
        }
        public static void GetTipoPagoAsync(string formaPago, AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(formaPago, GetTipoPagoAction, resultadoOperacion);
        }
        public static void GetPaisAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetPaisAction, resultadoOperacion);
        }

        public static void GetAsync(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetAction, resultadoOperacion);
        }

        public static void GetDelay(AsyncTaskCompletedEventHandler resultadoOperacion)
        {
            new AsyncTask(null, GetDelayAction, resultadoOperacion);
        }

        private static void GetDepartamentosAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                List<NeuronCloud.Atpc.Co.Modelos.Departamento> output = context.Departamento.Select(ProxyDBtoModelo.Departamento).ToList();
                e.Result = output;
            }
        }

        private static void GetPaisAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                List<NeuronCloud.Atpc.Co.Modelos.Pais> output = context.Pais.Select(ProxyDBtoModelo.Pais).ToList();
                e.Result = output;
            }
        }

        private static void GetGetMunicipiosAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                List<NeuronCloud.Atpc.Co.Modelos.Municipio> output = context.Municipio.Select(ProxyDBtoModelo.Municipio).ToList();
                e.Result = output;
            }
        }

        private static void GetFormasDePagoAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                List<NeuronCloud.Atpc.Co.Modelos.FormaDePago> output = context.CRUD_FormaPagoSelecciona().Select(ProxyDBtoModelo.FormaDePago).ToList();
                e.Result = output;
            }
        }

        private static void GetTipoPagoAction(object sender, DoWorkEventArgs e)
        {
            string formaDePago = e.Argument as string;
            if (!string.IsNullOrEmpty(formaDePago))
            {
                using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
                {
                    List<NeuronCloud.Atpc.Co.Modelos.TipoPago> output =
                        context.PRO_FormaPagoMedioPagoSelecciona(formaDePago).Select(ProxyDBtoModelo.TipoPago).ToList();
                    e.Result = output;
                }
            }
        }
        

        private static void GetAction(object sender, DoWorkEventArgs e)
        {
            using (NeuronOscEntitites context = new NeuronOscEntitites(NeuronOSCStorage.EntityConnectionString))
            {
                List<NeuronCloud.Atpc.Co.Modelos.TipoIdentificacion> output = context.TipoIdentificacion.Where(ti => ti.RazonSocialCHK.ToUpper() == "NO").Select(ProxyConverters.DbTipoIdentificacionToModel).ToList();
                e.Result = output;
            }
        }

        private static void GetDelayAction(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5000);
        }
    }
}
