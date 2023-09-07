using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Viewer.Model
{
    class ImageModel : INotifyPropertyChanged
    {
        private string imageName;

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; OnPropertyChanged(nameof(ImageName)); }
        }

        private BitmapSource backgroundImage;
        public BitmapSource BackgroundImage
        {
            get => backgroundImage;
            set
            {
                if (backgroundImage != value)
                {
                    backgroundImage = value;
                    OnPropertyChanged(nameof(BackgroundImage));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
