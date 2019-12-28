// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuronMainWindow.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the NeuronMainWindow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.WPF
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Shapes;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed. Suppression is OK here.")]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_NormalButton", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_MinimizeButton", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_MaximizeButton", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_NResizer", Type = typeof(Path))]
    [TemplatePart(Name = "PART_SResizer", Type = typeof(Path))]
    [TemplatePart(Name = "PART_EResizer", Type = typeof(Path))]
    [TemplatePart(Name = "PART_WResizer", Type = typeof(Path))]
    [TemplatePart(Name = "PART_NWResizer", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_NEResizer", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SWResizer", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SEResizer", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_TitleBar", Type = typeof(Panel))]
    [TemplateVisualState(GroupName = NeuronMainWindowVisualStates.WindowAnimationStatesGroup, Name = NeuronMainWindowVisualStates.Invisible)]
    [TemplateVisualState(GroupName = NeuronMainWindowVisualStates.WindowAnimationStatesGroup, Name = NeuronMainWindowVisualStates.Normal)]
    [StyleTypedProperty(Property = "TextoTituloStyle", StyleTargetType = typeof(TextBlock))]
    public class NeuronMainWindow : Window
    {
        public static readonly DependencyProperty ColorBarraTituloProperty = DependencyProperty.Register("ColorBarraTitulo", typeof(Brush), typeof(NeuronMainWindow), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty TamanoIconoTituloProperty = DependencyProperty.Register("TamanoIconoTitulo", typeof(double), typeof(NeuronMainWindow), new PropertyMetadata(50.0));

        public static readonly DependencyProperty TextoTituloStyleProperty = DependencyProperty.Register("TextoTituloStyle", typeof(Style), typeof(NeuronMainWindow), new PropertyMetadata(default(Style)));

        private const int SC_SIZE = 0xF000;
        private const int WM_SYSCOMMAND = 0x112;

        private WindowInteropHelper interopHelper;

        private bool isModal;

        static NeuronMainWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NeuronMainWindow), new FrameworkPropertyMetadata(typeof(NeuronMainWindow)));
        }

        public enum SizingAction
        {
            North = 3,
            South = 6,
            East = 2,
            West = 1,
            NorthEast = 5,
            NorthWest = 4,
            SouthEast = 8,
            SouthWest = 7
        }

        public Brush ColorBarraTitulo
        {
            get
            {
                return (Brush)this.GetValue(ColorBarraTituloProperty);
            }

            set
            {
                this.SetValue(ColorBarraTituloProperty, value);
            }
        }

        public double TamanoIconoTitulo
        {
            get
            {
                return (double)this.GetValue(TamanoIconoTituloProperty);
            }

            set
            {
                this.SetValue(TamanoIconoTituloProperty, value);
            }
        }

        public Style TextoTituloStyle
        {
            get
            {
                return (Style)this.GetValue(TextoTituloStyleProperty);
            }

            set
            {
                this.SetValue(TextoTituloStyleProperty, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.interopHelper = new WindowInteropHelper(this);
            this.AttachToVisualTree();
            this.Loaded += this.NeuronMainWindowAnimate;
        }

        public new bool? ShowDialog()
        {
            this.isModal = true;
            return base.ShowDialog();
        }

        protected T GetChildControl<T>(string ctrlName) where T : DependencyObject
        {
            T ctrl = this.Template.FindName(ctrlName, this) as T;
            return ctrl;
        }

        protected void MoveWindow(Rect rect)
        {
            MoveWindow(this.interopHelper.Handle, (int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height, false);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        private void NeuronMainWindowAnimate(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, NeuronMainWindowVisualStates.Normal, true);
        }

        private void AttachToVisualTree()
        {
            // Close Button
            Button closeButton = this.GetChildControl<Button>("PART_CloseButton");
            if (closeButton != null)
            {
                closeButton.Click += this.OnCloseButtonClick;
            }
            else
            {
                FrameworkElement closeFrameworkElement = this.GetChildControl<FrameworkElement>("PART_CloseButton");
                if (closeFrameworkElement != null)
                {
                    closeFrameworkElement.MouseLeftButtonUp += this.OnCloseButtonClick;
                    closeFrameworkElement.MouseLeftButtonDown += (ss, ee) =>
                    {
                        ee.Handled = true;
                    };
                    if (this.isModal)
                    {
                        closeFrameworkElement.Visibility = Visibility.Collapsed;
                    }
                }
            }

            Button normalButton = this.GetChildControl<Button>("PART_NormalButton");
            if (normalButton != null)
            {
                normalButton.Click += this.OnNormalButtonClick;
            }
            else
            {
                FrameworkElement normalFrameworkElement = this.GetChildControl<FrameworkElement>("PART_NormalButton");
                if (normalFrameworkElement != null)
                {
                    normalFrameworkElement.MouseLeftButtonUp += this.OnNormalButtonClick;
                    normalFrameworkElement.MouseLeftButtonDown += (ss, ee) =>
                        {
                            ee.Handled = true;
                        };
                    if (this.isModal)
                    {
                        normalFrameworkElement.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Minimize Button
            Button minimizeButton = this.GetChildControl<Button>("PART_MinimizeButton");
            if (minimizeButton != null)
            {
                minimizeButton.Click += this.OnMinimizeButtonClick;
            }
            else
            {
                FrameworkElement minimizeFrameworkElement = this.GetChildControl<FrameworkElement>("PART_MinimizeButton");
                if (minimizeFrameworkElement != null)
                {
                    minimizeFrameworkElement.MouseLeftButtonUp += this.OnMinimizeButtonClick;
                    minimizeFrameworkElement.MouseLeftButtonDown += (ss, ee) =>
                    {
                        ee.Handled = true;
                    };
                    if (this.isModal)
                    {
                        minimizeFrameworkElement.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Maximize Button
            Button maximizeButton = this.GetChildControl<Button>("PART_MaximizeButton");
            if (maximizeButton != null)
            {
                maximizeButton.Click += this.OnMaximizeButtonClick;
            }
            else
            {
                FrameworkElement maximizeFrameworkElement = this.GetChildControl<FrameworkElement>("PART_MaximizeButton");
                if (maximizeFrameworkElement != null)
                {
                    maximizeFrameworkElement.MouseLeftButtonUp += this.OnMaximizeButtonClick;
                    maximizeFrameworkElement.MouseLeftButtonDown += (ss, ee) =>
                    {
                        ee.Handled = true;
                    };
                    if (this.isModal)
                    {
                        maximizeFrameworkElement.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Resizers
            if (this.ResizeMode != ResizeMode.NoResize)
            {
                Path sResizer = this.GetChildControl<Path>("PART_SResizer");
                if (sResizer != null)
                {
                    sResizer.MouseDown += this.OnSizeSouth;
                }

                Path nResizer = this.GetChildControl<Path>("PART_NResizer");
                if (nResizer != null)
                {
                    nResizer.MouseDown += this.OnSizeNorth;
                }

                Path eResizer = this.GetChildControl<Path>("PART_EResizer");
                if (eResizer != null)
                {
                    eResizer.MouseDown += this.OnSizeEast;
                }

                Path wResizer = this.GetChildControl<Path>("PART_WResizer");
                if (wResizer != null)
                {
                    wResizer.MouseDown += this.OnSizeWest;
                }

                Rectangle seResizer = this.GetChildControl<Rectangle>("PART_SEResizer");
                if (seResizer != null)
                {
                    seResizer.MouseDown += this.OnSizeSouthEast;
                }

                Rectangle swResizer = this.GetChildControl<Rectangle>("PART_SWResizer");
                if (swResizer != null)
                {
                    swResizer.MouseDown += this.OnSizeSouthWest;
                }

                Rectangle neResizer = this.GetChildControl<Rectangle>("PART_NEResizer");
                if (neResizer != null)
                {
                    neResizer.MouseDown += this.OnSizeNorthEast;
                }

                Rectangle nwResizer = this.GetChildControl<Rectangle>("PART_NWResizer");
                if (nwResizer != null)
                {
                    nwResizer.MouseDown += this.OnSizeNorthWest;
                }
            }

            // Title Bar
            Panel titleBar = this.GetChildControl<Panel>("PART_TitleBar");
            if (titleBar != null)
            {
                titleBar.MouseLeftButtonDown += this.OnTitleBarMouseDown;
            }
        }

        private void OnMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.ToggleMaximize();
        }

        private void ToggleMaximize()
        {
            this.WindowState = (this.WindowState == WindowState.Maximized)
                                ?
                                    WindowState.Normal
                                : WindowState.Maximized;
        }

        private void OnTitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ResizeMode != ResizeMode.NoResize && e.ClickCount == 2)
            {
                this.ToggleMaximize();
                return;
            }

            this.DragMove();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnNormalButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnSizeSouth(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.South);
        }

        private void OnSizeNorth(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.North);
        }

        private void OnSizeEast(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.East);
        }

        private void OnSizeWest(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.West);
        }

        private void OnSizeNorthWest(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.NorthWest);
        }

        private void OnSizeNorthEast(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.NorthEast);
        }

        private void OnSizeSouthEast(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.SouthEast);
        }

        private void OnSizeSouthWest(object sender, MouseButtonEventArgs e)
        {
            this.DragSize(this.interopHelper.Handle, SizingAction.SouthWest);
        }

        private void DragSize(IntPtr handle, SizingAction sizingAction)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                SendMessage(handle, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + sizingAction), IntPtr.Zero);
                SendMessage(handle, 514, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
