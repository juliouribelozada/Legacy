namespace NeuronCloud.Atpc.Co.WPF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Calendario : Calendar
    {
        private const string ElementMonth = "PART_CalendarItem";

        private readonly ObservableCollection<DateTime> fechasPermitidas;

        private CalendarItem monthControl;

        public Calendario()
        {
            this.fechasPermitidas = new ObservableCollection<DateTime>();
        }

        public ObservableCollection<DateTime> FechasPermitidas
        {
            get
            {
                return this.fechasPermitidas;
            }
        }

        internal CalendarItem MonthControl
        {
            get { return this.monthControl; }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.monthControl = GetTemplateChild(ElementMonth) as CalendarItem;
        }

        private void ActualizarDiasMarcados()
        {
            var mesControl = this.MonthControl;
            if (mesControl != null)
            {
            }
        }
    }
}
