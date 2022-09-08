using Avalonia.Controls;
using Avalonia.Threading;

namespace AvaloniaCommunityToolkitTest
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            currnetTIme = new DispatcherTimer();
            //currnetTIme 
        }

        DispatcherTimer currnetTIme;
    }
}
