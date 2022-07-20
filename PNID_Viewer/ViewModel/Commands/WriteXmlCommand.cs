using PNID_Viewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace PNID_Viewer.ViewModel.Commands
{
    class WriteXmlCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            writeXML();
        }

        private void writeXML()
        {
            XmlModel xmldatas = new XmlModel();

            string dir = @"C:\Edited_Xmls";
            string fname = xmldatas.Filename + "_edited.xml";
            string strXMLPath = Path.Combine(dir, fname);


            using (XmlWriter wr = XmlWriter.Create(strXMLPath))
            {
                wr.WriteStartDocument();
                wr.WriteStartElement("annotation");
                wr.WriteElementString("filmename", xmldatas.Filename + "_edited");

                wr.WriteStartElement("size");
                wr.WriteElementString("width", xmldatas.Width.ToString());   // 수정 要 : 데이터를 받아올 곳이 필요함
                wr.WriteElementString("height", xmldatas.Height.ToString());
                wr.WriteElementString("depth", xmldatas.Depth.ToString());
                wr.WriteEndElement();

                wr.WriteEndElement();
                wr.WriteEndDocument();
            }
        }
    }
}
