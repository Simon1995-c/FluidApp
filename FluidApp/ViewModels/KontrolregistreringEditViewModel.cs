using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Devices.Spi;
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
        private string _udvidelse;
        private string _udvidIkon;
        private bool _nyDataVis;
        private string _sletIkon;

        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand NavFærdigCommand { get; set; }
        public RelayCommand NavProdCommand { get; set; }
        public RelayCommand NavSkemaCommand { get; set; }
        public RelayCommand<string> ArkCommand { get; set; }
        public RelayCommand GemCommand { get; set; }
        public RelayCommand<int> RedigerCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public RelayCommand UdvidCommand { get; set; }
        public RelayCommand FortrydCommand { get; set; }
        public RelayCommand<int> SletCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        public List<string> VælgMuligheder { get; set; }
        public ObservableCollection<string> FærdivareNrList { get; set; }
        public ObservableCollection<string> ProdDatoList { get; set; }
        public ObservableCollection<string> HoldDatoList { get; set; }
        public ObservableCollection<string> HætteNrList { get; set; }
        public ObservableCollection<string> EtiketNrList { get; set; }

        public Kontrolregistrering testSkema1;  
        public Kontrolregistrering Registrering { get; set; }
        public ObservableCollection<Kontrolregistrering> RegUdsnit { get; set; }

        public Forside Info { get; set; }

        private List<string> _fustageList;
        private string _klokkeslæt;
        private string _holdbarhedsdato;
        private string _produktionsdato;
        private string _færdigvareNr;
        private string _kommentar;
        private string _spritkontrol;
        private string _hætteNr;
        private string _etiketNr;
        private string _fustage;
        private string _signatur;
        private string _title;




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

        public string Klokkeslæt
        {
            get { return _klokkeslæt; }
            set
            {
                _klokkeslæt = value;
                OnPropertyChanged();
            }
        }

        public string Holdbarhedsdato
        {
            get { return _holdbarhedsdato; }
            set
            {
                _holdbarhedsdato = value;
                OnPropertyChanged();
            }
        }

        public string Produktionsdato
        {
            get { return _produktionsdato; }
            set
            {
                _produktionsdato = value;
                OnPropertyChanged();
            }
        }

        public string FærdigvareNr
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

        public string Spritkontrol
        {
            get { return _spritkontrol; }
            set
            {
                _spritkontrol = value;
                OnPropertyChanged();
            }
        }

        public string HætteNr
        {
            get { return _hætteNr; }
            set
            {
                _hætteNr = value;
                OnPropertyChanged();
            }
        }

        public string EtiketNr
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

        public string Udvidelse
        {
            get { return _udvidelse; }
            set
            {
                _udvidelse = value;
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

        public bool NyDataVis
        {
            get { return _nyDataVis; }
            set
            {
                _nyDataVis = value;
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

        #endregion

        public Kontrolregistrering TestSkema1
        {
            get { return testSkema1; }
            set
            {
                testSkema1 = value;
                OnPropertyChanged();
                try
                {
                    if (testSkema1.ID != 0) Rediger(testSkema1.ID);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public List<string> FustageList
        {
            get { return _fustageList; }
            set
            {
                _fustageList = value;
                OnPropertyChanged();
            }
        }

        public KontrolregistreringEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            NavFærdigCommand = new RelayCommand(NavFærdigvarekontrol);
            NavProdCommand = new RelayCommand(NavProduktionsfølgeseddel);
            NavSkemaCommand = new RelayCommand(NavSkema);
            ArkCommand = new RelayCommand<string>(VisArk);
            GemCommand = new RelayCommand(GemData);
            RedigerCommand = new RelayCommand<int>(Rediger);
            UdvidCommand = new RelayCommand(UdvidUdsnit);
            OpdaterCommand = new RelayCommand(Opdater);
            Registrering = new Kontrolregistrering();
            FortrydCommand = new RelayCommand(Fortryd);
            SletCommand = new RelayCommand<int>(Slet);
            RefreshCommand = new RelayCommand(Indlæs);

            RegUdsnit = GetRegUdsnit();
            Info = new Forside();
            VælgMuligheder = new List<string>();
            VælgMuligheder.Add("OK");
            VælgMuligheder.Add("IKKE OK");
            VælgMuligheder.Add("(Blank)");
            
            FustageList = new List<string>();
            FustageList.Add("30L");
            FustageList.Add("25L");

            FærdivareNrList = new ObservableCollection<string>();
            ProdDatoList = new ObservableCollection<string>();
            HoldDatoList = new ObservableCollection<string>();
            HætteNrList = new ObservableCollection<string>();
            EtiketNrList = new ObservableCollection<string>();

            //Udfyld sidste indtastede data til suggestions
            FærdivareNrList.Add(RegUdsnit[RegUdsnit.Count - RegUdsnit.Count].FærdigvareNr.ToString());
            ProdDatoList.Add(RegUdsnit[RegUdsnit.Count - RegUdsnit.Count].FormattedPro);
            HoldDatoList.Add(RegUdsnit[RegUdsnit.Count - RegUdsnit.Count].FormattedHo);
            HætteNrList.Add(RegUdsnit[RegUdsnit.Count - RegUdsnit.Count].HætteNr.ToString());
            EtiketNrList.Add(RegUdsnit[RegUdsnit.Count - RegUdsnit.Count].EtiketNr.ToString());

            TestSkema1 = new Kontrolregistrering();
            GemVis = true;
            NyDataVis = true;
            ResetValues();

            Title = "Indsæt ny data";

            if (Application.Current.Resources.ContainsKey("forside"))
            {
                Forside f = (Forside)Application.Current.Resources["forside"];
                Info = f;
            }
            SletIkon = "https://visualpharm.com/assets/591/Delete-595b40b75ba036ed117d7c27.svg";
            UdvidIkon = "https://visualpharm.com/assets/833/Expand-595b40b75ba036ed117d6f8f.svg";
            Udvidelse = "170";
            Indlæs();
        }

        public void Indlæs()
        {
            RegUdsnit = GetRegUdsnit();
            OnPropertyChanged(nameof(RegUdsnit));
        }

         public void ResetValues()
         {
             Klokkeslæt = "";
             Holdbarhedsdato = "";
             Produktionsdato = "";
             FærdigvareNr = "";
             Kommentar = "";
             Spritkontrol = "(Blank)";
             HætteNr = "";
             EtiketNr = "";
             Fustage = "";
             Signatur = "";
        }

        public void Slet(int id)
        {
            Registrering.Delete(id);
            RegUdsnit = GetRegUdsnit();
            OnPropertyChanged(nameof(RegUdsnit));
        }

        public void Fortryd()
        {
            RegUdsnit = GetRegUdsnit();
            OnPropertyChanged(nameof(RegUdsnit));
            ResetValues();
            GemVis = true;
            UpdateVis = false;
            Title = "Indsæt ny data";
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

        #region Navigate

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

        #endregion



        public void Opdater()
        {
            SetValues();

            Registrering.Put(Registrering.ID, Registrering);
            UpdateSuggestions();
            Registrering = new Kontrolregistrering();
            RegUdsnit = GetRegUdsnit();
            OnPropertyChanged(nameof(RegUdsnit));

            GemVis = true;
            UpdateVis = false;
            ResetValues();

        }

        public void Rediger(int id)
        {
            Registrering = Registrering.GetOne(id);

            Klokkeslæt = Registrering.Klokkeslæt.TimeOfDay.ToString("hh\\:mm");
            Holdbarhedsdato = Registrering.Holdbarhedsdato.Date.ToString("dd-MM-yyyy");
            Produktionsdato = Registrering.Produktionsdato.Date.ToString("dd-MM-yyyy");
            FærdigvareNr = Registrering.FærdigvareNr.ToString();
            Kommentar = Registrering.Kommentar;
            Spritkontrol = ToString(Registrering.Spritkontrol);
            HætteNr = Registrering.HætteNr.ToString();
            EtiketNr = Registrering.EtiketNr.ToString();
            Fustage = Registrering.Fustage;
            Signatur = Registrering.Signatur;

            Registrering = new Kontrolregistrering();
            Registrering.ID = id;

            UpdateVis = true;
            GemVis = false;
            Title = "Rediger data";
        }

        public void GemData()
        {
            if (SetValues())
            {
                Registrering.Post(Registrering);
                UpdateSuggestions();
                Registrering = new Kontrolregistrering();
                OnPropertyChanged(nameof(Registrering));

                RegUdsnit = GetRegUdsnit();
                OnPropertyChanged(nameof(RegUdsnit));
                ResetValues();
            }
            
        }

        public bool SetValues()
        {
            //Errorhandling til klokkeslæt så det altid er rigtig format
            if (DateTime.TryParse(Klokkeslæt, out DateTime aDate) == false)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Fejl. Du har indtastet klokkeslættet forkert");
                dialog.ShowAsync();
                return false;
            }

            //Errorhandling til Holdbarhed så det altid er rigtig format
            if (DateTime.TryParseExact(Holdbarhedsdato, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime bDate) == false)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Fejl. Du har indtastet Holdbarhedsdato forkert");
                dialog.ShowAsync();
                return false;
            }

            //Errorhandling til klokkeslæt så det altid er rigtig format
            
            if (DateTime.TryParseExact(Produktionsdato, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None,out DateTime cDate) == false)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Fejl. Du har indtastet Produktionsdato forkert");
                dialog.ShowAsync();
                return false;
            }


            //double + int værdier skal parses
            //if (Ludkoncetration != "") NytSkema.Ludkoncentration = double.Parse(Ludkoncetration);
            // else NytSkema.Ludkoncentration = null;
            if (FærdigvareNr!= "") Registrering.FærdigvareNr = int.Parse(FærdigvareNr);
            else Registrering.FærdigvareNr = null;

            if (HætteNr != "") Registrering.HætteNr = int.Parse(HætteNr);
            else Registrering.HætteNr = null;

            if (EtiketNr != "") Registrering.EtiketNr = int.Parse(EtiketNr);
            else Registrering.EtiketNr = null;

            //datetime skal også parses men med specifikt format
            Registrering.Klokkeslæt = DateTime.Parse(Klokkeslæt, new DateTimeFormatInfo());
            Registrering.Holdbarhedsdato = DateTime.ParseExact(Holdbarhedsdato, "dd-MM-yyyy", new CultureInfo("en-US"));
            Registrering.Produktionsdato = DateTime.ParseExact(Produktionsdato, "dd-MM-yyyy", new CultureInfo("en-US"));

            //strings er guds gave til dovenskab
            Registrering.Kommentar = Kommentar;
            Registrering.Fustage = Fustage;
            Registrering.Signatur = Signatur;

            //bools skal omdannes til enten "OK" eller "IKKE OK"
            // Registrering.Spritkontrol = ToBool(Spritkontrol);
            Registrering.Spritkontrol = ToBool(Spritkontrol);

            //FK_kolonne skal hentes fra tilsvarende Forside
            Registrering.FK_Kolonne = Info.FK_Kolonne;

            return true;
        }

        public string ToString(bool? svar)
        {
            string strSvar;
            if (svar == true) strSvar = "OK";
            else if (svar == false) strSvar = "IKKE OK";
            else strSvar = "(Blank)";

            return strSvar;
        }

        public bool? ToBool(string svar)
        {
            bool? boolSvar = null;
            if (svar == "OK") return boolSvar = true;
            else if (svar == "IKKE OK") return boolSvar = false;
            else return boolSvar;
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

        public void UpdateSuggestions()
        {
            if (Registrering.FærdigvareNr != null && !FærdivareNrList.Contains(Registrering.FærdigvareNr.ToString()))
            {
                FærdivareNrList.Add(Registrering.FærdigvareNr.ToString());
            }
            if (!ProdDatoList.Contains(Registrering.FormattedPro))
            {
                ProdDatoList.Add(Registrering.FormattedPro);
            }
            if (!HoldDatoList.Contains(Registrering.FormattedHo))
            {
                HoldDatoList.Add(Registrering.FormattedHo);
            }
            if (Registrering.HætteNr != null && !HætteNrList.Contains(Registrering.HætteNr.ToString()))
            {
                HætteNrList.Add(Registrering.HætteNr.ToString());
            }
            if (Registrering.EtiketNr != null && !EtiketNrList.Contains(Registrering.EtiketNr.ToString()))
            {
                EtiketNrList.Add(Registrering.EtiketNr.ToString());
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