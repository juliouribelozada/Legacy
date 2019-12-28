// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cita.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Define la estructura de una Cita.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define la estructura de una Cita.
    /// </summary>
    public class Cita : ObjetoValidable, IDataErrorInfo
    {
        private DateTime inicio;

        private DateTime fin;

        private string descripcion;

        private string id;

        [Required]
        public DateTime Inicio
        {
            get
            {
                return this.inicio;
            }

            set
            {
                if (value.Equals(this.inicio))
                {
                    return;
                }

                this.inicio = value;
                this.RaisePropertyChanged("Inicio");
            }
        }

        [Required]
        public DateTime Fin
        {
            get
            {
                return this.fin;
            }

            set
            {
                if (value.Equals(this.fin))
                {
                    return;
                }

                this.fin = value;
                this.RaisePropertyChanged("Fin");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "Cita_Descripcion_Caption")]
        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }

            set
            {
                if (value == this.descripcion)
                {
                    return;
                }

                this.descripcion = value;
                this.RaisePropertyChanged("Descripcion");
            }
        }

        [Key]
        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value == this.id)
                {
                    return;
                }

                this.id = value;
                this.RaisePropertyChanged("Id");
            }
        }

        public new string this[string propertyName]
        {
            get
            {
                if (propertyName == "Fin")
                {
                    if (this.Fin != default(DateTime) && this.Inicio != default(DateTime) && this.Fin < this.Inicio)
                    {
                        if (!this.ErroresPorPropiedad.ContainsKey(propertyName))
                        {
                            this.ErroresPorPropiedad.Add(propertyName, new List<ValidationResult>());
                        }

                        var results = new List<ValidationResult>(1);
                        var errror = new ValidationResult("El fin de la cita debe ser posterion al Inicio");
                        results.Add(errror);
                        this.ErroresPorPropiedad[propertyName] = results;

                        return errror.ErrorMessage;
                    }
                }

                return this.OnValidate(propertyName);
            }
        }
    }
}
