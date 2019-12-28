// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronHeader.xaml.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Interaction logic for NeuronHeader.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.UI.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for NeuronHeader.xaml
    /// </summary>
    public partial class NeuronHeader : UserControl
    {
        public static readonly DependencyProperty MainWindowProperty = DependencyProperty.Register("MainWindow", typeof(Window), typeof(NeuronHeader), new PropertyMetadata(default(Window), OnMainWindowChanged));

        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(NeuronHeader), new PropertyMetadata(default(string), HeaderTextChanged));

        public NeuronHeader()
        {
            this.InitializeComponent();
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

        public string HeaderText
        {
            get
            {
                return (string)this.GetValue(HeaderTextProperty);
            }

            set
            {
                this.SetValue(HeaderTextProperty, value);
            }
        }

        private static void HeaderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NeuronHeader)d).HeaderTextChanged();
        }

        private static void OnMainWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NeuronHeader)d).OnMainWindowChanged();
        }

        private void OnMainWindowChanged()
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.Title = this.HeaderText;
            }
        }

        private void HeaderTextChanged()
        {
            this.TitleTextBlock.Text = this.HeaderText;
            if (this.VerifyMainWindow())
            {
                this.MainWindow.Title = this.HeaderText;
            }
        }

        private void DragableAreaMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.VerifyMainWindow())
            {
                this.MainWindow.DragMove();
            }
        }

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

        private bool VerifyMainWindow()
        {
            return this.MainWindow != null;
        }
    }
}
