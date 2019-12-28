// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechnicianObsViewModel.cs" company="Luis Soler">
//   Copyright © 2012-2015
// </copyright>
// <summary>
//   Defines the TechnicianObsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using ATPC.Controls;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public class TechnicianObsViewModel : NotifiableObject
    {
        private string newObservation;

        private AtpcBindableCommand saveNewObservationCommand;

        public TechnicianObsViewModel(string sampleId, string sourceCode)
        {
            this.SampleId = sampleId;
            this.SourceCode = sourceCode;
        }

        public AtpcBindableCommand SaveNewObservationCommand => this.saveNewObservationCommand ?? (this.saveNewObservationCommand=new AtpcBindableCommand(this.SaveNewObservationAction));

        public ObservableCollection<PRO_EstudioObservacion_Result> Observations { get; } = new ObservableCollection<PRO_EstudioObservacion_Result>();

        public string SampleId { get; }

        public string SourceCode { get; }

        public string NewObservation
        {
            get
            {
                return this.newObservation;
            }

            set
            {
                if (value == this.newObservation)
                {
                    return;
                }
                this.newObservation = value;
                this.NotifyPropertyChanged(nameof(this.NewObservation));
            }
        }

        private void SaveNewObservationAction(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(this.newObservation))
            {
                StoreProceduresOps.SaveTechnicianObsAsync(Tuple.Create(this.SampleId, this.SourceCode,this.User,this.NewObservation),
               (sender, args) =>
               {
                   if (args.Cancelled)
                   {

                   }
                   else if (args.Error != null)
                   {

                   }
                   else
                   {
                       this.NewObservation = string.Empty;
                       this.LoadObservations();
                   }
               });
            }
        }

        public string User { get; set; }

        public void LoadObservations()
        {
            this.Observations.Clear();
            StoreProceduresOps.GetTechnicianObsAsync(Tuple.Create(this.SampleId, this.SourceCode),
                (sender, args) =>
                    {
                        if (args.Cancelled)
                        {

                        }
                        else if (args.Error != null)
                        {

                        }
                        else
                        {
                            var observacionResults = args.Result as List<PRO_EstudioObservacion_Result>;

                            if (observacionResults != null)
                            {
                                foreach (PRO_EstudioObservacion_Result proEstudioObservacionResult in observacionResults)
                                {
                                    this.Observations.Add(proEstudioObservacionResult);
                                }
                            }
                        }
                    });
        }
    }
}
