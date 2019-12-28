// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Objects.DataClasses;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Neuron.Satelite.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;
    using Neuron.Satelite.CapturaManual.Data.ViewModels;
    using Neuron.Satellite.CapturaManual.Controls;
    using Neuron.Satellite.CapturaManual.Controls.Windows;

    using ValidationResult = Neuron.Satelite.CapturaManual.Data.Model.ValidationResult;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IdentificadorMuestra identificadorMuestraViewModel = new IdentificadorMuestra();
        private readonly InfoPersonaViewModel infoPersonaViewModel = new InfoPersonaViewModel();
        private readonly ExamenesViewModel examenesViewModel = new ExamenesViewModel();
        private readonly ProfesionalViewModel profesionalViewModel = new ProfesionalViewModel();
        private readonly BrandingViewModel brandingViewModel = new BrandingViewModel();
        private readonly List<EstudioValorNormal> ValoresNormales = new List<EstudioValorNormal>();
        private string pass = string.Empty;
        private string user = string.Empty;

        private PasswordBox controlPass;
        private TextBox controlUser;

        public MainWindow()
        {
            this.InitializeComponent();
            this.IdentificadorMuestraGrid.DataContext = this.identificadorMuestraViewModel;
            this.InfoPersonaDataGrid.DataContext = this.infoPersonaViewModel;
            this.ServicioTextBox.DataContext = this.infoPersonaViewModel;
            this.ObservacionMuestraPanel.DataContext = this.infoPersonaViewModel;
            this.ExamenesContainer.DataContext = this.examenesViewModel;
            this.InfoProfesionalGrid.DataContext = this.profesionalViewModel;
            this.BrandingPanel.DataContext = this.brandingViewModel;
            this.Loaded += this.MainWindowLoaded;
            this.identificadorMuestraViewModel.IdChanged += (sender, args) => this.BuscarInformacion();
            GeneralDispatcher.Dispatcher = this.Dispatcher;
            this.ValidaTodoButton.DataContext = CapturaManualFunctionsViewModel.Instance;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.GetSampleTakingCenters();
            this.AutorizacionBusyIndicator.IsBusy = true;
        }

        private void GetSampleTakingCenters()
        {
            // this.DebugLog("Start: GetSampleTakingCenters");
            SampleTakingCenterOps.GetAsync((s, e) =>
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Cancelado");
                }
                else if (e.Error != null)
                {
                    MessageBox.Show("Error:\n" + e.Error.Message);
                }
                else
                {
                    var centroTomaMuestras = e.Result as List<SampleTakingCenter>;

                    if (centroTomaMuestras != null)
                    {
                        this.identificadorMuestraViewModel.CentrosDeTomaList.Clear();
                        foreach (var centroTomaMuestra in centroTomaMuestras)
                        {
                            this.identificadorMuestraViewModel.CentrosDeTomaList.Add(centroTomaMuestra.Prefix);
                        }

                        if (this.identificadorMuestraViewModel.CentrosDeTomaList.Count > 0)
                        {
                            this.centrosDeTomaComboBox.SelectedIndex = 0;
                        }

                        // this.getWorkListControl.CentrosDeToma = this.centrosDeTomaList;
                    }
                }

                // this.DebugLog("End: GetSampleTakingCenters");
            });

            ValorNormalAsynOps.GetAsync((s, e) =>
                {
                    if (e.Cancelled)
                    {

                    }
                    else if (e.Error != null)
                    {
                    }
                    else
                    {
                        var estudioValorNormals = e.Result as List<EstudioValorNormal>;

                        if (estudioValorNormals != null)
                        {
                            foreach (var estudioValorNormal in estudioValorNormals)
                            {
                                this.ValoresNormales.Add(estudioValorNormal);
                            }
                        }
                    }
                });
        }

        private void NumeroTomaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                this.ActualizarNumeroMuestra();
            }
        }

        private void BuscarInformacion()
        {
            if (!string.IsNullOrWhiteSpace(this.identificadorMuestraViewModel.Identificador))
            {
                StoreProceduresOps.PRO_CargaDatosPersoMuestraAsync(
                    this.identificadorMuestraViewModel.Identificador,
                    (s, e) =>
                    {
                        if (e.Cancelled)
                        {
                            MessageBox.Show("Cancelado");
                        }
                        else if (e.Error != null)
                        {
                            MessageBox.Show("Error:\n" + e.Error.Message);
                        }
                        else
                        {
                            var cargaDatosPersoMuestraResult = e.Result as PRO_CargaDatosPersoMuestra_Result;

                            this.infoPersonaViewModel.Result = cargaDatosPersoMuestraResult
                                                               ?? new PRO_CargaDatosPersoMuestra_Result();
                        }
                    });

                this.examenesViewModel.Secciones.Clear();
                StoreProceduresOps.PRO_CargaCapturaSeccionAsync(
                    this.identificadorMuestraViewModel.Identificador,
                    (s, e) =>
                    {
                        if (e.Cancelled)
                        {
                            MessageBox.Show("Cancelado");
                        }
                        else if (e.Error != null)
                        {
                            MessageBox.Show("Error:\n" + e.Error.Message);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                var proCargaCapturaSeccionResults = e.Result as List<PRO_CargaCapturaSeccion_Result>;

                                if (proCargaCapturaSeccionResults != null)
                                {
                                    IEnumerable<Seccion> secciones = from resultados in proCargaCapturaSeccionResults
                                                                     group resultados by resultados.NomSeccion
                                                                         into grupo
                                                                     select new Seccion { Name = grupo.Key, Examenes = grupo.ToList() };

                                    foreach (var seccion in secciones)
                                    {
                                        this.examenesViewModel.Secciones.Add(seccion);
                                        foreach (var proCargaCapturaSeccionResult in seccion.Examenes)
                                        {
                                            PRO_CargaCapturaSeccion_Result result = proCargaCapturaSeccionResult;
                                            result.IdMuestra = this.identificadorMuestraViewModel.Identificador;

                                            if (result.Formato == TipoCaptura.CampoLargo)
                                            {
                                                StoreProceduresOps.PRO_CargaCapturaCampoLargoAsync(
                                                    new CapturaManualAnalitoParameters { CodigoFuente = result.CodigoFuente, IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador },
                                                    (ss, ee) =>
                                                    {
                                                        if (ee.Cancelled)
                                                        {
                                                            // MessageBox.Show("Cancelado");
                                                        }
                                                        else if (ee.Error != null)
                                                        {
                                                            // MessageBox.Show("Error:\n" + ee.Error.Message);
                                                        }
                                                        else
                                                        {
                                                            var largoResult = ee.Result as List<PRO_CargaCapturaCampoLargo_Result>;

                                                            if (largoResult != null)
                                                            {
                                                                result.Analitos = new ObservableCollection<ComplexObject>(largoResult);
                                                                foreach (var proCargaCapturaCampoLargoResult in result.Analitos)
                                                                {
                                                                    PRO_CargaCapturaCampoLargo_Result campoLargoResult = proCargaCapturaCampoLargoResult as PRO_CargaCapturaCampoLargo_Result;

                                                                    campoLargoResult.VerHistorico +=
                                                                        (oooo, eeee) =>
                                                                        {
                                                                            var tempWindows = new HistoricosW();
                                                                            tempWindows.ViewModel.Parametros =
                                                                                new CapturaManualHistoricoParameters
                                                                                {
                                                                                    Analito = campoLargoResult.Analito,
                                                                                    CodigoFuente = campoLargoResult.CodigoFuente,
                                                                                    IdMuestra = campoLargoResult.IdentificadorMuestra
                                                                                };
                                                                            tempWindows.Owner = this;
                                                                            tempWindows.ShowDialog();
                                                                        };

                                                                    campoLargoResult.DatoLargoChanged += (ooo, eee) =>
                                                                        {
                                                                            result.IsReady = false;
                                                                            StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                    new CapturaManualAnalitoParameters
                                                                                    {
                                                                                        CodigoFuente = campoLargoResult.CodigoFuente,
                                                                                        Analito = campoLargoResult.Analito,
                                                                                        IdentificadorMuestra = campoLargoResult.IdentificadorMuestra,
                                                                                        UsuarioSistema = "Neuron",
                                                                                        Formato = result.Formato,
                                                                                        DatoLargo = campoLargoResult.DatoLargo,
                                                                                        CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                    },
                                                                                    (d, f) =>
                                                                                    {
                                                                                        if (f.Error != null)
                                                                                        {
                                                                                            MessageBox.Show(f.Error.Message);
                                                                                        }

                                                                                        result.IsReady = true;
                                                                                    });
                                                                        };
                                                                }
                                                            }
                                                        }
                                                    });
                                            }
                                            else if (result.Formato == TipoCaptura.CuatroCampos)
                                            {
                                                StoreProceduresOps.PRO_CargaCapturaCuatroCamposAsync(
                                                    new CapturaManualAnalitoParameters { CodigoFuente = result.CodigoFuente, IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador },
                                                    (ss, ee) =>
                                                    {
                                                        if (ee.Cancelled)
                                                        {
                                                            // MessageBox.Show("Cancelado");
                                                        }
                                                        else if (ee.Error != null)
                                                        {
                                                            //MessageBox.Show("Error:\n" + e.Error.Message);
                                                        }
                                                        else
                                                        {
                                                            var largoResult = ee.Result as List<PRO_CargaCapturaCuatroCampos_Result>;

                                                            if (largoResult != null)
                                                            {
                                                                result.Analitos = new ObservableCollection<ComplexObject>(largoResult);
                                                                foreach (var proCargaCapturaCampoLargoResult in result.Analitos)
                                                                {
                                                                    PRO_CargaCapturaCuatroCampos_Result cuatroCamposResult = proCargaCapturaCampoLargoResult as PRO_CargaCapturaCuatroCampos_Result;
                                                                    if (cuatroCamposResult != null)
                                                                    {
                                                                        if (cuatroCamposResult.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                        {
                                                                            result.HasFormula = true;
                                                                        }
                                                                        cuatroCamposResult.VerHistorico += (oooo, eeee) =>
                                                                        {
                                                                            var tempWindows = new HistoricosW();
                                                                            tempWindows.ViewModel.Parametros =
                                                                                new CapturaManualHistoricoParameters
                                                                                {
                                                                                    Analito = cuatroCamposResult.Analito,
                                                                                    CodigoFuente = cuatroCamposResult.CodigoFuente,
                                                                                    IdMuestra = cuatroCamposResult.IdentificadorMuestra
                                                                                };
                                                                            tempWindows.Owner = this;
                                                                            tempWindows.ShowDialog();
                                                                        };

                                                                        cuatroCamposResult.DatoLargoChanged += (ooo, eee) =>
                                                                        {
                                                                            var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                            if (fieldToUpdate != null)
                                                                            {
                                                                                if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                {
                                                                                    return;
                                                                                }
                                                                            }

                                                                            result.IsReady = false;
                                                                            StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                new CapturaManualAnalitoParameters
                                                                                {
                                                                                    CodigoFuente = cuatroCamposResult.CodigoFuente,
                                                                                    Analito = cuatroCamposResult.Analito,
                                                                                    IdentificadorMuestra = cuatroCamposResult.IdentificadorMuestra,
                                                                                    UsuarioSistema = "Neuron",
                                                                                    Formato = result.Formato,
                                                                                    DatoLargo = cuatroCamposResult.DatoLargo,
                                                                                    Dato = cuatroCamposResult.Dato,
                                                                                    CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                },
                                                                                (d, f) =>
                                                                                {
                                                                                    if (f.Error != null)
                                                                                    {
                                                                                        MessageBox.Show(f.Error.Message);
                                                                                    }

                                                                                    result.IsReady = true;
                                                                                    result.UpdateFormula();
                                                                                });
                                                                        };

                                                                        cuatroCamposResult.DatoChanged += (ooo, eee) =>
                                                                        {
                                                                            var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                            if (fieldToUpdate != null)
                                                                            {
                                                                                if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                {
                                                                                    return;
                                                                                }
                                                                            }

                                                                            result.IsReady = false;
                                                                            StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                new CapturaManualAnalitoParameters
                                                                                {
                                                                                    CodigoFuente = cuatroCamposResult.CodigoFuente,
                                                                                    Analito = cuatroCamposResult.Analito,
                                                                                    IdentificadorMuestra = cuatroCamposResult.IdentificadorMuestra,
                                                                                    UsuarioSistema = "Neuron",
                                                                                    Formato = result.Formato,
                                                                                    DatoLargo = cuatroCamposResult.DatoLargo,
                                                                                    Dato = cuatroCamposResult.Dato,
                                                                                    CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                },
                                                                                (d, f) =>
                                                                                {
                                                                                    if (f.Error != null)
                                                                                    {
                                                                                        MessageBox.Show(f.Error.Message);
                                                                                    }

                                                                                    result.IsReady = true;
                                                                                    result.UpdateFormula();
                                                                                });
                                                                        };

                                                                        ////ValorNormalAsynOps.GetAsync(
                                                                        ////    new ConsultaValorNormalParameters { Analito = cuatroCamposResult.Analito, CodigoFuente = cuatroCamposResult.CodigoFuente, Genero = this.infoPersonaViewModel.Result.Genero },
                                                                        ////    (df, fg) =>
                                                                        ////    {
                                                                        ////        if (fg.Cancelled)
                                                                        ////        {
                                                                        ////        }
                                                                        ////        else if (fg.Error != null)
                                                                        ////        {
                                                                        ////        }
                                                                        ////        else
                                                                        ////        {
                                                                        ////            var estudioValorNormal = fg.Result as EstudioValorNormal;

                                                                        ////            if (estudioValorNormal != null)
                                                                        ////            {
                                                                        ////                cuatroCamposResult.ValorNormal = estudioValorNormal;
                                                                        ////            }
                                                                        ////        }
                                                                        ////    });

                                                                        cuatroCamposResult.ValorNormal = this.ValoresNormales.FirstOrDefault(
                                                                            valorNormal =>
                                                                                valorNormal.TomaEvento == cuatroCamposResult.TomaEvento
                                                                                && valorNormal.Analito == cuatroCamposResult.Analito
                                                                                && valorNormal.CodigoFuente == cuatroCamposResult.CodigoFuente
                                                                                && (!this.infoPersonaViewModel.Result.EdadDias.HasValue || (valorNormal.EdadDesde < this.infoPersonaViewModel.Result.EdadDias.Value && this.infoPersonaViewModel.Result.EdadDias.Value < valorNormal.EdadHasta))
                                                                                && (valorNormal.Genero == "AMBOS" ? valorNormal.Genero == "AMBOS" : (string.Compare(valorNormal.Genero, this.infoPersonaViewModel.Result.Genero, true) == 0)));

                                                                        cuatroCamposResult.VerObservacionesAlerta += (ooo, eee) =>
                                                                            {
                                                                                var tempWindows = new AlertasW();
                                                                                tempWindows.ViewModel.ListaAlertas = cuatroCamposResult.Alerts;
                                                                                tempWindows.ViewModel.Examen = proCargaCapturaSeccionResult;
                                                                                tempWindows.Owner = this;
                                                                                tempWindows.ShowDialog();
                                                                            };

                                                                        cuatroCamposResult.Loading = false;
#if DEBUG
                                                                        if (cuatroCamposResult.ValorNormal != null)
                                                                        {
                                                                            Debug.Print(cuatroCamposResult.ValorNormal.VrNormalCualitativo);
                                                                        }

                                                                        Trace.WriteLineIf((cuatroCamposResult.ValorNormal != null), cuatroCamposResult.Analito + ": Tiene Valor Normal");
#endif
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    });
                                            }
                                            else if (result.Formato == TipoCaptura.CuatroCamposDosBloques)
                                            {
                                                StoreProceduresOps.PRO_CargaCapturaCuatroCamposDosBloquesAsync(
                                                    new CapturaManualAnalitoParameters { CodigoFuente = result.CodigoFuente, IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador },
                                                    (ss, ee) =>
                                                    {
                                                        if (ee.Cancelled)
                                                        {
                                                            // MessageBox.Show("Cancelado");
                                                        }
                                                        else if (ee.Error != null)
                                                        {
                                                            //MessageBox.Show("Error:\n" + e.Error.Message);
                                                        }
                                                        else
                                                        {
                                                            var largoResult = ee.Result as List<DatosEnColumnas>;

                                                            if (largoResult != null)
                                                            {
                                                                result.Analitos = new ObservableCollection<ComplexObject>(largoResult);
                                                                foreach (var proCargaCapturaCampoLargoResult in result.Analitos)
                                                                {
                                                                    DatosEnColumnas cuatroCamposDosBloquesResult = proCargaCapturaCampoLargoResult as DatosEnColumnas;
                                                                    if (cuatroCamposDosBloquesResult != null)
                                                                    {
                                                                        if (cuatroCamposDosBloquesResult.Columna0 != null)
                                                                        {
                                                                            if (cuatroCamposDosBloquesResult.Columna0.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                            {
                                                                                result.HasFormula = true;
                                                                            }

                                                                            cuatroCamposDosBloquesResult.Columna0.VerHistorico += (oooo, eeee) =>
                                                                            {
                                                                                var tempWindows = new HistoricosW();
                                                                                tempWindows.ViewModel.Parametros =
                                                                                    new CapturaManualHistoricoParameters
                                                                                    {
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna0.Analito,
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna0.CodigoFuente,
                                                                                        IdMuestra = cuatroCamposDosBloquesResult.Columna0.IdentificadorMuestra
                                                                                    };
                                                                                tempWindows.Owner = this;
                                                                                tempWindows.ShowDialog();
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna0.DatoLargoChanged += (ooo, eee) =>
                                                                            {
                                                                                var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                                if (fieldToUpdate != null)
                                                                                {
                                                                                    if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                    {
                                                                                        return;
                                                                                    }
                                                                                }

                                                                                result.IsReady = false;
                                                                                StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                    new CapturaManualAnalitoParameters
                                                                                    {
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna0.CodigoFuente,
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna0.Analito,
                                                                                        IdentificadorMuestra = cuatroCamposDosBloquesResult.Columna0.IdentificadorMuestra,
                                                                                        UsuarioSistema = "Neuron",
                                                                                        Formato = result.Formato,
                                                                                        DatoLargo = cuatroCamposDosBloquesResult.Columna0.DatoLargo,
                                                                                        Dato = cuatroCamposDosBloquesResult.Columna0.Dato,
                                                                                        CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                    },
                                                                                    (d, f) =>
                                                                                    {
                                                                                        if (f.Error != null)
                                                                                        {
                                                                                            MessageBox.Show(f.Error.Message);
                                                                                        }

                                                                                        result.IsReady = true;
                                                                                        result.UpdateFormula();
                                                                                    });
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna0.DatoChanged += (ooo, eee) =>
                                                                            {
                                                                                var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                                if (fieldToUpdate != null)
                                                                                {
                                                                                    if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                    {
                                                                                        return;
                                                                                    }
                                                                                }

                                                                                result.IsReady = false;
                                                                                StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                    new CapturaManualAnalitoParameters
                                                                                    {
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna0.CodigoFuente,
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna0.Analito,
                                                                                        IdentificadorMuestra = cuatroCamposDosBloquesResult.Columna0.IdentificadorMuestra,
                                                                                        UsuarioSistema = "Neuron",
                                                                                        Formato = result.Formato,
                                                                                        DatoLargo = cuatroCamposDosBloquesResult.Columna0.DatoLargo,
                                                                                        Dato = cuatroCamposDosBloquesResult.Columna0.Dato,
                                                                                        CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                    },
                                                                                    (d, f) =>
                                                                                    {
                                                                                        if (f.Error != null)
                                                                                        {
                                                                                            MessageBox.Show(f.Error.Message);
                                                                                        }

                                                                                        result.IsReady = true;
                                                                                        result.UpdateFormula();
                                                                                    });
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna0.ValorNormal = this.ValoresNormales.FirstOrDefault(
                                                                                valorNormal =>
                                                                                    valorNormal.TomaEvento == cuatroCamposDosBloquesResult.Columna0.TomaEvento
                                                                                    && valorNormal.Analito == cuatroCamposDosBloquesResult.Columna0.Analito
                                                                                    && valorNormal.CodigoFuente == cuatroCamposDosBloquesResult.Columna0.CodigoFuente
                                                                                    && (!this.infoPersonaViewModel.Result.EdadDias.HasValue || (valorNormal.EdadDesde < this.infoPersonaViewModel.Result.EdadDias.Value && this.infoPersonaViewModel.Result.EdadDias.Value < valorNormal.EdadHasta))
                                                                                    && (valorNormal.Genero == "AMBOS" ? valorNormal.Genero == "AMBOS" : (string.Compare(valorNormal.Genero, this.infoPersonaViewModel.Result.Genero, true) == 0)));

                                                                            cuatroCamposDosBloquesResult.Columna0.VerObservacionesAlerta += (ooo, eee) =>
                                                                            {
                                                                                var tempWindows = new AlertasW();
                                                                                tempWindows.ViewModel.ListaAlertas = cuatroCamposDosBloquesResult.Columna0.Alerts;
                                                                                tempWindows.ViewModel.Examen = proCargaCapturaSeccionResult;

                                                                                tempWindows.Owner = this;
                                                                                tempWindows.ShowDialog();
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna0.Loading = false;
#if DEBUG
                                                                            Trace.WriteLineIf((cuatroCamposDosBloquesResult.Columna0.ValorNormal != null), cuatroCamposDosBloquesResult.Columna0.Analito + ": Tiene Valor Normal");
#endif
                                                                        }

                                                                        if (cuatroCamposDosBloquesResult.Columna1 != null)
                                                                        {
                                                                            if (cuatroCamposDosBloquesResult.Columna1.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                            {
                                                                                result.HasFormula = true;
                                                                            }
                                                                            cuatroCamposDosBloquesResult.Columna1.VerHistorico += (oooo, eeee) =>
                                                                            {
                                                                                var tempWindows = new HistoricosW();
                                                                                tempWindows.ViewModel.Parametros =
                                                                                    new CapturaManualHistoricoParameters
                                                                                    {
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna1.Analito,
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna1.CodigoFuente,
                                                                                        IdMuestra = cuatroCamposDosBloquesResult.Columna1.IdentificadorMuestra
                                                                                    };
                                                                                tempWindows.Owner = this;
                                                                                tempWindows.ShowDialog();
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna1.DatoLargoChanged += (ooo, eee) =>
                                                                            {
                                                                                var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                                if (fieldToUpdate != null)
                                                                                {
                                                                                    if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                    {
                                                                                        return;
                                                                                    }
                                                                                }

                                                                                result.IsReady = false;
                                                                                StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                    new CapturaManualAnalitoParameters
                                                                                    {
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna1.CodigoFuente,
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna1.Analito,
                                                                                        IdentificadorMuestra = cuatroCamposDosBloquesResult.Columna1.IdentificadorMuestra,
                                                                                        UsuarioSistema = "Neuron",
                                                                                        Formato = result.Formato,
                                                                                        DatoLargo = cuatroCamposDosBloquesResult.Columna1.DatoLargo,
                                                                                        Dato = cuatroCamposDosBloquesResult.Columna1.Dato,
                                                                                        CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                    },
                                                                                    (d, f) =>
                                                                                    {
                                                                                        if (f.Error != null)
                                                                                        {
                                                                                            MessageBox.Show(f.Error.Message);
                                                                                        }

                                                                                        result.IsReady = true;
                                                                                        result.UpdateFormula();
                                                                                    });
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna1.DatoChanged += (ooo, eee) =>
                                                                            {
                                                                                var fieldToUpdate = ooo as PRO_CargaCapturaCuatroCampos_Result;
                                                                                if (fieldToUpdate != null)
                                                                                {
                                                                                    if (fieldToUpdate.RotuloTipo.ToUpperInvariant() == "FORMULA")
                                                                                    {
                                                                                        return;
                                                                                    }
                                                                                }

                                                                                result.IsReady = false;
                                                                                StoreProceduresOps.PRO_CapturaManualAnalitolAsync(
                                                                                    new CapturaManualAnalitoParameters
                                                                                    {
                                                                                        CodigoFuente = cuatroCamposDosBloquesResult.Columna1.CodigoFuente,
                                                                                        Analito = cuatroCamposDosBloquesResult.Columna1.Analito,
                                                                                        IdentificadorMuestra = cuatroCamposDosBloquesResult.Columna1.IdentificadorMuestra,
                                                                                        UsuarioSistema = "Neuron",
                                                                                        Formato = result.Formato,
                                                                                        DatoLargo = cuatroCamposDosBloquesResult.Columna1.DatoLargo,
                                                                                        Dato = cuatroCamposDosBloquesResult.Columna1.Dato,
                                                                                        CodPersoAsisten = this.profesionalViewModel.Codigo
                                                                                    },
                                                                                    (d, f) =>
                                                                                    {
                                                                                        if (f.Error != null)
                                                                                        {
                                                                                            MessageBox.Show(f.Error.Message);
                                                                                        }

                                                                                        result.IsReady = true;
                                                                                        result.UpdateFormula();
                                                                                    });
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna1.ValorNormal = this.ValoresNormales.FirstOrDefault(
                                                                                valorNormal =>
                                                                                    valorNormal.TomaEvento == cuatroCamposDosBloquesResult.Columna1.TomaEvento
                                                                                    && valorNormal.Analito == cuatroCamposDosBloquesResult.Columna1.Analito
                                                                                    && valorNormal.CodigoFuente == cuatroCamposDosBloquesResult.Columna1.CodigoFuente
                                                                                    && (!this.infoPersonaViewModel.Result.EdadDias.HasValue || (valorNormal.EdadDesde < this.infoPersonaViewModel.Result.EdadDias.Value && this.infoPersonaViewModel.Result.EdadDias.Value < valorNormal.EdadHasta))
                                                                                    && (valorNormal.Genero == "AMBOS" ? valorNormal.Genero == "AMBOS" : (string.Compare(valorNormal.Genero, this.infoPersonaViewModel.Result.Genero, true) == 0)));

                                                                            cuatroCamposDosBloquesResult.Columna1.VerObservacionesAlerta += (ooo, eee) =>
                                                                            {
                                                                                var tempWindows = new AlertasW();
                                                                                tempWindows.ViewModel.ListaAlertas = cuatroCamposDosBloquesResult.Columna1.Alerts;
                                                                                tempWindows.ViewModel.Examen = proCargaCapturaSeccionResult;
                                                                                tempWindows.Owner = this;
                                                                                tempWindows.ShowDialog();
                                                                            };

                                                                            cuatroCamposDosBloquesResult.Columna1.Loading = false;
#if DEBUG
                                                                            Trace.WriteLineIf((cuatroCamposDosBloquesResult.Columna1.ValorNormal != null), cuatroCamposDosBloquesResult.Columna1.Analito + ": Tiene Valor Normal");
#endif

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    });
                                            }

                                            proCargaCapturaSeccionResult.ObservacionChanged += (o, ee) => StoreProceduresOps.PRO_CapturaManualAnalitolAsync(new CapturaManualAnalitoParameters { CodigoFuente = result.CodigoFuente, Observacion = result.Observacion, IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador, UsuarioSistema = "Neuron", CodPersoAsisten = this.profesionalViewModel.Codigo });
                                            proCargaCapturaSeccionResult.ShowOptionsWindow += this.OpenOptionalAnalitesWindow;
                                            proCargaCapturaSeccionResult.ShowTechnicianObsWindow += this.OpenTechnicianObsWindow;

                                            proCargaCapturaSeccionResult.Validar += (ooo, eee) =>
                                                {
                                                    result.IsBusy = true;
                                                    StoreProceduresOps.PRO_CapturaManualValidaAsync(
                                                        new CapturaManualValidacionParameters
                                                        {
                                                            CodigoFuente = result.CodigoFuente,
                                                            CodPersoAsisten = this.profesionalViewModel.Codigo,
                                                            IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador,
                                                            Tipo = "Parcial",
                                                            UsuarioSistema = "Neuron"
                                                        },
                                                        (h, m) =>
                                                        {
                                                            result.IsBusy = false;
                                                            if (m.Cancelled)
                                                            {
                                                                MessageBox.Show("Cancelado");
                                                            }
                                                            else if (m.Error != null)
                                                            {
                                                                MessageBox.Show("Error:\n" + m.Error.Message);
                                                            }
                                                            else
                                                            {
                                                                var validaResult = m.Result as PRO_CapturaManualValida_Result;

                                                                if (validaResult != null)
                                                                {
                                                                    string mensajeError = string.Empty;
                                                                    if (!string.IsNullOrWhiteSpace(validaResult.Error))
                                                                    {
                                                                        mensajeError = string.Format("Han Ocurrido errores: {0}", validaResult.Error);
                                                                    }

                                                                    MessageBox.Show(string.Format("Se han Validado {0} registro(s).\n{1}", validaResult.NoRegProc, mensajeError));
                                                                    this.BuscarInformacion();
                                                                }
                                                            }
                                                        });
                                                };
                                        }
                                    }

                                    if (this.SeccionesList.Items.Count > 0)
                                    {
                                        //Thread.Sleep(50);
                                        this.SeccionesList.SelectedIndex = 0;
                                    }
                                }
                            }));
                        }
                    });
            }
        }

        private void OpenOptionalAnalitesWindow(object sender, EventArgs e)
        {
            var seccionResult = sender as PRO_CargaCapturaSeccion_Result;

            if (seccionResult != null)
            {
                var optionalWindow = new OptionalAnalitesWindow(seccionResult.AnalitosOpcionales, seccionResult.Panels);
                optionalWindow.Closed += (o, args) =>
                {
                    seccionResult.AnalitosView.Refresh();
                };
                optionalWindow.ShowDialog();
            }

        }

        private void OpenTechnicianObsWindow(object sender, EventArgs e)
        {
            var seccionResult = sender as PRO_CargaCapturaSeccion_Result;

            if (seccionResult != null)
            {
                var optionalWindow = new TechnicianObsWindow(this.identificadorMuestraViewModel.Identificador, seccionResult.CodigoFuente, this.user);
                optionalWindow.Closed += (o, args) =>
                {
                    seccionResult.AnalitosView.Refresh();
                };
                optionalWindow.ShowDialog();
            }

        }

        private void ActualizarNumeroMuestra()
        {
            if (this.NumeroTomaTextBox.Text != this.identificadorMuestraViewModel.NumeroMuestra)
            {
                this.identificadorMuestraViewModel.NumeroMuestra = this.NumeroTomaTextBox.Text;
            }
        }

        private void NumeroTomaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ActualizarNumeroMuestra();
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                var box = sender as PasswordBox;

                if (box != null)
                {
                    this.pass = box.Password;
                    this.Autorizar();
                }
            }
        }

        private void User_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                var box = sender as TextBox;

                if (box != null)
                {
                    this.user = box.Text;
                    if (this.controlPass != null)
                    {
                        this.controlPass.Focus();
                    }
                }
            }
        }

        private void Autorizar()
        {
            if (string.IsNullOrWhiteSpace(this.pass))
            {
                MessageBox.Show("Ingrese su Contreseña");
            }
            else
            {
                this.AutorizacionBusyIndicator.ProgressBarVisibility = Visibility.Visible;
                StoreProceduresOps.PRO_AutorizaPersonalAsistencialAsync(
                    new ValidationParameters { UserName = this.user, Password = this.pass, SpecialityCode = "353", State = "ACTIVO" },
                    (s, e) =>
                    {
                        this.AutorizacionBusyIndicator.ProgressBarVisibility = Visibility.Hidden;
                        this.controlPass.Password = string.Empty;

                        if (e.Cancelled)
                        {
                            MessageBox.Show("Cancelado");
                        }
                        else if (e.Error != null)
                        {
                            MessageBox.Show("Error:\n" + e.Error.Message);
                        }
                        else
                        {
                            var validationResult = e.Result as ValidationResult;

                            if (validationResult != null && validationResult.Result)
                            {
                                StoreProceduresOps.GetClaimsAsync(validationResult.IdNumber,
                                    (sender, args) =>
                                        {
                                            if (args.Cancelled)
                                            {
                                                MessageBox.Show("Cancelado");
                                            }
                                            else if (args.Error != null)
                                            {
                                                MessageBox.Show("Error:\n" + args.Error.Message);
                                            }
                                            else
                                            {
                                                bool resultClaims;

                                                if (args.Result is bool)
                                                {
                                                    resultClaims = (bool)args.Result;
                                                }
                                                else
                                                {
                                                    resultClaims = false;
                                                }

                                                if (!resultClaims)
                                                {
                                                    MessageBox.Show("Error Obteniendo autorización");
                                                }
                                            }

                                            CapturaManualFunctionsViewModel.Instance.UpdateModel();
                                        });

                                this.AutorizacionBusyIndicator.IsBusy = false;
                                this.profesionalViewModel.Update(validationResult);
                            }
                            else
                            {
                                MessageBox.Show("Contraseña Erronea");
                                this.controlPass.Focus();
                            }
                        }
                    });
            }
        }

        private void Pass_OnLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Loaded");

            var interno = sender as TextBox;

            if (interno != null)
            {
                this.controlUser = sender as TextBox;
                this.controlUser.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => interno.Focus()));
            }
            else
            {
                this.controlPass = sender as PasswordBox;
            }
        }

        private void SeccionesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var seccion = this.SeccionesList.SelectedItem as Seccion;

            foreach (Seccion seccione in this.examenesViewModel.Secciones)
            {
                seccione.IsSelected = false;
            }

            if (seccion != null)
            {
                seccion.IsSelected = true;
            }
        }

        private void CerrarSesionButton_Click(object sender, RoutedEventArgs e)
        {
            CapturaManualClaims.ClearClaims();
            CapturaManualFunctionsViewModel.Instance.UpdateModel();

            this.AutorizacionBusyIndicator.IsBusy = true;
            this.profesionalViewModel.Clear();

            this.infoPersonaViewModel.Result = new PRO_CargaDatosPersoMuestra_Result();
            this.examenesViewModel.Secciones.Clear();
            this.identificadorMuestraViewModel.NumeroMuestra = this.NumeroTomaTextBox.Text = string.Empty;
        }

        private void BuscarPendienteButton_Click(object sender, RoutedEventArgs e)
        {
            var tempWindows = new ExamenesPendientes();
            tempWindows.ViewModel.IdMuestra = this.identificadorMuestraViewModel.Identificador;
            tempWindows.ViewModel.InfoPersona = this.infoPersonaViewModel;
            tempWindows.ViewModel.InfoProfesional = this.profesionalViewModel;
            tempWindows.Owner = this;
            tempWindows.ShowDialog();
            if (tempWindows.ViewModel.Reload)
            {
                this.BuscarInformacion();
            }
        }
        private void DoTrackingBySample(object sender, RoutedEventArgs e)
        {

            //this.identificadorMuestraViewModel.NumeroMuestra
            var tempWindows = new TrackingInfoWindow();
            tempWindows.ViewModel.SampleId = this.identificadorMuestraViewModel.Identificador;
            tempWindows.Owner = this;
            tempWindows.ShowDialog();

        }

        private void ValidaTodoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Valida))
            {
                return;
            }
            var button = sender as Button;

            if (button != null)
            {
                button.IsEnabled = false;
            }

            StoreProceduresOps.PRO_CapturaManualValidaAsync(
                new CapturaManualValidacionParameters
                {
                    CodPersoAsisten = this.profesionalViewModel.Codigo,
                    IdentificadorMuestra = this.identificadorMuestraViewModel.Identificador,
                    Tipo = "TOTAL",
                    UsuarioSistema = "Neuron"
                },
                (h, m) =>
                {
                    if (m.Cancelled)
                    {
                        MessageBox.Show("Cancelado");
                    }
                    else if (m.Error != null)
                    {
                        MessageBox.Show("Error:\n" + m.Error.Message);
                    }
                    else
                    {
                        var validaResult = m.Result as PRO_CapturaManualValida_Result;

                        if (validaResult != null)
                        {
                            string mensajeError = string.Empty;
                            if (!string.IsNullOrWhiteSpace(validaResult.Error))
                            {
                                mensajeError = string.Format("Han Ocurrido errores: {0}", validaResult.Error);
                            }

                            MessageBox.Show(string.Format("Se han Validado {0} registro(s).\n{1}", validaResult.NoRegProc, mensajeError));
                            this.BuscarInformacion();

                        }
                    }

                    if (button != null)
                    {
                        button.IsEnabled = true;
                    }
                });
        }

        private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;

            if (textBox != null)
            {
                var parametro = e.Parameter as string;

                if (!string.IsNullOrWhiteSpace(parametro))
                {
                    if (string.IsNullOrEmpty(textBox.SelectedText))
                    {
                        var newCarret = textBox.CaretIndex + parametro.Length;
                        textBox.Text = textBox.Text.Insert(textBox.CaretIndex, parametro);
                        textBox.CaretIndex = newCarret;
                    }
                    else
                    {
                        textBox.SelectedText = parametro;
                        textBox.CaretIndex += textBox.SelectedText.Length;
                        textBox.SelectionLength = 0;
                    }
                }
            }
        }

        private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CanExecuteDownNavCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CanExecuteUpNavCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExecutedUpNavCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;

            if (textBox != null)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Previous);
                textBox.MoveFocus(request);
            }
        }

        private void ExecutedDownNavCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;

            if (textBox != null)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                textBox.MoveFocus(request);
            }
        }

        private void User_Initialized(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Inizializado");
        }

        private void CloseMenuActionCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var contextMenu = e.Parameter as ContextMenu;
            if (contextMenu != null)
            {
                contextMenu.IsOpen = false;

                var textBox = e.OriginalSource as TextBox;

                if (textBox != null)
                {
                    var data = textBox.DataContext as PRO_CargaCapturaCuatroCampos_Result;

                    string textToInsert = string.Empty;

                    if (data != null)
                    {
                        textToInsert = data.SuggestionsMultiDatoLargo.Where(sugerenciasAutoTexto => sugerenciasAutoTexto.IsSelected).Aggregate(string.Empty, (current, sugerenciasAutoTexto) => current + " " + sugerenciasAutoTexto.Texto + ",");
                        textToInsert = textToInsert.TrimStart();

                        if (!string.IsNullOrWhiteSpace(data.AditionalObservations))
                        {
                            textToInsert = string.IsNullOrWhiteSpace(textToInsert)
                                               ? data.AditionalObservations
                                               : textToInsert + "\n" + data.AditionalObservations;
                        }
                    }



                    if (!string.IsNullOrWhiteSpace(textToInsert))
                    {
                        if (string.IsNullOrEmpty(textBox.SelectedText))
                        {
                            var newCarret = textBox.CaretIndex + textToInsert.Length;
                            textBox.Text = textBox.Text.Insert(textBox.CaretIndex, textToInsert);
                            textBox.CaretIndex = newCarret;
                        }
                        else
                        {
                            textBox.SelectedText = textToInsert;
                            textBox.CaretIndex += textBox.SelectedText.Length;
                            textBox.SelectionLength = 0;
                        }
                    }
                }
            }
        }

        private void CanCloseMenuCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void User_LostFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;

            if (box != null)
            {
                this.user = box.Text;
                this.controlPass?.Focus();
            }
        }
    }
}
