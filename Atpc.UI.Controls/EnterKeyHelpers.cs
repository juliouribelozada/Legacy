// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterKeyHelpers.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the EnterKeyHelpers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    public static class EnterKeyHelpers
    {

        public static readonly DependencyProperty EnterUpdatesTextSourceProperty = DependencyProperty.RegisterAttached("EnterUpdatesTextSource", typeof(bool), typeof(EnterKeyHelpers), new PropertyMetadata(false, EnterUpdatesTextSourcePropertyChanged));

        public static readonly DependencyProperty SetFocusProperty = DependencyProperty.RegisterAttached("SetFocus", typeof(bool), typeof(EnterKeyHelpers), new PropertyMetadata(false, SetFocusPropertyChanged));

        public static readonly DependencyProperty SetFocusAndClearProperty = DependencyProperty.RegisterAttached("SetFocusAndClear", typeof(bool), typeof(EnterKeyHelpers), new PropertyMetadata(false, SetFocusAndClearPropertyChanged));

        public static readonly DependencyProperty SetFocusDelayedProperty = DependencyProperty.RegisterAttached("SetFocusDelayed", typeof(bool), typeof(EnterKeyHelpers), new PropertyMetadata(false, SetFocusDelayedPropertyChanged));

        public static bool GetEnterUpdatesTextSource(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnterUpdatesTextSourceProperty);
        }

        public static void SetEnterUpdatesTextSource(DependencyObject obj, bool value)
        {
            obj.SetValue(EnterUpdatesTextSourceProperty, value);
        }

        public static bool GetSetFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SetFocusProperty);
        }

        public static void SetSetFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SetFocusProperty, value);
        }

        public static bool GetSetFocusAndClear(DependencyObject obj)
        {
            return (bool)obj.GetValue(SetFocusAndClearProperty);
        }

        public static void SetSetFocusAndClear(DependencyObject obj, bool value)
        {
            obj.SetValue(SetFocusAndClearProperty, value);
        }

        public static bool GetSetFocusDelayed(DependencyObject obj)
        {
            return (bool)obj.GetValue(SetFocusDelayedProperty);
        }

        public static void SetSetFocusDelayed(DependencyObject obj, bool value)
        {
            obj.SetValue(SetFocusDelayedProperty, value);
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

        private static void SetFocusPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var sender = obj as UIElement;
            if (obj != null)
            {
                if (!(bool)e.NewValue)
                {
                    TextBox textBox = sender as TextBox;
                    if (textBox != null)
                    {
                        Debug.WriteLine("TextBox: " + textBox.Name + " - " + textBox.IsEnabled);
                        textBox.Focus();
                        textBox.SelectAll();
                        BindingExpression textBinding = BindingOperations.GetBindingExpression(obj, TextBox.TextProperty);

                        if (textBinding != null)
                        {
                            textBinding.UpdateSource();
                        }
                    }
                }
            }
        }

        private static void SetFocusAndClearPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var sender = obj as UIElement;
            if (obj != null)
            {
                if (!(bool)e.NewValue)
                {
                    TextBox textBox = sender as TextBox;
                    if (textBox != null)
                    {
                        Debug.WriteLine("TextBox: " + textBox.Name + " - " + textBox.IsEnabled);
                        textBox.Focus();
                        textBox.Clear();
                        BindingExpression textBinding = BindingOperations.GetBindingExpression(obj, TextBox.TextProperty);

                        if (textBinding != null)
                        {
                            textBinding.UpdateSource();
                        }
                    }
                }
            }
        }

        private static void SetFocusDelayedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var sender = obj as UIElement;
            if (obj != null)
            {
                if (!(bool)e.NewValue)
                {
                    TextBox textBox = sender as TextBox;
                    if (textBox != null)
                    {
                        Debug.WriteLine("TextBox: " + textBox.Name + " - " + textBox.IsEnabled);
                        textBox.Dispatcher.BeginInvoke(
                            DispatcherPriority.Background,
                            new Action(
                                () =>
                                {
                                    if (textBox.IsEnabled)
                                    {
                                        textBox.Focus();
                                        textBox.SelectAll();
                                        BindingExpression textBinding =
                                            BindingOperations.GetBindingExpression(obj, TextBox.TextProperty);

                                        if (textBinding != null)
                                        {
                                            textBinding.UpdateSource();
                                        }
                                    }
                                }));

                    }
                }
                return;
            }

           // var senderAuto = obj as ;

        }

        // If key being pressed is the Enter key, and EnterUpdatesTextSource is set to true, then update source for Text property 
        private static void OnPreviewKeyDownUpdateSourceIfEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
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
                    }
                }
            }
        }
    }
}