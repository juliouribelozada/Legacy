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
using System.Windows.Shapes;

namespace NeuronCloud.Atpc.Co.WPF
{
    /// <summary>
    /// Interaction logic for AutorizacionWindow.xaml
    /// </summary>
    public partial class AutorizacionWindow : NeuronMainWindow
    {
        public AutorizacionWindow()
        {
            this.InitializeComponent();
        }

        private void PassBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ValidarCommand.RaiseCanExecuteChanged();
        }

        private void PassKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                var box = sender as PasswordBox;

                if (box != null)
                {
                    this.ViewModel.ValidarCommand.Execute(box);
                }
            }
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.ViewModel.ValidarCommand.RaiseCanExecuteChanged();
        }

        private void NeuronMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UserNameTxt.Focus();
        }
    }
}
