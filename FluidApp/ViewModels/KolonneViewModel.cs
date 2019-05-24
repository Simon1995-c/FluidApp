using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Handlers;
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
{
    class KolonneViewModel : INotifyPropertyChanged
    {
        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand NavigerOpretSkemaCommand { get; set; }
        public RelayCommand GenindlæsCommand { get; set; }
        public RelayCommand<int> SeMere { get; set; }
        public RelayCommand<int> SletSkemaRelayCommand { get; set; }
        public RelayCommand<int> RedigerSkemaRelayCommand { get; set; }


        public Visibility OpretSkemaVisibility { get; set; }
        public List<string> SorteringsMuligheder { get; set; }
        public string _sorteringsValg;
        public Visibility PageContentVisibility { get; set; }
        public Visibility ErrorVisibility { get; set; }
        private bool _loadingVisibility;

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

        public bool LoadingVisibility
        {
            get { return _loadingVisibility; }
            set
            {
                _loadingVisibility = value; 
                OnPropertyChanged();
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

            KolonneListe = new ObservableCollection<Forside>(KolonneListe.OrderByDescending(e => e.Dato));
        }

        public KolonneViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            NavigerOpretSkemaCommand = new RelayCommand(OpretNytSkema);
            GenindlæsCommand = new RelayCommand(UpdateList);
            SeMere = new RelayCommand<int>(SeMereFunc);

            SletSkemaRelayCommand = new RelayCommand<int>(SletSkema);
            RedigerSkemaRelayCommand = new RelayCommand<int>(RedigerSkema);

            ErrorVisibility = Visibility.Collapsed;

            CheckIp();
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

        private async void CheckIp()
        {
            LoadingVisibility = true;
            ipHandler h = new ipHandler();
            //If the IP isn't allowed -> send them to an error page
            if (!Application.Current.Resources.ContainsKey("allowedIP"))
            {
                bool check = await h.isAllowedIp();
                if (!check)
                {
                    PageContentVisibility = Visibility.Collapsed;
                    ErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Application.Current.Resources["allowedIP"] = true;
                }
            }

            LoadingVisibility = false;
        }

        private void RedigerSkema(int id)
        {
            Forside f = new Forside();
            Application.Current.Resources["editForside"] = f.GetOne(id);

            var frame = new Frame();
            frame.Navigate(typeof(OpretKolonne), null);
            Window.Current.Content = frame;
        }

        private async void SletSkema(int id)
        {
            Kolonne2 k = new Kolonne2();
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Vil du slette dette skema permanent?",
                Content = "Dette kan ikke fortrydes.",
                CloseButtonText = "Ikke nu",
                PrimaryButtonText = "Slet",
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                k.Delete(id);
                var dialog = new MessageDialog("Skemaet er blevet slettet");

                UICommand okBtn = new UICommand("Luk");
                dialog.Commands.Add(okBtn);

                dialog.ShowAsync();

                UpdateList();
            }  
        }

        private void SeMereFunc(int id)
        {
            Forside f = new Forside();
            Application.Current.Resources["forside"] = f.GetOne(id);

            var frame = new Frame();
            frame.Navigate(typeof(KolonneEdit), null);
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

        public async Task<ObservableCollection<Forside>> GetDatasets()
        {
            Forside tempForside = new Forside();
            ObservableCollection<Forside> data = new ObservableCollection<Forside>();

            foreach (var att in await tempForside.GetAll())
            {
                data.Add(att);
            }

            data = new ObservableCollection<Forside>(data.OrderByDescending(e => e.Dato));

            return data;
        }

        public async void UpdateList()
        {
            LoadingVisibility = true;
            KolonneListe = await GetDatasets();
            LoadingVisibility = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
