using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp
{
    public class KolonneEditViewModel : INotifyPropertyChanged
    {
        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand NavProdCommand { get; set; }
        public RelayCommand NavRegCommand { get; set; }
        public RelayCommand NavFærdigCommand { get; set; }
        public RelayCommand GemCommand { get; set; }
        public RelayCommand<int> RedigerCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public KontrolSkema NytSkema { get; set; }
        public Kontrolregistrering Registrering { get; set; }
        public Produktionsfølgeseddel Seddel { get; set; }
        public ObservableCollection<KontrolSkema> SkemaUdsnit { get; set; }
        public ObservableCollection<Kontrolregistrering> RegUdsnit { get; set; }
        public List<string> vælgMuligheder { get; set; }
        private bool _skemaVis;
        private bool _regVis;
        private bool _seddelVis;
        private bool _updateVis;
        private bool _gemVis;

        private string _klokkeslæt;
        private double? _ludkoncetration;
        private string _fustage;
        private int? _kvittering;
        private double? _mS;
        private string _ludKontrol;
        private string _signatur;
        private string _mSKontrol;
        private double? _vægt;

        #region PropertyChanged
        public bool SkemaVis
        {
            get { return _skemaVis; }
            set
            {
                _skemaVis = value;
                OnPropertyChanged();
            }
        }

        public bool RegVis
        {
            get { return _regVis; }
            set
            {
                _regVis = value;
                OnPropertyChanged();
            }
        }

        public bool SeddelVis
        {
            get { return _seddelVis; }
            set
            {
                _seddelVis = value;
                OnPropertyChanged();
            }
        }

        public string Klokkeslæt
        {
            get { return _klokkeslæt; }
            set
            {
                _klokkeslæt = value;
                OnPropertyChanged();
            }
        }

        public double? Ludkoncetration
        {
            get { return _ludkoncetration; }
            set
            {
                _ludkoncetration = value; 
                OnPropertyChanged();
            }
        }

        public string Fustage
        {
            get { return _fustage; }
            set
            {
                _fustage = value; 
                OnPropertyChanged();
            }
        }

        public int? Kvittering
        {
            get { return _kvittering; }
            set
            {
                _kvittering = value; 
                OnPropertyChanged();
            }
        }

        public double? MS
        {
            get { return _mS; }
            set
            {
                _mS = value; 
                OnPropertyChanged();
            }
        }

        public string LudKontrol
        {
            get { return _ludKontrol; }
            set
            {
                _ludKontrol = value; 
                OnPropertyChanged();
            }
        }

        public string Signatur
        {
            get { return _signatur; }
            set
            {
                _signatur = value; 
                OnPropertyChanged();
            }
        }

        public string MSKontrol
        {
            get { return _mSKontrol; }
            set
            {
                _mSKontrol = value; 
                OnPropertyChanged();
            }
        }

        public double? Vægt
        {
            get { return _vægt; }
            set
            {
                _vægt = value; 
                OnPropertyChanged();
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

        public bool GemVis
        {
            get { return _gemVis; }
            set
            {
                _gemVis = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        public KolonneEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            NavProdCommand = new RelayCommand(NavProduktionsfølgeseddel);
            NavRegCommand = new RelayCommand(NavKontrolregistrering);
            NavFærdigCommand = new RelayCommand(NavFærdigvarekontrol);
            GemCommand = new RelayCommand(GemData);
            RedigerCommand = new RelayCommand<int>(Rediger);
            OpdaterCommand = new RelayCommand(Opdater);
            Registrering = new Kontrolregistrering();
            NytSkema = new KontrolSkema();
            vælgMuligheder = new List<string>();
            vælgMuligheder.Add("OK");
            vælgMuligheder.Add("IKKE OK");

            GemVis = true;
            SkemaUdsnit = GetSkemaUdsnit();
        }

        public void Opdater()
        {
            NytSkema.Klokkeslæt = DateTime.Parse(Klokkeslæt, new DateTimeFormatInfo());
            NytSkema.Ludkoncetration = Ludkoncetration;
            NytSkema.mSKontrol = ToBool(MSKontrol);
            NytSkema.Fustage = Fustage;
            NytSkema.Kvittering = Kvittering;
            NytSkema.Signatur = Signatur;
            NytSkema.Vægt = Vægt;
            NytSkema.mS = MS;
            NytSkema.LudKontrol = ToBool(LudKontrol);
            //Skal hentes fra Kolonne
            NytSkema.FK_Kolonne = 8;

            NytSkema.Put(NytSkema.ID, NytSkema);
            NytSkema = new KontrolSkema();
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));

            GemVis = true;
            UpdateVis = false;
        }

        public void Rediger(int id)
        {
            NytSkema = NytSkema.GetOne(id);

            Klokkeslæt = NytSkema.Klokkeslæt.TimeOfDay.ToString("hh\\:mm");
            Ludkoncetration = NytSkema.Ludkoncetration;
            MS = NytSkema.mS;
            Fustage = NytSkema.Fustage;
            Kvittering = NytSkema.Kvittering;
            Signatur = NytSkema.Signatur;
            Vægt = NytSkema.Vægt;
            MSKontrol = ToString(NytSkema.mSKontrol);
            LudKontrol = ToString(NytSkema.LudKontrol);

            UpdateVis = true;
            GemVis = false;
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        public void NavKontrolregistrering()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KontrolregistreringEdit), null);
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

        public string ToString(bool svar)
        {
            string strSvar;
            if (svar) strSvar = "OK";
            else strSvar = "IKKE OK";

            return strSvar;
        }

        public bool ToBool(string svar)
        {
            bool boolSvar = false;
            if (svar == "OK") return boolSvar = true;
            return boolSvar;
        }

        public void GemData()
        {
            NytSkema.Klokkeslæt = DateTime.Parse(Klokkeslæt, new DateTimeFormatInfo());
            NytSkema.Ludkoncetration = Ludkoncetration;
            NytSkema.mSKontrol = ToBool(MSKontrol);
            NytSkema.Fustage = Fustage;
            NytSkema.Kvittering = Kvittering;
            NytSkema.Signatur = Signatur;
            NytSkema.Vægt = Vægt;
            NytSkema.mS = MS;
            NytSkema.LudKontrol = ToBool(LudKontrol);
            //Skal hentes fra Kolonne
            NytSkema.FK_Kolonne = 8;

            NytSkema.Post(NytSkema);
            NytSkema = new KontrolSkema();

            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
        }

        public ObservableCollection<KontrolSkema> GetSkemaUdsnit()
        {
            KontrolSkema tempData = new KontrolSkema();
            //Skal inkludere tjek af FK_kolonne (lige nu bare valgt nr 8)
            ObservableCollection<KontrolSkema> udsnit = new ObservableCollection<KontrolSkema>();
            foreach (var data in tempData.GetAll())
            {
                if (data.FK_Kolonne == 8) udsnit.Add(data);
            }
            udsnit = new ObservableCollection<KontrolSkema>(udsnit.OrderByDescending(e => e.ID));

            return udsnit;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}