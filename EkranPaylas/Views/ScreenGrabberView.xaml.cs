using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Controls;
using EkranPaylas.Core;
using EkranPaylas.Extensions;
using EkranPaylas.Utilities;
using EkranPaylas.ViewModels;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for ScreenGrabberView.xaml
    /// </summary>
    public partial class ScreenGrabberView : IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;

        public ScreenGrabberView()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            MaxWidth = Width = SystemParameters.PrimaryScreenWidth;
            MaxHeight = Height = SystemParameters.PrimaryScreenHeight;
            Top = Left = 0;

            _eventAggregator.Subscribe(this);

            InitializeComponent();
        }

        public new ScreenGrabberViewModel DataContext
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScreenGrabberViewModel>();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            UpdateBlackArea();

            UpdateToolbox();
        }

        protected void UpdateBlackArea()
        {
            LeftBorder.Width = Math.Max(Canvas.GetLeft(ContentSelector), 0);
            Canvas.SetTop(LeftBorder, Canvas.GetTop(ContentSelector));
            LeftBorder.Height = ContentSelector.ActualHeight;

            Canvas.SetTop(RightBorder, Canvas.GetTop(ContentSelector));
            Canvas.SetLeft(RightBorder, Canvas.GetLeft(ContentSelector) + ContentSelector.ActualWidth);
            RightBorder.Width = 9999;
            RightBorder.Height = ContentSelector.ActualHeight;

            TopBorder.Height = Math.Max(Canvas.GetTop(ContentSelector), 0);
            TopBorder.Width = 9999;

            Canvas.SetTop(BottomBorder, Canvas.GetTop(ContentSelector) + ContentSelector.Height);
            BottomBorder.Height = BottomBorder.Width = 9999;
        }

        protected void UpdateToolbox()
        {
            var left = Canvas.GetLeft(ContentSelector) + ContentSelector.ActualWidth;
            var top = Canvas.GetTop(ContentSelector) + ContentSelector.ActualHeight;

            Canvas.SetBottom(ToolBoxBorder, SystemParameters.PrimaryScreenHeight - top + 10);
            Canvas.SetRight(ToolBoxBorder, SystemParameters.PrimaryScreenWidth - left + 10);

            ToolBoxBorder.Opacity = ToolBoxBorder.IsMouseOver ? 1 : 0.5;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (!ContentSelector.IsMouseOver)
            {
                ContentSelector.Visibility = Visibility.Visible;

                Canvas.SetLeft(ContentSelector, e.GetPosition(this).X);
                Canvas.SetTop(ContentSelector, e.GetPosition(this).Y);

                ContentSelector.Width = 1;
                ContentSelector.Height = 1;

                var züleyman = ContentSelector.FindChild<ResizeThumb>(
                    f => f.HorizontalAlignment == HorizontalAlignment.Right &&
                         f.VerticalAlignment == VerticalAlignment.Bottom);

                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                züleyman.CaptureMouse();
            }
        }

        public void Handle(ScreenGrabberState message)
        {
            if (message != ScreenGrabberState.Select) Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            _eventAggregator.Unsubscribe(this);

            base.OnClosed(e);
        }
    }
}
