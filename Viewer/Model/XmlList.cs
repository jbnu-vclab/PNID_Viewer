using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer.Model
{
    class XmlList : INotifyPropertyChanged
    {
        private string xmlName;
        public string XmlName
        {
            get { return xmlName; }
            set { xmlName = value; OnPropertyChanged(nameof(XmlName)); }
        }
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged(nameof(IsChecked)); }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; OnPropertyChanged(nameof(Color)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
