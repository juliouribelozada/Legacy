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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Threading;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public partial class PRO_CargaCapturaCuatroCampos_Result : IDataErrorInfo
    {
        private readonly ObservableCollection<string> suggestions = new ObservableCollection<string>();
        private readonly ObservableCollection<string> alerts = new ObservableCollection<string>();
        private readonly ObservableCollection<SugerenciasAutoTexto> suggestionsDatoLargo = new ObservableCollection<SugerenciasAutoTexto>();
        private readonly ObservableCollection<SugerenciasAutoTexto> suggestionsMultiDatoLargo = new ObservableCollection<SugerenciasAutoTexto>();

        private AtpcBindableCommand verHistoricoCommand;

        private EstudioValorNormal valorNormal;

        private AnalyteAbnormalFlags flag;

        private bool esColumna;

        private string aditionalObservations;

        private DataCaptureType fieldType;

        private bool selectedToShow;

        public PRO_CargaCapturaCuatroCampos_Result()
        {
            this.Loading = true;
            this.Suggestions.CollectionChanged += (ss, oo) => this.ReportPropertyChanged("Suggestions");
            StoreProceduresOps.GetSugestions(this);
            StoreProceduresOps.GetAlerts(this);
            StoreProceduresOps.GetDatoLargoSugestions(
                this,
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
                        var sugerenciasAutoTextos = e.Result as List<SugerenciasAutoTexto>;

                        if (sugerenciasAutoTextos != null)
                        {
                            foreach (var sugerenciasAutoTexto in sugerenciasAutoTextos)
                            {
                                System.Windows.Application.Current.Dispatcher.Invoke(
                                    (Action)(() =>
                                        {
                                            this.SuggestionsDatoLargo.Add(sugerenciasAutoTexto);
                                        }));
                            }
                        }
                    }
                });

            StoreProceduresOps.GetDatoLargoMultiSugestions(
                 this,
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
                         var sugerenciasAutoTextos = e.Result as List<SugerenciasAutoTexto>;

                         if (sugerenciasAutoTextos != null)
                         {
                             foreach (var sugerenciasAutoTexto in sugerenciasAutoTextos)
                             {
                                 var texto = sugerenciasAutoTexto;
                                 System.Windows.Application.Current.Dispatcher.Invoke((Action)(() => { this.SuggestionsMultiDatoLargo.Add(texto); }));
                             }
                         }
                     }
                 });
        }

        public event EventHandler DatoLargoChanged;

        public event EventHandler DatoChanged;

        public event EventHandler VerHistorico;

        public event EventHandler VerObservacionesAlerta;

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

        public string Error
        {
            get
            {
                return null;
            }
        }

        public bool EsColumna
        {
            get
            {
                return this.esColumna;
            }

            set
            {
                if (value != this.esColumna)
                {
                    this.esColumna = value;
                    this.ReportPropertyChanged("EsColumna");
                }
            }
        }
        public bool SelectedToShow
        {
            get
            {
                return this.selectedToShow;
            }

            set
            {
                if (value != this.selectedToShow)
                {
                    this.selectedToShow = value;
                    this.ReportPropertyChanged("SelectedToShow");
                    this.OnSelectedToShowChanged();
                }
            }
        }

        public DataCaptureType FieldType
        {
            get
            {
                return this.fieldType;
            }

            set
            {
                if (value != this.fieldType)
                {
                    this.fieldType = value;
                    this.ReportPropertyChanged("FieldType");
                }
            }
        }

        public bool Loading { get; set; }

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

        public ObservableCollection<SugerenciasAutoTexto> SuggestionsDatoLargo
        {
            get
            {
                return this.suggestionsDatoLargo;
            }
        }

        public ObservableCollection<SugerenciasAutoTexto> SuggestionsMultiDatoLargo
        {
            get
            {
                return this.suggestionsMultiDatoLargo;
            }
        }

        public string AditionalObservations
        {
            get
            {
                return this.aditionalObservations;
            }
            set
            {
                if (value != this.aditionalObservations)
                {
                    this.aditionalObservations = value;
                    this.ReportPropertyChanged("AditionalObservations");
                }
            }
        }

        public AtpcBindableCommand VerHistoricoCommand
        {
            get
            {
                return this.verHistoricoCommand ?? (this.verHistoricoCommand = new AtpcBindableCommand(this.OnVerHistorico, obj => true));
            }
        }

        public string this[string columnName]
        {
            get
            {
                string validation = null;
                if (this.Requerido)
                {
                    switch (columnName)
                    {
                        case "Dato":
                            if (this.RotuloTipo.ToUpper() == "DATO")
                            {
                                if (string.IsNullOrWhiteSpace(this.Dato))
                                {
                                    validation = "Este valor es Requerido";
                                }
                            }

                            break;

                        case "DatoLargo":
                            if (this.RotuloTipo.ToUpper() == "DATOLARGO" || this.RotuloTipo.ToUpper() == "DATO LARGO")
                            {
                                if (string.IsNullOrWhiteSpace(this.DatoLargo))
                                {
                                    validation = "Este valor es Requerido";
                                }
                            }

                            break;
                    }
                }

                return validation;
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

        private void OnSelectedToShowChanged()
        {
            if (this.SelectedToShow)
            {
                // ToDo No debe ser asi.
                this.RotuloTipo = "DATO";
                this.FieldType = this.FieldType | DataCaptureType.Data | DataCaptureType.Optional;
            }
            else
            {
                this.FieldType = ~(~this.FieldType | DataCaptureType.Data);
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
                    if (!string.IsNullOrEmpty(this.Dato) && !string.IsNullOrWhiteSpace(this.ValorNormal.VrNormalCualitativo))
                    {
                        if (this.ValorNormal.VrNormalCualitativo.ToUpperInvariant() != this.Dato.ToUpperInvariant())
                        {
                            return AnalyteAbnormalFlags.Abnormal;
                        }

                        return AnalyteAbnormalFlags.Normal;
                    }

                    return AnalyteAbnormalFlags.None;
                }

                decimal datoDecimal;
                if (decimal.TryParse(this.Dato, NumberStyles.Number, CultureInfo.InvariantCulture, out datoDecimal))
                {
                    Debug.WriteLine(this.ValorNormal.Tipo + ":" + datoDecimal);
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

                    return AnalyteAbnormalFlags.None;
                }

                return AnalyteAbnormalFlags.Invalid;
            }

            return AnalyteAbnormalFlags.None;
        }

        private void ReportToUser()
        {
            if (CapturaGlobal.ShowAlertsWindow)
            {
                if (this.Loading || this.Alerts.Count <= 0)
                {
                    return;
                }

                if (this.flag != AnalyteAbnormalFlags.None && this.flag != AnalyteAbnormalFlags.Normal)
                {
                    this.OnVerObservacionesAlerta();
                }
            }
        }

        partial void OnRotuloTipoChanged()
        {
            if (string.IsNullOrEmpty(this.RotuloTipo))
            {
                this.FieldType = DataCaptureType.Null;
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }
            switch (this.RotuloTipo.ToUpperInvariant())
            {
                case "FORMULA":
                    this.FieldType = DataCaptureType.Formula;
                    break;

                case "DATO":
                    this.FieldType = DataCaptureType.Data;
                    break;

                case "SUBTITULO":
                    this.FieldType = DataCaptureType.Subtitle;
                    break;

                case "OPCIONAL":
                    this.FieldType = DataCaptureType.Optional;
                    if (Debugger.IsAttached)
                    {
                        Debug.WriteLine("Analito: " + this.Analito + "Es opcional");
                    }

                    break;


                case "DATO LARGO":
                case "DATOLARGO":
                case "LARGODATO":
                    this.FieldType = DataCaptureType.LongData;
                    break;



                default:
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    break;
            }
        }
    }
}
