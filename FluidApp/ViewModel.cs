using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;

namespace FluidApp
{
    class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> temp { get; set; }
        public ViewModel()
        {
            ipHandler h = new ipHandler();

            //If the IP isn't allowed -> send them to an error page
            if (!h.isAllowedIp())
            {
                var frame = new Frame();
                frame.Navigate(typeof(errorPageIPrange), null);
                Window.Current.Content = frame;
            }

            temp = new ObservableCollection<string>
            {
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
