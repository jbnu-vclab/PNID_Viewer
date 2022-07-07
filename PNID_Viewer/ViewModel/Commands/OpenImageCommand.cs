using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PNID_Viewer.Model;
using System.ComponentModel;

namespace PNID_Viewer.ViewModel.Commands
{
    class OpenImageCommand : ICommand, INotifyPropertyChanged
    {
        //TODO: Model 사용하기
        //FilePath filePath { get; set; }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
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
            //TODO: 파일탐색기 -> 원하는 사진 선택 -> 사진 띄우기
            //TODO: 이름바꾸기 OpenCommand로
            ImagePath = FileExplorer();
            MessageBox.Show(ImagePath);

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
