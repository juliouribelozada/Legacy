// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyDBtoModelo.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity
{
    using System.Linq;

    using NeuronCloud.Atpc.Co.Modelos.Helpers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProxyDBtoModelo
    {
        public static NeuronCloud.Atpc.Co.Modelos.Departamento Departamento(Neuron.OSC.Data.Departamento departamentoDB)
        {
            var salida = new Departamento
            {
                Nombre = departamentoDB.NomDepto,
                Codigo = departamentoDB.CodDepto,
                CodigoPais = departamentoDB.CodPais,
                Pais = GlobalModelosCache.PaisList.FirstOrDefault(pais => pais.Codigo == departamentoDB.CodPais)
            };
            return salida;
        }
        public static NeuronCloud.Atpc.Co.Modelos.FormaDePago FormaDePago(Neuron.OSC.Data.CRUD_FormaPagoSelecciona_Result formaDePagoDB)
        {
            var salida = new FormaDePago
            {
                TipoPago = formaDePagoDB.TipoPago,
                Credito = formaDePagoDB.Credito,
                MedioDePago = formaDePagoDB.MedioPago,
                NumeroDeDocumentoDePago = formaDePagoDB.NoDocPago,
                TextoEtiquetaDocumentoDePago = formaDePagoDB.NomLblDocPago,

            };
            return salida;
        }
        public static NeuronCloud.Atpc.Co.Modelos.TipoPago TipoPago(Neuron.OSC.Data.PRO_FormaPagoMedioPagoSelecciona_Result formaDePagoDB)
        {
            var salida = new TipoPago
            {
                MedioDePago = formaDePagoDB.TipoPago
            };
            return salida;
        }

        public static NeuronCloud.Atpc.Co.Modelos.Pais Pais(Neuron.OSC.Data.Pais paisDB)
        {
            var salida = new Pais()
            {
                Nombre = paisDB.NomPais,
                Codigo = paisDB.CodPais
            };
            return salida;
        }

        public static NeuronCloud.Atpc.Co.Modelos.Municipio Municipio(Neuron.OSC.Data.Municipio municipioDB)
        {
            var salida = new Municipio()
            {
                Nombre = municipioDB.NomMpio,
                Codigo = municipioDB.CodMpio,
                CodigoPais = municipioDB.CodPais,
                CodigoDepartamento = municipioDB.CodDepto,
                Pais = GlobalModelosCache.PaisList.FirstOrDefault(pais => pais.Codigo == municipioDB.CodPais),
                Departamento = GlobalModelosCache.DepartamentoList.FirstOrDefault(departamento => departamento.Codigo == municipioDB.CodDepto)
            };
            return salida;
        }
    }
}
