using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNID_Viewer.Model
{

    public class XmlModel : INotifyPropertyChanged
    {
        private string xmlFilename;
        public string XmlFilename
        {
            get { return xmlFilename; }
            set { xmlFilename = value; OnPropertyChanged(nameof(XmlFilename)); }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        private string _class;

        public string Class
        {
            get { return _class; }
            set { _class = value; OnPropertyChanged(nameof(Class)); }
        }

        private string flip;

        public string Flip
        {
            get { return flip; }
            set { flip = value; OnPropertyChanged(nameof(Flip)); }
        }


        private double degree;
        public double Degree
        {
            get { return degree; }
            set { degree = value; OnPropertyChanged(nameof(Degree)); }
        }

        private int xmin;
        public int Xmin
        {
            get { return xmin; }
            set { xmin = value; OnPropertyChanged(nameof(Xmin)); }
        }

        private int x1;

        public int X1
        {
            get { return x1; }
            set { x1 = value; OnPropertyChanged(nameof(X1)); }
        }

        private int y1;

        public int Y1
        {
            get { return y1; }
            set { y1 = value; OnPropertyChanged(nameof(Y1)); }
        }

        private int x2;

        public int X2
        {
            get { return x2; }
            set { x2 = value; OnPropertyChanged(nameof(X2)); }
        }

        private int y2;

        public int Y2
        {
            get { return y2; }
            set { y2 = value; OnPropertyChanged(nameof(Y2)); }
        }
        private int x3;

        public int X3
        {
            get { return x3; }
            set { x3 = value; OnPropertyChanged(nameof(X3)); }
        }

        private int y3;

        public int Y3
        {
            get { return y3; }
            set { y3 = value; OnPropertyChanged(nameof(Y3)); }
        }

        private int x4;

        public int X4
        {
            get { return x4; }
            set { x4 = value; OnPropertyChanged(nameof(X4)); }
        }

        private int y4;

        public int Y4
        {
            get { return y4; }
            set { y4 = value; OnPropertyChanged(nameof(Y4)); }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; OnPropertyChanged(nameof(Color)); }
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