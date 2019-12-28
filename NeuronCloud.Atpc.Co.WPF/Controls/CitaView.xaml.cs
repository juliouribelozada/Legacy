// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CitaView.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Vista para una cita.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Microsoft.Practices.Prism.Commands;

    using NeuronCloud.Atpc.Co.Modelos;

    /// <summary>
    /// Vista para una cita.
    /// </summary>
    public partial class CitaView : UserControl
    {
        public static readonly DependencyProperty CitaProperty =
            DependencyProperty.Register("Cita", typeof(Cita), typeof(CitaView), new PropertyMetadata(default(Cita), OnCitaChanged));

        private static readonly TimeSpan DiaTimeSpan = new TimeSpan(1, 0, 0, 0);

        private DateTime inicioDia;

        public CitaView()
        {
            this.InitializeComponent();
        }

        public DelegateCommand<Cita> OnClickMe { get; set; }

        public Cita Cita
        {
            get
            {
                return (Cita)this.GetValue(CitaProperty);
            }

            set
            {
                this.SetValue(CitaProperty, value);
            }
        }

        private static void OnCitaChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((CitaView)dependencyObject).OnCitaChanged();
        }

        private void OnCitaChanged()
        {
            this.inicioDia = this.Cita.Inicio.Date;
            TimeSpan duracionCita = Cita.Fin - Cita.Inicio;
            TimeSpan duracionManana = Cita.Inicio - this.inicioDia;
            float porcientoAntes = (float)duracionManana.Ticks / DiaTimeSpan.Ticks;
            float porcientoCita;
            float porcientoDespues;
            if (duracionCita.Days > 0)
            {
                porcientoCita = 1 - porcientoAntes;
                porcientoDespues = 0;
            }
            else
            {
                porcientoCita = (float)duracionCita.Ticks / DiaTimeSpan.Ticks;
                porcientoDespues = 1 - porcientoCita - porcientoAntes;
            }

            this.Antes.Height = new GridLength(porcientoAntes, GridUnitType.Star);
            this.Despues.Height = new GridLength(porcientoDespues, GridUnitType.Star);
            this.Contenido.Height = new GridLength(porcientoCita, GridUnitType.Star);
        }

        private void BorderElement_Click(object sender, RoutedEventArgs e)
        {
            if (this.OnClickMe != null)
            {
                this.OnClickMe.Execute(this.Cita);
            }
        }
    }
}
