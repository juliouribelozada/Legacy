// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonalAsistencial.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Pesonal Asistencial.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Pesonal Asistencial.
    /// </summary>
    public class PersonalAsistencial : ObjetoValidable
    {
        private string codigo;

        private string nombreCompleto;

        private string profesion;

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "PersonalAsistencial_Codigo_Caption")]
        [Required]
        public string Codigo
        {
            get
            {
                return this.codigo;
            }

            set
            {
                if (value == this.codigo)
                {
                    return;
                }

                this.codigo = value;
                this.RaisePropertyChanged("Codigo");
                this.RaisePropertyChanged("TextoABuscar");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "PersonalAsistencial_Nombre_Caption")]
        public string NombreCompleto
        {
            get
            {
                return this.nombreCompleto;
            }

            set
            {
                if (value == this.nombreCompleto)
                {
                    return;
                }

                this.nombreCompleto = value;
                this.RaisePropertyChanged("NombreCompleto");
                this.RaisePropertyChanged("TextoABuscar");
            }
        }

        [Display(ResourceType = typeof(WPF.RecursosComunes.Modelos), Name = "PersonalAsistencial_Profesion_Caption")]
        public string Profesion
        {
            get
            {
                return this.profesion;
            }

            set
            {
                if (value == this.profesion)
                {
                    return;
                }

                this.profesion = value;
                this.RaisePropertyChanged("Profesion");
            }
        }

        public string TextoABuscar
        {
            get
            {
                return string.Concat(this.Codigo, " - ", this.NombreCompleto);
            }
        }
    }
}
