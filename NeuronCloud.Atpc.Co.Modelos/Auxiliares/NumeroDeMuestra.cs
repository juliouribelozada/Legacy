// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumeroDeMuestra.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the NumeroDeMuestra type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Auxiliares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class NumeroDeMuestra : ObjetoValidable, IDataErrorInfo
    {
        private CentroDeTomaMuestra centroDeToma;

        private int mes;

        private int dia;

        private string consecutivo = string.Empty;

        private AsigNoMuestra tipoAsignacion;

        [Required]
        [Range(1, 31)]
        public int Dia
        {
            get
            {
                return this.dia;
            }

            set
            {
                if (value == this.dia)
                {
                    return;
                }

                this.dia = value;
                this.RaisePropertyChanged("Dia");
                this.RaisePropertyChanged("NumeroAsignado");
            }
        }

        [Required]
        [Range(1, 12)]
        public int Mes
        {
            get
            {
                return this.mes;
            }

            set
            {
                if (value == this.mes)
                {
                    return;
                }

                this.mes = value;
                this.RaisePropertyChanged("Mes");
                this.RaisePropertyChanged("NumeroAsignado");
            }
        }

        public string Consecutivo
        {
            get
            {
                return this.consecutivo;
            }

            set
            {
                if (value == this.consecutivo)
                {
                    return;
                }

                this.consecutivo = value;
                this.RaisePropertyChanged("Consecutivo");
                this.RaisePropertyChanged("NumeroAsignado");
            }
        }

        [Required]
        public CentroDeTomaMuestra CentroDeToma
        {
            get
            {
                return this.centroDeToma;
            }

            set
            {
                if (object.Equals(value, this.centroDeToma))
                {
                    return;
                }

                this.centroDeToma = value;
                this.RaisePropertyChanged("CentroDeToma");
                this.RaisePropertyChanged("NumeroAsignado");
            }
        }

        public string NumeroAsignado
        {
            get
            {
                return string.Concat(this.Mes.ToString("00"), this.Dia.ToString("00"), this.CentroDeToma == null ? "--" : this.CentroDeToma.Prefijo, this.Consecutivo.PadLeft(3, '0'));
            }
        }

        public AsigNoMuestra TipoAsignacion
        {
            get
            {
                return this.tipoAsignacion;
            }

            set
            {
                if (Equals(value, this.tipoAsignacion))
                {
                    return;
                }

                this.tipoAsignacion = value;
                this.RaisePropertyChanged("TipoAsignacion");
            }
        }

        public new string this[string propertyName]
        {
            get
            {
                if (propertyName == "Consecutivo")
                {
                    if (this.TipoAsignacion == AsigNoMuestra.DIA && string.IsNullOrWhiteSpace(this.Consecutivo))
                    {
                        if (!this.ErroresPorPropiedad.ContainsKey(propertyName))
                        {
                            this.ErroresPorPropiedad.Add(propertyName, new List<ValidationResult>());
                        }

                        var results = new List<ValidationResult>(1);
                        var errror = new ValidationResult("El Consecutivo no puede ser Vacio");
                        results.Add(errror);
                        this.ErroresPorPropiedad[propertyName] = results;

                        return errror.ErrorMessage;
                    }
                }

                return this.OnValidate(propertyName);
            }
        }

        public NumeroDeMuestra Copy()
        {
            return (NumeroDeMuestra)this.MemberwiseClone();
        }
    }
}
