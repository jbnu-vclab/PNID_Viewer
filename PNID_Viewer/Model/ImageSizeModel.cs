using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNID_Viewer.Model
{
    public class ImageSizeModel : INotifyPropertyChanged
    {
        private string xmlPath;

        public string XmlPath
        {
            get { return xmlPath; }
            set { xmlPath = value; OnPropertyChanged(nameof(XmlPath)); }
        }
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; OnPropertyChanged(nameof(Width)); }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; OnPropertyChanged(nameof(Height)); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
