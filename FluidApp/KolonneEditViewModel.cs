using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public KontrolSkema NytSkema { get; set; }
        public Kontrolregistrering Registrering { get; set; }
        public Produktionsfølgeseddel Seddel { get; set; }
        public ObservableCollection<KontrolSkema> Udsnit { get; set; }
        private bool _skemaVis;
        private bool _regVis;
        private bool _seddelVis;

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

        public KolonneEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
            ArkCommand = new RelayCommand<string>(VisArk);
            NytSkema = new KontrolSkema();

            Udsnit = GetUdsnit();
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

        public ObservableCollection<KontrolSkema> GetUdsnit()
        {
            ObservableCollection<KontrolSkema> udsnit = new ObservableCollection<KontrolSkema>();
            KontrolSkema tempSkema = new KontrolSkema();

            foreach (var skema in tempSkema.GetAll())
            {
                udsnit.Add(skema);
            }

            udsnit = new ObservableCollection<KontrolSkema>(udsnit.OrderByDescending(e => e.ID));

            for (int i = 0; i < udsnit.Count; i++)
            {
                if (i > 2) udsnit.RemoveAt(i);
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