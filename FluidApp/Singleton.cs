using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Models;

namespace FluidApp
{
    public sealed class Singleton
    {
        private static Singleton _instance = new Singleton();
        private ObservableCollection<Administrator> administrators;
        public static Singleton Instance
        {
            get { return _instance; }


        }

        private Singleton()
        {
            administrators = new ObservableCollection<Administrator>();
        }
    }

}
