using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Views;
using Models;
using GalaSoft.MvvmLight.Command;


namespace FluidApp.ViewModels
{
    public class FærdigvarekontrolEditViewModel : INotifyPropertyChanged
    {

        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand NavtilProdCommand { get; set; }
        public RelayCommand NavTilFærdigvareCommand { get; set; }
        public RelayCommand NavTilKontrolRegistreringCommand { get; set; }
        public RelayCommand NavTilKontrolSkema { get; set; }
        public RelayCommand GemCommand { get; set; }
        public RelayCommand FortrydCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public RelayCommand <int>RedigerCommand { get; set; }
        public RelayCommand <int>SletCommand { get; set; }
        public RelayCommand UdvidCommand { get; set; }


        public Færdigvarekontrol testSkema;
        public Færdigvarekontrol NytSkema { get; set; }
        public ObservableCollection<Færdigvarekontrol> SkemaUdsnit { get; set; }


        #region Properties

        public List<string> VælgMuligheder { get; set; }
        public Forside info { get; set; }

        public string RingFarve
        {
            get { return _ringFarve; }
            set
            {
                _ringFarve = value;
                OnPropertyChanged();
            }
        }

        public string LågFarve
        {
            get { return _lågFarve; }
            set
            {
                _lågFarve = value;
                OnPropertyChanged();
            }

           
        }

        public int DåseNr
        {
            get { return _dåseNr; }
            set
            {
                _dåseNr = value;
                OnPropertyChanged();
            }
        }

        public string Initialer
        {
            get { return _initialer; }
            set
            {
                _initialer = value;
                OnPropertyChanged();
            }
        }

        public int Enheder
        {
            get { return _enheder; }
            set
            {
                _enheder = value;
                OnPropertyChanged();
            }
        }

        public string Parametre
        {
            get { return _parametre; }
            set
            {
                _parametre = value;
                OnPropertyChanged();
            }
        }

        public int Multipack
        {
            get { return _multipack; }
            set
            {
                _multipack = value;
                OnPropertyChanged();
            }
        }

        public int FolieNr
        {
            get { return _folieNr; }
            set
            {
                _folieNr = value;
                OnPropertyChanged();
            }
        }

        public int KartonNr
        {
            get { return _kartonNr; }
            set
            {
                _kartonNr = value;
                OnPropertyChanged();
            }
        }

        public int PalleNr
        {
            get { return _palleNr; }
            set
            {
                _palleNr = value;
                OnPropertyChanged();
            }
        }

        public int LågNr
        {
            get { return _lågNr; }
            set
            {
                _lågNr = value;
                OnPropertyChanged();
            }
        }

        public bool NytDataVis
        {
            get { return _nytDataVis; }
            set
            {
                _nytDataVis = value;
                OnPropertyChanged();
            }
        }

        public bool GemVis
        {
            get { return _gemVis; }
            set
            {
                _gemVis = value;
                OnPropertyChanged();
            }
        }

        public string SletIkon
        {
            get { return _sletIkon; }
            set
            {
                _sletIkon = value;
                OnPropertyChanged();
            }
        }

        public string UdvidIkon
        {
            get { return _udvidIkon; }
            set
            {
                _udvidIkon = value;
                OnPropertyChanged();
            }
        }

        public string Udvidelse
        {
            get { return _udvidelse; }
            set
            {
                _udvidelse = value;
                OnPropertyChanged();
            }
        }

        public Færdigvarekontrol TestSkema
        {
            get { return testSkema; }
            set
            {
                testSkema = value;
                OnPropertyChanged();
                try
                {
                    if (testSkema.ProcessordreNr != 0) Rediger(testSkema.ProcessordreNr);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public bool UpdateVis
        {
            get { return _updateVis; }
            set
            {
                _updateVis = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Private properties

        private string _title;
        private bool _updateVis;
        private bool _gemVis;
        private bool _nytDataVis;
        private int _dåseNr;
        private string _initialer;
        private int _lågNr;
        private string _lågFarve;
        private string _ringFarve;
        private int _enheder;
        private string _parametre;
        private int _multipack;
        private int _folieNr;
        private int _kartonNr;
        private int _palleNr;
        private string _sletIkon;
        private string _udvidIkon;
        private string _udvidelse;

        #endregion

        public FærdigvarekontrolEditViewModel()
        {
            VælgMuligheder = new List<string>();
            VælgMuligheder.Add("Gul");
            VælgMuligheder.Add("Grøn");
            VælgMuligheder.Add("Rød");
            VælgMuligheder.Add("Blå");
            VælgMuligheder.Add("Hvid");
            VælgMuligheder.Add("Sort");
            Title = "Indsæt ny data";
            


            TestSkema = new Færdigvarekontrol();
            NytSkema = new Færdigvarekontrol();
            info =  new Forside();

            NytDataVis = true;
            GemVis = true;

            ResetValues();

            if (Application.Current.Resources.ContainsKey("forside"))
            {
                Forside f = (Forside)Application.Current.Resources["forside"];
                info = f;
            }

            SletIkon = "https://visualpharm.com/assets/591/Delete-595b40b75ba036ed117d7c27.svg";
            UdvidIkon = "https://visualpharm.com/assets/833/Expand-595b40b75ba036ed117d6f8f.svg";
            Udvidelse = "170";

            SkemaUdsnit = GetSkemaUdsnit();
            #region RelayCommands
            TilbageCommand = new RelayCommand(Tilbage);
            NavtilProdCommand = new RelayCommand(NavProduktionsfølgeseddel);
            NavTilKontrolRegistreringCommand = new RelayCommand(NavKontrolregistrering);
            NavTilFærdigvareCommand = new RelayCommand(NavFærdigvarekontrol);
            OpdaterCommand = new RelayCommand(Opdater);
            RedigerCommand = new RelayCommand<int>(Rediger);
            SletCommand = new RelayCommand<int>(Slet);
            UdvidCommand = new RelayCommand(UdvidUdsnit);
            GemCommand = new RelayCommand(GemData);
            NavTilKontrolSkema = new RelayCommand(NavKontrolSkema);
            FortrydCommand = new RelayCommand(Fortryd);

            #endregion

        }

        public void SetValues()

        {
            NytSkema.FK_Kolonne = info.FK_Kolonne;
            NytSkema.DåseNr = DåseNr;
            NytSkema.Initialer = Initialer;
            NytSkema.LågNr = LågNr;
            NytSkema.DatoMærkning = info.Dato;
            NytSkema.LågFarve = LågFarve;
            NytSkema.RingFarve = RingFarve;
            NytSkema.Enheder = Enheder;
            NytSkema.Parametre = Parametre;
            NytSkema.Multipack = Multipack;
            NytSkema.FolieNr = FolieNr;
            NytSkema.KartonNr = KartonNr;
            NytSkema.PalleNr = PalleNr;

        }


        public void UdvidUdsnit()
        {
            if (NytDataVis)
            {
                //Maksimer
                double ScreenHeight = ((Frame)Window.Current.Content).ActualHeight;
                double udvid = ScreenHeight - 300.0;
                Udvidelse = udvid.ToString();
                UdvidIkon = "https://visualpharm.com/assets/113/Collapse-595b40b75ba036ed117d6f0a.svg";
                NytDataVis = false;
            }
            else
            {
                //Minimer
                Udvidelse = "170";
                UdvidIkon = "https://visualpharm.com/assets/833/Expand-595b40b75ba036ed117d6f8f.svg";
                NytDataVis = true;
            }
        }


        public ObservableCollection<Færdigvarekontrol> GetSkemaUdsnit()
        {
            Færdigvarekontrol tempData = new Færdigvarekontrol();
            ObservableCollection<Færdigvarekontrol> udsnit = new ObservableCollection<Færdigvarekontrol>();
            foreach (var data in tempData.GetAll())
            {
                if (data.FK_Kolonne == info.FK_Kolonne) udsnit.Add(data);
            }
            udsnit = new ObservableCollection<Færdigvarekontrol>(udsnit.OrderByDescending(e => e.ProcessordreNr));

            return udsnit;
        }

        public void Slet(int id)
        {
            NytSkema.Delete(id);
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
        }


        public void Rediger(int id)
        {
            NytSkema = NytSkema.GetOne(id);
            DåseNr = NytSkema.DåseNr;
            Initialer = NytSkema.Initialer;
            LågNr = NytSkema.LågNr;
            LågFarve = NytSkema.LågFarve;
            RingFarve = NytSkema.RingFarve;
            Enheder = NytSkema.Enheder;
            Parametre = NytSkema.Parametre;
            Multipack = NytSkema.Multipack;
            FolieNr = NytSkema.FolieNr;
            KartonNr = NytSkema.KartonNr;
            PalleNr = NytSkema.PalleNr;


            NytSkema = new Færdigvarekontrol();
            NytSkema.ProcessordreNr = id;

            UpdateVis = true;
            GemVis = false;
            Title = "Rediger data";
        }


        public void Fortryd()
        {
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
            ResetValues();
            GemVis = true;
            UpdateVis = false;
            Title = "Indtast ny data";
        }

        public void ResetValues()
        {
            DåseNr = 0;
            Initialer = "";
            LågNr = 0;
            LågFarve = "(Blank)";
            RingFarve = "(Blank)";
            Enheder = 0;
            Parametre = "";
            Multipack = 0;
            FolieNr = 0;
            KartonNr = 0;
            PalleNr = 0;
        }

        public void Opdater()
        {
            SetValues();
            NytSkema.Put(NytSkema.ProcessordreNr, NytSkema);
            NytSkema = new Færdigvarekontrol();
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));

            GemVis = true;
            UpdateVis = false;
            ResetValues();
        }

        public void GemData()
        {
            SetValues();
            NytSkema.Post(NytSkema);
            NytSkema = new Færdigvarekontrol();
            OnPropertyChanged(nameof(NytSkema));

            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
            ResetValues();
        }

        public void NavKontrolregistrering()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KontrolregistreringEdit), null);
            Window.Current.Content = frame;
        }

        public void NavKontrolSkema()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KolonneEdit), null);
            Window.Current.Content = frame;
        }

        public void NavProduktionsfølgeseddel()
        {
            var frame = new Frame();
            frame.Navigate(typeof(ProduktionsfølgeseddelEdit), null);
            Window.Current.Content = frame;
        }

        public void NavFærdigvarekontrol()
        {
            var frame = new Frame();
            frame.Navigate(typeof(FærdigvarekontrolEdit), null);
            Window.Current.Content = frame;
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}