using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;

namespace FluidApp.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public RelayCommand navigateToKolonne2 { get; set; }
        public RelayCommand navigateToLogin { get; set; }
        public RelayCommand navigatetoAdmin { get; set; }
        public RelayCommand logUdRelayCommand { get; set; }
        public Visibility hideLogin { get; set; }
        public Visibility hideAdminPanel { get; set; }
        public Visibility LogudVisibility { get; set; }

        public ViewModel()
        {
            hideAdminPanel = Visibility.Collapsed;
            LogudVisibility = Visibility.Collapsed;
            navigateToKolonne2 = new RelayCommand(Navigate);
            navigateToLogin = new RelayCommand(NavigateToLogin);
            navigatetoAdmin = new RelayCommand(navigateToAdminPanel);
            logUdRelayCommand = new RelayCommand(logUd);

            


            if (Application.Current.Resources.ContainsKey("Administrator"))
            {
                if ((int)Application.Current.Resources["Administrator"] == 2)
                {
                    hideAdminPanel = Visibility.Visible;
                    LogudVisibility = Visibility.Visible;
                    hideLogin = Visibility.Collapsed;
                }
                else if ((int)Application.Current.Resources["Administrator"] == 0)
                {
                    hideLogin = Visibility.Visible;
                    hideAdminPanel = Visibility.Collapsed;
                    LogudVisibility = Visibility.Collapsed;
                }
                else
                {
                    hideAdminPanel = Visibility.Collapsed;
                }
            }
        }


        public void logUd()
        {
            LogudVisibility = Visibility.Collapsed;
            hideAdminPanel = Visibility.Collapsed;
            hideLogin = Visibility.Visible;
            OnPropertyChanged(nameof(LogudVisibility));
            OnPropertyChanged(nameof(hideAdminPanel));
            OnPropertyChanged(nameof(hideLogin));
            Application.Current.Resources["Administrator"] = 0;

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
