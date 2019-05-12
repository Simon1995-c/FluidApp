using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
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
        public RelayCommand UdvidCommand { get; set; }
        public RelayCommand FortrydCommand { get; set; }
        public RelayCommand<int> SletCommand { get; set; }

        public KontrolSkema testSkema;
        public KontrolSkema NytSkema { get; set; }
        public ObservableCollection<KontrolSkema> SkemaUdsnit { get; set; }
        public List<string> VælgMuligheder { get; set; }
        public Forside Info { get; set; }
        private string _title;
        private string _udvidIkon;
        private string _sletIkon;
        private string _udvidelse;
        private bool _nyDataVis;
        private bool _updateVis;
        private bool _gemVis;

        private string _klokkeslæt;
        private string _ludkoncentration;
        private string _fustage;
        private string _kvittering;
        private string _mS;
        private string _ludKontrol;
        private string _signatur;
        private string _mSKontrol;
        private string _vægt;

        #region PropertyChanged
        public string Klokkeslæt
        {
            get { return _klokkeslæt; }
            set
            {
                _klokkeslæt = value;
                OnPropertyChanged();
            }
        }

        public string Ludkoncetration
        {
            get { return _ludkoncentration; }
            set
            {
                _ludkoncentration = value; 
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

        public string Kvittering
        {
            get { return _kvittering; }
            set
            {
                _kvittering = value; 
                OnPropertyChanged();
            }
        }

        public string MS
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

        public string Vægt
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

        public string Udvidelse
        {
            get { return _udvidelse; }
            set
            {
                _udvidelse = value; 
                OnPropertyChanged();
            }
        }

        public bool NyDataVis
        {
            get { return _nyDataVis; }
            set
            {
                _nyDataVis = value; 
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

        public string SletIkon
        {
            get { return _sletIkon; }
            set { _sletIkon = value; }
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

        public KontrolSkema TestSkema
        {
            get { return testSkema; }
            set
            {
                testSkema = value; 
                OnPropertyChanged();
                try
                {
                    if (testSkema.ID != 0) Rediger(testSkema.ID);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
                
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
            UdvidCommand = new RelayCommand(UdvidUdsnit);
            FortrydCommand = new RelayCommand(Fortryd);
            SletCommand = new RelayCommand<int>(Slet);

            TestSkema = new KontrolSkema();
            NytSkema = new KontrolSkema();
            Info = new Forside();
            VælgMuligheder = new List<string>();
            VælgMuligheder.Add("OK");
            VælgMuligheder.Add("IKKE OK");
            VælgMuligheder.Add("(Blank)");
            
            GemVis = true;
            NyDataVis = true;
            ResetValues();

            if (Application.Current.Resources.ContainsKey("forside"))
            {
                Forside f = (Forside)Application.Current.Resources["forside"];
                Info = f;
            }

            SletIkon = "https://visualpharm.com/assets/591/Delete-595b40b75ba036ed117d7c27.svg";
            UdvidIkon = "https://visualpharm.com/assets/833/Expand-595b40b75ba036ed117d6f8f.svg";
            Udvidelse = "170";
            SkemaUdsnit = GetSkemaUdsnit();
        }

        public void ResetValues()
        {
            Klokkeslæt = null;
            Ludkoncetration = "";
            MSKontrol = "(Blank)";
            LudKontrol = "(Blank)";
            MS = "";
            Fustage = "";
            Kvittering = "";
            Vægt = "";
            Signatur = "";

            Title = "Indtast ny data";
        }

        public void UdvidUdsnit()
        {
            if (NyDataVis)
            {
                //Maksimer
                double ScreenHeight = ((Frame)Window.Current.Content).ActualHeight;
                double udvid = ScreenHeight - 300.0;
                Udvidelse = udvid.ToString();
                UdvidIkon = "https://visualpharm.com/assets/113/Collapse-595b40b75ba036ed117d6f0a.svg";
                NyDataVis = false;
            }
            else
            {
                //Minimer
                Udvidelse = "170";
                UdvidIkon = "https://visualpharm.com/assets/833/Expand-595b40b75ba036ed117d6f8f.svg";
                NyDataVis = true;
            }
        }

        public void Slet(int id)
        {
            NytSkema.Delete(id);
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
        }

        public void Fortryd()
        {
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
            ResetValues();
            GemVis = true;
            UpdateVis = false;
        }

        public void Opdater()
        {
            SetValues();
            
            NytSkema.Put(NytSkema.ID, NytSkema);
            NytSkema = new KontrolSkema();
            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));

            GemVis = true;
            UpdateVis = false;
            ResetValues();
        }

        public void Rediger(int id)
        {
            NytSkema = NytSkema.GetOne(id);

            Klokkeslæt = NytSkema.Klokkeslæt.TimeOfDay.ToString("hh\\:mm");
            Ludkoncetration = NytSkema.Ludkoncentration.ToString();
            MS = NytSkema.MS.ToString();
            Fustage = NytSkema.Fustage;
            Kvittering = NytSkema.Kvittering.ToString();
            Signatur = NytSkema.Signatur;
            Vægt = NytSkema.Vægt.ToString();
            MSKontrol = ToString(NytSkema.MSKontrol);
            LudKontrol = ToString(NytSkema.LudKontrol);
            
            NytSkema = new KontrolSkema();
            NytSkema.ID = id;

            UpdateVis = true;
            GemVis = false;
            Title = "Rediger data";
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

        public string ToString(bool? svar)
        {
            string strSvar;
            if (svar == true) strSvar = "OK";
            else if (svar == false) strSvar = "IKKE OK";
            else strSvar = "";

            return strSvar;
        }

        public bool? ToBool(string svar)
        {
            bool? boolSvar = null;
            if (svar == "OK") return boolSvar = true;
            else if (svar == "IKKE OK") return boolSvar = false;
            else return boolSvar;
        }

        public void SetValues()
        {
            //double + int værdier skal parses
            if (Ludkoncetration != "") NytSkema.Ludkoncentration = double.Parse(Ludkoncetration);
            else NytSkema.Ludkoncentration = null;
            if (Kvittering != "") NytSkema.Kvittering = int.Parse(Kvittering);
            else NytSkema.Kvittering = null;
            if (Vægt != "") NytSkema.Vægt = double.Parse(Vægt);
            else NytSkema.Vægt = null;
            if (MS != "") NytSkema.MS = double.Parse(MS);
            else NytSkema.MS = null;

            //datetime skal også parses men med specifikt format
            NytSkema.Klokkeslæt = DateTime.Parse(Klokkeslæt, new DateTimeFormatInfo());
            
            //strings er guds gave til dovenskab
            NytSkema.Fustage = Fustage;
            NytSkema.Signatur = Signatur;

            //bools skal omdannes til enten "OK" eller "IKKE OK"
            NytSkema.LudKontrol = ToBool(LudKontrol);
            NytSkema.MSKontrol = ToBool(MSKontrol);

            //FK_kolonne skal hentes fra tilsvarende Forside
            NytSkema.FK_Kolonne = Info.FK_Kolonne;
        }

        public void GemData()
        {
            SetValues();

            NytSkema.Post(NytSkema);
            NytSkema = new KontrolSkema();
            OnPropertyChanged(nameof(NytSkema));

            SkemaUdsnit = GetSkemaUdsnit();
            OnPropertyChanged(nameof(SkemaUdsnit));
            ResetValues();
        }

        public ObservableCollection<KontrolSkema> GetSkemaUdsnit()
        {
            KontrolSkema tempData = new KontrolSkema();
            ObservableCollection<KontrolSkema> udsnit = new ObservableCollection<KontrolSkema>();
            foreach (var data in tempData.GetAll())
            {
                if (data.FK_Kolonne == Info.FK_Kolonne) udsnit.Add(data);
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