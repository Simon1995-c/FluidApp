using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        private KontrolSkema _nytSkema;
        public Kontrolregistrering Registrering { get; set; }
        public Produktionsfølgeseddel Seddel { get; set; }
        private ObservableCollection<KontrolSkema> _udsnit;
        private string _msKontrol;
        private string _ludKontrol;
        public bool BoolMS { get; set; }
        public bool BoolLud { get; set; }
        private bool _skemaVis;
        private bool _regVis;
        private bool _seddelVis;
        public ObservableCollection<Kontrolregistrering> Udsnit1 { get; set; }

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

        public KontrolSkema NytSkema
        {
            get { return _nytSkema; }
            set
            {
                _nytSkema = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KontrolSkema> Udsnit
        {
            get { return _udsnit; }
            set
            {
                _udsnit = value; 
                OnPropertyChanged();
            }
        }

        public string MSKontrol
        {
            get { return _msKontrol; }
            set
            {
                _msKontrol = value;
                if (_msKontrol == "OK") BoolMS = true;
                else BoolMS = false;
            }
        }

        public string LudKontrol
        {
            get { return _ludKontrol; }
            set
            {
                _ludKontrol = value;
                if (_ludKontrol == "OK") BoolLud = true;
                else BoolLud = false;
            }
        }

        public KolonneEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            ArkCommand = new RelayCommand<string>(VisArk);
            GemCommand = new RelayCommand(GemData);
            Registrering = new Kontrolregistrering();
            NytSkema = new KontrolSkema()


            {
                FK_Kolonne = 8,
                Klokkeslæt = DateTime.Now,
                Ludkoncetration = 1.5,
                Fustage = "Test",
                Kvittering = 1,
                mS = 1.5,
                LudKontrol = true,
                Signatur = "Test",
                mSKontrol = true,
            };

            Udsnit = GetUdsnit();
            Udsnit1 = GetUdsnit1();
            VisArk("0");
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
            NytSkema.LudKontrol = BoolLud;
            NytSkema.mSKontrol = BoolMS;
            NytSkema.Post(NytSkema);
            NytSkema = new KontrolSkema() {FK_Kolonne = 8};
            Udsnit = GetUdsnit();
        }

        public ObservableCollection<KontrolSkema> GetUdsnit()
        {
            ObservableCollection<KontrolSkema> udsnit = new ObservableCollection<KontrolSkema>();
            KontrolSkema tempSkema = new KontrolSkema();

            foreach (var skema in tempSkema.GetAll())
            {
                udsnit.Add(skema);
            }

            udsnit = new ObservableCollection<KontrolSkema>(udsnit.OrderByDescending(e => e.ID));
            int udsnitSize = udsnit.Count;

            for (int i = 0; i < udsnitSize; i++)
            {
                if (i > 2) udsnit.RemoveAt(3);
            }
            Debug.WriteLine(udsnit.Count);
            return udsnit;
        }

        public ObservableCollection<Kontrolregistrering> GetUdsnit1()
        {
            ObservableCollection<Kontrolregistrering> udsnit1 = new ObservableCollection<Kontrolregistrering>();
            Kontrolregistrering tempSkema1 = new Kontrolregistrering();

            foreach (var skema1 in tempSkema1.GetAll())
            {
                udsnit1.Add(skema1);
            }

            udsnit1 = new ObservableCollection<Kontrolregistrering>(udsnit1.OrderByDescending(e => e.ID));

            for (int i = 0; i < udsnit1.Count; i++)
            {
                if (i > 2) udsnit1.RemoveAt(i);
            }
            return udsnit1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}