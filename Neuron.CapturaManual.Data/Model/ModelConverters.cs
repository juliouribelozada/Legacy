using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron.Satelite.CapturaManual.Data.Model
{
    class ModelConverters
    {
        public static TrackingItem FromDbToTrackingItem(PRO_EstudioTrazabilidad_Result DbItem)
        {
            var output = new TrackingItem
                             {
                                 Description = DbItem.Decripcion,
                                 User = DbItem.Usuario,
                                 EventDateTime = DbItem.Fecha,
                                 EventName = DbItem.Evento
                             };

            return output;
        }
    }
}
