using PNID_Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNID_Viewer.ViewModel
{
    class ViewerVM
    {
        public FilePath FilePath { get; set; }
        public ViewerVM()
        {
            FilePath = new FilePath();
        }
    }
}
