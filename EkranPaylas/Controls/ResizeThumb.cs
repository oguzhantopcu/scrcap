using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using EkranPaylas.Extensions;
using EkranPaylas.Utilities;

namespace EkranPaylas.Controls
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += this.ResizeThumb_DragDelta;
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var designerItem = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this.Parent) as Control)) as ContentControl);

            if (designerItem != null)
            {
                double horizontalChange,
                       horizontalDifference,
                       verticalChange,
                       verticalDifference,
                       deltaVertical,
                       deltaHorizontal;
                var remainingWidth = designerItem.ActualWidth - designerItem.MinWidth;
                var remainingHeight = designerItem.ActualHeight - designerItem.MinHeight;

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        verticalChange = -e.VerticalChange;
                        deltaVertical = Math.Min(remainingHeight, verticalChange);
                        verticalDifference = Math.Max(remainingHeight, verticalChange) - deltaVertical;
                        
                        if (remainingHeight < verticalChange)
                        {
                            Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) - verticalChange);
                            designerItem.Height = verticalDifference;

                            CancelDrag();
                            CaptureOppositeThumb(true);
                        }
                        else
                            designerItem.Height -= verticalChange;
                        break;
                    case VerticalAlignment.Top:
                        verticalChange = e.VerticalChange;
                        deltaVertical = Math.Min(verticalChange, remainingHeight);
                        verticalDifference = Math.Max(remainingHeight, verticalChange) - deltaVertical;
                        designerItem.Height -= deltaVertical;

                        if (verticalChange > remainingHeight)
                        {
                            Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                            designerItem.Height = verticalDifference;

                            CancelDrag();
                            CaptureOppositeThumb(true);
                        }
                        else
                            Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        horizontalChange = e.HorizontalChange;
                        deltaHorizontal = Math.Min(horizontalChange, remainingWidth);
                        horizontalDifference = Math.Max(remainingWidth, horizontalChange) - deltaHorizontal;
                        designerItem.Width -= deltaHorizontal;

                        if (remainingWidth < horizontalChange)
                        {
                            Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                            designerItem.Width = horizontalDifference;

                            CancelDrag();
                            CaptureOppositeThumb(false);
                        }
                        else
                            Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                        break;
                    case HorizontalAlignment.Right:
                        horizontalChange = -e.HorizontalChange;
                        deltaHorizontal = Math.Min(remainingWidth, horizontalChange);
                        horizontalDifference = Math.Max(remainingWidth, horizontalChange) - deltaHorizontal;

                        if (remainingWidth < horizontalChange)
                        {
                            Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) - horizontalChange);
                            designerItem.Width = horizontalDifference;

                            CancelDrag();
                            CaptureOppositeThumb(false);
                        }
                        else
                            designerItem.Width -= horizontalChange;
                        break;
                }
            }

            e.Handled = true;
        }

        protected void CaptureOppositeThumb(bool vertical = false)
        {
            var item = HorizontalAlignment != HorizontalAlignment.Center &&
                               HorizontalAlignment != HorizontalAlignment.Stretch &&
                               VerticalAlignment != VerticalAlignment.Center &&
                               VerticalAlignment != VerticalAlignment.Stretch
                                   ? Parent.FindChild<ResizeThumb>(
                                       f =>
                                       f.HorizontalAlignment ==
                                       (!vertical ? GetOpposite(HorizontalAlignment) : HorizontalAlignment) &&
                                       f.VerticalAlignment ==
                                       (vertical ? GetOpposite(VerticalAlignment) : VerticalAlignment))
                                   : Parent.FindChild<ResizeThumb>(
                                       f => f.HorizontalAlignment == GetOpposite(HorizontalAlignment) &&
                                            f.VerticalAlignment == GetOpposite(VerticalAlignment));

            item.CaptureMouse();

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
        }

        protected HorizontalAlignment GetOpposite(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    return HorizontalAlignment.Right;
                case HorizontalAlignment.Right:
                    return HorizontalAlignment.Left;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    return alignment;
                default:
                    throw new ArgumentOutOfRangeException("alignment");
            }
        }

        protected VerticalAlignment GetOpposite(VerticalAlignment alignment)
        {
            switch (alignment)
            {
                case VerticalAlignment.Top:
                    return VerticalAlignment.Bottom;
                case VerticalAlignment.Bottom:
                    return VerticalAlignment.Top;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    return alignment;
                default:
                    throw new ArgumentOutOfRangeException("alignment");
            }
        }
    }
}