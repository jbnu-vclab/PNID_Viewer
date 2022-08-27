using PNID_Viewer.Model;
using PNID_Viewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PNID_Viewer.Model;

namespace PNID_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ChangeFocusToSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    // TODO: ListView를 Selection했을 경우에도 호출됨. 왜?
        //    DataGrid dataGrid = sender as DataGrid;
        //    XmlModel viewData = dataGrid.SelectedItem as XmlModel;

        //    if (viewData != null)
        //    {
        //        int midX = (int)((viewData.Xmin + viewData.Xmax)/2.0f);
        //        int midY = (int)((viewData.Ymin + viewData.Ymax)/2.0f);
        //        border.ChangeFocusToTargetCentered(midX, midY);
        //    }
        //}
    }
}
