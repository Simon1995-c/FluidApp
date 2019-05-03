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
    class ViewModel : INotifyPropertyChanged
    {
        public RelayCommand navigateToKolonne2 { get; set; }
        public RelayCommand navigateToLogin { get; set; }

        public ViewModel()
        {
            navigateToKolonne2 = new RelayCommand(Navigate);
            navigateToLogin = new RelayCommand(NavigateToLogin);
            
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
