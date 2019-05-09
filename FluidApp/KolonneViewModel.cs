using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp
{
    class KolonneViewModel : INotifyPropertyChanged
    {
        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand SorterCommand { get; set; }
        public RelayCommand NavigerOpretSkemaCommand { get; set; }
        public RelayCommand GenindlæsCommand { get; set; }
        public RelayCommand<int> SeMere { get; set; }
        public Visibility OpretSkemaVisibility { get; set; }

        public ObservableCollection<Forside> _kolonneListe;
        public ObservableCollection<Forside> KolonneListe
        {
            get { return _kolonneListe; }
            set
            {
                _kolonneListe = value;
                OnPropertyChanged();
            }
        }

        public KolonneViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            SorterCommand = new RelayCommand(SortDatasets);
            NavigerOpretSkemaCommand = new RelayCommand(OpretNytSkema);
            GenindlæsCommand = new RelayCommand(UpdateList);
            SeMere = new RelayCommand<int>(SeMereFunc);

            ipHandler h = new ipHandler();

            //If the IP isn't allowed -> send them to an error page
            if (!h.isAllowedIp().Result)
            {
                var frame = new Frame();
                frame.Navigate(typeof(errorPageIPrange), null);
                Window.Current.Content = frame;
            }

            UpdateList();

            OpretSkemaVisibility = Visibility.Collapsed;

            if (Application.Current.Resources.ContainsKey("Administrator"))
            {
                if ((int)Application.Current.Resources["Administrator"] == 1 || (int)Application.Current.Resources["Administrator"] == 2)
                {
                    OpretSkemaVisibility = Visibility.Visible;
                }
                else
                {
                    OpretSkemaVisibility = Visibility.Collapsed;
                }

            }
        }

        private void SeMereFunc(int id)
        {
            Forside f = new Forside();
            Application.Current.Resources["forside"] = f.GetOne(id);

            var frame = new Frame();
            frame.Navigate(typeof(KolonneEdit), f.GetOne(id));
            Window.Current.Content = frame;
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), null);
            Window.Current.Content = frame;
        }

        public void OpretNytSkema()
        {
            var frame = new Frame();
            frame.Navigate(typeof(OpretKolonne), null);
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

        public void UpdateList()
        {
            KolonneListe = GetDatasets();
        }

        public void SortDatasets()
        {
            KolonneListe = new ObservableCollection<Forside>(KolonneListe.OrderBy(e => e.Dato));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
