using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Data.Objects.DataClasses;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public class OptionalAnalitesViewModel : NotifiableObject
    {
        private readonly ObservableCollection<ComplexObject> analites = new ObservableCollection<ComplexObject>();

        private readonly ObservableCollection<ComplexObject> panelsCollection = new ObservableCollection<ComplexObject>();

        private ObservableCollection<Panel> panels;

        private Panel selectedPanel;

        public ObservableCollection<ComplexObject> Analites
        {
            get
            {
                return this.analites;
            }
        }

        public ObservableCollection<ComplexObject> PanelsCollection
        {
            get
            {
                return this.panelsCollection;
            }
        }

        public Panel SelectedPanel
        {
            get
            {
                return this.selectedPanel;
            }

            set
            {
                if (value == this.selectedPanel)
                {
                    return;
                }
                this.selectedPanel = value;
                this.NotifyPropertyChanged("SelectedPanel");
                this.UpdateAnalites();
            }
        }

        public ObservableCollection<Panel> Panels
        {
            get
            {
                return this.panels;
            }

            set
            {
                if (Equals(value, this.panels))
                {
                    return;
                }

                this.panels = value;
                this.NotifyPropertyChanged("Panels");
            }
        }

        public void UpdatePanels()
        {
            IEnumerable<Panel> panel = from resultados in this.PanelsCollection.Cast<PRO_CargaCapturaPanelOpcional_Result>()
                                       group resultados by resultados.NomPanel
                                           into grupo
                                           select new Panel { Label = grupo.Key, AnalitosList = grupo.Select(result => result.Analito).ToList(), SelectedAction = this.SelectedAction };

            this.Panels = new ObservableCollection<Panel>(panel);
        }

        private void UpdateAnalites()
        {
            foreach (var panel in this.Panels)
            {
                panel.Selected = false;
            }

            if (this.SelectedPanel != null)
            {
                this.SelectedPanel.Selected = true;
            }
        }

        private void SelectedAction(Panel panel1)
        {
            var analitesFiltered = this.Analites.OfType<PRO_CargaCapturaCuatroCampos_Result>().Where(af => panel1.AnalitosList.Any(y => y == af.Analito));

            foreach (var camposResult in analitesFiltered)
            {
                camposResult.SelectedToShow = panel1.Selected;
            }
        }
    }
}
