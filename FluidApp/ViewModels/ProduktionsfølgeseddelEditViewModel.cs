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
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
{
    public class ProduktionsfølgeseddelEditViewModel : INotifyPropertyChanged
    {
        public RelayCommand NavSkemaCommand { get; set; }
        public RelayCommand NavRegCommand { get; set; }
        public RelayCommand NavFærdigCommand { get; set; }
        public RelayCommand TilbageCommand { get; set; }
        public RelayCommand FortrydCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand UdvidCommand { get; set; }
        public RelayCommand<int> SletCommand { get; set; }
        public RelayCommand<int> RedigerCommand { get; set; }
        public RelayCommand OpdaterCommand { get; set; }
        public RelayCommand GemCommand { get; set; }
        public Produktionsfølgeseddel NyProd { get; set; }
        public Produktionsfølgeseddel _testProd;
        public Forside Info { get; set; }
        public ObservableCollection<Produktionsfølgeseddel> ProdUdsnit { get; set; }
        private string _title;
        private string _udvidIkon;
        private string _sletIkon;
        private string _udvidelse;
        private bool _nyDataVis;
        private bool _updateVis;
        private bool _gemVis;

        private string _start;
        private string _slut;
        private string _bemanding;
        private string _signatur;
        private string _pause;
        private string _sumTimer;
        private string _sumMin;

        #region Get/Set

        public string Bemanding
        {
            get { return _bemanding; }
            set
            {
                _bemanding = value; 
                OnPropertyChanged();
            }
        }

        public string Start
        {
            get { return _start; }
            set
            {
                _start = value; 
                OnPropertyChanged();
            }
        }

        public string Slut
        {
            get { return _slut; }
            set
            {
                _slut = value; 
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

        public string Pause
        {
            get { return _pause; }
            set
            {
                _pause = value; 
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
            set
            {
                _sletIkon = value; 
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

        public Produktionsfølgeseddel TestProd
        {
            get { return _testProd; }
            set
            {
                _testProd = value;
                OnPropertyChanged();
                try
                {
                    if (_testProd.ID != 0) Rediger(_testProd.ID);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public string SumTimer
        {
            get { return _sumTimer; }
            set
            {
                _sumTimer = value; 
                OnPropertyChanged();
            }
        }

        public string SumMin
        {
            get { return _sumMin; }
            set
            {
                _sumMin = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        public ProduktionsfølgeseddelEditViewModel()
        {
            NavSkemaCommand = new RelayCommand(NavKontrolskema);
            NavRegCommand = new RelayCommand(NavKontrolregistrering);
            NavFærdigCommand = new RelayCommand(NavFærdigvarekontrol);
            TilbageCommand = new RelayCommand(Tilbage);
            FortrydCommand = new RelayCommand(Fortryd);
            RefreshCommand = new RelayCommand(Indlæs);
            UdvidCommand = new RelayCommand(UdvidUdsnit);
            SletCommand = new RelayCommand<int>(Slet);
            RedigerCommand = new RelayCommand<int>(Rediger);
            OpdaterCommand = new RelayCommand(Opdater);
            GemCommand = new RelayCommand(GemData);
            Info = new Forside();
            NyProd = new Produktionsfølgeseddel();

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
            Indlæs();
        }
        public void Indlæs()
        {
            ProdUdsnit = GetProdUdsnit();
            OnPropertyChanged(nameof(ProdUdsnit));

            SumTimer = SumHours()[0].ToString();
            SumMin = SumHours()[1].ToString();
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

        public double? UdregnTimer()
        {
            double? timer;
            //udregner timer
            if (Bemanding != "" && Pause != "")
            {
                TimeSpan span = NyProd.Slut.Subtract(NyProd.Start);
                timer = (span.TotalMinutes - NyProd.Pauser) * NyProd.Bemanding;
                timer /= 60;
            }
            else if (Bemanding != "" && Pause == "")
            {
                TimeSpan span = NyProd.Slut.Subtract(NyProd.Start);
                timer = span.TotalHours * NyProd.Bemanding;
            }
            else
            {
                timer = null;
            }

            return timer;
        }

        public void SetValues()
        {
            //Datetime
            NyProd.Start = DateTime.Parse(Start, new DateTimeFormatInfo());
            NyProd.Slut = DateTime.Parse(Slut, new DateTimeFormatInfo());

            //double + int
            if (Bemanding != "") NyProd.Bemanding = int.Parse(Bemanding);
            else NyProd.Bemanding = null;
            if (Pause != "") NyProd.Pauser = int.Parse(Pause);
            else NyProd.Pauser = null;

            //timer
            NyProd.Timer = UdregnTimer();
            
            //string
            NyProd.Signatur = Signatur;

            //FK_Kolonne
            NyProd.FK_Kolonne = Info.FK_Kolonne;
        }

        public void ResetValues()
        {
            Start = "";
            Slut = "";
            Bemanding = "";
            Signatur = "";
            Pause = "";

            Title = "Indtast ny data";
        }

        public void Fortryd()
        {
            Indlæs();
            ResetValues();
            GemVis = true;
            UpdateVis = false;
        }

        public void GemData()
        {
            SetValues();

            NyProd.Post(NyProd);
            NyProd = new Produktionsfølgeseddel();
            OnPropertyChanged(nameof(NyProd));

            Indlæs();
            ResetValues();
        }

        public void Opdater()
        {
            SetValues();

            NyProd.Put(NyProd.ID, NyProd);
            NyProd = new Produktionsfølgeseddel();
            Indlæs();

            GemVis = true;
            UpdateVis = false;
            ResetValues();
        }

        public void Rediger(int id)
        {
            NyProd = NyProd.GetOne(id);

            Start = NyProd.Start.TimeOfDay.ToString("hh\\:mm");
            Slut = NyProd.Slut.TimeOfDay.ToString("hh\\:mm");
            Bemanding = NyProd.Bemanding.ToString();
            Pause = NyProd.Pauser.ToString();
            Signatur = NyProd.Signatur;

            NyProd = new Produktionsfølgeseddel();
            NyProd.ID = id;

            UpdateVis = true;
            GemVis = false;
            Title = "Rediger data";
        }

        public void Slet(int id)
        {
            NyProd.Delete(id);
            Indlæs();
        }

        public ObservableCollection<Produktionsfølgeseddel> GetProdUdsnit()
        {
            Produktionsfølgeseddel tempData = new Produktionsfølgeseddel();
            ObservableCollection<Produktionsfølgeseddel> udsnit = new ObservableCollection<Produktionsfølgeseddel>();
            foreach (var att in tempData.GetAll())
            {
                if (att.FK_Kolonne == Info.FK_Kolonne) udsnit.Add(att);
            }
            udsnit = new ObservableCollection<Produktionsfølgeseddel>(udsnit.OrderByDescending(e => e.ID));

            return udsnit;
        }

        public List<int> SumHours()
        {
            List<int> sumTime = new List<int>();
            double tempSum = 0;
            foreach (var prod in ProdUdsnit)
            {
                tempSum += double.Parse(prod.Timer.ToString());
            }
            sumTime.Add((int)Math.Floor(tempSum));
            double rest = tempSum % sumTime[0];

            sumTime.Add(Convert.ToInt32(rest*60));

            return sumTime;
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        public void NavKontrolskema()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KolonneEdit), null);
            Window.Current.Content = frame;
        }

        public void NavFærdigvarekontrol()
        {
            var frame = new Frame();
            frame.Navigate(typeof(FærdigvarekontrolEdit), null);
            Window.Current.Content = frame;
        }

        public void NavKontrolregistrering()
        {
            var frame = new Frame();
            frame.Navigate(typeof(KontrolregistreringEdit), null);
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