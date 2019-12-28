// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HistoricoViewModel.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the HistoricoViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public class HistoricoViewModel : NotifiableObject
    {
        private readonly ObservableCollection<PRO_CapturaManualHistorico_Result> historicos = new ObservableCollection<PRO_CapturaManualHistorico_Result>();
        private readonly ObservableCollection<ValorGraficaFecha> listaValores = new ObservableCollection<ValorGraficaFecha>();

        private CapturaManualHistoricoParameters parametros;

        public ObservableCollection<ValorGraficaFecha> ListaValores
        {
            get
            {
                return this.listaValores;
            }
        }

        public ObservableCollection<PRO_CapturaManualHistorico_Result> Historicos
        {
            get
            {
                return this.historicos;
            }
        }

        public CapturaManualHistoricoParameters Parametros
        {
            get
            {
                return this.parametros;
            }

            set
            {
                if (value != this.parametros)
                {
                    this.parametros = value;
                    this.NotifyPropertyChanged(() => this.Parametros);
                }
            }
        }

        public void Consultar()
        {
            StoreProceduresOps.PRO_CapturaManualHistoricoAsync(
                this.Parametros,
                (s, e) =>
                {
                    this.Historicos.Clear();
                    if (e.Cancelled)
                    {
                    }
                    else if (e.Error != null)
                    {
                    }
                    else
                    {
                        var results = e.Result as List<PRO_CapturaManualHistorico_Result>;

                        if (results != null)
                        {
                            foreach (var result in results)
                            {
                                this.Historicos.Add(result);
                            }
                        }
                    }

                    if (this.Historicos.Count > 0)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(this.EvaluarLista));
                    }
                });
        }

        private void EvaluarLista()
        {
            this.listaValores.Clear();
            foreach (var proCapturaManualHistoricoResult in this.Historicos)
            {
                double salida;
                if (double.TryParse(proCapturaManualHistoricoResult.Dato, NumberStyles.Number, CultureInfo.InstalledUICulture, out salida))
                {
                    this.ListaValores.Add(new ValorGraficaFecha { Fecha = proCapturaManualHistoricoResult.FechaHoraValidacion, Valor = salida });
                }
            }
        }
    }
}
