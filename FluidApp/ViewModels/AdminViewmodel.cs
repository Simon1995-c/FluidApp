using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluidApp.Annotations;
using FluidApp.Views;
using GalaSoft.MvvmLight.Command;
using Models;

namespace FluidApp.ViewModels
{
    public class AdminViewmodel : INotifyPropertyChanged
    {
        //Relaycommands
        public RelayCommand TilbageCommandRelayCommand { get; set; }
        public RelayCommand<int> SeIPRelayCommand { get; set; }
        public RelayCommand SletIpRelayCommand { get; set; }
        public RelayCommand UpdateIpRelayCommand { get; set; }
        public RelayCommand ResetIPViewRelayCommand { get; set; }
        public RelayCommand OpretIPRelayCommand { get; set; }
        public RelayCommand NavigateOpretIpRelayCommand { get; set; }
        public RelayCommand ManageIpRangeRelayCommand { get; set; }
        public RelayCommand ManageAdminsRelayCommand { get; set; }
        public RelayCommand<int> SeAdminRelayCommand { get; set; }
        public RelayCommand ResetAdminsRelayCommand { get; set; }
        public RelayCommand NavigateOpretAdminsRelayCommand { get; set; }
        public RelayCommand UpdateAdminRelayCommand { get; set; }
        public RelayCommand SletAdminRelayCommand { get; set; }
        public RelayCommand OpretAdminRelayCommand { get; set; }
        public RelayCommand LogUdRelayCommand { get; set; }
        public RelayCommand NavigateGraferRelayCommand { get; set; }
        public RelayCommand NavigateVægtGrafRelayCommand { get; set; }
        public RelayCommand NavigateMsRelayCommand { get; set; }

        //Lister
        public List<IPrange> IpRange { get; set; }
        public List<Administrator> Administratorer { get; set; }
        public List<string> SorteringsMuligheder { get; set; }

        //Grafer
        private List<Record> _graf;

        //Visiblities
        public Visibility IpRangeContainerVisiblity { get; set; }
        public Visibility AllIpRangeVisibility { get; set; }
        public Visibility UpdateIpRangeVisibility { get; set; }
        public Visibility OpretIpRangeVisibility { get; set; }
        public Visibility AdminsContainerVisiblity { get; set; }
        public Visibility AllAdminsVisibility { get; set; }
        public Visibility UpdateAdminsVisiblity { get; set; }
        public Visibility OpretAdminsVisiblity { get; set; }
        public Visibility AdminKodeordErrorVisiblity { get; set; }
        public Visibility LogUdVisibility { get; set; }

        public Visibility LudkoncentrationVisibility { get; set; }
        public Visibility VægtVisibility { get; set; }
        public Visibility MsVisiblity { get; set; }

        //Holders
        public IPrange CurrentIp { get; set; } //Holder den IP der er i gang med at blive redigeret
        public Administrator CurrentAdmin { get; set; } //Holder den Admin der er i gang med at blive redigeret

        //String
        public string OpretIP { get; set; }
        public string KodeordIgen { get; set; }
        public string OpretAdminBrugernavn { get; set; }
        public int OpretAdminRolle { get; set; }
        public string OpretAdminKodeord { get; set; }

        public string GraphHolder { get; set; }

        private string _sorteringsValg;
        public string SorteringsValg
        {
            get { return _sorteringsValg; }
            set
            {
                _sorteringsValg = value;
                OnPropertyChanged();
                GraphHandler g = new GraphHandler();
                switch (SorteringsValg)
                {
                    case "2 dage":
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(2);
                        }
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(2);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(2);
                        }
                        break;
                    case "7 dage":
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(7);
                        }
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(7);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(7);
                        }
                        break;
                    case "14 dage":
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(14);
                        }
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(14);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(14);
                        }
                        break;
                    case "30 dage":
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(30);
                        }
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(30);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(30);
                        }
                        break;
                    case "365 dage":
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(365);
                        }
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(365);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(365);
                        }
                        break;
                    default:
                        if (GraphHolder == "VA")
                        {
                            Graf = g.DrawVægt(0);
                        }
                        if (GraphHolder == "LK")
                        {
                            Graf = g.DrawLudKoncentration(0);
                        }
                        if (GraphHolder == "MS")
                        {
                            Graf = g.DrawMs(0);
                        }
                        break;
                }
            }
        }

        public List<Record> Graf
        {
            get { return _graf; }
            set
            {
                _graf = value;
                OnPropertyChanged();
            }
        }

        public AdminViewmodel()
        {
            TilbageCommandRelayCommand = new RelayCommand(Tilbage);
            SeIPRelayCommand = new RelayCommand<int>(SeIpFunc);
            SletIpRelayCommand = new RelayCommand(SletIpFunc);
            UpdateIpRelayCommand = new RelayCommand(UpdateIpFunc);
            ResetIPViewRelayCommand = new RelayCommand(ResetIPViewFunc);
            OpretIPRelayCommand = new RelayCommand(OpretIPFunc);
            NavigateOpretIpRelayCommand = new RelayCommand(NavigateOpretIp);
            ManageIpRangeRelayCommand = new RelayCommand(ManageIpRange);
            ManageAdminsRelayCommand = new RelayCommand(ManageAdmins);
            SeAdminRelayCommand = new RelayCommand<int>(SeAdmin);
            ResetAdminsRelayCommand = new RelayCommand(ResetAdminViewFunc);
            NavigateOpretAdminsRelayCommand = new RelayCommand(NavigateOpretAdmins);
            UpdateAdminRelayCommand = new RelayCommand(UpdateAdmin);
            SletAdminRelayCommand = new RelayCommand(SletAdmin);
            OpretAdminRelayCommand = new RelayCommand(OpretAdmin);
            LogUdRelayCommand = new RelayCommand(Logud);
            NavigateGraferRelayCommand = new RelayCommand(NavigateLudkoncentration);
            NavigateVægtGrafRelayCommand = new RelayCommand(NavigateVægt);
            NavigateMsRelayCommand = new RelayCommand(NavigateMs);


            //Udfyld lister
            IpRange = UpdateIPrange();

            //visibilities
            //Iprange
            UpdateIpRangeVisibility = Visibility.Collapsed;
            OpretIpRangeVisibility = Visibility.Collapsed;

            //Admins
            AdminsContainerVisiblity = Visibility.Collapsed;
            UpdateAdminsVisiblity = Visibility.Collapsed;
            OpretAdminsVisiblity = Visibility.Collapsed;
            AdminKodeordErrorVisiblity = Visibility.Collapsed;

            //Grafer
            LudkoncentrationVisibility = Visibility.Collapsed;
            VægtVisibility = Visibility.Collapsed;
            MsVisiblity = Visibility.Collapsed;

            SorteringsMuligheder = new List<string>();
            SorteringsMuligheder.Add("Alle");
            SorteringsMuligheder.Add("2 dage");
            SorteringsMuligheder.Add("7 dage");
            SorteringsMuligheder.Add("14 dage");
            SorteringsMuligheder.Add("30 dage");
            SorteringsMuligheder.Add("365 dage");
        }

        private void NavigateMs()
        {
            ResetMange();

            MsVisiblity = Visibility.Visible;
            OnPropertyChanged(nameof(MsVisiblity));

            GraphHandler g = new GraphHandler();
            Graf = g.DrawMs(0);

            GraphHolder = "MS";

            SorteringsValg = "Alle";
        }

        private void NavigateVægt()
        {
            ResetMange();

            VægtVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(VægtVisibility));

            GraphHandler g = new GraphHandler();
            Graf = g.DrawVægt(0);

            GraphHolder = "VA";

            SorteringsValg = "Alle";
        }

        private void NavigateLudkoncentration()
        {
            ResetMange();

            LudkoncentrationVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(LudkoncentrationVisibility));
            
            GraphHandler g = new GraphHandler();
            Graf = g.DrawLudKoncentration(0);

            GraphHolder = "LK";

            SorteringsValg = "Alle";
        }

        private void OpretAdmin()
        {
            Administrator a = new Administrator();

            if (OpretAdminKodeord == KodeordIgen && OpretAdminBrugernavn != "" && OpretAdminKodeord != "")
            {
                a.Post(new Administrator()
                {
                    Brugernavn = OpretAdminBrugernavn,
                    Kodeord = OpretAdminKodeord,
                    Rolle = OpretAdminRolle
                });

                ResetAdminViewFunc();
            }
            else
            {
                AdminKodeordErrorVisiblity = Visibility.Visible;
                OnPropertyChanged(nameof(AdminKodeordErrorVisiblity));
            }

        }

        private void UpdateAdmin()
        {
            if (CurrentAdmin.Kodeord == KodeordIgen && CurrentAdmin.Brugernavn != "" && CurrentAdmin.Kodeord != "")
            {
                Administrator a = new Administrator();

                a.Put(CurrentAdmin.ID, new Administrator() { ID = CurrentAdmin.ID, Brugernavn = CurrentAdmin.Brugernavn, Kodeord = CurrentAdmin.Kodeord, Rolle = CurrentAdmin.Rolle });
                ResetAdminViewFunc();
            }
            else
            {
                AdminKodeordErrorVisiblity = Visibility.Visible;
                OnPropertyChanged(nameof(AdminKodeordErrorVisiblity));
            }
        }

        private void SletAdmin()
        {
            Administrator a = new Administrator();
            a.Delete(CurrentAdmin.ID);

            ResetAdminViewFunc();
        }

        private void SeAdmin(int id)
        {
            Administrator i = new Administrator();
            CurrentAdmin = i.GetOne(id);

            AllAdminsVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(AllAdminsVisibility));

            UpdateAdminsVisiblity = Visibility.Visible;
            OnPropertyChanged(nameof(UpdateAdminsVisiblity));

            OnPropertyChanged(nameof(CurrentAdmin));
        }

        private void NavigateOpretAdmins()
        {
            AllAdminsVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(AllAdminsVisibility));
            UpdateAdminsVisiblity = Visibility.Collapsed;
            OnPropertyChanged(nameof(UpdateAdminsVisiblity));
            OpretAdminsVisiblity = Visibility.Visible;
            OnPropertyChanged(nameof(OpretAdminsVisiblity));
        }

        private void ManageAdmins()
        {
            Administrator a = new Administrator();
            ResetMange();
            AdminsContainerVisiblity = Visibility.Visible;
            OnPropertyChanged(nameof(AdminsContainerVisiblity));
            Administratorer = a.GetAll();
            OnPropertyChanged(nameof(Administratorer));
        }

        private void ManageIpRange()
        {
            ResetMange();
            IpRangeContainerVisiblity = Visibility.Visible;
            OnPropertyChanged(nameof(IpRangeContainerVisiblity));
        }

        private void ResetMange()
        {
            IpRangeContainerVisiblity = Visibility.Collapsed;
            AdminsContainerVisiblity = Visibility.Collapsed;
            LudkoncentrationVisibility = Visibility.Collapsed;
            VægtVisibility = Visibility.Collapsed;
            MsVisiblity = Visibility.Collapsed;
            OnPropertyChanged(nameof(IpRangeContainerVisiblity));
            OnPropertyChanged(nameof(AdminsContainerVisiblity));
            OnPropertyChanged(nameof(LudkoncentrationVisibility));
            OnPropertyChanged(nameof(VægtVisibility));
            OnPropertyChanged(nameof(MsVisiblity));
        }

        private void NavigateOpretIp()
        {
            OpretIpRangeVisibility = Visibility.Visible;
            AllIpRangeVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(OpretIpRangeVisibility));
            OnPropertyChanged(nameof(AllIpRangeVisibility));
        }

        private void OpretIPFunc()
        {
            IPrange i = new IPrange();

            i.Post(new IPrange() { IP = OpretIP });
            ResetIPViewFunc();
        }

        private void ResetIPViewFunc()
        {
            IpRange = UpdateIPrange();
            OnPropertyChanged(nameof(IpRange));

            UpdateIpRangeVisibility = Visibility.Collapsed;
            AllIpRangeVisibility = Visibility.Visible;
            OpretIpRangeVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(UpdateIpRangeVisibility));
            OnPropertyChanged(nameof(AllIpRangeVisibility));
            OnPropertyChanged(nameof(OpretIpRangeVisibility));
        }

        private void ResetAdminViewFunc()
        {
            Administrator a = new Administrator();
            Administratorer = a.GetAll();

            OnPropertyChanged(nameof(Administratorer));

            UpdateAdminsVisiblity = Visibility.Collapsed;
            AllAdminsVisibility = Visibility.Visible;
            OpretAdminsVisiblity = Visibility.Collapsed;
            OnPropertyChanged(nameof(UpdateAdminsVisiblity));
            OnPropertyChanged(nameof(AllAdminsVisibility));
            OnPropertyChanged(nameof(OpretAdminsVisiblity));
            AdminKodeordErrorVisiblity = Visibility.Collapsed;
            OnPropertyChanged(nameof(AdminKodeordErrorVisiblity));
            KodeordIgen = "";
            OnPropertyChanged(nameof(KodeordIgen));
        }

        private void SeIpFunc(int id)
        {
            IPrange i = new IPrange();
            CurrentIp = i.GetOne(id);

            UpdateIpRangeVisibility = Visibility.Visible;
            AllIpRangeVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(UpdateIpRangeVisibility));
            OnPropertyChanged(nameof(AllIpRangeVisibility));

            OnPropertyChanged(nameof(CurrentIp));
        }

        private void UpdateIpFunc()
        {
            IPrange i = new IPrange();
            i.Put(CurrentIp.ID, new IPrange() { ID = CurrentIp.ID, IP = CurrentIp.IP });

            ResetIPViewFunc();
        }

        private void SletIpFunc()
        {
            IPrange i = new IPrange();
            i.Delete(CurrentIp.ID);

            ResetIPViewFunc();
        }

        private List<IPrange> UpdateIPrange()
        {
            IPrange i = new IPrange();
            return i.GetAll();
        }

        private void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), null);
            Window.Current.Content = frame;

        }




        public void Logud()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Login), null);
            Window.Current.Content = frame;
           
            Application.Current.Resources["Administrator"] = 0;

            LogUdVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(LogUdVisibility));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
