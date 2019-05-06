using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp
{
    class KolonneViewModel
    {
        public RelayCommand TilbageCommand { get; set; }
        public ObservableCollection<Forside> KolonneListe { get; set; }

        public KolonneViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            ipHandler h = new ipHandler();

            //If the IP isn't allowed -> send them to an error page
            if (!h.isAllowedIp().Result)
            {
                var frame = new Frame();
                frame.Navigate(typeof(errorPageIPrange), null);
                Window.Current.Content = frame;
            }

            KolonneListe = GetDatasets();
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), null);
            Window.Current.Content = frame;
        }

        public ObservableCollection<Forside> GetDatasets()
        {
            Forside tempForside = new Forside();
            ObservableCollection<Forside> data = new ObservableCollection<Forside>();

            foreach (var att in tempForside.GetAll())
            {
                
                
                data.Add(att);
            }
            return data;
        }
    }
}
