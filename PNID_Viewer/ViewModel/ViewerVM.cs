using Microsoft.Win32;
using PNID_Viewer.Model;
using PNID_Viewer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        //질문: ObservableCollection<Model> 1개 만들기 vs ObservableCollection<int> 여러 개 만들기
        //TODO해결: ListBox에 바인딩
        public ObservableCollection<string> XmlPathList { get; set; }
        //질문: XmlDatas가 없을 때만 객체 생성하는 것과 생성자에서 생성하는 것과 차이점은? 초기화 시점의 차이
        public ObservableCollection<XmlModel> XmlDatas { get; set; }
        //private ObservableCollection<XmlModel> xmlDatas = null;
        //public ObservableCollection<XmlModel> XmlDatas
        //{
        //    get       //get 호출시 객체 생성
        //    {
        //        if (xmlDatas == null)
        //        {
        //            xmlDatas = new ObservableCollection<XmlModel>();
        //        }
        //        return xmlDatas;
        //    }
        //    set
        //    {
        //        xmlDatas = value;
        //    }
        //}
        public OpenXmlCommand OpenXmlCommand { get; set; }
        public ObservableCollection<RectangleModel> RectItems { get; set; }

        //생성자
        public ViewerVM()
        {
            XmlModel = new XmlModel();
            FilePathModel = new FilePathModel();
            XmlDatas = new ObservableCollection<XmlModel>();
            XmlPathList = new ObservableCollection<string>();
            RectItems = new ObservableCollection<RectangleModel>();
            OpenXmlCommand = new OpenXmlCommand(this);
        }
        //Xml불러오는 함수
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

                //XmlNameAndPathModel.XmlPath = FilePathModel.XmlPath;
                XmlPathList.Add(FilePathModel.XmlPath);
            }
            else
            {
                //XmlNameAndPathModel.XmlPath = FilePathModel.XmlPath;
                XmlPathList.Add(FilePathModel.XmlPath);
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

                                RectangleModel temp1 = new RectangleModel();
                                temp1.X = temp.Xmin;
                                temp1.Y = temp.Ymin;
                                temp1.Width = temp.Xmax - temp.Xmin;
                                temp1.Height = temp.Ymax - temp.Ymin;

                                RectItems.Add(temp1);
                                XmlDatas.Add(temp);

                                break;
                        }
                    }
                }
            }
        }
        private string FileExplorer()
        {
            OpenFileDialog dig = new OpenFileDialog();
            bool? result = dig.ShowDialog();

            if (result == true) return dig.FileName;
            else return string.Empty;
        }
    }
}
