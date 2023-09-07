using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Viewer.Model;
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace Viewer.ViewModel.Utilities
{
    class SaveDataToXml
    {
        public void WriteXml(ObservableCollection<XmlModel> A)
        {
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

                foreach (var item in A)
                {
                    wr.WriteStartElement("symbol_object");

                    wr.WriteElementString("type", item.Type);
                    wr.WriteElementString("class", item._Class);

                    wr.WriteStartElement("bndbox");
                    wr.WriteElementString("xmin", item.Xmin.ToString());
                    wr.WriteElementString("ymin", item.Ymin.ToString());
                    wr.WriteElementString("xmax", item.Xmax.ToString());
                    wr.WriteElementString("ymax", item.Ymax.ToString());
                    wr.WriteEndElement();  // bndbox

                    wr.WriteElementString("degree", item.Degree.ToString());
                    wr.WriteElementString("flip", item.Flip);
                    wr.WriteElementString("etc", null);

                    wr.WriteEndElement();  // symbol_object
                }

                wr.WriteEndElement();  // annotation
                wr.WriteEndDocument();
            }
        }

    }
}
