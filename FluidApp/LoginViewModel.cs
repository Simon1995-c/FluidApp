using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Propeties

        private int _rolle;
        private string _forkertKode;
        
        private string _brugernavn;
        public string Brugernavn
        {
            get { return _brugernavn; }
            set
            {
                _brugernavn = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Brugernavn"));
            }
        }
        private string _kodeord;

        public string Kodeord
        {
            get { return _kodeord; }
            set
            {
                _kodeord = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Kodeord"));
            }
        }

        public int Rolle
        {
            get { return _rolle; }
            set
            {
                _rolle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Rolle"));
            }
        }


        public string ForkertKode
        {
            get { return _forkertKode; }
            set
            {
                _forkertKode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ForkertKode"));
            }
        }

        #endregion

        #region ICommand Execute

        public ICommand LoginCommand
        {
            get { return new RelayCommand(() => Login()); }
        }

        public ICommand TilbageCommand
        {
            get { return new RelayCommand(() => Tilbage());}
        }

        

        #endregion

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), null);
            Window.Current.Content = frame;
        }

        public void Login()
        {
            // checking username and password vs database here.

            Administrator admin = new Administrator();


            foreach (var godkendt in admin.GetAll())
            {
                

                if (!String.IsNullOrEmpty(Brugernavn) && !String.IsNullOrEmpty(Kodeord))
                
                    
                        if (Brugernavn == godkendt.Brugernavn && Kodeord == godkendt.Kodeord && godkendt.Rolle == 2)
                        {
                            var frame = new Frame();
                            frame.Navigate(typeof(Kolonne), null);
                            Window.Current.Content = frame;


                        }
                        else if (Brugernavn == godkendt.Brugernavn && Kodeord == godkendt.Kodeord && godkendt.Rolle == 1)
                        {
                            var frame = new Frame();
                            frame.Navigate(typeof(FluidApp.AdminPage), null);
                            Window.Current.Content = frame;


                        }
                        else
                        {
                           ForkertKode = "Forkert Adgangskode! Prøv igen";
                           Brugernavn = "";
                           Kodeord = "";

                        }








            }
        }


        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs propertyname,
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
