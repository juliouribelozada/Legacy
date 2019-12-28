// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC
{
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Data.SqlClient;

    using Microsoft.VisualBasic;
    using DPFP;
    using DPFP.Capture;
    using System.Windows.Forms;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    

    using Lysis.Commom.BarCode;

    using Neuron.HIS.Models.Common;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.WPF;
    using System.Drawing;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NeuronMainWindow, DPFP.Capture.EventHandler
    {
        private MainWindowControler controller;
        private DPFP.Capture.Capture Captura;
        private DPFP.Processing.Enrollment Enroller;
        private delegate void _delegatoMuestra(string text);
        private delegate void _delegatoControles();
        private DPFP.Template template;

        private bool moveNext;

        public MainWindow()
        {
            this.InitializeComponent();
            this.controller = new MainWindowControler(this, this.ViewModel);
            this.ViewModel.CargarPrefijosOSC<string>(OrigenDatos.DesdeCSV, true, Properties.Settings.Default.PrefijosOsc, valorPorDefecto: Properties.Settings.Default.PrefijoOSCDefault);
            this.ViewModel.MainWindow = this;
            this.ViewModel.TotalModificable = App.TotalModificable;
            this.ViewModel.BuscarServiciosPrevios = App.BuscarServiciosPrevios;
            this.Loaded += (sender, args) =>
                {
                    if (Properties.Settings.Default.MostrarNomSede)
                    {
                        this.Title = $"{this.Title}: {Properties.Settings.Default.NomSede}";
                    }

                    var origin = this.WindowState;
                    this.WindowState = WindowState.Maximized;
                    var max = this.InternalServicesGrid.ActualHeight;
                    this.InternalServicesGrid.MaxHeight = max;
                    this.WindowState = origin;
                    this.VerificarImpresora();
                };
            this.Closing += (sender, args) =>
                {
                    Properties.Settings.Default.PrefijoOSCDefault = this.ViewModel.OscPrefix;
                    if (this.ViewModel.NumeroDeMuestraAsignado != null && this.ViewModel.NumeroDeMuestraAsignado.CentroDeToma != null)
                    {
                        Properties.Settings.Default.PrefijoCentroDeTomaDefault = this.ViewModel.NumeroDeMuestraAsignado.CentroDeToma.Prefijo;
                    }
                };
            this.ViewModel.DiagnosticosCargados += (sender, args) => this.abDiagnostico.PopulateComplete();
            this.ViewModel.PersonalAsistencialEvento += (sender, args) => this.abPersonalAsistencial.PopulateComplete();

            // TODO: Add event handler implementation here.
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);

        }
        WebCam webcam;

        
        private void AutoCompleteBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                this.ViewModel.SelectedDiagnose = autoCompleteBox.SelectedItem as Diagnose;
            }
        }

        private void AutoCompleteBox_OnPopulating(object sender, PopulatingEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                this.ViewModel.SearchDiagnoses(autoCompleteBox.Text);
            }

            e.Cancel = true;
        }

        private void AbPersonalAsistencial_OnPopulating(object sender, PopulatingEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                this.ViewModel.LoadPersonalAsistencial(new Tuple<string, string>(this.ViewModel.SelectedAgreement.Code, autoCompleteBox.Text));
            }

            e.Cancel = true;
        }

        private void AbPersonalAsistencial_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;

            if (autoCompleteBox != null)
            {
                this.ViewModel.SelectedPersonalAsistencial = autoCompleteBox.SelectedItem as PersonalAsistencial;
            }
        }

        private void TextBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                this.ViewModel.SeleccionarRemitente(Direccion.Arriba);
            }
            else if (e.Key == Key.Down)
            {
                this.ViewModel.SeleccionarRemitente(Direccion.Abajo);
            }
            else if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                this.ListaRemitentesCb.IsDropDownOpen = false;

                var transRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(transRequest);
                }

                e.Handled = true;
            }
        }

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.ListaRemitentesCb.IsDropDownOpen = true;
            Debug.WriteLine("Abrir Lista");
            e.Handled = true;
        }

        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            this.ListaRemitentesCb.IsDropDownOpen = false;
            Debug.WriteLine("Cerrar Lista");
            this.CmbEditable.Visibility = Visibility.Hidden;
            this.cmbNoEditable.Visibility = Visibility.Visible;
        }

        private void TextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Debug.WriteLine("Get KeyboardFocus");
        }

        private void TextBox_OnGotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Debug.WriteLine("TextBox_GotMouseCapture");
            Debug.WriteLine(this.ListaRemitentesCb.IsDropDownOpen);

            this.ListaRemitentesCb.IsDropDownOpen = true;

            e.Handled = true;
        }

        private void TextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Debug.WriteLine("TextBox_LostKeyboardFocus");
        }

        private void TextBox_OnLostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Debug.WriteLine("TextBox_LostMouseCapture");
            Debug.WriteLine(this.ListaRemitentesCb.IsDropDownOpen);
        }

        private void ListaRemitentesCb_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("ListaRemitentesCb_MouseDown");
        }

        private void ListaRemitentesCb_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("ListaRemitentesCb_PreviewMouseDown");
        }

        private void _updateModel(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("_updateModel");
            this.moveNext = true;
            e.Handled = true;
        }

        private void ListaRemitentesCb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("ListaRemitentesCb_SelectionChanged");
            if (this.moveNext)
            {
                this.moveNext = false;
                var transRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = this.CmbEditable;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(transRequest);
                }
            }
        }

        private void CmbNoEditable_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.CmbEditable.Visibility = Visibility.Visible;
            this.cmbNoEditable.Visibility = Visibility.Hidden;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => this.CmbEditable.Focus()));
        }


        ////private void ListaRemitentesCb_OnSourceUpdated(object sender, DataTransferEventArgs e)
        ////{
        ////    var comboBox = sender as ComboBox;

        ////    if (comboBox != null)
        ////    {
        ////        if (comboBox.Items.Count == 1)
        ////        {
        ////            comboBox.Items.MoveCurrentToFirst();
        ////        }
        ////    }
        ////}

        private void VerificarImpresora()
        {
            //if (App.ImprimirCodigosDeBarras)
            {
                if (string.IsNullOrWhiteSpace(App.NombreImpresoraCodigoDeBarras))
                {
                    this.ViewModel.AlertsAndWarningsCollection.Add("Nombre de Impresora No Definido");
                    this.ViewModel.HayAlerta = true;
                    return;
                }

                Utils.PrintLabelAsync(null, App.NombreImpresoraCodigoDeBarras, "Test Lysis: " + DateTime.Now)
                    .ContinueWith(
                        except => Task.Factory.StartNew(
                            () =>
                                {
                                    if (except != null && except.Result != null)
                                    {
                                        this.Dispatcher.Invoke(
                                            new Action(
                                                () =>
                                                    {
                                                        this.ViewModel.AlertsAndWarningsCollection.Add("Error: " + except.Result.Message);
                                                        this.ViewModel.HayAlerta = true;
                                                    }));
                                    }
                                    else
                                    {
                                        this.Dispatcher.Invoke(
                                            new Action(() => this.ViewModel.AlertsAndWarningsCollection.Add("Impresora de Código de Barras en Uso: \"" + App.NombreImpresoraCodigoDeBarras + "\"")));
                                    }
                                }));
            }
        }

        private void bntCapture_Click(object sender, RoutedEventArgs e)
        {
            imgCapture.Source = imgVideo.Source;
        }

        private void bntSaveImage_Click(object sender, RoutedEventArgs e)
        {
            Helper.SaveImageCapture((BitmapSource)imgCapture.Source, DocIdent.Text);
            System.Windows.MessageBox.Show("Imagen Guardada con el documento: " + DocIdent.Text, "IMAGEN GRABADA", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void bntResolution_Click(object sender, RoutedEventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void bntSetting_Click(object sender, RoutedEventArgs e)
        {
            webcam.AdvanceSetting();
        }

        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, RoutedEventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, RoutedEventArgs e)
        {
            webcam.Continue();
        }


        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstName.Text != null)
            {
                WebCam.IsEnabled = true;
                Finger.IsEnabled = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        protected virtual void Init()
        {
            try
            {
                Captura = new Capture();
                if (Captura != null)
                {
                    Captura.EventHandler = this;
                    Enroller = new DPFP.Processing.Enrollment();
                    StringBuilder text = new StringBuilder();
                    text.AppendFormat("Necesitas pasar el dedo {0} veces", Enroller.FeaturesNeeded);
                    vecesdedo.Content = text.ToString();
                }
                else
                    System.Windows.Forms.MessageBox.Show("No se pudo");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se pudo Inicializar el lector de huella");
            }
        }
        protected void IniciarCaptura()
        {
            if (Captura != null)
            {
                try
                {
                    Captura.StartCapture();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo Inicializar la Captura de huella");
                }
            }
        }
        protected void PararCpatura()
        {
            if (Captura != null)
            {
                try
                {
                    Captura.StopCapture();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo detener");
                }
            }
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            ponerimagen(ConvetirSampleaMapaBits(Sample));
            Procesar(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {

        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {

        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
        }

        private void Finger_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Captura == null)
            {
                try
                {
                    Init();
                    IniciarCaptura();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo detener");
                }
            }

        }

        protected System.Drawing.Bitmap ConvetirSampleaMapaBits(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion convertidor = new DPFP.Capture.SampleConversion();
            System.Drawing.Bitmap mapaBits = null/* TODO Change to default(_) if this is not a reference type */;
            convertidor.ConvertToPicture(Sample, ref mapaBits);
            return mapaBits;
        }
        private void ponerimagen(System.Drawing.Bitmap bmp)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                ImageHuella.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      bmp.GetHbitmap(),
      IntPtr.Zero,
      System.Windows.Int32Rect.Empty,
      BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
                if (this.ViewModel != null)
                {
                    this.ViewModel.HuellaActual = (byte[])(new ImageConverter()).ConvertTo(bmp, typeof(byte[]));
                }
            }));


        }


        protected DPFP.FeatureSet extraercaracteristicas(DPFP.Sample Sample, DPFP.Processing.DataPurpose Porpuse)
        {
            DPFP.Processing.FeatureExtraction extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback alimentacion = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet caracteristicas = new DPFP.FeatureSet();
            extractor.CreateFeatureSet(Sample, Porpuse, ref alimentacion, ref caracteristicas);
            if ((alimentacion == DPFP.Capture.CaptureFeedback.Good))
                return caracteristicas;
            else
                return null/* TODO Change to default(_) if this is not a reference type */;
        }
        //private void modificarcontroles()
        //{
        //    if (btonguardar.Invoke)
        //}

        private void mostrarveces(string texto)
        {
            this.Dispatcher.BeginInvoke((Action)(() => { vecesdedo.Content = texto; }));

        }
        protected void Procesar(DPFP.Sample Sample)
        {
            DPFP.FeatureSet caracteristicas = extraercaracteristicas(Sample, DPFP.Processing.DataPurpose.Enrollment);
            if ((caracteristicas != null))
            {
                try
                {
                    Enroller.AddFeatures(caracteristicas);
                }
                finally
                {
                    StringBuilder text = new StringBuilder();
                    text.AppendFormat("Necesitas pasar el dedo {0} veces", Enroller.FeaturesNeeded);
                    mostrarveces(text.ToString());
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:
                            {
                                template = Enroller.Template;
                                PararCpatura();
                                this.Dispatcher.BeginInvoke((Action)(() => { btonguardar.IsEnabled = true; }));
                                this.Dispatcher.BeginInvoke((Action)(() => { btonguardar.Visibility = Visibility.Visible ; }));
                                //btonguardar.IsEnabled = true;
                                break;
                            }

                        case DPFP.Processing.Enrollment.Status.Failed:
                            {
                                Enroller.Clear();
                                PararCpatura();
                                IniciarCaptura();
                                break;
                            }
                    }
                }
            }
        }


        private void CapturaFirmaCtrl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void CapturaFirmaCtrl_Unloaded(object sender, RoutedEventArgs e)
        {
        }
        private void Btonguardar_Click(object sender, RoutedEventArgs e)
        {
            
            string StrConn = "Password = Saneuron1.; Persist Security Info = True; User ID = sa; Initial Catalog = LYSISMD; Data Source = Lysis1.eastus2.cloudapp.azure.com";
            Int16 Num = 1;
            string sql2 = "INSERT CITA.TURNO (NomTurno,DuracionMinuto,Huella) Values( @Date," + Num + ",@Firm)";
            SqlCommand cmd2 = new SqlCommand(sql2);
            cmd2.Connection = new SqlConnection(StrConn);
            cmd2.Parameters.AddWithValue("@DATE", DocIdent.Text);
            using (MemoryStream fm = new MemoryStream(template.Bytes))
            {
                cmd2.Parameters.AddWithValue("@Firm", fm.ToArray());
            }
            cmd2.Connection.Open();
            cmd2.ExecuteNonQuery();
            cmd2.Connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PararCpatura();
            var ventana = new FingerSearch();
            ventana.ShowDialog();
        }
    }
}
