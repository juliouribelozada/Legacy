// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PRO_CargaCapturaCuatroCamposDosBloques_Result.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public partial class PRO_CargaCapturaCuatroCamposDosBloques_Result
    {
        private readonly ObservableCollection<string> suggestions = new ObservableCollection<string>();
        private readonly ObservableCollection<string> alerts = new ObservableCollection<string>();

        private AtpcBindableCommand verHistoricoCommand;

        private EstudioValorNormal valorNormal;

        private AnalyteAbnormalFlags flag;

        public PRO_CargaCapturaCuatroCamposDosBloques_Result()
        {
            this.Loading = true;
            StoreProceduresOps.GetSugestions(this);
            StoreProceduresOps.GetAlerts(this);
        }

        public event EventHandler DatoLargoChanged;

        public event EventHandler DatoChanged;

        public event EventHandler VerHistorico;

        public event EventHandler VerObservacionesAlerta;

        public int Columna
        {
            get
            {
                return this.PosicionReporte % 2 > 0 ? 0 : 1;
            }
        }

        public AnalyteAbnormalFlags Flag
        {
            get
            {
                return this.flag;
            }

            set
            {
                if (value != this.flag)
                {
                    this.flag = value;
                    this.ReportPropertyChanged("Flag");
                    this.ReportToUser();
                }
            }
        }

        public bool Loading { get; set; }

        public EstudioValorNormal ValorNormal
        {
            get
            {
                return this.valorNormal;
            }

            set
            {
                if (value != this.valorNormal)
                {
                    this.valorNormal = value;
                    this.ReportPropertyChanged("EstudioValorNormal");
                    this.Flag = this.VerificarValorNormar();
                }
            }
        }

        public ObservableCollection<string> Suggestions
        {
            get
            {
                return this.suggestions;
            }
        }

        public ObservableCollection<string> Alerts
        {
            get
            {
                return this.alerts;
            }
        }

        public AtpcBindableCommand VerHistoricoCommand
        {
            get
            {
                return this.verHistoricoCommand ?? (this.verHistoricoCommand = new AtpcBindableCommand(this.OnVerHistorico, obj => true));
            }
        }

        public void OnVerHistorico(object o)
        {
            EventHandler handler = this.VerHistorico;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        partial void OnDatoLargoChanged()
        {
            EventHandler handler = this.DatoLargoChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        partial void OnDatoChanged()
        {
            EventHandler handler = this.DatoChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }

            this.Flag = this.VerificarValorNormar();
        }

        partial void OnPosicionReporteChanged()
        {
            ReportPropertyChanged("Columna");
        }

        private void OnVerObservacionesAlerta()
        {
            EventHandler handler = this.VerObservacionesAlerta;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private AnalyteAbnormalFlags VerificarValorNormar()
        {
            if (this.ValorNormal != null)
            {
                if (this.ValorNormal.TipoVrNormal.ToUpperInvariant() == "Cualitativo".ToUpperInvariant())
                {
                    if (!string.IsNullOrEmpty(this.Dato))
                    {
                        if (this.ValorNormal.VrNormalCualitativo.ToUpperInvariant() != this.Dato.ToUpperInvariant())
                        {
                            return AnalyteAbnormalFlags.AlarmHigh;
                        }

                        return AnalyteAbnormalFlags.Normal;
                    }

                    return AnalyteAbnormalFlags.None;
                }

                decimal datoDecimal;
                if (decimal.TryParse(this.Dato, NumberStyles.Number, CultureInfo.InvariantCulture, out datoDecimal))
                {
                    // Debug.WriteLine(this.ValorNormal.Tipo + ":" + datoDecimal);
                    if (this.ValorNormal.Tipo == RangeType.Full && datoDecimal > this.ValorNormal.VrAlarmaHasta)
                    {
                        return AnalyteAbnormalFlags.AlarmHigh;
                    }

                    if (datoDecimal > this.ValorNormal.VrNormalHasta)
                    {
                        return AnalyteAbnormalFlags.High;
                    }

                    if (this.ValorNormal.Tipo == RangeType.Full && datoDecimal < this.ValorNormal.VrAlarmaDesde)
                    {
                        return AnalyteAbnormalFlags.AlarmLow;
                    }

                    if (datoDecimal < this.ValorNormal.VrNormalDesde)
                    {
                        return AnalyteAbnormalFlags.Low;
                    }

                    if (datoDecimal <= this.ValorNormal.VrNormalHasta && datoDecimal >= this.ValorNormal.VrNormalDesde)
                    {
                        return AnalyteAbnormalFlags.Normal;
                    }

                    return AnalyteAbnormalFlags.Abnormal;
                }

                return AnalyteAbnormalFlags.Invalid;
            }

            return AnalyteAbnormalFlags.None;
        }

        private void ReportToUser()
        {
            if (this.Loading || this.Alerts.Count <= 0)
            {
                return;
            }

            this.OnVerObservacionesAlerta();
        }
    }
}
