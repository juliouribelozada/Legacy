// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabeledTextBox.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the LabeledTextBox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using NeuronCloud.Atpc.Co.Modelos.Helpers;
    using NeuronCloud.Atpc.Co.WPF.Helper;

    public class LabeledTextBox : TextBox
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledTextBox), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Dock), typeof(LabeledTextBox), new PropertyMetadata(Dock.Top));

        public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(LabeledTextBox), new PropertyMetadata(default(Visibility)));

        static LabeledTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabeledTextBox), new FrameworkPropertyMetadata(typeof(LabeledTextBox)));
        }

        public LabeledTextBox()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Label = this.DataContext != null ? this.DataContext.GetType().ToString() : "Unbound";
                return;
            }

            this.Initialized += this.LabeledTextBoxInitialized;
            this.DataContextChanged += this.LabeledTextBox_DataContextChanged;
        }

        public Dock Orientation
        {
            get
            {
                return (Dock)this.GetValue(OrientationProperty);
            }

            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        public Visibility LabelVisibility
        {
            get
            {
                return (Visibility)this.GetValue(LabelVisibilityProperty);
            }

            set
            {
                this.SetValue(LabelVisibilityProperty, value);
            }
        }

        public string Label
        {
            get
            {
                return (string)this.GetValue(LabelProperty);
            }

            set
            {
                this.SetValue(LabelProperty, value);
            }
        }

        private void LabeledTextBoxInitialized(object sender, EventArgs e)
        {
            this.GetLabelValue();
        }

        private void LabeledTextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.GetLabelValue();
        }

        private void GetLabelValue()
        {
            var binding = this.GetBindingExpression(TextProperty);
            if (binding != null && binding.ParentBinding != null && binding.ParentBinding.Path != null)
            {
                var entity = binding.DataItem ?? this.DataContext;
                if (entity != null)
                {
                    Metadatos metadatos = MetadatosHelper.ParseMetadata(binding.ParentBinding.Path.Path, entity);
                    if (metadatos != null)
                    {
                        this.Label = metadatos.Nombre + ": ";
                    }
                }
            }
        }
    }
}
