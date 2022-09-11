namespace AvaloniaUIDashboard.ViewModels
{
    [ObservableObject]
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            MainViewModel = new MainViewModel();
        }

        [ObservableProperty]
        private MainViewModel mainViewModel;
    }
}
