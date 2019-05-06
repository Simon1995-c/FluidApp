using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;

namespace FluidApp
{
    public class KolonneEditViewModel
    {
        public RelayCommand TilbageCommand { get; set; }
         

        public KolonneEditViewModel()
        {
            TilbageCommand = new RelayCommand(Tilbage);
        }

        public void Tilbage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(Kolonne), null);
            Window.Current.Content = frame;
        }
    }
}