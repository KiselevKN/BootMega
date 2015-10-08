using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BootMega.Theme.Core;

namespace BootMega.Theme
{
    [TemplatePart(Name = "PART_Icon", Type = typeof(Image))]
    [TemplatePart(Name = "PART_MinButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaxButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class MainWindow : WindowBase
    {
        #region fields

        private Image icon;
        private Button minButton;
        private Button maxButton;
        private Button closeButton;

        #endregion

        #region ctors

        static MainWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(typeof(MainWindow)));
            ResizeModeProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(ResizeMode.CanResize, OnResizeModeChanged, OnCoerceResizeMode));
        }

        private static object OnCoerceResizeMode(DependencyObject d, object baseValue)
        {
            var w = (MainWindow)d;
            ResizeMode newResizeMode = (ResizeMode)baseValue;

            w.CopyResizeMode = newResizeMode;

            if (newResizeMode == ResizeMode.CanResizeWithGrip)
                newResizeMode = ResizeMode.CanResize;

            return newResizeMode;
        }

        private static void OnResizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            icon = GetTemplateChild("PART_Icon") as Image;

            if (icon != null)
            {
                icon.MouseLeftButtonDown += icon_MouseLeftButtonDown;
                icon.MouseUp += icon_MouseUp;
            }

            minButton = GetTemplateChild("PART_MinButton") as Button;
            maxButton = GetTemplateChild("PART_MaxButton") as Button;
            closeButton = GetTemplateChild("PART_CloseButton") as Button;


            if (minButton != null)
                minButton.Click += button_Click;

            if (maxButton != null)
                maxButton.Click += button_Click;

            if (closeButton != null)
                closeButton.Click += button_Click;
        }

        #endregion

        #region methods

        private void icon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element != null)
            {
                var point = element.PointToScreen(new Point(element.ActualWidth / 2, element.ActualHeight));
                SystemCommands.ShowSystemMenu(this, point);
            }
        }

        private void icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element != null)
            {
                var point = element.PointToScreen(new Point(element.ActualWidth / 2, element.ActualHeight));
                SystemCommands.ShowSystemMenu(this, point);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                switch (b.Name)
                {
                    case "PART_MinButton":
                        WindowState = WindowState.Minimized;
                        break;
                    case "PART_MaxButton":
                        SizeToContent = SizeToContent.Manual;
                        WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
                        break;
                    case "PART_CloseButton":
                        //SystemCommands.CloseWindow(this);
                        break;
                }
            }
        }

        #endregion

        #region dependency Properties

        #region public ResizeMode CopyResizeMode

        public ResizeMode CopyResizeMode
        {
            get { return (ResizeMode)GetValue(CopyResizeModeProperty); }
            set { SetValue(CopyResizeModeProperty, value); }
        }

        public static readonly DependencyProperty CopyResizeModeProperty =
            DependencyProperty.Register("CopyResizeMode", typeof(ResizeMode), typeof(MainWindow), new PropertyMetadata(ResizeMode.CanResize));

        #endregion

        #endregion

    }
}
