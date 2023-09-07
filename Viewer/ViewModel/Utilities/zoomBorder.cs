using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Viewer.Model;

//Canvas의 줌/패닝/화면 이동을 위한 클래스
//https://stackoverflow.com/questions/741956/pan-zoom-image 참조

namespace Viewer.ViewModel.Utilities
{
    class ZoomBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;
        private Point _end;

        public int SelectedItemIndex
        {
            get { return (int)GetValue(SelectedItemIndexProperty); }
            set { SetValue(SelectedItemIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemIndexProperty =
            DependencyProperty.Register("SelectedItemIndex", typeof(int), typeof(ZoomBorder), new PropertyMetadata(0, OnSelectionChanged));

        private static void OnSelectionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ZoomBorder zoomBorder = source as ZoomBorder;
            zoomBorder.ChangeFocusToSelectedCentered();
        }

        private const int SELECTION_THRESHOLD = 10;

        public ObservableCollection<XmlModel> ViewXmlReference
        {
            get { return (ObservableCollection<XmlModel>)GetValue(ViewXmlReferenceProperty); }
            set { SetValue(ViewXmlReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewXmlReference.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewXmlReferenceProperty =
            DependencyProperty.Register("ViewXmlReference", typeof(ObservableCollection<XmlModel>), typeof(ZoomBorder), new PropertyMetadata(null, OnChanged));

        private static void OnChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            // Empty Callback
        }


        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        public void ChangeSelectedDataStroke(XmlModel selectedData)
        {
            foreach (var item in ViewXmlReference)
            {
                item._Stroke = 4;
            }
            selectedData._Stroke = 8;
        }


        public void ChangeFocusToSelectedCentered()
        {
            if (SelectedItemIndex >= ViewXmlReference.Count)
                return;
            if (SelectedItemIndex < 0) // TODO: Bug? ListView를 눌렀을 때에도 호출됨. Focus가 변경되는 경우에 호출되는 수도 있을 것으로 예상
                return;

            XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
            int midX = (int)((selectedData.Xmin + selectedData.Xmax) / 2.0f);
            int midY = (int)((selectedData.Ymin + selectedData.Ymax) / 2.0f);

            // (Note)Image에 대한 Render Transform이기 때문에, X,Y 값이 증가하면 이미지가 우측 아래로 이동
            var tt = GetTranslateTransform(child);
            var st = GetScaleTransform(child);

            // 왼쪽 위가 기준점이으로 화면 가운데로 가져오기 위해 캔버스크기(RenderSize)의 절반만큼 보정 이동
            int renderCenterX = (int)(child.RenderSize.Width / 2.0);
            int renderCenterY = (int)(child.RenderSize.Height / 2.0);

            tt.X = (-midX) * st.ScaleX + renderCenterX;
            tt.Y = (-midY) * st.ScaleY + renderCenterY;

            ChangeSelectedDataStroke(selectedData);
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);

                this.MouseWheel += child_MouseWheel;
                this.MouseLeftButtonDown += child_MouseLeftButtonDown;
                this.MouseRightButtonDown += child_MouseRightButtonDown;
                this.MouseLeftButtonUp += child_MouseLeftButtonUp;
                this.MouseRightButtonUp += child_MouseRightButtonUp;
                this.MouseMove += child_MouseMove;
                //this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(child_PreviewMouseRightButtonDown);
            }
        }
        public void Reset()     //이 부분을 수정하면 줌/패닝 기본값 변경 가능
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 0.15;
                st.ScaleY = 0.15;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = -250.0;
                tt.Y = -200.0;
            }
        }

        #region Child Events

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                double zoom = e.Delta > 0 ? .05 : -.05;
                if (!(e.Delta > 0) && (st.ScaleX <= .15 || st.ScaleY <= .15))   //이 부분을 수정하면 줌/패닝 배율 수정 가능
                    return;

                Point relative = e.GetPosition(child);
                double absoluteX;
                double absoluteY;

                absoluteX = relative.X * st.ScaleX + tt.X;
                absoluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX += zoom;
                st.ScaleY += zoom;

                tt.X = absoluteX - relative.X * st.ScaleX;
                tt.Y = absoluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }
        private void child_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                start = e.GetPosition(this);
            }
        }

        private Point ConvertCanvasCoordToImageCoord(Point p)
        {
            Point converted = new Point();
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);

            converted.X = ((-tt.X) + p.X) / st.ScaleX;
            converted.Y = ((-tt.Y) + p.Y) / st.ScaleY;

            return converted;
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                // Border에서의 Box Selection Logic
                // 마우스를 좌클릭 하는 경우가 두 가지(패닝 & Selection)이므로, 불편함이 없으려면 두 가지 인터랙션의 구분이 필요
                // 클릭-클릭해제 간 좌표값의 차이가 거의 없는 경우 Selection으로 판단함

                Point end = e.GetPosition(this);
                double squaredDistance = Math.Pow((end.X - start.X), 2.0) + Math.Pow((end.Y - start.Y), 2.0);
                if (squaredDistance < SELECTION_THRESHOLD)
                {
                    // Selection으로 판단된 경우
                    // 우선 클릭 위치를 이미지 좌표로 변환
                    Point clickImageCoord = ConvertCanvasCoordToImageCoord(start);

                    for (int i = 0; i < ViewXmlReference.Count; i++)
                    {
                        XmlModel item = ViewXmlReference[i];
                        if (clickImageCoord.X > item.Xmin && clickImageCoord.X < item.Xmax &&
                            clickImageCoord.Y > item.Ymin && clickImageCoord.Y < item.Ymax)
                        {
                            // 박스 안에 포함된 좌표라면 SelectedBox에 저장하고 종료
                            SelectedItemIndex = i;
                            break;
                        }
                    }
                }

                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }
        private void child_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                    Point mousePosition = e.GetPosition(this);
                    Point imageCoord = ConvertCanvasCoordToImageCoord(mousePosition);
                    if ((int)imageCoord.X < selectedData.Xmax || (int)imageCoord.Y < selectedData.Ymax)
                    {
                        selectedData.Xmin = (int)imageCoord.X;
                        selectedData.Ymin = (int)imageCoord.Y;
                        selectedData.Width = selectedData.Xmax - selectedData.Xmin;
                        selectedData.Height = selectedData.Ymax - selectedData.Ymin;
                        selectedData.CenterX = selectedData.Width / 2;
                        selectedData.CenterY = selectedData.Height / 2;
                    }
                }
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                    Point mousePosition = e.GetPosition(this);
                    Point imageCoord = ConvertCanvasCoordToImageCoord(mousePosition);
                    if ((int)imageCoord.X > selectedData.Xmin || (int)imageCoord.Y > selectedData.Ymin)
                    {
                        selectedData.Xmax = (int)imageCoord.X;
                        selectedData.Ymax = (int)imageCoord.Y;
                        selectedData.Width = selectedData.Xmax - selectedData.Xmin;
                        selectedData.Height = selectedData.Ymax - selectedData.Ymin;
                        selectedData.CenterX = selectedData.Width / 2;
                        selectedData.CenterY = selectedData.Height / 2;
                    }
                }
                else
                {
                    XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                    _end = e.GetPosition(this);
                    selectedData.Degree += (int)(_end.X - start.X) / 20;
                    if (selectedData.Degree > 90)
                    {
                        selectedData.Degree = selectedData.Degree % 90;
                    }
                }
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                        Point mousePosition = e.GetPosition(this);
                        Point imageCoord = ConvertCanvasCoordToImageCoord(mousePosition);
                        if ((int)imageCoord.X < selectedData.Xmax || (int)imageCoord.Y < selectedData.Ymax)
                        {
                            selectedData.Xmin = (int)imageCoord.X;
                            selectedData.Ymin = (int)imageCoord.Y;
                            selectedData.Width = selectedData.Xmax - selectedData.Xmin;
                            selectedData.Height = selectedData.Ymax - selectedData.Ymin;
                            selectedData.CenterX = selectedData.Width / 2;
                            selectedData.CenterY = selectedData.Height / 2;
                        }
                    }
                    else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                    {
                        XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                        Point mousePosition = e.GetPosition(this);
                        Point imageCoord = ConvertCanvasCoordToImageCoord(mousePosition);
                        if ((int)imageCoord.X > selectedData.Xmin || (int)imageCoord.Y > selectedData.Ymin)
                        {
                            selectedData.Xmax = (int)imageCoord.X;
                            selectedData.Ymax = (int)imageCoord.Y;
                            selectedData.Width = selectedData.Xmax - selectedData.Xmin;
                            selectedData.Height = selectedData.Ymax - selectedData.Ymin;
                            selectedData.CenterX = selectedData.Width / 2;
                            selectedData.CenterY = selectedData.Height / 2;
                        }
                    }
                    else
                    {
                        XmlModel selectedData = ViewXmlReference[SelectedItemIndex];
                        _end = e.GetPosition(this);
                        selectedData.Degree += (int)(_end.X - start.X) / 20;
                        //selectedData.Degree = selectedData.Degree % 90;
                    }
                }

                if (child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(child);
                    Vector v = start - e.GetPosition(this);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
            }
        }


        #endregion
    }
}