using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Viewer.Model;

namespace Viewer.ViewModel.Utilities
{
    class ModifyDatas
    {
        public ObservableCollection<XmlModel> AddAToB(ObservableCollection<XmlModel> A, ObservableCollection<XmlModel> B)
        {
            foreach (var item in A)
            {
                B.Add(item);
            }
            return B;
        }
        //이름에 해당하는 데이터만 넣기
        public ObservableCollection<XmlModel> Add_FindXmlDataByName(string name, ObservableCollection<XmlModel> B)
        {
            ObservableCollection<XmlModel> A = new ObservableCollection<XmlModel>();

            var matchingXmlData = B.Where(Data => Data.XmlName == name);
            foreach (var xmlData in matchingXmlData)
            {
                A.Add(xmlData);
            }
            return A;
        }
        public ObservableCollection<XmlModel> Delete_FindXmlDataByName(string name, ObservableCollection<XmlModel> A)
        {
            List<XmlModel> matchingXmlData = A.Where(data => data.XmlName == name).ToList();
            foreach (var xmlData in matchingXmlData)
            {
                A.Remove(xmlData);
            }
            return A;

        }

        public ImageModel FindImageDataByName(string name, ImageModel A, ObservableCollection<ImageModel> B)
        {
            var matchingImageData = B.FirstOrDefault(Data => Data.ImageName == name);
            A.BackgroundImage = matchingImageData.BackgroundImage;
            A.ImageName = matchingImageData.ImageName;
            return A;
        }


    }
}
