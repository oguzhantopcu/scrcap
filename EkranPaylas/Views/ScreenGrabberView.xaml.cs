using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Controls;
using EkranPaylas.Core;
using EkranPaylas.Extensions;
using EkranPaylas.Utilities;
using EkranPaylas.ViewModels;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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

            this.MaxWidth = this.Width = SystemParameters.PrimaryScreenWidth;
            this.MaxHeight = this.Height = SystemParameters.PrimaryScreenHeight;
            this.Top = this.Left = 0;

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

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.S)
                    SaveCurrent();

                if (e.Key == Key.U)
                    DataContext.StartUpload();
            }
        }

        public void SaveCurrent()
        {
            var dialog = new SaveFileDialog() {Filter = "to be fixed"};
            if (System.Windows.Forms.DialogResult.OK == dialog.ShowDialog())
            {
                DataContext.Save(dialog.FileName);
            }
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
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

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
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
            Canvas.SetLeft(ContentSelector, 10000);
            Canvas.SetTop(ContentSelector, 10000); 
            
            this.Visibility = message == ScreenGrabberState.Select ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
