using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PNID_Viewer.ViewModel.Commands
{
    public class IsCheckedCommand : ICommand
    {
        public ViewerVM VM { get; set; }
        public IsCheckedCommand(ViewerVM vm)
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
            CheckBox checkBox = (CheckBox)parameter;
            TextBlock textBlock = (TextBlock)checkBox.Content;

            String cb = textBlock.Text.ToString();

            if (checkBox.IsChecked == true)
            {
                //체크O - 컬랙션에 넣기               
                VM.AddData(cb);
            }
            else
            {
                //체크X - 컬랙션에서 제거
                VM.DeleteData(cb);


            }
            //Listview클릭했을 때
            VM.ViewData(cb);
        }
    }
}
