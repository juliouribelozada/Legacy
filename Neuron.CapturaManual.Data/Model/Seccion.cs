// -----------------------------------------------------------------------
// <copyright file="Seccion.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Seccion : NotifiableObject
    {
        private readonly List<PRO_CargaCapturaSeccion_Result> examenes = new List<PRO_CargaCapturaSeccion_Result>();

        private string name;

        private bool isSelected;

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.NotifyPropertyChanged(() => this.Name);
                }
            }
        }

        public List<PRO_CargaCapturaSeccion_Result> Examenes
        {
            get
            {
                return this.examenes;
            }

            set
            {
                if (value != this.examenes)
                {
                    this.Examenes.Clear();
                    if (value != null)
                    {
                        foreach (PRO_CargaCapturaSeccion_Result seccionResult in value)
                        {
                            this.Examenes.Add(seccionResult);
                        }
                    }

                    this.NotifyPropertyChanged("ExamenesView");
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    this.NotifyPropertyChanged(() => this.IsSelected);
                }
            }
        }
    }
}
