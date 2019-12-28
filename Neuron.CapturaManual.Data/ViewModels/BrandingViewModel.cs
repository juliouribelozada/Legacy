// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrandingViewModel.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BrandingViewModel : NotifiableObject
    {
        private BitmapImage logo;

        public BrandingViewModel()
        {
            FileInfo logoFile = new FileInfo("comm.config");
            this.Logo = logoFile.Exists ? 
                new BitmapImage(new Uri(logoFile.FullName, UriKind.RelativeOrAbsolute)) : 
                new BitmapImage(new Uri(@"pack://application:,,,/Lysis.CapturaManual;component/Resources/Lysis2.png"));
        }

        public BitmapImage Logo
        {
            get
            {
                return this.logo;
            }

            set
            {
                if (value != this.logo)
                {
                    this.logo = value;
                    this.NotifyPropertyChanged("Logo");
                }
            }
        }
    }
}
