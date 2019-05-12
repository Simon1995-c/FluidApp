using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluidApp.Annotations;

namespace FluidApp.ViewModels
{
    public class ProduktionsfølgeseddelEditViewModel : INotifyPropertyChanged
    {
        public ProduktionsfølgeseddelEditViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}