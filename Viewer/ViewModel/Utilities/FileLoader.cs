using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Xml;
using Viewer.Model;
using System.IO;

namespace Viewer.ViewModel.Utilities
{
    class FileLoader
    {

        public string GetFileNameWithoutExtension(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return fileName;
        }

        public List<XmlModel> ParseXml(string filePath)
        {
            List<XmlModel> xmlDatas = new List<XmlModel>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            string fileNameWithoutExtension = GetFileNameWithoutExtension(filePath);

            XmlNodeList symbolObjects = xmlDoc.SelectNodes("/annotation/symbol_object");
            foreach (XmlNode symbolObjectNode in symbolObjects)
            {
                XmlModel obj = new XmlModel();

                obj.Type = symbolObjectNode.SelectSingleNode("type").InnerText;
                obj._Class = symbolObjectNode.SelectSingleNode("class").InnerText;
                obj.Degree = Convert.ToDouble(symbolObjectNode.SelectSingleNode("degree").InnerText);
                obj.Flip = symbolObjectNode.SelectSingleNode("flip").InnerText;

                XmlNode bndboxNode = symbolObjectNode.SelectSingleNode("bndbox");
                obj.Xmin = Convert.ToInt32(bndboxNode.SelectSingleNode("xmin").InnerText);
                obj.Ymin = Convert.ToInt32(bndboxNode.SelectSingleNode("ymin").InnerText);
                obj.Xmax = Convert.ToInt32(bndboxNode.SelectSingleNode("xmax").InnerText);
                obj.Ymax = Convert.ToInt32(bndboxNode.SelectSingleNode("ymax").InnerText);

                obj.Width = obj.Xmax - obj.Xmin;
                obj.Height = obj.Ymax - obj.Ymin;

                obj.CenterX = obj.Width / 2;
                obj.CenterY = obj.Height / 2;

                obj.XmlName = fileNameWithoutExtension;
                obj._Stroke = 4;

                xmlDatas.Add(obj);
            }
            return xmlDatas;
        }
        public string LoadImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public string LoadXmlFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files(*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
