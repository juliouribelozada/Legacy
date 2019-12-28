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
    public partial class GeneralUI : UserControl
    {
        public static readonly DependencyProperty InterfazNameProperty =
            DependencyProperty.Register("InterfazName", typeof(string), typeof(GeneralUI), new PropertyMetadata(default(string), InterfazNameChanged));

        public static readonly DependencyProperty MainWindowProperty =
            DependencyProperty.Register("MainWindow", typeof(Window), typeof(GeneralUI), new PropertyMetadata(default(Window)));

        public static readonly DependencyProperty WindowContentProperty =
            DependencyProperty.Register("WindowContent", typeof(FrameworkElement), typeof(GeneralUI), new PropertyMetadata(default(FrameworkElement)));

        public GeneralUI()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public FrameworkElement WindowContent
        {
            get
            {
                return (FrameworkElement)this.GetValue(WindowContentProperty);
            }

            set
            {
                this.SetValue(WindowContentProperty, value);
            }
        }

        public string InterfazName
        {
            get
            {
                return (string)this.GetValue(InterfazNameProperty);
            }

            set
            {
                this.SetValue(InterfazNameProperty, value);
            }
        }

        public Window MainWindow
        {
            get
            {
                return (Window)this.GetValue(MainWindowProperty);
            }

            set
            {
                this.SetValue(MainWindowProperty, value);
            }
        }

        private static void InterfazNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((GeneralUI)d).InterfazNameChanged();
        }

        private void InterfazNameChanged()
        {
            this.TitleTextBlock.Text = this.InterfazName;
        }

        #region Botones de la Barra de Titulo

        private void CloseButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.Close();
            }
        }

        private void MaximizeButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.WindowState = this.MainWindow.WindowState == WindowState.Maximized
                                                  ? WindowState.Normal
                                                  : WindowState.Maximized;
                this.MainWindow.Topmost = this.MainWindow.WindowState == WindowState.Maximized;
            }
        }

        private void ChangeViewButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.WindowState = WindowState.Normal;
                this.MainWindow.Topmost = false;
            }
        }

        private void MinimizeButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        private void DragableAreaMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.DragMove();
            }
        }

        private void ContentControlMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.WindowState = this.MainWindow.WindowState == WindowState.Maximized
                                                  ? WindowState.Normal
                                                  : WindowState.Maximized;
                this.MainWindow.Topmost = this.MainWindow.WindowState == WindowState.Maximized;
            }
        }

        #endregion

        private bool VerifyMainWindow()
        {
            return this.MainWindow != null;
        }
    }
}
