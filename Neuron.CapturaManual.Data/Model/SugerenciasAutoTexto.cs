// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SugerenciasAutoTexto.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    using System.Windows;

    using ATPC.Controls;

    public class SugerenciasAutoTexto : NotifiableObject
    {
        private string etiqueta;
        private string texto;

        private bool isSelected;

        public string Etiqueta
        {
            get
            {
                return this.etiqueta;
            }

            set
            {
                if (value != this.etiqueta)
                {
                    this.etiqueta = value;
                    this.NotifyPropertyChanged("Etiqueta");
                }
            }
        }

        public string Texto
        {
            get
            {
                return this.texto;
            }

            set
            {
                if (value != this.texto)
                {
                    this.texto = value;
                    this.NotifyPropertyChanged("Texto");
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
                    this.NotifyPropertyChanged("IsSelected");
                }
            }
        }
    }
}
