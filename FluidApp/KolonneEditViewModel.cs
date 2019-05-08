using System;
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
        public RelayCommand<string> ArkCommand { get; set; }
        public RelayCommand GemCommand { get; set; }
        public RelayCommand<int> RedigerCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public KontrolSkema NytSkema { get; set; }
        public Kontrolregistrering Registrering { get; set; }
        public Produktionsfølgeseddel Seddel { get; set; }
        public ObservableCollection<KontrolSkema> SkemaUdsnit { get; set; }
        public ObservableCollection<Kontrolregistrering> RegUdsnit { get; set; }
        private bool _skemaVis;
        private bool _regVis;
        private bool _seddelVis;
        private bool _updateVis;
        private bool _gemVis;

        private string _klokkeslæt;
        private double _ludkoncetration;
        private string _fustage;
        private int _kvittering;
        private double _mS;
        private bool _ludKontrol;
        private string _signatur;
        private bool _mSKontrol;
        private double _vægt;

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

        public double Ludkoncetration
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

        public int Kvittering
        {
            get { return _kvittering; }
            set
            {
                _kvittering = value; 
                OnPropertyChanged();
            }
        }

        public double MS
        {
            get { return _mS; }
            set
            {
                _mS = value; 
                OnPropertyChanged();
            }
        }

        public bool LudKontrol
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

        public bool MSKontrol
        {
            get { return _mSKontrol; }
            set
            {
                _mSKontrol = value; 
                OnPropertyChanged();
            }
        }

        public double Vægt
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
            ArkCommand = new RelayCommand<string>(VisArk);
            GemCommand = new RelayCommand(GemData);
            RedigerCommand = new RelayCommand<int>(Rediger);
            OpdaterCommand = new RelayCommand(Opdater);
            Registrering = new Kontrolregistrering();
            NytSkema = new KontrolSkema();

            GemVis = true;
            SkemaUdsnit = GetSkemaUdsnit();
            RegUdsnit = GetRegUdsnit();
            VisArk("0");
        }

        public void Opdater()
        {
            NytSkema.Klokkeslæt = DateTime.Parse(Klokkeslæt, new DateTimeFormatInfo());
            NytSkema.Ludkoncetration = Ludkoncetration;
            NytSkema.mSKontrol = MSKontrol;
            NytSkema.Fustage = Fustage;
            NytSkema.Kvittering = Kvittering;
            NytSkema.Signatur = Signatur;
            NytSkema.Vægt = Vægt;
            NytSkema.mS = MS;
            NytSkema.LudKontrol = LudKontrol;
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
            LudKontrol = NytSkema.LudKontrol;
            MS = NytSkema.mS;
            Fustage = NytSkema.Fustage;
            Kvittering = NytSkema.Kvittering;
            Signatur = NytSkema.Signatur;
            Vægt = NytSkema.Vægt;
            MSKontrol = NytSkema.mSKontrol;

            UpdateVis = true;
            GemVis = false;
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        public void VisArk(string parameter)
        {
            switch (parameter)
            {
                case "0":
                    SkemaVis = true;
                    RegVis = false;
                    SeddelVis = false;
                    return;
                case "1":
                    SkemaVis = false;
                    RegVis = true;
                    SeddelVis = false;
                    return;
                case "2":
                    SkemaVis = false;
                    RegVis = false;
                    SeddelVis = true;
                    return;
                case "3":
                    SkemaVis = false;
                    RegVis = false;
                    SeddelVis = false;
                    return;
            }
        }

        public void GemData()
        {
            if (SkemaVis)
            {
                SkemaUdsnit = GetSkemaUdsnit();
                OnPropertyChanged(nameof(SkemaUdsnit));
            }
            else if (RegVis)
            {
                RegUdsnit = GetRegUdsnit();
                OnPropertyChanged(nameof(RegUdsnit));
            }
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

        public ObservableCollection<Kontrolregistrering> GetRegUdsnit()
        {
            ObservableCollection<Kontrolregistrering> udsnit = new ObservableCollection<Kontrolregistrering>();
            Kontrolregistrering tempData = new Kontrolregistrering();

            foreach (var skema in tempData.GetAll())
            {
                udsnit.Add(skema);
            }

            udsnit = new ObservableCollection<Kontrolregistrering>(udsnit.OrderByDescending(e => e.ID));
            int udsnitSize = udsnit.Count;

            for (int i = 0; i < udsnitSize; i++)
            {
                if (i > 2) udsnit.RemoveAt(3);
            }
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