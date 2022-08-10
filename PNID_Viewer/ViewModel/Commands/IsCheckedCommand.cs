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
            if(checkBox.IsChecked == true)
            {
                //MessageBox.Show("O");

                //체크O - 컬랙션에 넣기
                VM.AddData(checkBox.Content.ToString());
            }
            else
            {
                //MessageBox.Show("X");

                //체크X - 컬랙션에서 제거
                VM.DeleteData(checkBox.Content.ToString());


            }
            //Listview에서 우클릭 시 나오는 '보기' 메뉴 클릭했을 때
            String cb = checkBox.Content.ToString();
            VM.ViewData(cb);
        }
    }
}
