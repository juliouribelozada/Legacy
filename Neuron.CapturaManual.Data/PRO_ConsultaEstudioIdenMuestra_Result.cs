// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PRO_CargaCapturaCuatroCampos_Result.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the PRO_CargaCapturaCuatroCampos_Result type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public partial class PRO_ConsultaEstudioIdenMuestra_Result
    {
        private readonly ObservableCollection<PRO_ConsultaResultadoEstudio_Result> resultados = new ObservableCollection<PRO_ConsultaResultadoEstudio_Result>();

        public ObservableCollection<PRO_ConsultaResultadoEstudio_Result> Resultados
        {
            get
            {
                return this.resultados;
            }
        }


        public bool AnularVisible => (this.NoResultado != null && CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Anular));

        partial void OnNoResultadoChanged()
        {
            this.ReportPropertyChanged("AnularVisible");
            if (!string.IsNullOrWhiteSpace(this.NoResultado))
            {
                this.Resultados.Clear();
                StoreProceduresOps.PRO_ConsultaResultadoEstudioAsync(
                    this.NoResultado,
                    (s, e) =>
                    {
                        if (e.Cancelled)
                        {
                        }
                        else if (e.Error != null)
                        {
                        }
                        else
                        {
                            var consultaResultadoEstudioResults = e.Result as List<PRO_ConsultaResultadoEstudio_Result>;

                            if (consultaResultadoEstudioResults != null)
                            {
                                foreach (var proConsultaResultadoEstudioResult in consultaResultadoEstudioResults)
                                {
                                    this.Resultados.Add(proConsultaResultadoEstudioResult);
                                }
                            }
                        }
                    });
            }
        }

    }
}
