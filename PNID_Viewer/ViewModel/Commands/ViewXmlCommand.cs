using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace PNID_Viewer.ViewModel.Commands
{
    public class ViewXmlCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public ViewXmlCommand(ViewerVM vm)
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
            //Listview에서 우클릭 시 나오는 '보기' 메뉴 클릭했을 때
            String cb = (String)parameter;
            VM.ViewData(cb);
        }
    }
}
