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
        public string Brugernavn { get; set; }
        public string Kodeord { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand TilbageCommand { get; set; }


        public int Rolle { get; set; }

        public Visibility ForkertKode { get; set; }

        public LoginViewModel()
        {
            ForkertKode = Visibility.Collapsed;

            TilbageCommand = new RelayCommand(Tilbage);
            LoginCommand = new RelayCommand(Login);
        }

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

            int loggedIn = 0;

            foreach (var a in admin.GetAll())
            {
                

                if (a.Brugernavn == Brugernavn && a.Kodeord == Kodeord)
                {
                    loggedIn++;

                    if (a.Rolle == 1)
                    {
                        Rolle = 1;
                        

                    }

                    if (a.Rolle == 2)
                    {
                        Rolle = 2;

                    }

                    break;
                }
            }



            if (loggedIn > 0)
            {

               
                
                if (Rolle == 1)
                {
                    //Gemmer rollen i den ene session brugeren kører. Informationen slettes når programmet afsluttes.
                    Application.Current.Resources["Administrator"] = 1;
                    var frame = new Frame();
                    frame.Navigate(typeof(Kolonne), null);
                    Window.Current.Content = frame;
                }

                if (Rolle == 2)
                {
                    //Gemmer rollen i den ene session brugeren kører. Informationen slettes når programmet afsluttes.
                    Application.Current.Resources["Administrator"] = 2;
                    var frame = new Frame();
                    frame.Navigate(typeof(AdminPage), null);
                    Window.Current.Content = frame;
                }
            }
            else
            {
                
                ForkertKode = Visibility.Visible;
                OnPropertyChanged(nameof(ForkertKode));
            }
        }


        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
