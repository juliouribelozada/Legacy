// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusyIndicator.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Basado en el control BusyIndicator del WPF Toolkit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Atpc.UI.Controls
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    using System.Windows.Threading;

    [TemplateVisualState(Name = BusyIndicatorVisualStates.StateIdle, GroupName = BusyIndicatorVisualStates.GroupBusyStatus)]
    [TemplateVisualState(Name = BusyIndicatorVisualStates.StateBusy, GroupName = BusyIndicatorVisualStates.GroupBusyStatus)]
    [TemplateVisualState(Name = BusyIndicatorVisualStates.StateVisible, GroupName = BusyIndicatorVisualStates.GroupVisibility)]
    [TemplateVisualState(Name = BusyIndicatorVisualStates.StateHidden, GroupName = BusyIndicatorVisualStates.GroupVisibility)]
    [StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof(Rectangle))]
    [StyleTypedProperty(Property = "ProgressBarStyle", StyleTargetType = typeof(ProgressBar))]
    public class BusyIndicator : ContentControl
    {
        /// <summary>
        /// Identifies the BusyContent dependency property.
        /// </summary>
        public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register("BusyContent", typeof(object), typeof(BusyIndicator), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the BusyTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty BusyContentTemplateProperty = DependencyProperty.Register("BusyContentTemplate", typeof(DataTemplate), typeof(BusyIndicator), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the OverlayStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register("OverlayStyle", typeof(Style), typeof(BusyIndicator), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the IsBusy dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(false, OnIsBusyChanged));

        /// <summary>
        /// Identifies the DisplayAfter dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayAfterProperty = DependencyProperty.Register("DisplayAfter", typeof(TimeSpan), typeof(BusyIndicator), new PropertyMetadata(TimeSpan.FromSeconds(0.1)));

        /// <summary>
        /// Identifies the ProgressBarStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressBarStyleProperty = DependencyProperty.Register("ProgressBarStyle", typeof(Style), typeof(BusyIndicator), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the ProgressBarVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressBarVisibilityProperty = DependencyProperty.Register("ProgressBarVisibility", typeof(Visibility), typeof(BusyIndicator), new PropertyMetadata(null));

        /// <summary>
        /// Timer used to delay the initial display and avoid flickering.
        /// </summary>
        private readonly DispatcherTimer displayAfterTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyIndicator"/> class. 
        /// </summary>
        public BusyIndicator()
        {
            DefaultStyleKey = typeof(BusyIndicator);
            this.displayAfterTimer = new DispatcherTimer();
            this.displayAfterTimer.Tick += this.DisplayAfterTimerElapsed;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the busy indicator should show.
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)this.GetValue(IsBusyProperty); }
            set { this.SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating the busy content to display to the user.
        /// </summary>
        public object BusyContent
        {
            get { return this.GetValue(BusyContentProperty); }
            set { this.SetValue(BusyContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating the template to use for displaying the busy content to the user.
        /// </summary>
        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate)this.GetValue(BusyContentTemplateProperty); }
            set { this.SetValue(BusyContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating the style to use for the progress bar.
        /// </summary>
        public Style ProgressBarStyle
        {
            get { return (Style)this.GetValue(ProgressBarStyleProperty); }
            set { this.SetValue(ProgressBarStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how long to delay before displaying the busy content.
        /// </summary>
        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan)this.GetValue(DisplayAfterProperty); }
            set { this.SetValue(DisplayAfterProperty, value); }
        }

        public Visibility ProgressBarVisibility
        {
            get { return (Visibility)this.GetValue(ProgressBarVisibilityProperty); }
            set { this.SetValue(ProgressBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating the style to use for the overlay.
        /// </summary>
        public Style OverlayStyle
        {
            get { return (Style)this.GetValue(OverlayStyleProperty); }
            set { this.SetValue(OverlayStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the BusyContent is visible.
        /// </summary>
        protected bool IsContentVisible { get; set; }

        /// <summary>
        /// Overrides the OnApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ChangeVisualState(false);
        }

        /// <summary>
        /// Changes the control's visual state(s).
        /// </summary>
        /// <param name="useTransitions">True if state transitions should be used.</param>
        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.IsBusy ? BusyIndicatorVisualStates.StateBusy : BusyIndicatorVisualStates.StateIdle, useTransitions);
            VisualStateManager.GoToState(this, this.IsContentVisible ? BusyIndicatorVisualStates.StateVisible : BusyIndicatorVisualStates.StateHidden, useTransitions);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Cambio IsBusy: " + this.IsBusy);
            if (this.IsBusy)
            {
                if (this.DisplayAfter.Equals(TimeSpan.Zero))
                {
                    // Go visible now
                    this.IsContentVisible = true;
                }
                else
                {
                    // Set a timer to go visible
                    this.displayAfterTimer.Interval = this.DisplayAfter;
                    this.displayAfterTimer.Start();
                }
            }
            else
            {
                // No longer visible
                this.displayAfterTimer.Stop();
                this.IsContentVisible = false;
            }

            this.ChangeVisualState(true);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="d">BusyIndicator that changed its IsBusy.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator)d).OnIsBusyChanged(e);
        }

        /// <summary>
        /// Handler for the DisplayAfterTimer.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            this.displayAfterTimer.Stop();
            this.IsContentVisible = true;
            this.ChangeVisualState(true);
        }
    }
}
