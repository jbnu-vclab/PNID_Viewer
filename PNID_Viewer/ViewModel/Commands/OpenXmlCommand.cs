using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PNID_Viewer.ViewModel.Commands
{
    class OpenXmlCommand : ICommand, INotifyPropertyChanged
    {
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
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            XmlPath = FileExplorer();
            MessageBox.Show(XmlPath);
            
        }
        private string FileExplorer()
        {
            OpenFileDialog dig = new OpenFileDialog();
            bool? result = dig.ShowDialog();

            if (result == true) return dig.FileName;
            else return string.Empty;
        }
    }
}
