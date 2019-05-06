using System;
using System.Collections.Generic;
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
    class OpretKolonneViewModel
    {
        public int FærdigvareNr { get; set; }
        public int ProcessordreNr { get; set; }
        public string Færdigvarenavn { get; set; }
        public string Produktionsinitialer { get; set; }

        public RelayCommand Tilbage { get; set; }
        public RelayCommand Opret { get; set; }

        public OpretKolonneViewModel()
        {
            Tilbage = new RelayCommand(TilbageFunc);
            Opret = new RelayCommand(OpretFunc);
        }

        private void TilbageFunc()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }

        private void OpretFunc()
        {
            //Opret ny kolonne 2 og gem dens id
            Kolonne2 k2 = new Kolonne2();
            k2.Post(new Kolonne2());
            List<Kolonne2> allk2 = k2.GetAll();
            int lastK2Id = allk2[allk2.Count - 1].ID;

            //Opret forside med ID fra kolonne 2 der lige er blevet oprettet
            Forside fors = new Forside()
            {
                FK_Kolonne = lastK2Id,
                FærdigvareNr = FærdigvareNr,
                FærdigvareNavn = Færdigvarenavn,
                ProcessordreNr = ProcessordreNr,
                Produktionsinitialer = Produktionsinitialer,
                Dato = DateTime.Now
            };

            fors.Post(fors);

            TilbageFunc();
        }
    }
}
