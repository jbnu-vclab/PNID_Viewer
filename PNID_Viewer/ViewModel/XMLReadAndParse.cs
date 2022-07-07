using PNID_Viewer.ViewModel.Commands;
using System;
using System.Windows;
using System.Xml;

namespace XMLReadAndParse
{
    class XMLReadandParse
    {
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
                                
                                break;
                            case "degree":

                                break;
                            case "xmin":

                                break;
                            case "ymin":

                                break;
                            case "xmax":

                                break;
                            case "ymax":

                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }

        }
    }
}