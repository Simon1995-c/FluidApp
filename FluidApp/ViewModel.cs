using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        public RelayCommand navigateToKolonne2 { get; set; }
        public RelayCommand navigateToLogin { get; set; }
        public RelayCommand navigatetoAdmin { get; set; }
        public Visibility hideLogin { get; set; }
        public Visibility hideAdminPanel { get; set; }

        public ViewModel()
        {
            hideAdminPanel = Visibility.Collapsed;

            navigateToKolonne2 = new RelayCommand(Navigate);
            navigateToLogin = new RelayCommand(NavigateToLogin);
            navigatetoAdmin = new RelayCommand(navigateToAdminPanel);

            if (Application.Current.Resources.Count > 0)
            {
                if ((int)Application.Current.Resources["Administrator"] == 2)
                {
                    hideAdminPanel = Visibility.Visible;
                    hideLogin = Visibility.Collapsed;
                }
                else if ((int)Application.Current.Resources["Administrator"] == 0)
                {
                    hideLogin = Visibility.Visible;
                    hideAdminPanel = Visibility.Collapsed;
                }
                else
                {
                    hideAdminPanel = Visibility.Collapsed;
                }
            }
           
            
            

            
            
           
            
        }


        public void navigateToAdminPanel()
        {
            var frame = new Frame();
            frame.Navigate(typeof(AdminPage), null);
            Window.Current.Content = frame;
        }

        private void Navigate()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        private void NavigateToLogin()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Login), null);
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
