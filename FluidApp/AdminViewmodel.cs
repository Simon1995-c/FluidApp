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



        //Lister
        public List<IPrange> IpRange { get; set; }
        public List<Administrator> Administratorer { get; set; }

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



        //Holders
        public IPrange CurrentIp { get; set; } //Holder den IP der er i gang med at blive redigeret
        public Administrator CurrentAdmin { get; set; } //Holder den Admin der er i gang med at blive redigeret

        //String
        public string OpretIP { get; set; }
        public string KodeordIgen { get; set; }
        public string OpretAdminBrugernavn { get; set; }
        public int OpretAdminRolle { get; set; }
        public string OpretAdminKodeord { get; set; }

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
            OnPropertyChanged(nameof(IpRangeContainerVisiblity));
            OnPropertyChanged(nameof(AdminsContainerVisiblity));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
