// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewActionsHelper.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the ViewActionsHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF.Helper
{
    using System.ComponentModel;
    using System.Windows;

      public class ViewActionsHelper : DependencyObject
    {
        public static readonly DependencyProperty CerrarVentanaProperty = DependencyProperty.RegisterAttached(
            "CerrarVentana",
            typeof(bool),
            typeof(ViewActionsHelper),
            new PropertyMetadata(
                default(bool),
                PropertyChangedCallback));

          private static void PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
          {
              if (DesignerProperties.GetIsInDesignMode(o))
              {
                  return;
              }

              var window = o as Window;
              if (window != null)
              {
                  if ((bool) args.NewValue)
                  {
                      window.DialogResult = true;
                  }
              }
          }

          public static void SetCerrarVentana(UIElement element, bool value)
        {
            element.SetValue(CerrarVentanaProperty, value);
        }

        public static bool GetCerrarVentana(UIElement element)
        {
            return (bool)element.GetValue(CerrarVentanaProperty);
        }
    }
}
