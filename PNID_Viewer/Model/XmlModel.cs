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


        private string filename;
        public string Filename
        {
            get { return filename; }
            set { filename = value; OnPropertyChanged(nameof(Filename)); }
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

        private int depth;
        public int Depth
        {
            get { return depth; }
            set { depth = value; OnPropertyChanged(nameof(Depth)); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
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

        private int rectangleWidth;

        public int RectangleWidth
        {
            get { return rectangleWidth; }
            set { rectangleWidth = value; OnPropertyChanged(nameof(RectangleWidth)); }
        }

        private int rectangleHeight;

        public int RectangleHeight
        {
            get { return rectangleHeight; }
            set { rectangleHeight = value; OnPropertyChanged(nameof(RectangleHeight)); }
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
                //질문 : 요소가 변화했을 때 
                if(propertyName.Equals("Xmin") || propertyName.Equals("Ymin") || 
                    propertyName.Equals("Xmax") || propertyName.Equals("Ymax"))
                {
                    if(Xmax > Xmin)
                        RectangleWidth = Xmax - Xmin;
                    else
                        RectangleWidth = Xmin - Xmax;

                    if(Ymax > Ymin)
                        RectangleHeight = Ymax - Ymin;
                    else
                        RectangleHeight = Ymin - Ymax;
                }
            }
        }
    }
}