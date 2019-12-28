using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DPFP;
using DPFP.Capture;

namespace Neuron.OSC
{
    /// <summary>
    /// Interaction logic for FingerSearch.xaml
    /// </summary>
    public partial class FingerSearch : Window, DPFP.Capture.EventHandler
    {
        private DPFP.Template template;
        private DPFP.Capture.Capture Captura;
        private DPFP.Verification.Verification verificador;
        public SqlConnection cn = new SqlConnection("Data Source= Lysis1.eastus2.cloudapp.azure.com;Initial Catalog=LYSISMD;Password =Saneuron1.; Persist Security Info = True; User ID =sa");
        public FingerSearch()
        {
            InitializeComponent();
        }
            protected virtual void Init()
            {
                try
                {
                    Captura = new Capture();
                    verificador = new DPFP.Verification.Verification();
                    template = new Template();

                    if (Captura != null)
                    {
                        Captura.EventHandler = this;

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
            private void ponerimagen(System.Drawing.Bitmap bmp)
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    ImageHuella.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
          bmp.GetHbitmap(),
          IntPtr.Zero,
          System.Windows.Int32Rect.Empty,
          BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));

                }));


            }
             public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
            {
                ponerimagen(ConvetirSampleaMapaBits(Sample));
            DPFP.FeatureSet caracteristicas = extraercaracteristicas(Sample,DPFP.Processing.DataPurpose.Verification);
            if (caracteristicas != null)
            {
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                //string StrConn = "Password = Saneuron1.; Persist Security Info = True; User ID = sa; Initial Catalog = LYSISMD; Data Source = Lysis1.eastus2.cloudapp.azure.com";
                
        //string sql2 = "Select * From CITA.Turno where huella is not null";
                SqlCommand cm = new SqlCommand("Select * From CITA.Turno where huella is not null", cn);
        //se abre la coneccion
                cn.Open();
                SqlDataReader read = cm.ExecuteReader();
                //    SqlDataReader dr = cm.ExecuteReader();
                //SqlCommand cmd = new SqlCommand(sql2);
                //cmd.Connection = new SqlConnection(StrConn);
                //cmd.Connection.Open();
                //SqlDataReader read = cmd.ExecuteReader();

                Boolean verificado = false;
                String nombre = "";
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        nombre = read.GetString(0);
                        //var memoria = new MemoryStream(Convert.ToByte(read.GetByte(5)));
                        byte[] buffer = (byte[])read["huella"];
                        MemoryStream memoria = new MemoryStream(buffer);
                        template.DeSerialize(memoria.ToArray());
                        verificador.Verify(caracteristicas, template, ref result);
                        if (result.Verified)
                        {
                            nombre = read.GetString(0);
                            verificado = true;
                            break;
                        }
                    }
                    if (verificado)
                    {
                        MessageBox.Show(nombre);
                    }
                  
                }
                else
                {
                    MessageBox.Show("No se encontro ningun registro");
                }

                read.Close();
                cm.Connection.Close();
            }
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
            protected System.Drawing.Bitmap ConvetirSampleaMapaBits(DPFP.Sample Sample)
            {
                DPFP.Capture.SampleConversion convertidor = new DPFP.Capture.SampleConversion();
                System.Drawing.Bitmap mapaBits = null/* TODO Change to default(_) if this is not a reference type */;
                convertidor.ConvertToPicture(Sample, ref mapaBits);
                return mapaBits;
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
        private void FingerSearch_load(object sender, RoutedEventArgs e)
        {
            Init();
            IniciarCaptura();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PararCpatura();
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
    }
    }

