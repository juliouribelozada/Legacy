// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutorizacionWindowViewModel.cs" company="ATPC.co">
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
    using System.ComponentModel;
    using System.Windows.Controls;

    using Microsoft.Practices.Prism.Commands;

    using NeuronCloud.Atpc.Co.Modelos.Core;

    public class AutorizacionWindowViewModel : NeuronWindowViewModelBase
    {
        private readonly DelegateCommand<object> validarCommand;

        private readonly BackgroundWorker validarWorker = new BackgroundWorker();

        public AutorizacionWindowViewModel()
        {
            this.TituloVentana = RecursosComunes.Vista.VentanaAutenticacionTitulo;
            this.InformacionLogin = new InformacionLogin();
            this.validarCommand = new DelegateCommand<object>(this.ExecuteValidar, this.CanExecuteValidar);
            this.validarWorker.DoWork += this.ValidarWorkerDoWork;
            this.validarWorker.RunWorkerCompleted += this.ValidarWorkerRunWorkerCompleted;
        }

        public DelegateCommand<object> ValidarCommand
        {
            get
            {
                return this.validarCommand;
            }
        }

        public Func<string, string, bool> AutenticarMethod { get; set; }

        public InformacionLogin InformacionLogin { get; set; }

        private void ValidarWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var pass = e.Argument as string;

            if (!string.IsNullOrWhiteSpace(pass))
            {
                if (this.AutenticarMethod != null)
                {
                    e.Result = this.AutenticarMethod(this.InformacionLogin.NombreDeUsuario, pass);
                }
            }
            else
            {
                e.Result = false;
            }
        }

        private void ValidarWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InformacionLogin.IsBusy = false;
            if (e.Cancelled)
            {
                this.InformacionLogin.LoginCorrecto = false;
            }
            else if (e.Error != null)
            {
                this.InformacionLogin.LoginCorrecto = false;
            }
            else
            {
                bool? resultado = (bool?)e.Result;

                this.InformacionLogin.LoginCorrecto = resultado.HasValue && resultado.Value;

                if (this.InformacionLogin.LoginCorrecto)
                {
                    this.CloseView = true;
                }
            }

            this.InformacionLogin.Validado = true;
        }

        private bool CanExecuteValidar(object arg)
        {
            var passwordBox = arg as PasswordBox;

            if (passwordBox != null)
            {
                if (string.IsNullOrWhiteSpace(passwordBox.Password) || string.IsNullOrWhiteSpace(this.InformacionLogin.NombreDeUsuario))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private void ExecuteValidar(object s)
        {
            var passwordBox = s as PasswordBox;

            if (passwordBox != null)
            {
                this.Autenticar(passwordBox.Password);
            }
        }

        private void Autenticar(string pass)
        {
            this.InformacionLogin.IsBusy = true;
            this.validarWorker.RunWorkerAsync(pass);
        }
    }
}
