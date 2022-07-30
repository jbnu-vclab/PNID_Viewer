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
            String cb = (String)parameter;
            VM.WriteXml(cb);
        }
    }
}
