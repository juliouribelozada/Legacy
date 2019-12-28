// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrearEditarTerceroViewModel.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.Modelos.Data.SqlServer.Entity;
    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos;
    using NeuronCloud.Atpc.Co.WPF.RecursosComunes;

    using Departamento = NeuronCloud.Atpc.Co.Modelos.Departamento;
    using Municipio = NeuronCloud.Atpc.Co.Modelos.Municipio;
    using Pais = NeuronCloud.Atpc.Co.Modelos.Pais;
    using Tercero = NeuronCloud.Atpc.Co.Modelos.Tercero;

    public class CrearEditarTerceroViewModel : NotificationObject
    {
        private readonly ObservableCollection<Pais> listaPaises;
        private readonly ObservableCollection<string> generos = new ObservableCollection<string>();
        private readonly ObservableCollection<string> zonas = new ObservableCollection<string>();

        private readonly ObservableCollection<Departamento> listaDepartamentos;

        private readonly ObservableCollection<Municipio> listaMunicipios;

        private readonly ObservableCollection<TipoIdentificacion> listaTipoIdentificacion;

        private DelegateCommand guardarTercero;

        private string tituloVentana;

        private ModoVentana modo;

        private Pais paisSeleccionado;

        private Departamento departamentoSeleccionado;

        private bool closeView;

        private Tercero terceroAEditar;

        public CrearEditarTerceroViewModel()
        {
            this.TerceroAEditar = new Tercero { Zona = "URBANA" };
            this.Generos.Add("MASCULINO");
            this.Generos.Add("FEMENINO");
            this.Zonas.Add("URBANA");
            this.Zonas.Add("RURAL");
            this.Modo = ModoVentana.Nuevo;
            this.listaPaises = GlobalModelosCache.PaisList;
            this.listaDepartamentos = GlobalModelosCache.DepartamentoList;
            this.listaMunicipios = GlobalModelosCache.MunicipioList;
            this.listaTipoIdentificacion = GlobalModelosCache.TipoDeIdentifiacionList;
            this.ListaPaises = CollectionViewSource.GetDefaultView(this.listaPaises);
            this.ListaMunicipios = CollectionViewSource.GetDefaultView(this.listaMunicipios);
            this.ListaDepartamentos = CollectionViewSource.GetDefaultView(this.listaDepartamentos);
            this.ListaDepartamentos.Filter = obj => false;
            this.ListaMunicipios.Filter = obj => false;
        }

        public CrearEditarTerceroViewModel(ModoVentana modoVentana)
        {
            this.Modo = modoVentana;
        }

        public event ParametroTextoEventHandler MostrarMensajeEnUI;

        public enum ModoVentana
        {
            Editar,
            Nuevo
        }

        public ObservableCollection<TipoIdentificacion> ListaTipoIdentificacion
        {
            get
            {
                return this.listaTipoIdentificacion;
            }
        }

        public ModoVentana Modo
        {
            get
            {
                return this.modo;
            }

            private set
            {
                this.modo = value;
                this.OnModoChanged();
            }
        }

        public Tercero TerceroAEditar
        {
            get
            {
                return this.terceroAEditar;
            }

            set
            {
                if (Equals(value, this.terceroAEditar))
                {
                    return;
                }

                this.terceroAEditar = value;
                this.RaisePropertyChanged("TerceroAEditar");
            }
        }

        public DelegateCommand GaurdarTercero
        {
            get
            {
                return this.guardarTercero ?? (this.guardarTercero = new DelegateCommand(this.GuardarTerceroAction));
            }
        }

        public bool CloseView
        {
            get
            {
                return this.closeView;
            }

            set
            {
                if (value.Equals(this.closeView))
                {
                    return;
                }

                this.closeView = value;
                this.RaisePropertyChanged("CloseView");
            }
        }

        public ICollectionView ListaPaises { get; set; }

        public ObservableCollection<string> Generos
        {
            get
            {
                return this.generos;
            }
        }

        public ObservableCollection<string> Zonas
        {
            get
            {
                return this.zonas;
            }
        }

        public ICollectionView ListaDepartamentos { get; set; }

        public ICollectionView ListaMunicipios { get; set; }

        public Pais PaisSeleccionado
        {
            get
            {
                return this.paisSeleccionado;
            }

            set
            {
                if (Equals(value, this.paisSeleccionado))
                {
                    return;
                }

                this.paisSeleccionado = value;
                this.OnPaisSeleccionadoChanged();
                this.RaisePropertyChanged("PaisSeleccionado");
            }
        }

        public Departamento DepartamentoSeleccionado
        {
            get
            {
                return this.departamentoSeleccionado;
            }

            set
            {
                if (Equals(value, this.departamentoSeleccionado))
                {
                    return;
                }

                this.departamentoSeleccionado = value;
                this.OnDepartamentoSeleccionadoChanged();
                this.RaisePropertyChanged("DepartamentoSeleccionado");
            }
        }

        public string TituloVentana
        {
            get
            {
                return this.tituloVentana;
            }

            private set
            {
                if (value == this.tituloVentana)
                {
                    return;
                }

                this.tituloVentana = value;
                this.RaisePropertyChanged("TituloVentana");
            }
        }

        public void GetTerceroAction()
        {
            this.Modo = ModoVentana.Editar;
            this.TerceroAEditar.IsBusy = true;
            Acciones.GetTerceroAsync(
                this.TerceroAEditar,
                (ss, ee) =>
                {
                    if (ee.Cancelled)
                    {
                        this.OnMostrarMensajeEvent("Cancelado");
                    }
                    else if (ee.Error != null)
                    {
                        this.OnMostrarMensajeEvent("Error: " + ee.Error.Message);
                    }
                    else
                    {
                        var tercero = ee.Result as Tercero;

                        if (tercero != null)
                        {
                            var tipoId = (from tid in listaTipoIdentificacion
                                          where tid.TipoIdentificacionId == tercero.TipoDocumento.TipoIdentificacionId
                                          select tid).FirstOrDefault();

                            var municipio = (from muni in listaMunicipios
                                             where muni.Codigo == tercero.Municipio.Codigo && muni.CodigoPais == tercero.Municipio.CodigoPais
                                             select muni).FirstOrDefault();

                            PaisSeleccionado = this.listaPaises.FirstOrDefault(paises => municipio != null && paises.Codigo == municipio.CodigoPais);
                            DepartamentoSeleccionado = this.listaDepartamentos.FirstOrDefault(dept => municipio != null && dept.Codigo == municipio.CodigoDepartamento);

                            tercero.TipoDocumento = tipoId;
                            tercero.Municipio = municipio;
                            this.TerceroAEditar = tercero;
                        }
                    }

                    this.TerceroAEditar.IsBusy = false;
                    this.OnModoChanged();
                });
        }

        private void OnDepartamentoSeleccionadoChanged()
        {
            this.ListaMunicipios.Filter =
                obj =>
                {
                    if (this.DepartamentoSeleccionado == null)
                    {
                        return false;
                    }

                    var municipio = obj as Municipio;

                    if (this.DepartamentoSeleccionado != null && this.PaisSeleccionado != null && municipio != null)
                    {
                        return municipio.CodigoPais == this.PaisSeleccionado.Codigo && municipio.CodigoDepartamento == this.DepartamentoSeleccionado.Codigo;
                    }

                    return false;
                };
        }

        private void GuardarTerceroAction()
        {
            this.TerceroAEditar.IsBusy = true;
            Acciones.GuardarTerceroAsync(
                this.TerceroAEditar,
                (ss, ee) =>
                {
                    if (ee.Cancelled)
                    {
                        this.OnMostrarMensajeEvent("Cancelado");
                    }
                    else if (ee.Error != null)
                    {
                        this.OnMostrarMensajeEvent("Error: " + ee.Error.Message);
                    }
                    else
                    {
                        this.CloseView = true;
                    }

                    this.TerceroAEditar.IsBusy = false;
                });
        }

        private void OnPaisSeleccionadoChanged()
        {
            this.ListaDepartamentos.Filter =
                obj =>
                {
                    if (this.PaisSeleccionado == null)
                    {
                        return false;
                    }

                    var departamento = obj as Departamento;

                    if (departamento != null)
                    {
                        return departamento.CodigoPais == this.PaisSeleccionado.Codigo;
                    }

                    return false;
                };
        }

        private void OnModoChanged()
        {
            switch (this.Modo)
            {
                case ModoVentana.Editar:
                    this.TituloVentana = string.Format(Vista.VentanaTerceroModoEdicion, string.Concat(this.TerceroAEditar.PrimerNombre, " ", this.TerceroAEditar.PrimerApellido));
                    break;
                case ModoVentana.Nuevo:
                    this.TituloVentana = Vista.VentanaTerceroModoNuevo;
                    break;
                default:
                    this.TituloVentana = Vista.VentanaTerceroModoNuevo;
                    break;
            }
        }

        private void OnMostrarMensajeEvent(string parametro)
        {
            ParametroTextoEventHandler handler = this.MostrarMensajeEnUI;
            if (handler != null)
            {
                handler(this, parametro);
            }
        }
    }
}
