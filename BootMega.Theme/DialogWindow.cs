using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using BootMega.Theme.Core;

namespace BootMega.Theme
{
    [TemplatePart(Name = "PART_TitleBar", Type = typeof(Border))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class DialogWindow : WindowBase
    {
        #region fields

        private Border caption;
        public Boolean Result;
        private Button closeButton;

        #endregion
   
        #region ctors

        static DialogWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(typeof(DialogWindow)));
            ResizeModeProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(ResizeMode.NoResize, OnResizeModeChanged, OnCoerceResizeMode));
            SizeToContentProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(SizeToContent.WidthAndHeight, OnSizeToContentChanged, OnCoerceSizeToContent));
            WindowStateProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(WindowState.Normal, OnWindowStateChanged, OnCoerceWindowState));
        }

        #endregion

        #region overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult = Result;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            caption = (Border)GetTemplateChild("PART_TitleBar");

            if (Owner != null)
                WindowStartupLocation = WindowStartupLocation.CenterOwner;

            closeButton = GetTemplateChild("PART_CloseButton") as Button;

            if (closeButton != null)
                closeButton.Click += button_Click;

            RegisterCaption();
        }

        #endregion

        #region methods

        private void RegisterCaption()
        {
            if (caption != null)
            {
                caption.MouseLeftButtonDown += (sender, e) =>
                {
                    DragMove();
                };
            }
        }

        private static object OnCoerceWindowState(DependencyObject d, object baseValue)
        {
            return WindowState.Normal;
        }

        private static void OnWindowStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static object OnCoerceSizeToContent(DependencyObject d, object baseValue)
        {
            return SizeToContent.WidthAndHeight;
        }

        private static void OnSizeToContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static object OnCoerceResizeMode(DependencyObject d, object baseValue)
        {
            return ResizeMode.NoResize;
        }

        private static void OnResizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                switch (b.Name)
                {
                    case "PART_CloseButton":
                        SystemCommands.CloseWindow(this);
                        break;
                }
            }
        }

        #endregion

        #region dependency properties

        #region bool ShowCloseButton

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(DialogWindow), new PropertyMetadata(true));

        #endregion

        #endregion
    }
}
