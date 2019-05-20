using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
{
    class OpretKolonneViewModel : INotifyPropertyChanged
    {
        public int FærdigvareNr { get; set; }
        public int ProcessordreNr { get; set; }
        public string Færdigvarenavn { get; set; }
        public string Produktionsinitialer { get; set; }
        public string Title { get; set; }

        public Forside currentForside { get; set; }

        public RelayCommand Tilbage { get; set; }
        public RelayCommand Opret { get; set; }
        public RelayCommand OpdaterRelayCommand { get; set; }

        public Visibility ErrorVisibility { get; set; }
        public Visibility EditModeVisibility { get; set; }
        public Visibility OpdaterVisibility { get; set; }

        

        public OpretKolonneViewModel()
        {
            Tilbage = new RelayCommand(TilbageFunc);
            Opret = new RelayCommand(OpretFunc);
            OpdaterRelayCommand = new RelayCommand(Opdater);

            ErrorVisibility = Visibility.Collapsed;
            OpdaterVisibility = Visibility.Collapsed;

            Title = "Opret Skema";

            if (Application.Current.Resources.ContainsKey("editForside"))
            {
                Title = "Rediger Skema";
                Forside f = (Forside)Application.Current.Resources["editForside"];
                currentForside = new Forside();
                currentForside.ID = f.ID;
                currentForside.Dato = f.Dato;
                currentForside.FK_Kolonne = f.FK_Kolonne;
                FærdigvareNr = f.FærdigvareNr;
                ProcessordreNr = f.ProcessordreNr;
                Færdigvarenavn = f.FærdigvareNavn;
                Produktionsinitialer = f.Produktionsinitialer;

                EditModeVisibility = Visibility.Collapsed;
                OpdaterVisibility = Visibility.Visible;

                Application.Current.Resources.Remove("editForside");
            }
        }

        private async void Opdater()
        {
            Forside f = new Forside();

            f.Put(currentForside.ID, new Forside()
            {
                ID = currentForside.ID,
                FK_Kolonne = currentForside.FK_Kolonne,
                FærdigvareNr = FærdigvareNr,
                ProcessordreNr = ProcessordreNr,
                FærdigvareNavn = Færdigvarenavn,
                Produktionsinitialer = Produktionsinitialer,
                Dato = currentForside.Dato
                
            });

            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Du har opdateret skemaet.",
                Content = "",
                CloseButtonText = "Luk",
                PrimaryButtonText = "Gå til alle skemaer",
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                TilbageFunc();
            }
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

            //Tjekker om de er tal og at de ikke er = 0
            if (Int32.TryParse(FærdigvareNr.ToString(), out int val1) &&
                Int32.TryParse(ProcessordreNr.ToString(), out int val2) && FærdigvareNr != 0 && ProcessordreNr != 0)
            {
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
            else
            {
                ErrorVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(ErrorVisibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
