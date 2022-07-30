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
    public class ViewButtonCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public ViewButtonCommand(ViewerVM vm)
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
            VM.ViewData(cb);
        }
    }
}
