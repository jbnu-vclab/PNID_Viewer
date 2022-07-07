using PNID_Viewer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace XMLReadAndParse
{
    class XMLReadandParse
    {
        List<string> name = new List<string>();
        List<double> degree = new List<double>();
        List<int> ymin = new List<int>();
        List<int> xmax = new List<int>();
        List<int> xmin = new List<int>();
        List<int> ymax = new List<int>();

        OpenImageCommand openImageCommand { get; set; }

        public void ReadandParse()
        {
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create(openImageCommand.ImagePath))
            {
                MessageBox.Show(openImageCommand.ImagePath);
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "name":
                                name.Add(reader.ReadString());
                                break;
                            case "degree":
                                degree.Add(Convert.ToDouble(reader.ReadString()));
                                break;
                            case "xmin":
                                xmin.Add(Convert.ToInt32(reader.ReadString()));
                                break;
                            case "ymin":
                                ymin.Add(Convert.ToInt32(reader.ReadString()));
                                break;
                            case "xmax":
                                xmax.Add(Convert.ToInt32(reader.ReadString()));
                                break;
                            case "ymax":
                                ymax.Add(Convert.ToInt32(reader.ReadString()));
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }

        }
    }
}