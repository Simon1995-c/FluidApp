using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluidApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();

            ((LineSeries) LudChart.Series[0]).DependentRangeAxis = new LinearAxis()
            {
                Orientation = AxisOrientation.Y,
                Maximum = 3,
                Minimum = 0,
                ShowGridLines = true
            };

            ((LineSeries) VægtChart.Series[0]).DependentRangeAxis = new LinearAxis()
            {
                Orientation = AxisOrientation.Y,
                Maximum = 50,
                Minimum = 30,
                ShowGridLines = true
            };

            ((LineSeries)MsChart.Series[0]).DependentRangeAxis = new LinearAxis()
            {
                Orientation = AxisOrientation.Y,
                Maximum = 28,
                Minimum = 22,
                ShowGridLines = true
            };
        }
    }
}
