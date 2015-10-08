using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using BootMega.Theme.Enums;
using BootMega.Theme.Extensions;

namespace BootMega.Theme.Core
{
    [TemplatePart(Name = "PART_Chrome", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_BorderLeft", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderTopLeft", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderTop", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderTopRight", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderRight", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderBottomRight", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderBottom", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BorderBottomLeft", Type = typeof(Border))]
    [TemplatePart(Name = "PART_Caption", Type = typeof(Border))]
    public class WindowChrome : Control
    {
        #region fields
        
        private Point cursorOffset;
        private double restoreTop;

        private Grid chrome;
        private Border borderLeft;
        private Border borderTopLeft;
        private Border borderTop;
        private Border borderTopRight;
        private Border borderRight;
        private Border borderBottomRight;
        private Border borderBottom;
        private Border borderBottomLeft;
        private Border caption;
        private MainWindow window;

        #endregion

        #region ctors

        static WindowChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowChrome), new FrameworkPropertyMetadata(typeof(WindowChrome)));
        }

        #endregion

        #region overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            chrome = (Grid)GetTemplateChild("PART_Chrome");
            borderLeft = (Border)GetTemplateChild("PART_BorderLeft");
            borderTopLeft = (Border)GetTemplateChild("PART_BorderTopLeft");
            borderTop = (Border)GetTemplateChild("PART_BorderTop");
            borderTopRight = (Border)GetTemplateChild("PART_BorderTopRight");
            borderRight = (Border)GetTemplateChild("PART_BorderRight");
            borderBottomRight = (Border)GetTemplateChild("PART_BorderBottomRight");
            borderBottom = (Border)GetTemplateChild("PART_BorderBottom");
            borderBottomLeft = (Border)GetTemplateChild("PART_BorderBottomLeft");
            caption = (Border)GetTemplateChild("PART_Caption");

            window = this.GetParentByType<MainWindow>();

            UpdateChromeMargin();

            RegisterBorderEvents(DirectionOfResize.Left, borderLeft);
            RegisterBorderEvents(DirectionOfResize.TopLeft, borderTopLeft);
            RegisterBorderEvents(DirectionOfResize.Top, borderTop);
            RegisterBorderEvents(DirectionOfResize.TopRight, borderTopRight);
            RegisterBorderEvents(DirectionOfResize.Right, borderRight);
            RegisterBorderEvents(DirectionOfResize.BottomRight, borderBottomRight);
            RegisterBorderEvents(DirectionOfResize.Bottom, borderBottom);
            RegisterBorderEvents(DirectionOfResize.BottomLeft, borderBottomLeft);

            if (window != null)
            {
                var grip = (ResizeGrip)window.Template.FindName("PART_ResizeGrip", window);
                RegisterBorderEvents(DirectionOfResize.BottomRight, grip);
            }

            RegisterCaption();
        }

        #endregion

        #region methods

        private void UpdateChromeMargin()
        {
            if (chrome != null)
            {
                chrome.Margin = new Thickness(-ResizeBorderThickness.Left, -ResizeBorderThickness.Top, 
                    -ResizeBorderThickness.Right, -ResizeBorderThickness.Bottom);
            }
        }

        private void RegisterBorderEvents(DirectionOfResize DirectionOfResize, FrameworkElement border)
        {
            border.MouseLeftButtonDown += (sender, e) =>
            {
                Point cursorLocation = e.GetPosition(window);
                Point cursorOffset = new Point();

                switch (DirectionOfResize)
                {
                    case DirectionOfResize.Left:
                        cursorOffset.X = cursorLocation.X;
                        break;
                    case DirectionOfResize.TopLeft:
                        cursorOffset.X = cursorLocation.X;
                        cursorOffset.Y = cursorLocation.Y;
                        break;
                    case DirectionOfResize.Top:
                        cursorOffset.Y = cursorLocation.Y;
                        break;
                    case DirectionOfResize.TopRight:
                        cursorOffset.X = (window.Width - cursorLocation.X);
                        cursorOffset.Y = cursorLocation.Y;
                        break;
                    case DirectionOfResize.Right:
                        cursorOffset.X = (window.Width - cursorLocation.X);
                        break;
                    case DirectionOfResize.BottomRight:
                        cursorOffset.X = (window.Width - cursorLocation.X);
                        cursorOffset.Y = (window.Height - cursorLocation.Y);
                        break;
                    case DirectionOfResize.Bottom:
                        cursorOffset.Y = (window.Height - cursorLocation.Y);
                        break;
                    case DirectionOfResize.BottomLeft:
                        cursorOffset.X = cursorLocation.X;
                        cursorOffset.Y = (window.Height - cursorLocation.Y);
                        break;
                }

                this.cursorOffset = cursorOffset;

                border.CaptureMouse();
            };

            border.MouseMove += (sender, e) =>
            {
                if (border.IsMouseCaptured)
                {
                    window.SizeToContent = SizeToContent.Manual;
                    Point cursorLocation = e.GetPosition(window);

                    double nHorizontalChange = (cursorLocation.X - cursorOffset.X);
                    double pHorizontalChange = (cursorLocation.X + cursorOffset.X);
                    double nVerticalChange = (cursorLocation.Y - cursorOffset.Y);
                    double pVerticalChange = (cursorLocation.Y + cursorOffset.Y);

                    switch (DirectionOfResize)
                    {
                        case DirectionOfResize.Left:
                            if (window.Width - nHorizontalChange <= window.MinWidth)
                                break;
                            window.Left += nHorizontalChange;
                            window.Width -= nHorizontalChange;
                            break;
                        case DirectionOfResize.TopLeft:
                            if (window.Width - nHorizontalChange <= window.MinWidth)
                                break;
                            window.Left += nHorizontalChange;
                            window.Width -= nHorizontalChange;
                            if (window.Height - nVerticalChange <= window.MinHeight)
                                break;
                            window.Top += nVerticalChange;
                            window.Height -= nVerticalChange;
                            break;
                        case DirectionOfResize.Top:
                            if (window.Height - nVerticalChange <= window.MinHeight)
                                break;
                            window.Top += nVerticalChange;
                            window.Height -= nVerticalChange;
                            break;
                        case DirectionOfResize.TopRight:
                            if (pHorizontalChange <= window.MinWidth)
                                break;
                            window.Width = pHorizontalChange;
                            if (window.Height - nVerticalChange <= window.MinHeight)
                                break;
                            window.Top += nVerticalChange;
                            window.Height -= nVerticalChange;
                            break;
                        case DirectionOfResize.Right:
                            if (pHorizontalChange <= window.MinWidth)
                                break;
                            window.Width = pHorizontalChange;
                            break;
                        case DirectionOfResize.BottomRight:
                            if (pHorizontalChange <= window.MinWidth)
                                break;
                            window.Width = pHorizontalChange;
                            if (pVerticalChange <= window.MinHeight)
                                break;
                            window.Height = pVerticalChange;
                            break;
                        case DirectionOfResize.Bottom:
                            if (pVerticalChange <= window.MinHeight)
                                break;
                            window.Height = pVerticalChange;
                            break;
                        case DirectionOfResize.BottomLeft:
                            if (window.Width - nHorizontalChange <= window.MinWidth)
                                break;
                            window.Left += nHorizontalChange;
                            window.Width -= nHorizontalChange;
                            if (pVerticalChange <= window.MinHeight)
                                break;
                            window.Height = pVerticalChange;
                            break;
                    }
                }
            };

            border.MouseLeftButtonUp += (sender, e) =>
            {
                border.ReleaseMouseCapture();
            };
        }

        private void RegisterCaption()
        {
            if (caption != null)
            {
                caption.MouseLeftButtonDown += (sender, e) =>
                {
                    restoreTop = e.GetPosition(window).Y;

                    if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left &&
                        (window.ResizeMode != ResizeMode.CanMinimize && window.ResizeMode != ResizeMode.NoResize))
                    {
                        window.SizeToContent = SizeToContent.Manual;
                        if (window.WindowState != WindowState.Maximized)
                        {
                            window.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            window.WindowState = WindowState.Normal;
                        }
                        return;
                    }
                    window.DragMove();
                };

                caption.MouseMove += (sender, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed && caption.IsMouseOver)
                    {
                        if (window.WindowState == WindowState.Maximized)
                        {
                            window.WindowState = WindowState.Normal;
                            window.Top = restoreTop - 10;
                            window.DragMove();
                        }
                    }
                };
            }
        }

        #endregion

        #region dependency properties

        #region public double CaptionHeight

        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowChrome), new PropertyMetadata(32.0));

        #endregion

        #region public Thickness ResizeBorderThickness

        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(WindowChrome), 
            new PropertyMetadata(new Thickness(10), OnResizeBorderThicknessChanged));

        private static void OnResizeBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindowChrome chrome = d as WindowChrome;
            if (chrome != null)
                chrome.UpdateChromeMargin();
        }

        #endregion

        #region public ResizeMode ResizeMode

        public ResizeMode ResizeMode
        {
            get { return (ResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }

        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(ResizeMode), typeof(WindowChrome), 
            new PropertyMetadata(ResizeMode.CanResize));

        #endregion

        #region public WindowState WindowState

        public WindowState WindowState
        {
            get { return (WindowState)GetValue(WindowStateProperty); }
            set { SetValue(WindowStateProperty, value); }
        }

        public static readonly DependencyProperty WindowStateProperty =
            DependencyProperty.Register("WindowState", typeof(WindowState), typeof(WindowChrome), new PropertyMetadata(WindowState.Normal));

        #endregion

        #endregion
    }
}
