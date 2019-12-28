// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListadoOscViewModel.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ListadoOscViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using NeuronCloud.Atpc.Co.Modelos.Auxiliares;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;
    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;
    using NeuronCloud.Atpc.Co.WPF.Configuration;

    public class ListadoOscViewModel : NotificationObject
    {
        private readonly ObservableCollection<OrdenDeServicio> listaDeResultados = new ObservableCollection<OrdenDeServicio>();

        private readonly ICollectionView vistaNavegable;

        private readonly ICommand moverAlPrimero;

        private readonly ICommand moverAlUltimo;

        private readonly ICommand moverAlSiguiente;

        private readonly ICommand moverAlAnterior;

        private readonly ICommand buscar;

        private readonly ICommand limpiarBusqueda;

        private readonly ControlsSettings settings;

        private readonly ICommand ordenImprimir;
        private readonly ICommand ordenImprimirLabels;

        private int posicionActual;

        private ParametrosBusquedaOrdenDeServicio parametros = new ParametrosBusquedaOrdenDeServicio();

        private bool isBusy;

        public ListadoOscViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            this.vistaNavegable = CollectionViewSource.GetDefaultView(this.listaDeResultados);
            this.vistaNavegable.CurrentChanged += this.VistaNavegableCurrentChanged;

            this.settings = ControlsSettings.Default;

            this.Parametros.NumeroDeResultados = this.settings.NumeroDeResultados;

            this.moverAlPrimero = new DelegateCommand(this.ExecuteMoverAlPrimero);
            this.moverAlSiguiente = new DelegateCommand(this.ExecuteMoverAlSiguiente);
            this.moverAlAnterior = new DelegateCommand(this.ExecuteMoverAlAnterior);
            this.moverAlUltimo = new DelegateCommand(this.ExecuteMoverAlUltimo);
            this.buscar = new DelegateCommand(this.EjecutarBusqueda);
            this.limpiarBusqueda = new DelegateCommand(() =>
                {
                    this.Parametros = new ParametrosBusquedaOrdenDeServicio { NumeroDeResultados = this.settings.NumeroDeResultados };
                    this.ListaDeResultados.Clear();
                });
            this.ordenImprimir = new DelegateCommand<string>(this.OnOrdenImprimirCommand);
            this.ordenImprimirLabels = new DelegateCommand<OrdenDeServicio>(this.OnOrdenImprimirLabelsCommand);
        }

        public event ParametroTextoEventHandler MostrarVentanaReporteEvent;
        public event ParametroTextoObjetoEventHandler ImprimirLabelsEvent;

        public int PosicionActual
        {
            get
            {
                return this.posicionActual;
            }

            set
            {
                if (value == this.posicionActual)
                {
                    return;
                }

                this.posicionActual = value;
                this.RaisePropertyChanged("PosicionActual");
                this.ActualizarPosicion();
            }
        }

        public ParametrosBusquedaOrdenDeServicio Parametros
        {
            get
            {
                return this.parametros;
            }

            set
            {
                if (object.Equals(value, this.parametros))
                {
                    return;
                }

                this.parametros = value;
                this.RaisePropertyChanged("Parametros");
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
                if (value.Equals(this.isBusy))
                {
                    return;
                }

                this.isBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        public ICollectionView VistaNavegable
        {
            get
            {
                return this.vistaNavegable;
            }
        }

        public ObservableCollection<OrdenDeServicio> ListaDeResultados
        {
            get
            {
                return this.listaDeResultados;
            }
        }

        public ICommand MoverAlPrimero
        {
            get
            {
                return this.moverAlPrimero;
            }
        }

        public ICommand MoverAlUltimo
        {
            get
            {
                return this.moverAlUltimo;
            }
        }

        public ICommand MoverAlSiguiente
        {
            get
            {
                return this.moverAlSiguiente;
            }
        }

        public ICommand MoverAlAnterior
        {
            get
            {
                return this.moverAlAnterior;
            }
        }

        public ICommand Buscar
        {
            get
            {
                return this.buscar;
            }
        }

        public ICommand LimpiarBusqueda
        {
            get
            {
                return this.limpiarBusqueda;
            }
        }

        internal ICommand OrdenImprimir
        {
            get
            {
                return this.ordenImprimir;
            }
        }
        internal ICommand OrdenImprimirLabels
        {
            get
            {
                return this.ordenImprimirLabels;
            }
        }

        private void VistaNavegableCurrentChanged(object sender, EventArgs e)
        {
            if (this.vistaNavegable.CurrentPosition < 0)
            {
                this.PosicionActual = 0;
            }
            else
            {
                this.posicionActual = this.vistaNavegable.CurrentPosition + 1;
                this.RaisePropertyChanged("PosicionActual");
            }
        }

        private void ActualizarPosicion()
        {
            if (this.PosicionActual > this.ListaDeResultados.Count)
            {
                this.PosicionActual = this.VistaNavegable.CurrentPosition + 1;
                return;
            }

            if (this.VistaNavegable.CurrentPosition != this.PosicionActual - 1)
            {
                this.VistaNavegable.MoveCurrentTo(this.ListaDeResultados[this.PosicionActual - 1]);
            }
        }

        private void OnOrdenImprimirCommand(string obj)
        {
            this.OnMostrarVentanaReporteEvent(obj);
        }
        private void OnOrdenImprimirLabelsCommand(OrdenDeServicio obj)
        {
            this.OnImprimirLabelsEvent(obj);
        }

        private void ExecuteMoverAlPrimero()
        {
            if (this.vistaNavegable.IsEmpty)
            {
                return;
            }

            this.vistaNavegable.MoveCurrentToFirst();
        }

        private void ExecuteMoverAlSiguiente()
        {
            if (this.vistaNavegable.IsEmpty)
            {
                return;
            }

            this.vistaNavegable.MoveCurrentToNext();
            if (this.vistaNavegable.IsCurrentAfterLast)
            {
                this.vistaNavegable.MoveCurrentToLast();
            }
        }

        private void ExecuteMoverAlUltimo()
        {
            if (this.vistaNavegable.IsEmpty)
            {
                return;
            }

            this.vistaNavegable.MoveCurrentToLast();
        }

        private void ExecuteMoverAlAnterior()
        {
            if (this.vistaNavegable.IsEmpty)
            {
                return;
            }

            this.vistaNavegable.MoveCurrentToPrevious();
            if (this.vistaNavegable.IsCurrentBeforeFirst)
            {
                this.vistaNavegable.MoveCurrentToFirst();
            }
        }

        private void EjecutarBusqueda()
        {
            // ToDo:Verificar Parametros
            this.ListaDeResultados.Clear();
            this.IsBusy = true;
            Acciones.BuscarOrdenesDeServicioAsync(
                this.Parametros,
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
                        var ordenDeServicios = ee.Result as List<OrdenDeServicio>;

                        if (ordenDeServicios != null)
                        {
                            foreach (var ordenDeServicio in ordenDeServicios)
                            {
                                ordenDeServicio.Imprimir = this.OrdenImprimir;
                                ordenDeServicio.ImprimirLabels = this.OrdenImprimirLabels;
                                this.listaDeResultados.Add(ordenDeServicio);
                            }

                            this.vistaNavegable.MoveCurrentToFirst();
                        }
                    }

                    this.vistaNavegable.Refresh();
                    this.IsBusy = false;
                });
        }

        private void OnMostrarVentanaReporteEvent(string parametro)
        {
            ParametroTextoEventHandler handler = this.MostrarVentanaReporteEvent;
            if (handler != null)
            {
                handler(this, parametro);
            }
        }
        private void OnImprimirLabelsEvent(OrdenDeServicio parametro)
        {
            ParametroTextoObjetoEventHandler handler = this.ImprimirLabelsEvent;
            if (handler != null)
            {
                handler(this, string.Empty, parametro);
            }
        }
    }
}
