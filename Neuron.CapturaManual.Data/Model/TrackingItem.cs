namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;

    public class TrackingItem : NotifiableObject
    {
        private string description;

        private DateTime? eventDateTime;

        private string user;

        private string eventName;

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value == this.description)
                {
                    return;
                }

                this.description = value;
                this.NotifyPropertyChanged(nameof(this.Description));
            }
        }

        public DateTime? EventDateTime
        {
            get
            {
                return this.eventDateTime;
            }
            set
            {
                if (value.Equals(this.eventDateTime))
                {
                    return;
                }
                this.eventDateTime = value;
                this.NotifyPropertyChanged(nameof(this.EventDateTime));
            }
        }

        public string User
        {
            get
            {
                return this.user;
            }
            set
            {
                if (value == this.user)
                {
                    return;
                }
                this.user = value;
                this.NotifyPropertyChanged(nameof(this.User));
            }
        }

        public string EventName
        {
            get
            {
                return this.eventName;
            }
            set
            {
                if (value == this.eventName)
                {
                    return;
                }
                this.eventName = value;
                this.NotifyPropertyChanged(nameof(this.EventName));
            }
        }
    }
}