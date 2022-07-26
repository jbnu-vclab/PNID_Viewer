using Microsoft.Win32;
using PNID_Viewer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace PNID_Viewer.ViewModel.Commands
{
    public class OpenXmlCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public OpenXmlCommand(ViewerVM vm)
        {
            VM = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //VM의 함수 호출
            VM.OpenXml();
            //XML 불러오기 & XML 정보 저장
            VM.GetXmlDatas();
        }
    }
}
