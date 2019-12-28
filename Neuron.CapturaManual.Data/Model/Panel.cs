// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Panel.cs" company="Luis Soler">
//   Copyright © 2012-2014
// </copyright>
// <summary>
//   Defines the Panel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Panel : NotifiableObject
    {
        private bool selected;

        private string label;

        private List<string> analitosList;

        private Action<Panel> selectedAction;

        public Action<Panel> SelectedAction
        {
            get
            {
                return this.selectedAction;
            }

            set
            {
                if (Equals(value, this.selectedAction))
                {
                    return;
                }

                this.selectedAction = value;
                this.NotifyPropertyChanged("SelectedAction");
            }
        }

        public bool Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                if (value != this.Selected)
                {
                    this.selected = value;
                    this.NotifyPropertyChanged("Selected");
                    this.SelectedAction(this);
                }
            }
        }

        public string Label
        {
            get
            {
                return this.label;
            }

            set
            {
                if (value == this.label)
                {
                    return;
                }
            
                this.label = value;
                this.NotifyPropertyChanged("Label");
            }
        }

        public List<string> AnalitosList
        {
            get
            {
                return this.analitosList;
            }
            set
            {
                if (Equals(value, this.analitosList))
                {
                    return;
                }

                this.analitosList = value;
                this.NotifyPropertyChanged("AnalitosList");
            }
        }
    }
}
