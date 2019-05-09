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
        public RelayCommand NavigerOpretSkemaCommand { get; set; }
        public RelayCommand GenindlæsCommand { get; set; }
        public RelayCommand<int> SeMere { get; set; }
        public Visibility OpretSkemaVisibility { get; set; }
        public List<string> SorteringsMuligheder { get; set; }
        public string _sorteringsValg;

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

        public string SorteringsValg
        {
            get { return _sorteringsValg; }
            set
            {
                _sorteringsValg = value;

                switch (SorteringsValg)
                {
                    case "2 dage":
                        SorterForsider(2);
                        break;
                    case "7 dage":
                        SorterForsider(7);
                        break;
                    case "14 dage":
                        SorterForsider(14);
                        break;
                    case "30 dage":
                        SorterForsider(30);
                        break;
                    case "365 dage":
                        SorterForsider(365);
                        break;
                    default:
                        UpdateList();
                        break;
                }
            }
        }

        public void SorterForsider(int dage)
        {
            UpdateList();

            ObservableCollection<Forside> temp = KolonneListe;
            KolonneListe = new ObservableCollection<Forside>();

            foreach (var forside in temp)
            {
                if (forside.Dato > DateTime.Now.Subtract(TimeSpan.FromDays(dage)))
                {
                    KolonneListe.Add(forside);
                }
            }
        }

        public KolonneViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
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
            SorteringsMuligheder = new List<string>();
            SorteringsMuligheder.Add("Alle");
            SorteringsMuligheder.Add("2 dage");
            SorteringsMuligheder.Add("7 dage");
            SorteringsMuligheder.Add("14 dage");
            SorteringsMuligheder.Add("30 dage");
            SorteringsMuligheder.Add("365 dage");
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

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
