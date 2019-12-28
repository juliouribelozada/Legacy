// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PRO_CargaCapturaSeccion_Result.cs" company="ATPC.co">
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
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class PRO_CargaCapturaSeccion_Result
    {
        private readonly ObservableCollection<SugerenciasAutoTexto> suggestionsObservaciones = new ObservableCollection<SugerenciasAutoTexto>();

        private readonly ObservableCollection<ComplexObject> analitosOpcionales = new ObservableCollection<ComplexObject>();

        private readonly ObservableCollection<ComplexObject> panels = new ObservableCollection<ComplexObject>();

        private AtpcBindableCommand validarCommand;

        private AtpcBindableCommand optionsCommand;
        private AtpcBindableCommand tecnicianObsCommand; 

        private bool isReady;

        private bool isBusy;

        private bool hasFormula;

        private ObservableCollection<ComplexObject> analitos;

        private Panel selectedPanel;

        public PRO_CargaCapturaSeccion_Result()
        {
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
                                this.SuggestionsObservaciones.Add(sugerenciasAutoTexto);
                            }
                        }
                    }
                });
        }

        public event EventHandler ObservacionChanged;

        public event EventHandler Validar;

        public event EventHandler ShowOptionsWindow;

        public event EventHandler ShowTechnicianObsWindow;
        
        public ICollectionView AnalitosView { get; set; }

        public ObservableCollection<ComplexObject> AnalitosOpcionales
        {
            get
            {
                return this.analitosOpcionales;
            }
        }

        public ObservableCollection<ComplexObject> Analitos
        {
            get
            {
                return this.analitos;
            }

            set
            {
                if (value != this.analitos)
                {
                    this.analitos = value;
                    this.ReportPropertyChanged("Analitos");
                    this.UpdateView();
                    this.ReportPropertyChanged("AnalitosView");
                    this.ReportPropertyChanged("AnalitosOpcionalesView");
                }
            }
        }

        public ObservableCollection<ComplexObject> Panels
        {
            get
            {
                return this.panels;
            }
        }

        

        public ObservableCollection<SugerenciasAutoTexto> SuggestionsObservaciones
        {
            get
            {
                return this.suggestionsObservaciones;
            }
        }

        public bool HasFormula
        {
            get
            {
                return this.hasFormula;
            }

            set
            {
                if (value != this.hasFormula)
                {
                    this.hasFormula = value;
                    this.ReportPropertyChanged("HasFormula");
                }
            }
        }

        public bool IsReady
        {
            get
            {
                return this.isReady;
            }

            set
            {
                if (value != this.isReady)
                {
                    this.isReady = value;
                    this.ReportPropertyChanged("IsReady");
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                if (value != this.isBusy)
                {
                    this.isBusy = value;
                    this.ReportPropertyChanged("IsBusy");
                }
            }
        }

        public bool MostrarOpciones
        {
            get
            {
                return this.Opcional.GetValueOrDefault(false);
            }
        }

        public AtpcBindableCommand ValidarCommand
        {
            get
            {
                return this.validarCommand ?? (this.validarCommand = new AtpcBindableCommand(this.OnValidar, this.PuedeValidar));
            }
        }

        public AtpcBindableCommand OptionsCommand
        {
            get
            {
                return this.optionsCommand ?? (this.optionsCommand = new AtpcBindableCommand(this.ShowOptionsWindowAction));
            }
        }

        public AtpcBindableCommand TecnicianObsCommand => this.tecnicianObsCommand ?? (this.tecnicianObsCommand = new AtpcBindableCommand(this.OnShowTechnicianObsWindow));

        public string IdMuestra { get; set; }

        public void OnValidar(object o)
        {
            EventHandler handler = this.Validar;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void UpdateFormula()
        {
            if (this.HasFormula)
            {
                StoreProceduresOps.CargaFormulaAsync(
                    new CapturaManualFormulaParameters { CodigoFuente = this.CodigoFuente, IdMuestra = this.IdMuestra },
                    (sender, args) =>
                    {
                        if (args.Cancelled)
                        {

                        }
                        else if (args.Error != null)
                        {

                        }
                        else
                        {
                            var resultados = args.Result as List<PRO_CargaCapturaFormula_Result>;
                            if (resultados != null)
                            {
                                foreach (var proCargaCapturaFormulaResult in resultados)
                                {
                                    foreach (var complexObject in this.Analitos)
                                    {
                                        var analito = complexObject as PRO_CargaCapturaCuatroCampos_Result;
                                        if (analito != null)
                                        {
                                            if (proCargaCapturaFormulaResult.Analito == analito.Analito)
                                            {
                                                analito.Dato = proCargaCapturaFormulaResult.Dato;
                                            }
                                        }

                                        var analito2 = complexObject as DatosEnColumnas;
                                        if (analito2 != null)
                                        {
                                            if (proCargaCapturaFormulaResult.Analito == analito2.Columna0.Analito)
                                            {
                                                analito2.Columna0.Dato = proCargaCapturaFormulaResult.Dato;
                                            }

                                            if (proCargaCapturaFormulaResult.Analito == analito2.Columna1.Analito)
                                            {
                                                analito2.Columna1.Dato = proCargaCapturaFormulaResult.Dato;
                                            }

                                            if (proCargaCapturaFormulaResult.Analito == analito2.Columna1.Analito)
                                            {
                                                analito2.Columna1.Dato = proCargaCapturaFormulaResult.Dato;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
            }
        }

        private void ShowOptionsWindowAction(object obj)
        {
            this.OnShowOptionsWindow();
        }
        private void OnShowTechnicianObsWindow(object obj)
        {
            this.OnShowTechnicianObsWindow();
        }

        private bool PuedeValidar(object obj)
        {
            return CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Valida);
        }

        partial void OnObservacionChanged()
        {
            EventHandler handler = this.ObservacionChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        partial void OnOpcionalChanged()
        {
            this.ReportPropertyChanged("Mostrar");
            this.LoadPanels();
        }

        private void LoadPanels()
        {
            this.Panels.Clear();
            if (this.Opcional.GetValueOrDefault(false))
            {
                StoreProceduresOps.LoadPanelsAsync(
                    new CapturaManualAnalitoParameters { CodigoFuente = this.CodigoFuente },
                    (s, e) =>
                    {
                        if (e.Cancelled)
                        {

                        }
                        else if (e.Error != null)
                        {
                            Trace.TraceError(e.Error.Message);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(
                                (Action)(() =>
                                    {
                                        var panelsRaw = e.Result as IList<PRO_CargaCapturaPanelOpcional_Result>;

                                        if (panelsRaw != null)
                                        {
                                            foreach (var panelRaw in panelsRaw)
                                            {
                                                this.Panels.Add(panelRaw);
                                            }
                                        }
                                    }));
                        }

                    });
            }
        }

        private void UpdateView()
        {
            this.AnalitosOpcionales.Clear();

            if (this.Analitos == null)
            {
                return;
            }

            foreach (var proCargaCapturaCuatroCamposResult in this.Analitos.OfType<PRO_CargaCapturaCuatroCampos_Result>().Where(proCargaCapturaCuatroCamposResult => (proCargaCapturaCuatroCamposResult.FieldType & DataCaptureType.Optional) == DataCaptureType.Optional))
            {
                this.AnalitosOpcionales.Add(proCargaCapturaCuatroCamposResult);
            }

            this.AnalitosView = CollectionViewSource.GetDefaultView(this.Analitos);

            this.AnalitosView.Filter = o =>
            {
                var proCargaCapturaCuatroCamposResult = o as PRO_CargaCapturaCuatroCampos_Result;

                if (proCargaCapturaCuatroCamposResult != null)
                {
                    if (proCargaCapturaCuatroCamposResult.FieldType == DataCaptureType.Optional)
                    {
                        return false;
                    }

                    return true;
                }

                return true;
            };
        }

        protected virtual void OnShowOptionsWindow()
        {
            this.ShowOptionsWindow?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnShowTechnicianObsWindow()
        {
            this.ShowTechnicianObsWindow?.Invoke(this, EventArgs.Empty);
        }
    }
}
