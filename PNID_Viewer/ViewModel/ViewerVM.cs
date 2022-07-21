using Microsoft.Win32;
using PNID_Viewer.Model;
using PNID_Viewer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace PNID_Viewer.ViewModel
{

    public class ViewerVM
    {
        public XmlModel XmlModel { get; set; }
        public RectangleModel RectangleModel { get; set; }
        public FilePathModel FilePathModel { get; set; }

        public BindingList<string> XmlPathList { get; set; }
        public ObservableCollection<string> XmlFileNameList { get; set; }

        public ObservableCollection<XmlModel> XmlDatas { get; set; }
        public ObservableCollection<XmlModel> CheckedXmlDatas { get; set; }
        public ObservableCollection<RectangleModel> RectItems { get; set; }
        public ObservableCollection<RectangleModel> CheckedRectItems { get; set; }

        public OpenXmlCommand OpenXmlCommand { get; set; }
        public WriteXmlCommand WriteXmlCommand { get; set; }
        public IsCheckedCommand IsCheckedCommand { get; set; }

        //생성자
        public ViewerVM()
        {
            XmlModel = new XmlModel();
            FilePathModel = new FilePathModel();
            RectangleModel = new RectangleModel();

            XmlPathList = new BindingList<string>();
            XmlFileNameList = new ObservableCollection<string>();

            XmlDatas = new ObservableCollection<XmlModel>();
            CheckedXmlDatas = new ObservableCollection<XmlModel>();
            RectItems = new ObservableCollection<RectangleModel>();
            CheckedRectItems = new ObservableCollection<RectangleModel>();

            OpenXmlCommand = new OpenXmlCommand(this);
            WriteXmlCommand = new WriteXmlCommand(this);
            IsCheckedCommand = new IsCheckedCommand(this);
        }
        //ObservableCollection<XmlModel>에 추가하는 함수
        public void AddData(string _XmlFileName)
        {
            //TODO: 요소의 개수가 0일떄 오류 안나는지 확인
            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    //TODO: 깊은복사 안해도 되는지 확인
                    CheckedXmlDatas.Add(item);
                }
            }
            foreach (var item in RectItems)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    CheckedRectItems.Add(item);
                }
            }

        }

        //ObservableCollection<XmlModel>에서 제거하는 함수
        public void DeleteData(string _XmlFileName)
        {
            //TODO: 요소의 개수가 0일떄 오류 안나는지 확인
            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    //TODO: 깊은복사 안해도 되는지 확인
                    CheckedXmlDatas.Remove(item);

                }
            }
            foreach (var item in RectItems)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    CheckedRectItems.Remove(item);
                }
            }
        }
        //Xml 경로 불러오는 함수
        public void OpenXml()
        {
            FilePathModel.XmlPath = FileExplorer();
        }
        public void GetXmlDatas()
        {
            //TODO해결: xml파일만 진행되게(빈파일도 X)
            if (String.IsNullOrEmpty(FilePathModel.XmlPath) || (FilePathModel.XmlPath.Length <= 3)) return;
            else
            {
                string CheckXmlFile = FilePathModel.XmlPath.Substring(FilePathModel.XmlPath.Length - 3, 3);
                if (!CheckXmlFile.Equals("xml")) return;
            }

            //TODO해결: 이미 열린 파일의 경우 XmlDatas에 추가하지 않기(메세지 띄우기)
            //주의) 같은 파일이더라도 경로가 달라지면 다른 파일로 인지함
            if (XmlPathList.Count != 0)
            {
                for (int i = 0; i < XmlPathList.Count; i++)
                {
                    if (XmlPathList[i].Equals(FilePathModel.XmlPath))
                    {
                        MessageBox.Show("이미 열어본 파일입니다.");
                        return;
                    }
                }
                
                XmlPathList.Add(FilePathModel.XmlPath);
                XmlFileNameList.Add(FindNameToXmlPath(FilePathModel.XmlPath));
            }
            else
            {
                XmlPathList.Add(FilePathModel.XmlPath);
                XmlFileNameList.Add(FindNameToXmlPath(FilePathModel.XmlPath));
            }

            ReadXml(FilePathModel.XmlPath);


        }
        private void ReadXml(string filePath)
        {
            //TODO해결: XmlDatas에 같은 게 들어감 -> DataGrid문제X, 모델 깊은 복사
            string _filename = "";
            string _width = "";
            string _height = "";
            string _depth = "";
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            //TODO해결 : filename ~ depth는 1번만 들어감
                            case "filename":
                                _filename = reader.ReadString();
                                break;
                            case "width":
                                _width = reader.ReadString();
                                break;
                            case "height":
                                _height = reader.ReadString();
                                break;
                            case "depth":
                                _depth = reader.ReadString();
                                break;
                            case "name":
                                XmlModel.Name = reader.ReadString();
                                XmlModel.Filename = _filename;
                                XmlModel.Width = Convert.ToInt32(_width);
                                XmlModel.Height = Convert.ToInt32(_height);
                                XmlModel.Depth = Convert.ToInt32(_depth);

                                XmlModel.XmlFilename = FindNameToXmlPath(FilePathModel.XmlPath);

                                break;
                            case "degree":
                                XmlModel.Degree = Convert.ToDouble(reader.ReadString());
                                break;
                            case "xmin":
                                XmlModel.Xmin = Convert.ToInt32(reader.ReadString());
                                break;
                            case "ymin":
                                XmlModel.Ymin = Convert.ToInt32(reader.ReadString());
                                break;
                            case "xmax":
                                XmlModel.Xmax = Convert.ToInt32(reader.ReadString());
                                break;
                            case "ymax":
                                XmlModel.Ymax = Convert.ToInt32(reader.ReadString());

                                XmlModel temp = new XmlModel();
                                temp.XmlFilename = XmlModel.XmlFilename;
                                temp.Name = XmlModel.Name;
                                temp.Filename = XmlModel.Filename;
                                temp.Width = XmlModel.Width;
                                temp.Height = XmlModel.Height;
                                temp.Depth = XmlModel.Depth;
                                temp.Degree = XmlModel.Degree;
                                temp.Xmin = XmlModel.Xmin;
                                temp.Ymin = XmlModel.Ymin;
                                temp.Xmax = XmlModel.Xmax;
                                temp.Ymax = XmlModel.Ymax;

                                XmlDatas.Add(temp);

                                RectangleModel temp1 = new RectangleModel();
                                temp1.XmlFilename = temp.XmlFilename;
                                temp1.Name = temp.Name;
                                temp1.X = temp.Xmin;
                                temp1.Y = temp.Ymin;
                                temp1.Width = temp.Xmax - temp.Xmin;
                                temp1.Height = temp.Ymax - temp.Ymin;

                                RectItems.Add(temp1);

                                break;
                        }
                    }
                }
            }
        }
        public void WriteXml()
        {
            //using (XmlWriter wr = XmlWriter.Create(CreateFile()))
            //{
            //    wr.WriteStartDocument();
            //    wr.WriteStartElement("annotation");
            //    wr.WriteElementString("filmename", XmlDatas. + "_edited");

            //    wr.WriteStartElement("size");
            //    wr.WriteElementString("width", XmlDatas[].ToString());   // 수정 要 : 데이터를 받아올 곳이 필요함
            //    wr.WriteElementString("height", XmlDatas.Height.ToString());
            //    wr.WriteElementString("depth", XmlDatas.Depth.ToString());
            //    wr.WriteEndElement();

            //    wr.WriteEndElement();
            //    wr.WriteEndDocument();
            //}
        }
        //주의)이 함수는 xml 불러올 떄만 사용됨. image불러오는 함수는 OpenImageCommand에서 작성됨.
        private string FileExplorer()
        {
            OpenFileDialog dig = new OpenFileDialog();
            dig.Filter = "Xml Files(*.xml;)|*.xml;|All files (*.*)|*.*";
            bool? result = dig.ShowDialog();
            
            if (result == true) return dig.FileName;
            else return string.Empty;
        }
        private string CreateFile()
        {
            string path = @"C:\Edited_Xmls";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);


            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                MessageBox.Show(@"'C:\Edited_Xmls'에 파일이 저장됩니다.");
                return @"C:\Edited_Xmls";
            }
            return @"C:\Edited_Xmls";
        }
        //파일 경로 -> 파일 이름
        private string FindNameToXmlPath(string Path)
        {
            string[] words = Path.Split('\\');
            string lastWord = words[words.Length - 1];
            return lastWord;
        }
    }
}
