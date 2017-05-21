namespace DesktopClient
{
    public class MainWindowViewModel
    {
        public StatusBarViewModel StatusBar { get; set; }

        public MainWindowViewModel()
        {
            StatusBar = new StatusBarViewModel();
            StatusBar.Clear();
        }
    }
}