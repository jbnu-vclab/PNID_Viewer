using Microsoft.Win32;
using PNID_Viewer.Model;
using PNID_Viewer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace PNID_Viewer.ViewModel
{
    //TODO: 표 수정시 ctrl + Z 기능이 없음
    public class ViewerVM : Canvas
    {
        public XmlModel XmlModel { get; set; }
        public FilePathModel FilePathModel { get; set; }

        //XmlPathList는 이미 열린 파일인지 확인을 위한 것, XmlFileNameList은 리스트에 바인딩하기 위한 것
        public BindingList<string> XmlPathList { get; set; }
        public ObservableCollection<string> XmlFileNameList { get; set; }

        //파일을 열면 XmlDatas에 전부 추가됨, 리스트에서 체크된 파일만 CheckedXmlDatas에 추가되어 화면에 보임
        public ObservableCollection<XmlModel> XmlDatas { get; set; }
        public ObservableCollection<XmlModel> CheckedXmlDatas { get; set; }
        public ObservableCollection<XmlModel> ViewXmlDatas { get; set; }
        public ObservableCollection<XmlModel> TempXmlDatas { get; set; }

        public OpenXmlCommand OpenXmlCommand { get; set; }              //Xml을 열기
        public WriteXmlCommand WriteXmlCommand { get; set; }            //Xml을 내보내기
        public IsCheckedCommand IsCheckedCommand { get; set; }          //Xml을 Checkbox에서 선택하기

        //생성자
        public ViewerVM()
        {
            XmlModel = new XmlModel();
            FilePathModel = new FilePathModel();

            XmlPathList = new BindingList<string>();
            XmlFileNameList = new ObservableCollection<string>();

            XmlDatas = new ObservableCollection<XmlModel>();
            CheckedXmlDatas = new ObservableCollection<XmlModel>();
            ViewXmlDatas = new ObservableCollection<XmlModel>();

            OpenXmlCommand = new OpenXmlCommand(this);
            WriteXmlCommand = new WriteXmlCommand(this);
            IsCheckedCommand = new IsCheckedCommand(this);

            this.MouseLeftButtonDown += OnMouseLeftButtonDownCommand;
            this.MouseMove += OnMouseMoveCommand;
        }

        Point start;       //Box의 시작점을 저장
        Point end;         //Box의 끝점을 저장
        int tempNum = -1;  //시작점이 찍혔는지 확인하는 변수

        //Ctrl + 마우스 좌클릭
        public void OnMouseLeftButtonDownCommand(object sender, MouseButtonEventArgs e)
        {
            //시작점을 찍을 때
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && tempNum == -1)
            {
                XmlModel temp = new XmlModel();
                start = e.GetPosition((IInputElement)sender);
                CheckedXmlDatas.Insert(0, temp);
                XmlDatas.Insert(0, temp);
                tempNum = 1;
            }

            //끝점을 찍을 때
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && tempNum == 1)
            {
                end = e.GetPosition((IInputElement)sender);

                XmlModel temp = new XmlModel();
                temp.XmlFilename = ViewXmlDatas[0].XmlFilename;
                temp.Color = ViewXmlDatas[0].Color;
                temp.Xmax = Math.Max((int)start.X, (int)end.X);
                temp.Xmin = Math.Min((int)start.X, (int)end.X);
                temp.Ymax = Math.Max((int)start.Y, (int)end.Y);
                temp.Ymin = Math.Min((int)start.Y, (int)end.Y);

                temp.RectangleWidth = temp.Xmax - temp.Xmin;
                temp.RectangleHeight = temp.Ymax - temp.Ymin;
                XmlDatas[0] = temp;
                CheckedXmlDatas[0] = temp;
                ViewXmlDatas.Insert(0, temp);
                tempNum = -1;
            }
        }

        //Box의 시작점이 찍히고 마우스가 움직일 때
        public void OnMouseMoveCommand(object sender, MouseEventArgs e)
        {
            if (tempNum == 1) //시작점이 찍혔을 때만
            {
                end = e.GetPosition((IInputElement)sender);

                XmlModel temp = new XmlModel();
                temp.XmlFilename = ViewXmlDatas[0].XmlFilename;
                temp.Color = ViewXmlDatas[0].Color;

                temp.Xmax = Math.Max((int)start.X, (int)end.X);
                temp.Xmin = Math.Min((int)start.X, (int)end.X);
                temp.Ymax = Math.Max((int)start.Y, (int)end.Y);
                temp.Ymin = Math.Min((int)start.Y, (int)end.Y);

                temp.RectangleWidth = temp.Xmax - temp.Xmin;
                temp.RectangleHeight = temp.Ymax - temp.Ymin;
                CheckedXmlDatas[0] = temp;

                XmlDatas[0] = temp;
            }
        }

        //ViewXmlCommand에서 사용
        public void ViewData(string _XmlFileName)
        {
            ViewXmlToXml();

            ViewXmlDatas.Clear();
            //XmlDatas -> ViewXmlDaatas
            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    ViewXmlDatas.Add(item);
                }
            }
        }

        //IsCheckedCommand에서 사용
        //Checked -> XmlDatas에서 CheckedXmlDatas으로 정보 전달
        public void AddData(string _XmlFileName)
        {
            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    
                    CheckedXmlDatas.Add(item);
                }
            }

        }
        //Unchecked -> CheckedXmlDatas의 데이터를 XmlDatas에 전달후 CheckedXmlDatas에서 해당 데이터 삭제
        public void DeleteData(string _XmlFileName)
        {
            TempXmlDatas = new ObservableCollection<XmlModel>();

            foreach (var item in CheckedXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    TempXmlDatas.Add(item);
                }
            }
            foreach (var item in TempXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Remove(item);
                }
            }
            foreach (var item in CheckedXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Add(item);

                }
            }
            
            foreach (var item in TempXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    CheckedXmlDatas.Remove(item);
                }
            }
        }

        //Xml의 경로를 불러옴
        public void OpenXml()
        {
            FilePathModel.XmlPath = FileExplorer();
        }

        int tempFilenum = -1;   //Xml 파일을 하나라도 열었는지 확인하는 변수

        public void GetXmlDatas()
        {
            //xml파일만 진행됨(빈파일도 X)
            if (String.IsNullOrEmpty(FilePathModel.XmlPath) || (FilePathModel.XmlPath.Length <= 3)) return;
            else
            {
                string CheckXmlFile = FilePathModel.XmlPath.Substring(FilePathModel.XmlPath.Length - 3, 3);
                if (!CheckXmlFile.Equals("xml")) return;
            }

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

            tempFilenum = 1;
        }


        int tempColor = 0;  //몇 번 째로 열린 파일인지 확인하기 위한 변수

        //Xml의 정보를 읽음
        private void ReadXml(string filePath)
        {
            string colorinfo = "";

            if (tempColor == 0)     //열린 순서에 따라 색깔 구분
                colorinfo = "Red";

            else if (tempColor == 1)
                colorinfo = "RoyalBlue";

            else if (tempColor == 2)
                colorinfo = "Green";

            else if (tempColor == 3)
                colorinfo = "Purple";

            else if (tempColor == 4)
                colorinfo = "Coral";

            else if (tempColor == 5)
                colorinfo = "Navy";

            else if (tempColor == 6)
                colorinfo = "SpringGreen";

            tempColor++;

            if (tempColor == 7)
                tempColor = 0;

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "type":
                                XmlModel.Type = reader.ReadString();
                                break;
                            case "class":
                                XmlModel.Class = reader.ReadString();
                                XmlModel.XmlFilename = FindNameToXmlPath(FilePathModel.XmlPath);
                                break;
                            case "degree":
                                XmlModel.Degree = Convert.ToDouble(reader.ReadString());
                                break;
                            case "xmin":
                                double s = reader.ReadElementContentAsDouble();
                                XmlModel.Xmin = Convert.ToInt32(s);
                                break;
                            case "ymin":
                                XmlModel.Ymin = Convert.ToInt32(reader.ReadString());
                                break;
                            case "xmax":
                                XmlModel.Xmax = Convert.ToInt32(reader.ReadString());
                                break;
                            case "ymax":
                                XmlModel.Ymax = Convert.ToInt32(reader.ReadString());
                                break;
                            case "flip":
                                XmlModel.Flip = reader.ReadString();

                                XmlModel.RectangleWidth = XmlModel.Xmax - XmlModel.Xmin;
                                XmlModel.RectangleHeight = XmlModel.Ymax - XmlModel.Ymin;

                                XmlModel temp = new XmlModel();
                                temp.XmlFilename = XmlModel.XmlFilename;
                                temp.Type = XmlModel.Type;
                                temp.Class = XmlModel.Class;
                                temp.Flip = XmlModel.Flip;
                                temp.Degree = XmlModel.Degree;
                                temp.Xmin = XmlModel.Xmin;
                                temp.Ymin = XmlModel.Ymin;
                                temp.Xmax = XmlModel.Xmax;
                                temp.Ymax = XmlModel.Ymax;
                                temp.RectangleWidth = XmlModel.RectangleWidth;
                                temp.RectangleHeight = XmlModel.RectangleHeight;
                                temp.Color = colorinfo;
                                XmlDatas.Add(temp);

                                if (tempFilenum == -1)      //처음 불러오는 Xml이라면 Datagrid에 바로 보여줌
                                    ViewXmlDatas.Add(temp);

                                break;
                        }
                    }
                }
            }
        }

        //WriteXmlCommand에서 사용
        public void WriteXml(string _XmlFileName)
        {
            ViewXmlToXml(_XmlFileName);

            string str;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xml Files(*.xml;)|*.xml;|All files (*.*)|*.*";

            if (dialog.ShowDialog() == true)
                str = dialog.FileName;
            else
                return;

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false
            };

            using (XmlWriter wr = XmlWriter.Create(str, settings))
            {
                
                wr.WriteStartDocument();
                wr.WriteStartElement("annotation");
                foreach (var item in XmlDatas)  //XmlDatas에서 전달된 파일명과 같은 파일명을 가진 데이터들만 불러옴
                {
                    if (item.XmlFilename.Equals(_XmlFileName))
                    {
                        wr.WriteStartElement("symbol_object");
                        //TODO :name에 빈 값이 나올 때 오류 발생
                        wr.WriteElementString("type", item.Type);
                        wr.WriteElementString("class", item.Class);
                        wr.WriteStartElement("bndbox");
                        wr.WriteElementString("xmin", item.Xmin.ToString());
                        wr.WriteElementString("ymin", item.Ymin.ToString());
                        wr.WriteElementString("xmax", item.Xmax.ToString());
                        wr.WriteElementString("ymax", item.Ymax.ToString());
                        wr.WriteEndElement();  //bndbox
                        wr.WriteElementString("degree", item.Degree.ToString());
                        wr.WriteElementString("flip", item.Flip);
                        wr.WriteElementString("etc", null);
                        wr.WriteEndElement();  //object
                    }
                }
                wr.WriteEndElement();  //annotation
                wr.WriteEndDocument();
            }
        }

        //ViewXmlDatas의 데이터를 XmlDatas에 전달
        public void ViewXmlToXml(string _XmlFileName)
        {
            TempXmlDatas = new ObservableCollection<XmlModel>();

            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    TempXmlDatas.Add(item);
                }
            }

            foreach (var item in TempXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Remove(item);
                }
            }
            foreach (var item in ViewXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Add(item);

                }
            }
        }
        public void ViewXmlToXml()
        {
            TempXmlDatas = new ObservableCollection<XmlModel>();
            string _XmlFileName = ViewXmlDatas[0].XmlFilename;

            foreach (var item in XmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    TempXmlDatas.Add(item);
                }
            }

            foreach (var item in TempXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Remove(item);
                }
            }
            //TODO: ViewXmlDatas 교체시....
            foreach (var item in ViewXmlDatas)
            {
                if (item.XmlFilename.Equals(_XmlFileName))
                {
                    XmlDatas.Add(item);

                }
            }
        }

        //주의)이 함수는 xml 불러올 떄만 사용됨. image불러오는 함수는 OpenImageCommand에 작성됨.
        private string FileExplorer()
        {
            OpenFileDialog dig = new OpenFileDialog();
            dig.Filter = "Xml Files(*.xml;)|*.xml;|All files (*.*)|*.*";
            bool? result = dig.ShowDialog();

            if (result == true) return dig.FileName;
            else return string.Empty;
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
