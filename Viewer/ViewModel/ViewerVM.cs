using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viewer.ViewModel.Commands;
using Viewer.ViewModel.Utilities;
using System.Windows.Input;
using System.ComponentModel;
using Viewer.Model;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;

//TODO: 같은 Xml파일을 열면 생기는 오류 고치기
namespace Viewer.ViewModel
{
    class ViewerVM
    {
        // ObservableCollection
        public ObservableCollection<XmlModel> AllXmlDatas { get; set; }
        public ObservableCollection<XmlModel> CurrentXmlDatasInDatagrid { get; set; }
        public ObservableCollection<XmlModel> CurrentXmlDatasInCanvas { get; set; }

        public ObservableCollection<XmlList> XmlLists { get; set; }
        public ObservableCollection<ImageModel> ImageList { get; set; }

        // Class
        private FileLoader fileLoader = new FileLoader();
        private IsSelected isSelected = new IsSelected();
        private ModifyDatas ModifyDatas = new ModifyDatas();
        private SaveDataToXml SaveDataToXml = new SaveDataToXml();

        // Model
        public FilePathModel FilePathModel { get; private set; }
        public ImageModel CurrentImageInCanvas { get; private set; }

        // Property
        private XmlList selectedXmlListItem;
        public XmlList SelectedXmlListItem
        {
            get => selectedXmlListItem;
            set
            {
                if (selectedXmlListItem != value)
                {
                    selectedXmlListItem = value;

                    SelectedXmlList(SelectedXmlListItem);
                }
            }
        }
        private ImageModel selectedImageListItem;
        public ImageModel SelectedImageListItem
        {
            get => selectedImageListItem;
            set
            {
                if (selectedImageListItem != value)
                {
                    selectedImageListItem = value;

                    SelectedImageList(SelectedImageListItem);
                }
            }
        }

        // Command
        public ICommand OpenImageCommand { get; private set; }
        public ICommand OpenXmlCommand { get; private set; }
        public ICommand IsCheckCommand { get; private set; }
        public ICommand CellEditEndingCommand { get; private set; }
        public ICommand SaveDataToXmlCommand { get; private set; }

        public ViewerVM()
        {
            // ObservableCollection
            AllXmlDatas = new ObservableCollection<XmlModel>();
            XmlLists = new ObservableCollection<XmlList>();
            CurrentXmlDatasInDatagrid = new ObservableCollection<XmlModel>();
            CurrentXmlDatasInCanvas = new ObservableCollection<XmlModel>();
            ImageList = new ObservableCollection<ImageModel>();

            // Model
            FilePathModel = new FilePathModel();
            CurrentImageInCanvas = new ImageModel();

            // Command
            OpenImageCommand = new RelayCommand(OpenImage);
            OpenXmlCommand = new RelayCommand(OpenXml);
            IsCheckCommand = new RelayCommand(IsCheckedXmlList);
            CellEditEndingCommand = new RelayCommand(OnCellEditEnding);
            SaveDataToXmlCommand = new RelayCommand(SaveXml);

            // Event Handler
            CurrentXmlDatasInDatagrid.CollectionChanged += CurrentXmlDatasInDatagrid_CollectionChanged;
        }

        // Datagrid - 셀 수정
        private void OnCellEditEnding(object parameter)

        {
            if (CurrentXmlDatasInDatagrid == null)
                return;
            foreach (XmlModel xmlData in CurrentXmlDatasInDatagrid)
            {
                xmlData.Width = Math.Abs(xmlData.Xmax - xmlData.Xmin);
                xmlData.Height = Math.Abs(xmlData.Ymax - xmlData.Ymin);
                xmlData.CenterX = xmlData.Width / 2;
                xmlData.CenterY = xmlData.Height / 2;
            }

            UpdateDatagridToCanvas();
        }

        // Datagrid - 라인수정
        private void CurrentXmlDatasInDatagrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateDatagridToCanvas();
        }
        // Datagrid의 값이 변경되면 Canvas로 값 이동
        public void UpdateDatagridToCanvas()
        {
            if (CurrentXmlDatasInDatagrid.Count == 0)
                return;

            string name = CurrentXmlDatasInDatagrid[0].XmlName;

            // AllXmlDatas에서 name과 동일한 요소를 전부 삭제
            var itemsToRemoveInAll = AllXmlDatas.Where(data => data.XmlName == name).ToList();
            foreach (var item in itemsToRemoveInAll)
            {
                AllXmlDatas.Remove(item);
            }

            // CurrentXmlDatasInDatagrid에 있는 요소를 AllXmlDatas에 추가
            foreach (var item in CurrentXmlDatasInDatagrid)
            {
                AllXmlDatas.Add(item);
            }

            // CurrentXmlDatasInCanvas에서 name과 동일한 요소를 삭제
            var itemsToRemoveInCanvas = CurrentXmlDatasInCanvas.Where(data => data.XmlName == name).ToList();
            foreach (var item in itemsToRemoveInCanvas)
            {
                CurrentXmlDatasInCanvas.Remove(item);
            }

            // AllXmlDatas에서 name과 동일한 요소를 CurrentXmlDatasInCanvas에 추가
            var itemsToAddToCanvas = AllXmlDatas.Where(data => data.XmlName == name).ToList();
            foreach (var item in itemsToAddToCanvas)
            {
                CurrentXmlDatasInCanvas.Add(item);
            }
        }

        //xml리스트에서 선택
        private void SelectedXmlList(XmlList SelectedXmlListItem)
        {
            string name = isSelected.getxmlItemName(SelectedXmlListItem);
            //AllXmlDatas에서 name에 해당하는 xmlData추출
            CurrentXmlDatasInDatagrid.Clear();
            var temp = ModifyDatas.Add_FindXmlDataByName(name, AllXmlDatas);
            foreach (var xmlData in temp)
            {
                CurrentXmlDatasInDatagrid.Add(xmlData);
            }
        }

        //Image리스트에서 선택
        private void SelectedImageList(ImageModel SelectedImageListItem)
        {
            string name = isSelected.getImageItemName(SelectedImageListItem);
            CurrentImageInCanvas = ModifyDatas.FindImageDataByName(name, CurrentImageInCanvas, ImageList);
        }
        //XmlList체크박스
        private void IsCheckedXmlList(object parameter)
        {
            CheckBox checkBox = parameter as CheckBox;
            string name = checkBox.Content.ToString();
            if (checkBox.IsChecked == true)
            {
                var temp = ModifyDatas.Add_FindXmlDataByName(name, AllXmlDatas);
                foreach (var xmlData in temp)
                {
                    CurrentXmlDatasInCanvas.Add(xmlData);
                }
                
            }
            else
            {
                var temp = CurrentXmlDatasInCanvas.Where(data => data.XmlName == name).ToList();
                foreach (var xmlData in temp)
                {
                    CurrentXmlDatasInCanvas.Remove(xmlData);
                }
            }
        }

        private void SaveXml(object parameter)
        {
            XmlList xmlList = parameter as XmlList;
            ObservableCollection<XmlModel> temp =  ModifyDatas.Add_FindXmlDataByName(xmlList.XmlName, AllXmlDatas);
            SaveDataToXml.WriteXml(temp);
        }

        //이미지 열기
        private void OpenImage(object parameter)
        {
            string imagePath = fileLoader.LoadImageFile();
            
            if (string.IsNullOrEmpty(imagePath))
                return;

            FilePathModel.ImagePath = imagePath;
            string imageName = fileLoader.GetFileNameWithoutExtension(imagePath);

            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            ImageList.Add(new ImageModel { ImageName = imageName, BackgroundImage = bitmapImage });
            CurrentImageInCanvas.ImageName = imageName;
            CurrentImageInCanvas.BackgroundImage = bitmapImage;
        }

        //Xml 열기
        private int tempColor = 0;
        private string[] colorList = { "Red", "Green", "Purple", "Coral", "Navy", "SpringGreen" };

        private void OpenXml(object parameter)
        {
            string xmlPath = fileLoader.LoadXmlFile();

            if (string.IsNullOrEmpty(xmlPath))
                return;

            FilePathModel.XmlPath = xmlPath;


            List<XmlModel> parsedData = fileLoader.ParseXml(FilePathModel.XmlPath);
            //주의: 기존의 XmlDatas의 정보가 사라짐
            CurrentXmlDatasInDatagrid.Clear();
            foreach (var item in parsedData)
            {
                item.Color = colorList[tempColor];
                CurrentXmlDatasInDatagrid.Add(item);
            }
            tempColor++;
            if (tempColor == colorList.Length)
                tempColor = 0;

            AllXmlDatas = ModifyDatas.AddAToB(CurrentXmlDatasInDatagrid, AllXmlDatas);

            XmlLists.Add(new XmlList { XmlName = parsedData[0].XmlName, Color = parsedData[0].Color, IsChecked = true});

        }

    }
}