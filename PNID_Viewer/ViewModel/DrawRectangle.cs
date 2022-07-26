using PNID_Viewer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PNID_Viewer.ViewModel
{
    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class DrawRectangle
    {
        public ObservableCollection<RectItem> RectItems { get; set; }

        public DrawRectangle()
        {
            readXML xml = new readXML();

            RectItems = new ObservableCollection<RectItem>();

            for (int idx = 0; idx< xml._xmin.count; idx++)
            {
                RectItems.Add(new RectItem { X = idx * 40, Y = 10, Width = 30, Height = 30 });
            }
        }
    }

}
