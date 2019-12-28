// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarioDiaView.xaml.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Muestra las citas de Un dia Dado.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Globalization;
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
    using System.Windows.Threading;

    using Microsoft.Practices.Prism.Commands;

    using NeuronCloud.Atpc.Co.Modelos;
    using NeuronCloud.Atpc.Co.WPF.Helper;

    /// <summary>
    /// Muestra las citas de Un dia Dado.
    /// </summary>
    public partial class CalendarioDiaView : UserControl
    {
        public static readonly DependencyProperty DiaProperty =
            DependencyProperty.Register("Dia", typeof(DateTime?), typeof(CalendarioDiaView), new PropertyMetadata(default(DateTime?), OnDiaChanged));

        public static readonly DependencyProperty CitasProperty =
            DependencyProperty.Register("Citas", typeof(ObservableCollection<Cita>), typeof(CalendarioDiaView), new PropertyMetadata(default(ObservableCollection<Cita>), OnCitasChanged));

        public static readonly DependencyProperty OnCitaSeleccionadaProperty =
            DependencyProperty.Register("OnCitaSeleccionada", typeof(DelegateCommand<Cita>), typeof(CalendarioDiaView), new PropertyMetadata(default(DelegateCommand<Cita>)));

        private readonly ObservableCollection<Cita> citasDibujadas = new ObservableCollection<Cita>();

        public CalendarioDiaView()
        {
            this.InitializeComponent();
            this.Root.DataContext = this;
            CultureInfo cultureInfo = GeneralHelper.GetCulture(this);
        }

        public DelegateCommand<Cita> OnCitaSeleccionada
        {
            get
            {
                return (DelegateCommand<Cita>)this.GetValue(OnCitaSeleccionadaProperty);
            }

            set
            {
                this.SetValue(OnCitaSeleccionadaProperty, value);
            }
        }

        public ObservableCollection<Cita> Citas
        {
            get
            {
                return (ObservableCollection<Cita>)this.GetValue(CitasProperty);
            }

            set
            {
                this.SetValue(CitasProperty, value);
            }
        }

        public DateTime? Dia
        {
            get
            {
                return (DateTime?)this.GetValue(DiaProperty);
            }

            set
            {
                this.SetValue(DiaProperty, value);
            }
        }

        private static void OnCitasChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((CalendarioDiaView)dependencyObject).OnCitasChanged(dependencyPropertyChangedEventArgs);
        }

        private static void OnDiaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CalendarioDiaView)d).OnDiaChanged();
        }

        private void OnDiaChanged()
        {
        }

        private void OnCitasChanged(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var coleccionVieja = dependencyPropertyChangedEventArgs.OldValue as ObservableCollection<Cita>;

            if (coleccionVieja != null)
            {
                try
                {
                    coleccionVieja.CollectionChanged -= this.CitasCollectionChanged;
                }
                catch
                {
                    Debug.WriteLine("No Tiene Handler");
                }
            }

            var coleccionNueva = dependencyPropertyChangedEventArgs.NewValue as ObservableCollection<Cita>;

            if (coleccionNueva != null)
            {
                coleccionNueva.CollectionChanged += this.CitasCollectionChanged;
            }

            this.Redraw();
        }

        private void CitasCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Cita cita in e.NewItems)
                    {
                        Cita cita1 = cita;
                        this.Dispatcher.BeginInvoke(
                            DispatcherPriority.Render,
                            new Action(() =>
                            {
                                var vistaCita = new CitaView { Cita = cita1, OnClickMe = new DelegateCommand<Cita>(ExecuteMethod) };
                                this.CitasRoot.Children.Add(vistaCita);
                                this.Container.ScrollToVerticalOffset(this.Container.ActualHeight / 2);
                            }));
                    }

                    Debug.WriteLine("Cita : Add");
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Debug.WriteLine("Cita : Remove");
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Debug.WriteLine("Cita : Replace");
                    break;

                case NotifyCollectionChangedAction.Move:
                    Debug.WriteLine("Cita : Move");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Debug.WriteLine("Cita : Reset");
                    this.CitasRoot.Children.Clear();
                    this.citasDibujadas.Clear();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ExecuteMethod(Cita cita)
        {
            if (this.OnCitaSeleccionada != null)
            {
                this.OnCitaSeleccionada.Execute(cita);
            }
        }

        private void Redraw()
        {
            this.CitasRoot.Children.Clear();
            foreach (Cita cita in this.Citas)
            {
                Cita cita1 = cita;
                this.Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        var vistaCita = new CitaView
                            {
                                Cita = cita1,
                                OnClickMe = new DelegateCommand<Cita>(ExecuteMethod)
                            };
                        this.CitasRoot.Children.Add(vistaCita);
                    }));
            }

            this.Container.ScrollToVerticalOffset(this.Container.ActualHeight / 2);
        }
    }
}
