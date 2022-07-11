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

//TODO: 이름바꾸기 OpenCommand로
namespace PNID_Viewer.ViewModel.Commands
{
    class OpenImageCommand : ICommand
    {
        //TODO: Model 사용하기
        public ViewerVM viewerVM { get; set; }
        public OpenImageCommand(ViewerVM vm)
        {
            viewerVM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO: 파일탐색기 -> 원하는 사진 선택 -> 사진 띄우기
            filePath.ImagePath = FileExplorer();
            //MessageBox.Show(ImagePath);       //확인완료

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
