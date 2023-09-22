﻿using System;
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

using GalaSoft.MvvmLight.Messaging;
using Viewer.ViewModel;


namespace Viewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void XmlContentsDataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        }
        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(canvas1);
            Viewer.ViewModel.Utilities.Messenger.Send("MouseClickMessage", mousePosition);
        }

    }
}
