// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarCodeEPL.cs" company="Atpc.Co">
//   Luis Antonio Soler Barrera 2013
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lysis.Commom.BarCode
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class BarCodeEPL : IDisposable
    {
        private readonly string printerName;

        private readonly string docName;

        private bool isPrinterReady;

        private IntPtr myHandle;

        public BarCodeEPL(string printerName, string docName = "Bar Code From Lysis.")
        {
            this.printerName = printerName;
            this.docName = docName;
        }

        public bool InitilizePrinter()
        {
            try
            {
                if (RawPrinterComponent.OpenPrinter(this.printerName.Normalize(), out this.myHandle, IntPtr.Zero))
                {
                    RawPrinterComponent.DOCINFOA docInfo = new RawPrinterComponent.DOCINFOA
                                                               {
                                                                   pDocName = this.docName,
                                                                   pDataType = "RAW"
                                                               };

                    if (!RawPrinterComponent.StartDocPrinter(this.myHandle, 1, docInfo))
                    {
                        Trace.WriteLine("Error: No se ha podido Iniciar la Impresión del Documento");
                    }
                    else
                    {
                        this.isPrinterReady = true;
                    }
                }
                else
                {
                    Trace.WriteLine("Error: No se ha podido Abrir la Impresora");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error Al Inicializar BarCodeEPL : " + ex.Message);
            }

            return this.isPrinterReady;
        }

        public void EndPrinterJob(bool appendFormFeed = false)
        {
            if (!this.isPrinterReady)
            {
                return;
            }

            if (this.myHandle != IntPtr.Zero)
            {
                try
                {
                    RawPrinterComponent.EndPagePrinter(this.myHandle);
                    RawPrinterComponent.EndDocPrinter(this.myHandle);
                    RawPrinterComponent.ClosePrinter(this.myHandle);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error Al Finalizar BarCodeEPL : " + ex.Message);
                }
                finally
                {
                    this.myHandle = IntPtr.Zero;
                }
            }
        }

        public void WriteLine(string lineToPrint)
        {
            if (!this.isPrinterReady)
            {
                throw new InvalidOperationException("No se ha Inicializado la Impresora");
            }

            if (!lineToPrint.EndsWith("\r\n"))
            {
                lineToPrint = lineToPrint + "\r\n";
            }

            int dwCount = lineToPrint.Length;

            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(lineToPrint);

            if (this.myHandle != IntPtr.Zero)
            {
                try
                {
                    int dataWrittenSize;
                    if (RawPrinterComponent.WritePrinter(this.myHandle, pBytes, dwCount, out dataWrittenSize))
                    {
                        if (dwCount != dataWrittenSize)
                        {
                            Trace.WriteLine("Advertencia Al Imprimir BarCodeEPL");
                        }
                    }
                    else
                    {
                        Trace.WriteLine("Error Al Imprimir BarCodeEPL");
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error Al Imprimir BarCodeEPL : " + ex.Message);
                }
            }

            Marshal.FreeCoTaskMem(pBytes);
        }

        public void Dispose()
        {
            if (this.myHandle != IntPtr.Zero)
            {
                this.EndPrinterJob();
            }
        }
    }
}
