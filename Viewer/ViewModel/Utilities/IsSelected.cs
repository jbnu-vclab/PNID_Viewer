using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Viewer.Model;

namespace Viewer.ViewModel.Utilities
{
    class IsSelected
    {
        public string getxmlItemName (XmlList SelectedXmlListItem)
        {
            return SelectedXmlListItem.XmlName;
        }
        public string getImageItemName(ImageModel SelectedXmlListItem)
        {
            return SelectedXmlListItem.ImageName;
        }
    }
}
