// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlDeCitas.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for ControlDeCitas.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using NeuronCloud.Atpc.Co.Modelos;

    /// <summary>
    /// Interaction logic for ControlDeCitas.xaml
    /// </summary>
    public partial class ControlDeCitas : NeuronMainWindow
    {
        public ControlDeCitas()
        {
            this.InitializeComponent();
            this.calendar1.SelectedDatesChanged += this.Calendar1SelectedDatesChanged;
            this.ViewModel.MostrarMensajeEnUI += (o, s) => MessageBox.Show(this, s);
        }

        private void Calendar1SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.calendar1.SelectedDate.HasValue)
            {

            }
        }

        private void Calendar1PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void Calendar1DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            if (calendar != null)
            {
                Debug.WriteLine(calendar.DisplayDate);
            }
        }
    }
}
