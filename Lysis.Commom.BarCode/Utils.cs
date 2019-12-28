// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="Atpc.Co">
//   Luis Antonio Soler Barrera 2013
// </copyright>
// <summary>
//   Defines the Utils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lysis.Commom.BarCode
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    public static class Utils
    {
        public static string SelectPrinter()
        {
            PrintDialog printerDialog = new PrintDialog();
            printerDialog.ShowDialog();
          

            using (BarCodeEPL documentEpl = new BarCodeEPL(printerDialog.PrintQueue.FullName))
            {
                if (documentEpl.InitilizePrinter())
                {
                    documentEpl.WriteLine(string.Empty);
                    documentEpl.WriteLine("N");
                    documentEpl.WriteLine("q480");
                    documentEpl.WriteLine("A40,20,0,1,1,1,N,\"" + "hora" + "\"");
                    documentEpl.WriteLine("A40,40,0,2,1,1,N,\"" + "Nombre" + "\"");
                    documentEpl.WriteLine("A40,65,0,1,1,1,N,\"" + "Edad" + "\"");
                    documentEpl.WriteLine("B70,80,0,1,2,5,70,N,\"" + "123654" + "\"");
                    documentEpl.WriteLine("A40,160,0,2,1,1,N,\"" + "Sangre" + ":" + "Jumm" + "\"");
                    documentEpl.WriteLine("P1");
                }
                return printerDialog.PrintQueue.FullName;
            }
        }

        public static Task<Exception> PrintLabelAsync(IEnumerable<string> labelLines, string printerName, string printJobName = "Código de Barras de Lysis")
        {
            return Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        using (BarCodeEPL documentEpl = new BarCodeEPL(printerName, printJobName))
                        {
                            if (documentEpl.InitilizePrinter())
                            {
                                if (labelLines != null)
                                {
                                    foreach (var labelLine in labelLines)
                                    {
                                        documentEpl.WriteLine(labelLine);
                                    }
                                }
                            }
                            else
                            {
                                throw new ArgumentException("La Impresora Especificada no se pudo Usar: \"" + printerName + "\"");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex;
                    }

                    return null;
                });
        }
    }
}
