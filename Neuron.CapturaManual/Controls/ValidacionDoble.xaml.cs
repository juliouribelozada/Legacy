// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidacionDoble.xaml.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for ValidacionDoble.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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

namespace Neuron.Satellite.CapturaManual.Controls
{
    public partial class ValidacionDoble : UserControl
    {
        public static readonly DependencyProperty ValidatedValueProperty = DependencyProperty.Register("ValidatedValue", typeof(string), typeof(ValidacionDoble), new PropertyMetadata(default(string), ValidatedValueChanged));
        private readonly DummyContext viewModel = new DummyContext();
        private readonly ValidationError error;

        public ValidacionDoble()
        {
            this.InitializeComponent();
            this.root.DataContext = this.viewModel;
            this.error = new ValidationError(new ExceptionValidationRule(), this.FirstText.GetBindingExpression(TextBox.TextProperty));
            this.error.ErrorContent = "Los datos Ingresados no son Iguales";
            this.viewModel.Texto1 = this.ValidatedValue;
        }

        public string ValidatedValue
        {
            get
            {
                return (string)this.GetValue(ValidatedValueProperty);
            }

            set
            {
                this.SetValue(ValidatedValueProperty, value);
            }
        }

        private static void ValidatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValidacionDoble)d).ValidatedValueChanged(e);
        }

        private void ValidatedValueChanged(DependencyPropertyChangedEventArgs e)
        {
            this.viewModel.Texto1 = this.ValidatedValue;
        }

        private void FirstText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Tab)
            {
                //if (!string.IsNullOrWhiteSpace(FirstText.Text))
                //{
                    if (FirstText.Text != this.ValidatedValue)
                    {
                        this.PrimerPanel.Visibility = Visibility.Hidden;
                        this.SegundoPanel.Visibility = Visibility.Visible;
                        e.Handled = true;
                        this.SecondText.Focus();
                    }
                //}
            }
        }

        private void SecondText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Tab)
            {
                this.PrimerPanel.Visibility = Visibility.Visible;
                this.SegundoPanel.Visibility = Visibility.Hidden;
                if (this.FirstText.Text == this.SecondText.Text)
                {
                    this.ValidatedValue = this.FirstText.Text;
                    Validation.ClearInvalid(this.FirstText.GetBindingExpression(TextBox.TextProperty));
                    this.viewModel.Mensaje = "OK";
                }
                else
                {
                    Validation.MarkInvalid(this.FirstText.GetBindingExpression(TextBox.TextProperty), this.error);
                    this.viewModel.Mensaje = "Los Valores Digitados no Coinciden.\nIntentelo de Nuevo";
                    e.Handled = true;
                    this.FirstText.Focus();
                }

                this.SecondText.Text = string.Empty;
            }
        }

        private void FirstText_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SecondText_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
