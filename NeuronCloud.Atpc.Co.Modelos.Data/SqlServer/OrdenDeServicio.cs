// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdenDeServicio.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Osc con Acciones.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace NeuronCloud.Atpc.Co.Modelos.Data.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Practices.Prism.Commands;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using NeuronCloud.Atpc.Co.Providers.Desktop;
    using Neuron.OSC.Data.Model;

    /// <summary>
    /// Osc con Acciones.
    /// </summary>
    public class OrdenDeServicio : OrdenDeServicioBase, ICrud
    {
        private ICommand imprimir;
        private ICommand imprimirLabel;


        public OrdenDeServicio()
        {
            this.Crear = new DelegateCommand(() => { });
            this.Actualizar = new DelegateCommand(() => { });
            this.Borrar = new DelegateCommand(() => { });
            this.Anular = new DelegateCommand(this.EjecutarAnular, PuedeEjecutarAnular);
        }

        public ICommand Crear { get; private set; }

        public ICommand Actualizar { get; set; }

        public ICommand Borrar { get; set; }

        public ICommand Anular { get; set; }

        public ICommand Imprimir
        {
            get
            {
                return this.imprimir ?? (this.imprimir = new DelegateCommand(() => MessageBox.Show("No se Ha Implementado la Impresión")));
            }

            set
            {
                this.imprimir = value;
            }
        }

        public ICommand ImprimirLabels
        {
            get
            {
                return this.imprimirLabel ?? (this.imprimirLabel = new DelegateCommand(() => MessageBox.Show("No se Ha Implementado la Impresión")));
            }

            set
            {
                this.imprimirLabel = value;
            }
        }

        public static bool PuedeEjecutarAnular()
        {
            var currentPrincipal = ConfiguracionGlobal.IPrincipalActual as NeuronCloudPrincipal;

            if (currentPrincipal != null)
            {
                if (currentPrincipal.PermisosOsc(new Func<bool>(() => OscClaims.ValidateClaims("ANULAR"))))
                {
                    return true;
                }
            }

            return false;
        }

        private void EjecutarAnular()
        {
            if (PuedeEjecutarAnular())
            {
                switch (MessageBox.Show("Desea anular la OSC No.: \"" + this.DocUnicoOsc + "\"?", "Anular OSC", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
                {
                    case MessageBoxResult.Yes:
                        Acciones.AnularOscAsyn(
                            this,
                            (ss, ee) =>
                            {
                                if (ee.Cancelled)
                                {
                                }
                                else if (ee.Error != null)
                                {
                                }
                                else
                                {
                                    this.FechaAnulacion = DateTime.Today;
                                }
                            });
                        break;
                }
            }
        }
    }
}
