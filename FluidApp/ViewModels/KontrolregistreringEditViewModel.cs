using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
{
    public class KontrolregistreringEditViewModel : INotifyPropertyChanged
    {
        private bool _skemaVis;
        private bool _regVis;
        private bool _seddelVis;
        private bool _updateVis;
        private bool _gemVis;

        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand NavFærdigCommand { get; set; }
        public RelayCommand NavProdCommand { get; set; }
        public RelayCommand NavSkemaCommand { get; set; }
        public RelayCommand<string> ArkCommand { get; set; }
        public RelayCommand GemCommand { get; set; }
        public RelayCommand<int> RedigerCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public Kontrolregistrering Registrering { get; set; }
        public ObservableCollection<Kontrolregistrering> RegUdsnit { get; set; }

       private DateTime _klokkeslæt;
       private DateTime _holdbarhedsdato;
       private DateTime _produktionsdato;
       private int _færdigvareNr;
       private string _kommentar;
       private bool _spritkontrol;
       private int _hætteNr;
       private int _etiketNr;
       private string _fustage;
       private string _signatur;


        #region Vis

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

        #region Reg

        private DateTime Klokkeslæt
        {
            get { return _klokkeslæt; }
            set
            {
                _klokkeslæt = value;
                OnPropertyChanged();
            }
        }

        public DateTime Holdbarhedsdato
        {
            get { return _holdbarhedsdato; }
            set
            {
                _holdbarhedsdato = value;
                OnPropertyChanged();
            }
        }

        public DateTime Produktionsdato
        {
            get { return _produktionsdato; }
            set
            {
                _produktionsdato = value;
                OnPropertyChanged();
            }
        }

        public int FærdigvareNr
        {
            get { return _færdigvareNr; }
            set
            {
                _færdigvareNr = value;
                OnPropertyChanged();
            }
        }

        public string Kommentar
        {
            get { return _kommentar; }
            set
            {
                _kommentar = value;
                OnPropertyChanged();
            }
        }

        public bool Spritkontrol
        {
            get { return _spritkontrol; }
            set
            {
                _spritkontrol = value;
                OnPropertyChanged();
            }
        }

        public int HætteNr
        {
            get { return _hætteNr; }
            set
            {
                _hætteNr = value;
                OnPropertyChanged();
            }
        }

        public int EtiketNr
        {
            get { return _etiketNr; }
            set
            {
                _etiketNr = value;
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

        public string Signatur
        {
            get { return _signatur; }
            set
            {
                _signatur = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public KontrolregistreringEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            NavFærdigCommand = new RelayCommand(NavFærdigvarekontrol);
            NavProdCommand = new RelayCommand(NavProduktionsfølgeseddel);
            NavSkemaCommand = new RelayCommand(NavSkema);
            ArkCommand = new RelayCommand<string>(VisArk);
            GemCommand = new RelayCommand(GemData);
            RedigerCommand = new RelayCommand<int>(Rediger);

            OpdaterCommand = new RelayCommand(Opdater);
            Registrering = new Kontrolregistrering();

            GemVis = true;
            RegUdsnit = GetRegUdsnit();
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
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
        public void NavSkema()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KolonneEdit), null);
            Window.Current.Content = frame;
        }

        public void Opdater()
        {
            Registrering.Klokkeslæt = Klokkeslæt;
            Registrering.Holdbarhedsdato = Holdbarhedsdato;
            Registrering.Produktionsdato = Produktionsdato;
            Registrering.FærdigvareNr = FærdigvareNr;
            Registrering.Kommentar = Kommentar;
            Registrering.Spritkontrol = Spritkontrol;
            Registrering.HætteNr = HætteNr;
            Registrering.EtiketNr = EtiketNr;
            Registrering.Fustage = Fustage;
            Registrering.Signatur = Signatur;
            Registrering.FK_Kolonne = 9;

            Registrering.Put(Registrering.ID, Registrering);
            Registrering = new Kontrolregistrering();
            RegUdsnit = GetRegUdsnit();
            OnPropertyChanged(nameof(RegUdsnit));

            GemVis = true;
            UpdateVis = false;

        }

        public void Rediger(int id)
        {

            Registrering = Registrering.GetOne(id);

            Klokkeslæt = Registrering.Klokkeslæt;
            Holdbarhedsdato = Registrering.Holdbarhedsdato;
            Produktionsdato = Registrering.Produktionsdato;
            FærdigvareNr = Registrering.FærdigvareNr;
            Kommentar = Registrering.Kommentar;
            Spritkontrol = Registrering.Spritkontrol;
            HætteNr = Registrering.HætteNr;
            EtiketNr = Registrering.EtiketNr;
            Fustage = Registrering.Fustage;
            Signatur = Registrering.Signatur;

            GemVis = true;
            UpdateVis = false;
        }

        public void GemData()
        {
            if (RegVis)
            {
                Registrering.Klokkeslæt = Klokkeslæt;
                Registrering.Holdbarhedsdato = Holdbarhedsdato;
                Registrering.Produktionsdato = Produktionsdato;
                Registrering.FærdigvareNr = FærdigvareNr;
                Registrering.Kommentar = Kommentar;
                Registrering.Spritkontrol = Spritkontrol;
                Registrering.HætteNr = HætteNr;
                Registrering.EtiketNr = EtiketNr;
                Registrering.Fustage = Fustage;
                Registrering.Signatur = Signatur;
                //Skal hentes fra Kolonne
               Registrering.FK_Kolonne = 9;

                Registrering.Post(Registrering);
                Registrering = new Kontrolregistrering();

                RegUdsnit = GetRegUdsnit();
                OnPropertyChanged(nameof(RegUdsnit));
            }
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