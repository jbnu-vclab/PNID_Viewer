using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PNID_Viewer.ViewModel.Commands
{
    public class SaveButtonClickCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public SaveButtonClickCommand(ViewerVM vm)
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
            //ViewXmlDatas를 Xml에 반영하는 함수
            VM.SaveButtonClick();
        }
    }
}
