// -----------------------------------------------------------------------
// <copyright file="ProfesionalViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProfesionalViewModel : NotifiableObject
    {
        private string nombre;
        private string codigo;
        private string especialidad;

        public string Nombre
        {
            get
            {
                return this.nombre;
            }

            set
            {
                if (value != this.nombre)
                {
                    this.nombre = value;
                    this.NotifyPropertyChanged(() => this.Nombre);
                }
            }
        }

        public string Codigo
        {
            get
            {
                return this.codigo;
            }

            set
            {
                if (value != this.codigo)
                {
                    this.codigo = value;
                    this.NotifyPropertyChanged(() => this.Codigo);
                }
            }
        }

        public string Especialidad
        {
            get
            {
                return this.especialidad;
            }

            set
            {
                if (value != this.especialidad)
                {
                    this.especialidad = value;
                    this.NotifyPropertyChanged(() => this.especialidad);
                }
            }
        }

        public void Update(ValidationResult validationResult)
        {
            this.Clear();
            this.Nombre = validationResult.Name;
            this.Codigo = validationResult.Code;
            this.Especialidad = validationResult.Especialidad;
        }

        public void Clear()
        {
            this.Nombre = string.Empty;
            this.Codigo = string.Empty;
            this.Especialidad = string.Empty;
        }
    }
}
