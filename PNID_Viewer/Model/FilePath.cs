using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//사용 못하고 있음
namespace PNID_Viewer.Model
{
    class FilePath : INotifyPropertyChanged
    {
        public FilePath()
        {
            ImagePath = "";
            XmlPath = "";
        }
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        private string xmlPath;
        public string XmlPath
        {
            get { return xmlPath; }
            set { xmlPath = value; OnPropertyChanged(nameof(XmlPath)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
