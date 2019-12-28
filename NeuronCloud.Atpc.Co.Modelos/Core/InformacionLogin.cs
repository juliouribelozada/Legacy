// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformacionLogin.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Información para Autenticar.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Información para Autenticar.
    /// </summary>
    public class InformacionLogin : ObjetoValidable, IDataErrorInfo
    {
        private string nombreDeUsuario;

        private string palabraClave;

        private bool loginCorrecto;

        private bool validado;

        [Required(ErrorMessageResourceType = typeof(WPF.RecursosComunes.Modelos),ErrorMessageResourceName = "Login_NombreDeUsuario_Required")]
        public string NombreDeUsuario
        {
            get
            {
                return this.nombreDeUsuario;
            }

            set
            {
                if (value == this.nombreDeUsuario)
                {
                    return;
                }

                this.nombreDeUsuario = value;
                this.Validado = false;
                this.RaisePropertyChanged("NombreDeUsuario");
            }
        }

        [Required]
        public string PalabraClave
        {
            get
            {
                return this.palabraClave;
            }

            set
            {
                if (value == this.palabraClave)
                {
                    return;
                }

                this.palabraClave = value;
                this.Validado = false;
                this.RaisePropertyChanged("PalabraClave");
            }
        }

        public bool LoginCorrecto
        {
            get
            {
                return this.loginCorrecto;
            }

            set
            {
                if (value.Equals(this.loginCorrecto))
                {
                    return;
                }

                this.loginCorrecto = value;
                this.RaisePropertyChanged("LoginCorrecto");
            }
        }

        public bool Validado
        {
            get
            {
                return this.validado;
            }

            set
            {
                if (value.Equals(this.validado))
                {
                    return;
                }

                this.validado = value;
                this.RaisePropertyChanged("Validado");
                this.RaisePropertyChanged("NombreDeUsuario");
                this.RaisePropertyChanged("PalabraClave");
            }
        }

        public new string this[string propertyName]
        {
            get
            {
                if ((propertyName == "NombreDeUsuario" || propertyName == "PalabraClave") && this.Validado && !this.LoginCorrecto)
                {
                    if (!this.ErroresPorPropiedad.ContainsKey(propertyName))
                    {
                        this.ErroresPorPropiedad.Add(propertyName, new List<ValidationResult>());
                    }

                    var results = new List<ValidationResult>(1);
                    var errror = new ValidationResult(WPF.RecursosComunes.Modelos.Login_Error);
                    results.Add(errror);
                    this.ErroresPorPropiedad[propertyName] = results;
                    this.RaisePropertyChanged("IsValid");
                    this.RaisePropertyChanged("Errores");

                    return errror.ErrorMessage;
                }

                return this.OnValidate(propertyName);
            }
        }
    }
}
