// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdenDeServicioBase.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Define la OSC.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// Define la OSC.
    /// </summary>
    public abstract class OrdenDeServicioBase : NotificationObject
    {
        private readonly ObservableCollection<DetalleOrdenDeServicio> detalle = new ObservableCollection<DetalleOrdenDeServicio>();

        private string docUnicoOsc;

        private string numeroUnicoDocumento;

        private DateTime? fechaAnulacion;

        private DateTime fecha;

        private DateTime fechaRegistro;

        private string segundoApellido;

        private string primerNombre;

        private string segundoNombre;

        private string primerApellido;

        private string usuarioCreacion;

        private string usuarioAnulo;

        private string codigoDiagnostico;

        private string codigoConvenio;

        private string codigoPrestador;

        private string codigoRemitente;

        private string remitente;

        private string prestador;

        private string convenio;

        private string diagnostico;

        private string observaciones;

        [Key]
        [Required]
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_NumeroDocumento_Caption")]
        public string DocUnicoOsc
        {
            get
            {
                return this.docUnicoOsc;
            }

            set
            {
                if (value == this.docUnicoOsc)
                {
                    return;
                }

                this.docUnicoOsc = value;
                this.RaisePropertyChanged("DocUnicoOsc");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_NumeroDocumento_Caption")]
        public string NumeroUnicoDocumento
        {
            get
            {
                return this.numeroUnicoDocumento;
            }

            set
            {
                if (value == this.numeroUnicoDocumento)
                {
                    return;
                }

                this.numeroUnicoDocumento = value;
                this.RaisePropertyChanged("NumeroUnicoDocumento");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_FechaAnulacion_Caption")]
        public DateTime? FechaAnulacion
        {
            get
            {
                return this.fechaAnulacion;
            }

            set
            {
                if (value.Equals(this.fechaAnulacion))
                {
                    return;
                }

                this.fechaAnulacion = value;
                this.RaisePropertyChanged("FechaAnulacion");
                this.RaisePropertyChanged("Anulada");
            }
        }
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_FechaRegistro_Caption")]
        public DateTime FechaRegistro
        {
            get
            {
                return this.fechaRegistro;
            }

            set
            {
                if (value.Equals(this.fechaRegistro))
                {
                    return;
                }

                this.fechaRegistro= value;
                this.RaisePropertyChanged("FechaRegistro");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Fecha_Caption")]
        public DateTime Fecha
        {
            get
            {
                return this.fecha;
            }

            set
            {
                if (value.Equals(this.fecha))
                {
                    return;
                }

                this.fecha = value;
                this.RaisePropertyChanged("Fecha");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_PrimerNombre_Caption")]
        [Required]
        public string PrimerNombre
        {
            get
            {
                return this.primerNombre;
            }

            set
            {
                if (value == this.primerNombre)
                {
                    return;
                }

                this.primerNombre = value;
                this.RaisePropertyChanged("PrimerNombre");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_SegundoNombre_Caption")]
        public string SegundoNombre
        {
            get
            {
                return this.segundoNombre;
            }

            set
            {
                if (value == this.segundoNombre)
                {
                    return;
                }

                this.segundoNombre = value;
                this.RaisePropertyChanged("SegundoNombre");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_PrimerApellido_Caption")]
        [Required]
        public string PrimerApellido
        {
            get
            {
                return this.primerApellido;
            }

            set
            {
                if (value == this.primerApellido)
                {
                    return;
                }

                this.primerApellido = value;
                this.RaisePropertyChanged("PrimerApellido");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_SegundoApellido_Caption")]
        public string SegundoApellido
        {
            get
            {
                return this.segundoApellido;
            }

            set
            {
                if (value == this.segundoApellido)
                {
                    return;
                }

                this.segundoApellido = value;
                this.RaisePropertyChanged("SegundoApellido");
            }
        }

        public bool Anulada
        {
            get
            {
                return this.FechaAnulacion.HasValue;
            }
        }

        public ObservableCollection<DetalleOrdenDeServicio> Detalle
        {
            get
            {
                return this.detalle;
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_UsuarioCreacion_Caption")]
        public string UsuarioCreacion
        {
            get
            {
                return this.usuarioCreacion;
            }

            set
            {
                if (value == this.usuarioCreacion)
                {
                    return;
                }

                this.usuarioCreacion = value;
                this.RaisePropertyChanged("UsuarioCreacion");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_UsuarioAnulacion_Caption")]
        public string UsuarioAnulo
        {
            get
            {
                return this.usuarioAnulo;
            }

            set
            {
                if (value == this.usuarioAnulo)
                {
                    return;
                }

                this.usuarioAnulo = value;
                this.RaisePropertyChanged("UsuarioAnulo");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "General_Codigo_Caption")]
        public string CodigoDiagnostico
        {
            get
            {
                return this.codigoDiagnostico;
            }

            set
            {
                if (value == this.codigoDiagnostico)
                {
                    return;
                }

                this.codigoDiagnostico = value;
                this.RaisePropertyChanged("CodigoDiagnostico");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "General_Codigo_Caption")]
        public string CodigoConvenio
        {
            get
            {
                return this.codigoConvenio;
            }

            set
            {
                if (value == this.codigoConvenio)
                {
                    return;
                }

                this.codigoConvenio = value;
                this.RaisePropertyChanged("CodigoConvenio");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "General_Codigo_Caption")]
        public string CodigoPrestador
        {
            get
            {
                return this.codigoPrestador;
            }

            set
            {
                if (value == this.codigoPrestador)
                {
                    return;
                }

                this.codigoPrestador = value;
                this.RaisePropertyChanged("CodigoPrestador");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "General_Codigo_Caption")]
        public string CodigoRemitente
        {
            get
            {
                return this.codigoRemitente;
            }

            set
            {
                if (value == this.codigoRemitente)
                {
                    return;
                }

                this.codigoRemitente = value;
                this.RaisePropertyChanged("CodigoRemitente");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Prestador_Caption")]
        public string Prestador
        {
            get
            {
                return this.prestador;
            }

            set
            {
                if (value == this.prestador)
                {
                    return;
                }

                this.prestador = value;
                this.RaisePropertyChanged("Prestador");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Remitente_Caption")]
        public string Remitente
        {
            get
            {
                return this.remitente;
            }

            set
            {
                if (value == this.remitente)
                {
                    return;
                }

                this.remitente = value;
                this.RaisePropertyChanged("Remitente");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Convenio_Caption")]
        public string Convenio
        {
            get
            {
                return this.convenio;
            }

            set
            {
                if (value == this.convenio)
                {
                    return;
                }

                this.convenio = value;
                this.RaisePropertyChanged("Convenio");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Diagnostico_Caption")]
        public string Diagnostico
        {
            get
            {
                return this.diagnostico;
            }

            set
            {
                if (value == this.diagnostico)
                {
                    return;
                }

                this.diagnostico = value;
                this.RaisePropertyChanged("Diagnostico");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Osc_Observaciones_Caption")]
        public string Observaciones
        {
            get
            {
                return this.observaciones;
            }

            set
            {
                if (value == this.observaciones)
                {
                    return;
                }

                this.observaciones = value;
                this.RaisePropertyChanged("Observaciones");
            }
        }
    }
}
