using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tms_Travel.Helpers;
using Tms_Travel.Model;

namespace Tms_Travel.ViewModel
{
    public class PicVM:INotifyPropertyChanged
    {
        private bool isSwitchToggled;
        public bool IsSwitchToggled
        {
            get { return isSwitchToggled; }
            set
            {
                if (isSwitchToggled != value)
                    isSwitchToggled = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSwitchToggled"));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Picture> pics { get; set; }

        public Picture PreviousPicSelected { get; set; }

        private Picture picSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public Picture Picselected
        {
            get { return picSelected; }
            set {
                    if (picSelected != value)
                    {
                      picSelected = value;
                      ExpandOrCollapseSelectedItem();
                    }
                }
        }

        
        private void ExpandOrCollapseSelectedItem()
        {
           if(PreviousPicSelected != null)
            {
                pics.Where(p => p.Id == PreviousPicSelected.Id).FirstOrDefault().IsVisible = false;
            }
            
                pics.Where(p => p.Id == picSelected.Id).FirstOrDefault().IsVisible = true;
                PreviousPicSelected = picSelected;
        }

        public PicVM()
        {
            pics = new ObservableCollection<Picture>();
           // pics = Picture.GetPicsByUserId(App.user.Id);
        }


        public async Task<bool> Updatepics()
        {

            try
            {
                var pictures = await Picture.GetPicturesByUserId(App.user.Id);
                if (pics != null)
                {
                    pics.Clear();
                    foreach (var picture in pictures)
                        pics.Add(picture);
                }
                return true;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return false;
            }
        }

        public async Task<List<Picture>> GetPicsByUserId(string id)
        {
            Picture pics = new Picture();
            var url = Constants.ApiPicture + id;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Picture>>(json);
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }

    }
}
