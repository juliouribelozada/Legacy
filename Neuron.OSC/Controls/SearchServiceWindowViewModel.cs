// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchServiceWindowViewModel.cs" company="NeuronCloud.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.OSC.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using Neuron.OSC.Data;
    using Neuron.OSC.Data.AsyncOps;
    using Neuron.OSC.Data.Model;

    public class SearchServiceWindowViewModel : NotificationObject
    {
        private readonly DelegateCommand seleccionarCommand;

        private readonly ObservableCollection<PRO_ConvenioPrestPortafoSeleccAutoComplete_Result> sugestions = new ObservableCollection<PRO_ConvenioPrestPortafoSeleccAutoComplete_Result>();

        private bool closeView;

        private SearchServiceParameters parameters;

        private PRO_ConvenioPrestPortafoSeleccAutoComplete_Result selectedSugestion;

        private bool setFocus;

        public SearchServiceWindowViewModel()
        {
            this.seleccionarCommand = new DelegateCommand(this.SeleccionarCommandExecute, this.SeleccionarCommandCanExecute);
        }

        public event EventHandler ListaCargada;

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

        public DelegateCommand SeleccionarCommand
        {
            get
            {
                return this.seleccionarCommand;
            }
        }

        public bool SetFocus
        {
            get
            {
                return this.setFocus;
            }

            set
            {
                if (value.Equals(this.setFocus))
                {
                    return;
                }

                this.setFocus = value;
                this.RaisePropertyChanged("SetFocus");
            }
        }

        public SearchServiceParameters Parameters
        {
            get
            {
                return this.parameters;
            }

            set
            {
                if (value == this.parameters)
                {
                    return;
                }

                this.parameters = value;
                this.SeleccionarCommand.RaiseCanExecuteChanged();
                this.RaisePropertyChanged("Parameters");
            }
        }

        public PRO_ConvenioPrestPortafoSeleccAutoComplete_Result SelectedSugestion
        {
            get
            {
                return this.selectedSugestion;
            }

            set
            {
                if (value == this.selectedSugestion)
                {
                    return;
                }

                this.selectedSugestion = value;
                this.RaisePropertyChanged("SelectedSugestion");
                this.SeleccionarCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PRO_ConvenioPrestPortafoSeleccAutoComplete_Result> Sugestions
        {
            get
            {
                return this.sugestions;
            }
        }

        public void LoadSugestions(string hint)
        {
            this.Parameters.SearchChars = hint;
            StoreProceduresAsync.GetServiceCodeByName(
                   this.parameters,
                   (ss, ee) =>
                   {
                       Trace.WriteLine("Resultado Para :" + this.Parameters.SearchChars);
                       if (ee.Cancelled)
                       {
                           Trace.WriteLine("Lista Cancelada");
                       }
                       else if (ee.Error != null)
                       {
                           Trace.WriteLine("Lista Error: + " + ee.Error.Message);
                       }
                       else
                       {
                           var service = ee.Result as List<PRO_ConvenioPrestPortafoSeleccAutoComplete_Result>;
                           if (service != null)
                           {
                               Trace.WriteLine("Lista: " + service.Count);
                               this.Sugestions.Clear();
                               foreach (var autoCompleteResult in service)
                               {
                                   this.Sugestions.Add(autoCompleteResult);
                               }
                           }
                       }

                       this.OnListaCargada();
                   });
        }

        protected virtual void OnListaCargada()
        {
            EventHandler handler = this.ListaCargada;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void SeleccionarCommandExecute()
        {
            this.CloseView = true;
        }

        private bool SeleccionarCommandCanExecute()
        {
            return this.SelectedSugestion != null;
        }
    }
}
