// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterKeyHelpers.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.Satellite.CapturaManual.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public static class EnterKeyHelpers
    {
        #region EnterUpdatesTextSource DependencyProperty

        public static readonly DependencyProperty EnterUpdatesTextSourceProperty = DependencyProperty.RegisterAttached("EnterUpdatesTextSource", typeof(bool), typeof(EnterKeyHelpers), new PropertyMetadata(false, EnterUpdatesTextSourcePropertyChanged));

        public static bool GetEnterUpdatesTextSource(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnterUpdatesTextSourceProperty);
        }

        public static void SetEnterUpdatesTextSource(DependencyObject obj, bool value)
        {
            obj.SetValue(EnterUpdatesTextSourceProperty, value);
        }

        private static void EnterUpdatesTextSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var sender = obj as UIElement;
            if (obj != null)
            {
                if ((bool)e.NewValue)
                {
                    if (sender != null)
                    {
                        sender.PreviewKeyDown += OnPreviewKeyDownUpdateSourceIfEnter;
                    }
                }
                else
                {
                    if (sender != null)
                    {
                        sender.PreviewKeyDown -= OnPreviewKeyDownUpdateSourceIfEnter;
                    }
                }
            }
        }

        // If key being pressed is the Enter key, and EnterUpdatesTextSource is set to true, then update source for Text property 
        private static void OnPreviewKeyDownUpdateSourceIfEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (GetEnterUpdatesTextSource((DependencyObject)sender))
                {
                    var obj = sender as UIElement;
                    if (obj != null)
                    {
                        if (obj is TextBox)
                        {
                            BindingExpression textBinding = BindingOperations.GetBindingExpression(
                                obj, TextBox.TextProperty);

                            if (textBinding != null)
                            {
                                textBinding.UpdateSource();
                            }
                        }
                        else if (obj is AutoCompleteBox)
                        {
                            BindingExpression textBinding = BindingOperations.GetBindingExpression(obj, AutoCompleteBox.TextProperty);

                            if (textBinding != null)
                            {
                                textBinding.UpdateSource();
                            }
                        }
                    }
                }
            }
        }

        #endregion //EnterUpdatesTextSource DependencyProperty
    }
}
