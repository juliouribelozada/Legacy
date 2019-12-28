using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    using Neuron.Satelite.CapturaManual.Data.AsyncOps;
    using Neuron.Satelite.CapturaManual.Data.Model;

    public class TrackingInfoViewModel : NotifiableObject
    {
        private string analyte;

        private string originCode;

        private string sampleId;

        public ObservableCollection<TrackingItem> TrackingItems { get; } = new ObservableCollection<TrackingItem>();

        public void DoTracking()
        {
            var sampleParameters = Tuple.Create(this.SampleId, this.OriginCode, this.Analyte);
            StoreProceduresOps.GetSampleTracking(sampleParameters,
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
                            var trackingItems = args.Result as List<TrackingItem>;

                            if (trackingItems != null)
                            {
                                foreach (TrackingItem trackingItem in trackingItems)
                                {
                                    this.TrackingItems.Add(trackingItem);
                                }
                            }
                        }
                    });
        }

        public string Analyte
        {
            get
            {
                return this.analyte;
            }
            set
            {
                if (value == this.analyte)
                {
                    return;
                }
                this.analyte = value;
                this.NotifyPropertyChanged(nameof(this.Analyte));
            }
        }

        public string OriginCode
        {
            get
            {
                return this.originCode;
            }
            set
            {
                if (value == this.originCode)
                {
                    return;
                }
                this.originCode = value;
                this.NotifyPropertyChanged(nameof(this.OriginCode));
            }
        }

        public string SampleId
        {
            get
            {
                return this.sampleId;
            }
            set
            {
                if (value == this.sampleId)
                {
                    return;
                }
                this.sampleId = value;
                this.NotifyPropertyChanged(nameof(this.SampleId));
            }
        }
    }
}
