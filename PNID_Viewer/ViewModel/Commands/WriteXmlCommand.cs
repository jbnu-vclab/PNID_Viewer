using PNID_Viewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace PNID_Viewer.ViewModel.Commands
{
    public class WriteXmlCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public WriteXmlCommand(ViewerVM vm)
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
            //Listview에서 우클릭 시 나오는 '내보내기' 메뉴 클릭했을 때
            String cb = (String)parameter;
            VM.WriteXml(cb);
        }
    }
}
