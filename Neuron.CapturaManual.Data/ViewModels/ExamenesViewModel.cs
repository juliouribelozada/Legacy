// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExamenesViewModel.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;

    using Neuron.Satelite.CapturaManual.Data.Model;

    public class ExamenesViewModel : NotifiableObject
    {
        public ExamenesViewModel()
        {
            this.Secciones = new ObservableCollection<Seccion>();
            this.SeccionesView = CollectionViewSource.GetDefaultView(this.Secciones);
            
            // this.SeccionesView.Filter = FiltroOpcionales;
        }

        public ICollectionView SeccionesView { get; set; }

        public ObservableCollection<Seccion> Secciones { get; set; }

        private bool FiltroOpcionales(object o)
        {
            return true;
        }
    }
}
