using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace NeuronCloud.Atpc.Co.Modelos
{
    public class TipoPago: NotificationObject
    {
        public string MedioDePago { get; set; }
    }
}
