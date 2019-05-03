using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluidApp
{
    class KolonneViewModel
    {
        public KolonneViewModel()
        {
            ipHandler h = new ipHandler();

            //If the IP isn't allowed -> send them to an error page
            if (!h.isAllowedIp().Result)
            {
                var frame = new Frame();
                frame.Navigate(typeof(errorPageIPrange), null);
                Window.Current.Content = frame;
            }
        }
    }
}
