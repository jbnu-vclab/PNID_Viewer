using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNID_Viewer.Model
{
    public class RectangleModel : INotifyPropertyChanged
    {
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

        private int x;
        public int X
        {
            get { return x; }
            set { x = value; OnPropertyChanged(nameof(X)); }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; OnPropertyChanged(nameof(Y)); }
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
