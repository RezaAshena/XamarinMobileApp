using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Tms_Travel.Model;
using Tms_Travel.ViewModel.Commands;

namespace Tms_Travel.ViewModel
{
   public  class ListPicVM: INotifyPropertyChanged
    {
        // public Picture Pictures { get; set;}

        private Picture picture;

        public Picture Picture
        {
            get { return picture; }
            set { picture = value;
                OnPropertyChanged("Picture");
                }
        }



        public PicCommand PicCommand { get; set; }

        
        private string filename;
        public string FileName
        {
            get { return filename; }
            set
            {
                filename = value;
                Picture = new Picture()
                {
                    FileName = this.FileName,
                    URL = this.URL

                };
                OnPropertyChanged("FileName");
            }
        }


        private string url;
        public string URL
        {
            get { return url; }
            set
            {
                url = value;
                filename = value;
                Picture = new Picture()
                {
                    FileName = this.FileName,
                    URL = this.URL

                };
                OnPropertyChanged("URL");
            }
        }

        private string content;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
                OnPropertyChanged("Image");
            }
        }


        //public ObservableCollection<Picture> Pictures { get; set; }

        public ListPicVM()
        {
            // Pictures = new ObservableCollection<Picture>();
            Picture = new Picture();
          // PicCommand = new PicCommand(this);

        }
        public static async Task<bool> GetPic()
        {
            try
            {
                var pics = await Picture.GetPicturesByUserId(App.user.Id);
                if(pics !=null)
                {
                    pics.Clear();
                    foreach (var pic in pics)
                        pics.Add(pic);
                }
                return true;
            }
            catch (Exception ex)
            {

                var a = ex.Message;
                throw;
            }
        }

        public static Picture Pics()
        {
            var pic = new Picture()
            {
               FileName= "005f056c-af46-4c57-ae2f-23246187f3b2.jpg",
               URL= "http://216.191.35.106:52792/MobileApp2019/api/pictures/pic/005f056c-af46-4c57-ae2f-23246187f3b2",
               CREATEDAT=DateTime.Now,
               UserId = "64d2e714-d79f-4c84-87ce-853013313dad"

            };
            return pic;
        }

        public static async Task<List<Picture>> GetPicByUserId()
        {
            try
            {
                var pics = await Picture.GetPicturesByUserId(App.user.Id);
                if (pics != null)
                {
                    pics.Clear();
                    foreach (var pic in pics)
                        pics.Add(pic);
                }
                return pics;
            }
            catch (Exception ex)
            {

                var a = ex.Message;
                throw;
            }

        }
    }
}
