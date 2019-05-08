using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp;
using FluidApp.Annotations;
using Models;

namespace FluidApp
{
    public sealed class Singleton : INotifyPropertyChanged
    {
        //logger ind, find rollen og gem rollen.
        private static Singleton _instance = new Singleton();
        private Administrator admin;
        public LoginViewModel ROLLE { get; set; }
        public static Singleton Instance
        {
            get { return _instance; }

        }

        private Singleton()
        {
            ROLLE = new LoginViewModel();
        }


        


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

        
}


