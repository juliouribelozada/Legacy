// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlertDetailWindowViewModel.cs" company="NeuronCloud.co">
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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using Neuron.OSC.Data;
    using Neuron.OSC.Data.AsyncOps;
    using Neuron.OSC.Data.Model;

    public class AlertDetailWindowViewModel : NotificationObject
    {
        private readonly ObservableCollection<Antecedente> antecedentes;

        private bool closeView;

        public AlertDetailWindowViewModel(IEnumerable<Antecedente> antecedentes)
        {
            this.antecedentes = new ObservableCollection<Antecedente>(antecedentes);
        }

        public AlertDetailWindowViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        public ObservableCollection<Antecedente> Antecedentes
        {
            get
            {
                return this.antecedentes;
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
    }
}
