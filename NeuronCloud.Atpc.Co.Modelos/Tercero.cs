// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tercero.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Tercero : ObjetoValidable
    {
        private string primerNombre;
        private TipoIdentificacion tipoDocumento;
        private string numeroDocumento;

        private string segundoNombre;

        private string primerApellido;

        private string direccion;

        private string telefonoResidencia;

        private string telefonoMovil;

        private string telefonoAlterno;

        private string genero;

        private DateTime? fechaNacimiento;

        private Municipio municipio;

        private string correoPersonal;

        private string correoCorporativo;

        private string zona;

        private string segundoApellido;

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
                this.RaisePropertyChanged("NombreCompleto");
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
                this.RaisePropertyChanged("NombreCompleto");
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
                this.RaisePropertyChanged("NombreCompleto");
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
                this.RaisePropertyChanged("NombreCompleto");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_NombreCompleto_Caption")]
        public string NombreCompleto
        {
            get
            {
                return string.Concat(this.PrimerApellido, " ", !string.IsNullOrWhiteSpace(this.SegundoApellido) ? this.SegundoApellido + " " : string.Empty, this.PrimerNombre, !string.IsNullOrWhiteSpace(this.SegundoNombre) ? " " + this.SegundoNombre : string.Empty);
            }
        }

        public TipoIdentificacion TipoDocumento
        {
            get
            {
                return this.tipoDocumento;
            }

            set
            {
                if (value == this.tipoDocumento)
                {
                    return;
                }

                this.tipoDocumento = value;
                this.RaisePropertyChanged("TipoDocumento");
                this.RaisePropertyChanged("NumeroUnicoDocumento");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_NumeroDocumento_Caption")]
        public string NumeroDocumento
        {
            get
            {
                return this.numeroDocumento;
            }

            set
            {
                if (value == this.numeroDocumento)
                {
                    return;
                }

                this.numeroDocumento = value;
                this.RaisePropertyChanged("NumeroDocumento");
                this.RaisePropertyChanged("NumeroUnicoDocumento");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_Direccion_Caption")]
        [Required]
        public string Direccion
        {
            get
            {
                return this.direccion;
            }

            set
            {
                if (value == this.direccion)
                {
                    return;
                }

                this.direccion = value;
                this.RaisePropertyChanged("Direccion");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_Barrio_Caption")]
        public string Barrio { get; set; }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_TelefonoResidencia_Caption")]
        public string TelefonoResidencia
        {
            get
            {
                return this.telefonoResidencia;
            }

            set
            {
                if (value == this.telefonoResidencia)
                {
                    return;
                }

                this.telefonoResidencia = value;
                this.RaisePropertyChanged("TelefonoResidencia");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_TelefonoMovil_Caption")]
        public string TelefonoMovil
        {
            get
            {
                return this.telefonoMovil;
            }

            set
            {
                if (value == this.telefonoMovil)
                {
                    return;
                }

                this.telefonoMovil = value;
                this.RaisePropertyChanged("TelefonoMovil");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_TelefonoAlterno_Caption")]
        public string TelefonoAlterno
        {
            get
            {
                return this.telefonoAlterno;
            }

            set
            {
                if (value == this.telefonoAlterno)
                {
                    return;
                }

                this.telefonoAlterno = value;
                this.RaisePropertyChanged("TelefonoAlterno");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_Fax_Caption")]
        public string Fax { get; set; }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_FechaNacimiento_Caption")]
        public DateTime? FechaNacimiento
        {
            get
            {
                return this.fechaNacimiento;
            }

            set
            {
                if (value.Equals(this.fechaNacimiento))
                {
                    return;
                }

                this.fechaNacimiento = value;
                this.RaisePropertyChanged("FechaNacimiento");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_Genero_Caption")]
        [Required]
        public string Genero
        {
            get
            {
                return this.genero;
            }

            set
            {
                if (value == this.genero)
                {
                    return;
                }

                this.genero = value;
                this.RaisePropertyChanged("Genero");
            }
        }

        [Required]
        public Municipio Municipio
        {
            get
            {
                return this.municipio;
            }

            set
            {
                if (Equals(value, this.municipio))
                {
                    return;
                }

                this.municipio = value;
                this.RaisePropertyChanged("Municipio");
            }
        }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(WPF.RecursosComunes.Modelos), ErrorMessageResourceName = "General_InvalidEmail")]
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_EmailPersonal_Caption")]
        public string CorreoPersonal
        {
            get
            {
                return this.correoPersonal;
            }

            set
            {
                if (value == this.correoPersonal)
                {
                    return;
                }

                this.correoPersonal = value;
                this.RaisePropertyChanged("CorreoPersonal");
            }
        }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(WPF.RecursosComunes.Modelos), ErrorMessageResourceName = "General_InvalidEmail")]
        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Tercero_EmailEmpresa_Caption")]
        public string CorreoCorporativo
        {
            get
            {
                return this.correoCorporativo;
            }

            set
            {
                if (value == this.correoCorporativo)
                {
                    return;
                }

                this.correoCorporativo = value;
                this.RaisePropertyChanged("CorreoCorporativo");
            }
        }

        public string Zona
        {
            get
            {
                return this.zona;
            }

            set
            {
                if (value == this.zona)
                {
                    return;
                }

                this.zona = value;
                this.RaisePropertyChanged("Zona");
            }
        }

        public string NumeroUnicoDocumento
        {
            get
            {
                return string.Concat(this.NumeroDocumento, this.TipoDocumento.TipoIdentificacionId);
            }
        }
    }
}
