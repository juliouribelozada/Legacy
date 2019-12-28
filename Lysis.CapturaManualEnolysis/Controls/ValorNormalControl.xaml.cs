// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValorNormalControl.xaml.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for ValorNormalControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Neuron.Satelite.CapturaManual.Data;
    using Neuron.Satelite.CapturaManual.Data.Model;

    /// <summary>
    /// Interaction logic for ValorNormalControl.xaml
    /// </summary>
    public partial class ValorNormalControl : UserControl
    {
        public static readonly DependencyProperty ValorNormalProperty = DependencyProperty.Register("ValorNormal", typeof(EstudioValorNormal), typeof(ValorNormalControl), new PropertyMetadata(default(EstudioValorNormal), OnRangeChanged));

        public static readonly DependencyProperty FlagProperty = DependencyProperty.Register("Flag", typeof(AnalyteAbnormalFlags), typeof(ValorNormalControl), new PropertyMetadata(default(AnalyteAbnormalFlags), OnValueChanged));

        public ValorNormalControl()
        {
            InitializeComponent();
            this.RenderFlags();
        }

        public AnalyteAbnormalFlags Flag
        {
            get
            {
                return (AnalyteAbnormalFlags)GetValue(FlagProperty);
            }

            set
            {
                SetValue(FlagProperty, value);
            }
        }

        public EstudioValorNormal ValorNormal
        {
            get
            {
                return (EstudioValorNormal)this.GetValue(ValorNormalProperty);
            }

            set
            {
                this.SetValue(ValorNormalProperty, value);
            }
        }

        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ValorNormalControl)d).OnRangeChanged(args);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ValorNormalControl)d).OnValueChanged(args);
        }

        private void OnRangeChanged(DependencyPropertyChangedEventArgs args)
        {
            this.RenderFlags();
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs args)
        {
            this.RenderFlags();
        }

        private void RenderFlags()
        {
            this.UpAlarm.Visibility = Visibility.Collapsed;
            this.UpNormal.Visibility = Visibility.Collapsed;
            this.DownAlarm.Visibility = Visibility.Collapsed;
            this.DownNormal.Visibility = Visibility.Collapsed;
            this.Normal.Visibility = Visibility.Collapsed;
            this.ValorNoValido.Visibility = Visibility.Collapsed;
            this.Nada.Visibility = Visibility.Collapsed;
            this.Anormal.Visibility = Visibility.Collapsed;

            switch (this.Flag)
            {
                case AnalyteAbnormalFlags.None:
                    this.Nada.Visibility = Visibility.Visible;
                    break;
                case AnalyteAbnormalFlags.Normal:
                    this.Normal.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.Invalid:
                    this.ValorNoValido.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.Abnormal:
                    this.Anormal.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.Low:
                    this.DownNormal.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.AlarmLow:
                    this.DownAlarm.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.High:
                    this.UpNormal.Visibility = Visibility.Visible;
                    break;

                case AnalyteAbnormalFlags.AlarmHigh:
                    this.UpAlarm.Visibility = Visibility.Visible;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
