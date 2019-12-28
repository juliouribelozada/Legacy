using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace NeuronCloud.Atpc.Co.Modelos
{
    public class FormaDePago: NotificationObject
    {
        public FormaDePago()
        {
            this.MediosDePago=new List<TipoPago>();
        }

        public string TipoPago { get; set; }
        public string Credito { get; set; }
        public bool? MedioDePago { get; set; }
        public bool? NumeroDeDocumentoDePago { get; set; }
        public string TextoEtiquetaDocumentoDePago { get; set; }

        public List<TipoPago> MediosDePago { get; set; }
    }
}
