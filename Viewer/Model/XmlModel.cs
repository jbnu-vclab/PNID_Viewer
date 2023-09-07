using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer.Model
{
    class XmlModel : INotifyPropertyChanged
    {
        private string xmlName;
        public string XmlName
        {
            get { return xmlName; }
            set { xmlName = value; OnPropertyChanged(nameof(XmlName)); }
        }

        private int xmin;
        public int Xmin
        {
            get { return xmin; }
            set { xmin = value; OnPropertyChanged(nameof(Xmin)); }
        }

        private int ymin;
        public int Ymin
        {
            get { return ymin; }
            set { ymin = value; OnPropertyChanged(nameof(Ymin)); }
        }

        private int xmax;
        public int Xmax
        {
            get { return xmax; }
            set { xmax = value; OnPropertyChanged(nameof(Xmax)); }
        }

        private int ymax;
        public int Ymax
        {
            get { return ymax; }
            set { ymax = value; OnPropertyChanged(nameof(Ymax)); }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }
        private string _class;

        public string _Class
        {
            get { return _class; }
            set { _class = value; OnPropertyChanged(nameof(_Class)); }
        }

        private double degree;
        public double Degree
        {
            get { return degree; }
            set { degree = value; OnPropertyChanged(nameof(Degree)); }
        }

        private string flip;

        public string Flip
        {
            get { return flip; }
            set { flip = value; OnPropertyChanged(nameof(Flip)); }
        }


        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; OnPropertyChanged(nameof(Color)); }
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

        private int centerX;

        public int CenterX
        {
            get { return centerX; }
            set { centerX = value; OnPropertyChanged(nameof(CenterX)); }
        }

        private int centerY;

        public int CenterY
        {
            get { return centerY; }
            set { centerY = value; OnPropertyChanged(nameof(CenterY)); }
        }

        private int _stroke;
        public int _Stroke
        {
            get { return _stroke; }
            set { _stroke = value; OnPropertyChanged(nameof(_Stroke)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            //if(name.Equals(Xmin)|| name.Equals(Ymin) || name.Equals(Xmax) || name.Equals(Ymax))
            //{
            //    Width = Math.Abs(Xmax - Xmin);
            //    Height = Math.Abs(Ymax - Ymin);
            //    CenterX = Width / 2;
            //    CenterY = Height / 2;
            //}
        }
    }
}
