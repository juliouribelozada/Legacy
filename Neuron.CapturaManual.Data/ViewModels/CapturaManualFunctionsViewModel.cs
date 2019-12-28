using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.Satelite.CapturaManual.Data.ViewModels
{
    using Neuron.Satelite.CapturaManual.Data.Model;

    public class CapturaManualFunctionsViewModel : NotifiableObject
    {

        private static volatile CapturaManualFunctionsViewModel instance;
        private static readonly object SyncRoot = new object();

        private CapturaManualFunctionsViewModel() { }

        public static CapturaManualFunctionsViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CapturaManualFunctionsViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        public void UpdateModel()
        {
            this.NotifyPropertyChanged(nameof(this.Anular));
            this.NotifyPropertyChanged(nameof(this.Validar));
        }

        public bool Validar => CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Valida);

        public bool Anular => CapturaManualClaims.ValidateClaims(CapturaManualFunctions.Anular);
    }

}
